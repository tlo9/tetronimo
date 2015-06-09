using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;
using System.IO;

namespace Tetromino
{
    public partial class TetrominoForm : Form
    {
        private const int BlockWidth = 20;
        private const int BlockHeight = 20;
        private HighScoresList highScoreList = new HighScoresList();
        private PlayerControls playerControls = new PlayerControls();
        private const Keys NewGameKey = Keys.F2;
        private const Keys PauseGameKey = Keys.F3;
        private const string SongsDirectory = "Songs";
        private int songIndex = -1;
        private bool musicEnabled = true;
        private bool isPaused = false;
        private GameLogic gameLogic = new GameLogic();
        private BlockGrid previewGrid = new BlockGrid(4, 5, 0);
        private System.Windows.Media.MediaPlayer mediaPlayer = new System.Windows.Media.MediaPlayer();
        private readonly LevelInterval[] levelIntervals = 
        { new LevelInterval(0,1,48), new LevelInterval(1,2,43), new LevelInterval(2,3,38),
            new LevelInterval(3,4,33), new LevelInterval(4,5,28), new LevelInterval(5,6,23),
            new LevelInterval(6,7,18), new LevelInterval(7,8,15), new LevelInterval(8,9,8),
            new LevelInterval(9,10,6), new LevelInterval(10,13,5), new LevelInterval(13,16,4),
            new LevelInterval(16,19,3), new LevelInterval(19,29,2), new LevelInterval(29,100,1) };

        private class LevelInterval
        {
            public readonly uint minLevel;
            public readonly uint maxLevel;
            public readonly uint value; // The value of the interval in frames

            public LevelInterval(uint min, uint max, uint value)
            {
                this.minLevel = min;
                this.maxLevel = max;
                this.value = value;
            }
        }

        public TetrominoForm()
        {
            InitializeComponent();
            mediaPlayer.MediaEnded += new EventHandler(loopMusicHandler);
        }

