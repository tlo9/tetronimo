using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetromino
{
    class GameLogic
    {
        private const int MaxLevel = 99;
        private const int SingleMultiplier = 40;
        private const int DoubleMultiplier = 100;
        private const int TripleMultiplier = 300;
        private const int QuadrupleMultiplier = 1200;
        private const int LinesPerLevel = 10;

        public bool GameOver { get; private set; }

        public uint Score { get; private set; }
        public uint Lines { get; private set; }
        public uint Level { get; private set; }

        public BlockGrid MainGrid { get; private set; }
        readonly Shape.Standard[] standardShapes = new Shape.Standard[] 
            { Shape.Standard.I, Shape.Standard.J, Shape.Standard.T, Shape.Standard.L,
              Shape.Standard.S, Shape.Standard.Z, Shape.Standard.O };
        public Shape ActiveShape { get; private set; }
        public Shape NextShape { get; private set; }
        public BlockGrid.Position ShapePosition { get; private set; }
        public enum Direction { Left, Right };

        public GameLogic()
        {
            Init();
        }

        private void Init()
        {
            ActiveShape = GenerateRandomShape();
            NextShape = GenerateRandomShape();

            MainGrid = new BlockGrid();

            ShapePosition = new BlockGrid.Position(-MainGrid.HiddenRows,
                (MainGrid.Columns - Shape.MaxWidth) / 2 - 1);
        }

        public void Reset()
        {
            Init();
            Score = 0;
            Level = 0;
            Lines = 0;
            GameOver = false;
        }

        public void DropShape()
        {
            var position = ShapePosition;

            do
            {
                position.Row += 1;

                // Stop dropping the shape if it will either
                // collide with other blocks in the grid or
                // fall past the grid
                if (!MainGrid.IsInsideGrid(position, ActiveShape)
                    || MainGrid.Overlaps(position, ActiveShape))
                {
                    position.Row -= 1;
                    break;
                }
            } while (true);

            ShapePosition = position;
        }

        /// <summary>
        /// Move the active Shape to the left or right by one unit.
        /// </summary>
        /// <returns>True, if moving to the left would cause a collision. False, if successful.</returns>


        public bool MoveShape(Direction direction)
        {
            var position = ShapePosition;

            position.Column += (direction == Direction.Left ? -1 : 1);

            // Move the shape horizontally unless it causes a collision
            // with other blocks or protrudes outside of the grid

            if (MainGrid.IsInsideGrid(position, ActiveShape)
                && !MainGrid.Overlaps(position, ActiveShape))
            {
                ShapePosition = position;
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Rotate the active Shape by a quarter-turn.
        /// </summary>
        /// <returns>True, if rotating the shae would cause a collision. False, if successful.</returns>

        public bool RotateShape()
        {
            var rotatedShape = new Shape(ActiveShape);
            rotatedShape.Rotate();

            // Rotate the shape unless it causes collisions
            // with other blocks or protrudes outside of the grid

            if (MainGrid.IsInsideGrid(ShapePosition, rotatedShape)
                && !MainGrid.Overlaps(ShapePosition, rotatedShape))
            {
                ActiveShape = rotatedShape;
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Drop the shape down by one unit, if possible.
        /// </summary>
        /// <param name="completedLines">Outputs the number of complete lines after a collision</param>
        /// <returns>True, on collision. False, otherwise.</returns>
        public bool Advance(out uint completedLines)
        {
            uint rows = 0;
            var newPosition = ShapePosition;
            newPosition.Row += 1;

            // If a shape will collide with other blocks or fall past the
            // bottom of the grid, then imprint the shape onto the grid,
            // check for filled rows, remove filled rows, and drop
            // rows into empty rows

            if (MainGrid.Overlaps(newPosition, ActiveShape)
                || MainGrid.OverflowsBottom(newPosition, ActiveShape))
            {
                MainGrid.Imprint(ShapePosition, ActiveShape);

                for (int i = 0; i < MainGrid.Rows; i++)
                {
                    if (MainGrid.RowIsFilled(i))
                    {
                        rows++;

                        for (int j = i; j > 0; j--)
                            MainGrid.CopyRow(j - 1, j);

                        MainGrid.ClearRow(0);
                    }
                }

                completedLines = rows;

                switch (rows)
                {
                    case 0:
                        break;
                    case 1:
                        Score += SingleMultiplier * (rows + 1);
                        break;
                    case 2:
                        Score += DoubleMultiplier * (rows + 1);
                        break;
                    case 3:
                        Score += TripleMultiplier * (rows + 1);
                        break;
                    case 4:
                        Score += QuadrupleMultiplier * (rows + 1);
                        break;
                    default:
                        // This should never happen
                        break;
                }

                Lines += rows;
                Level = Lines / LinesPerLevel;

                if (Level > MaxLevel)
                    Level = MaxLevel;

                ActiveShape = NextShape;
                NextShape = GenerateRandomShape();

                ShapePosition = new BlockGrid.Position(-MainGrid.HiddenRows,
                    (MainGrid.Columns - Shape.MaxWidth) / 2 - 1);

                // If placing a new shape at the top of the grid
                // causes a collision, end the game.
                if (MainGrid.Overlaps(ShapePosition, ActiveShape)
                    && MainGrid.OverflowsTop(ShapePosition, ActiveShape))
                {
                    GameOver = true;
                }

                return true;
            }
            else
            {
                ShapePosition = newPosition;
                completedLines = 0;
                return false;
            }
        }

        private Shape GenerateRandomShape()
        {
            return Shape.FromStandard(standardShapes[new Random().Next(standardShapes.Length)]);
        }
    }
}
