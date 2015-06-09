using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetromino
{
    public partial class HighScoreForm : Form
    {
        HighScoresList highScoreList;

        public HighScoreForm(HighScoresList list)
        {
            InitializeComponent();
            highScoreList = list;
        }

        /// <summary>
        /// Fill in the table in high scores window from the high score list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void HighScoreForm_Load(object sender, EventArgs e)
        {
            var entries = highScoreList.GetScores();

            for (int i=0; i < entries.Count(); i++)
            {
                var entry = entries.ElementAt(i);
                ((Label)Controls.Find(string.Format("nameLabel{0}", (i + 1)),true)[0]).Text = entry.name;
                ((Label)Controls.Find(string.Format("scoreLabel{0}", (i + 1)), true)[0]).Text = entry.score.ToString();
                ((Label)Controls.Find(string.Format("linesLabel{0}", (i + 1)), true)[0]).Text = entry.lines.ToString();
            }
        }

        /// <summary>
        /// Save the high scores upon closing the high scores window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void HighScoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            highScoreList.Save();
        }
    }
}