        private void startGame(object sender, EventArgs e)
        {
            gameTimer.Stop();

            gameLogic.Reset();

            setSong(0);
            startMusic();

            statusLabel.Text = "";
            levelLabel.Text = gameLogic.Level.ToString();
            scoreLabel.Text = gameLogic.Score.ToString();
            linesLabel.Text = gameLogic.Lines.ToString();

            blockBox.Invalidate();
            previewBox.Invalidate();

            gameTimer.Start();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            uint frames = 1;
            uint filledRows;
            bool restartTimer;

            restartTimer = gameLogic.Advance(out filledRows);

            if (restartTimer)
                gameTimer.Stop();

            switch (filledRows)
            {
                case 0:
                    break;
                case 1:
                    statusLabel.Text = Properties.Resources.Single;
                    break;
                case 2:
                    statusLabel.Text = Properties.Resources.Double;
                    break;
                case 3:
                    statusLabel.Text = Properties.Resources.Triple;
                    break;
                case 4:
                    statusLabel.Text = Properties.Resources.Quadruple;
                    break;
                default:
                    // This should never happen
                    break;
            }

            if (filledRows > 0 && filledRows <= 2)
                new System.Media.SoundPlayer(Properties.Resources.SmallClear).Play();
            else if (filledRows > 2)
                new System.Media.SoundPlayer(Properties.Resources.BigClear).Play();

            statusLabel.Text = "";
            levelLabel.Text = gameLogic.Level.ToString();
            scoreLabel.Text = gameLogic.Score.ToString();
            linesLabel.Text = gameLogic.Lines.ToString();

            frames = (from interval in levelIntervals
                      where gameLogic.Level >= interval.minLevel
                       && gameLogic.Level < interval.maxLevel
                      select interval).First().value;

            gameTimer.Interval = (int)((50 * frames) / 3); // Interval = 1000 * frames / 60

            if (gameLogic.GameOver)
            {
                if (highScoreList.IsHighScore(gameLogic.Score, gameLogic.Lines))
                {
                    var nameForm = new HighScoreInputForm();

                    if (nameForm.ShowDialog() == DialogResult.OK)
                    {
                        highScoreList.Insert(nameForm.PlayerName, gameLogic.Score,
                            gameLogic.Lines);

                        new HighScoreForm(highScoreList).ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(Properties.Resources.GameOver,
                        Properties.Resources.GameOverTitle);

                    new HighScoreForm(highScoreList).ShowDialog();
                }

                statusLabel.Text = Properties.Resources.New;
                return;
            }

            if (gameLogic.Level < 12)
            {
                setSong((int)gameLogic.Level / 2);
                mediaPlayer.Play();
            }

            blockBox.Invalidate();
            previewBox.Invalidate();

            if (restartTimer)
                gameTimer.Start();
        }

        /// <summary>
        /// Paint all blocks in a BlockGrid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="g"></param>

        private void PaintGrid(BlockGrid grid, Graphics g)
        {
            for (int row = 0; row < grid.Rows; row++)
                for (int col = 0; col < grid.Columns; col++)
                    PaintBlock(row, col, grid.Blocks[row, col], g);
        }

        /// <summary>
        /// Paint a single block at a particular row and column in a BlockGrid.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="c">The color to be painted</param>
        /// <param name="g"></param>

        private void PaintBlock(int row, int col, Color c, Graphics g)
        {
            PaintBlockXY(1 + col * (BlockWidth + 1), 1 + row * (BlockHeight + 1), c, g);
        }

        /// <summary>
        /// Paint a single block at a particular (x,y) coordinate. This allows more
        /// fine-grained control over position than PaintBlock()
        /// </summary>
        /// <param name="x">The horizontal component of the block's position</param>
        /// <param name="y">The vertical component of the block's position</param>
        /// <param name="c">The color to be painted</param>
        /// <param name="g"></param>

        private void PaintBlockXY(int x, int y, Color c, Graphics g)
        {
            var sb = new SolidBrush(c);

            g.FillRectangle(sb, (float)(x - 0.5), (float)(y - 0.5), BlockWidth, BlockHeight);
        }

        /// <summary>
        /// Paint a Tetronimo shape at a particular position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="shape"></param>
        /// <param name="grid"></param>
        /// <param name="g"></param>
        private void PaintShape(BlockGrid.Position position, Shape shape, BlockGrid grid,
            Graphics g)
        {
            for (int row = 0; row < Shape.MaxHeight; row++)
            {
                for (int col = 0; col < Shape.MaxWidth; col++)
                {
                    if (shape.BlockArray[row, col] && row + position.Row >= 0
                        && row + position.Row < grid.Rows
                        && col + position.Column < grid.Columns
                        && col + position.Column >= 0)
                    {
                        if (grid == previewGrid && shape.Equals(Shape.Standard.O))
                        {
                            PaintBlockXY(1 + (col + position.Column) * (BlockWidth + 1) + ((BlockWidth + 1) / 2),
                                1 + (row + position.Row) * (BlockHeight + 1), shape.Color, g);
                        }
                        else
                            PaintBlock(row + position.Row, col + position.Column,
                                shape.Color, g);
                    }

                }
            }
        }

        /// <summary>
        /// Set the current song
        /// </summary>
        /// <param name="index"></param>

        private void setSong(int index)
        {
            if (index >= Properties.Settings.Default.Songs.Count)
                return;

            if (index >= 0 && index != songIndex)
            {
                songIndex = index;

                mediaPlayer.Stop();
                mediaPlayer.Open(
                    new Uri(string.Format(@"{0}\{1}\{2}", Directory.GetCurrentDirectory(),
                        SongsDirectory, Properties.Settings.Default.Songs[index])));
                mediaPlayer.Position = new TimeSpan(0);
            }
        }

        private void pauseGame()
        {
            isPaused = true;
            gameTimer.Stop();
            statusLabel.Text = Properties.Resources.Paused;
        }

        private void stopMusic()
        {
            musicEnabled = false;
            mediaPlayer.Stop();
        }

        private void startMusic()
        {
            mediaPlayer.Position = new TimeSpan(0);
            musicEnabled = true;
            mediaPlayer.Play();
        }

        private void loopMusicHandler(object sender, EventArgs e)
        {
            if (!gameLogic.GameOver)
            {
                mediaPlayer.Position = new TimeSpan(0);
                mediaPlayer.Play();
            }
        }

        public void togglePause(object sender, EventArgs e)
        {
            if (gameLogic.GameOver)
                return;

            if (!isPaused)
                pauseGame();
            else
            {
                gameTimer.Start();
                statusLabel.Text = "";
            }

            isPaused = !isPaused;
        }

        private void blockBox_Paint(object sender, PaintEventArgs e)
        {
            if (gameLogic.ActiveShape == null)
                return;

            PaintGrid(gameLogic.MainGrid, e.Graphics);
            PaintShape(gameLogic.ShapePosition, gameLogic.ActiveShape, gameLogic.MainGrid, e.Graphics);
        }

        private void TetronimoForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool collided = false;

            if ((gameLogic.GameOver && e.KeyCode != NewGameKey)
                || (isPaused && (e.KeyCode != PauseGameKey
                                 && e.KeyCode != NewGameKey)))
            {
                return;
            }

            var g = blockBox.CreateGraphics();
            var playerInputs = playerControls.GetInputs();

            if (e.KeyCode == playerInputs.drop)
            {
                gameLogic.DropShape();
                gameTimer.Stop(); // Reset the timing interval
                gameTimer.Start();
            }
            else if (e.KeyCode == playerInputs.rotate)
                collided = gameLogic.RotateShape();
            else if (e.KeyCode == playerInputs.left || e.KeyCode == playerInputs.right)
            {
                if (e.KeyCode == playerInputs.left)
                    collided = gameLogic.MoveShape(GameLogic.Direction.Left);
                else
                    collided = gameLogic.MoveShape(GameLogic.Direction.Right);
            }

            blockBox.Invalidate();
        }

        private void previewBox_paint(object sender, PaintEventArgs e)
        {
            if (gameLogic.NextShape == null)
                return;

            PaintGrid(previewGrid, e.Graphics);
            PaintShape(new BlockGrid.Position(0, 0), gameLogic.NextShape, previewGrid, e.Graphics);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseGame();
            new AboutBox().ShowDialog();
        }

        private void viewHighScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseGame();
            new HighScoreForm(highScoreList).ShowDialog();
        }

        private void playerControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseGame();
            new PlayerControlsForm(playerControls).ShowDialog();
        }

        private void musicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (musicEnabled)
                stopMusic();
            else
                startMusic();
        }
    }
}
