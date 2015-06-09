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
    public partial class HighScoreInputForm : Form
    {
        public string PlayerName { get; private set; }

        public HighScoreInputForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            PlayerName = nameInput.Text;
            this.Close();
        }
    }
}
