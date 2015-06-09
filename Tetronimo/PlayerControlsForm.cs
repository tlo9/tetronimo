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
    public partial class PlayerControlsForm : Form
    {
        private PlayerControls playerControls;

        public PlayerControlsForm(PlayerControls controls)
        {
            var inputStrings = controls.GetInputStrings();
            InitializeComponent();

            playerControls = controls;

            leftKeyInput.Text = inputStrings.left;
            rightKeyInput.Text = inputStrings.right;
            rotateKeyInput.Text = inputStrings.rotate;
            dropKeyInput.Text = inputStrings.drop;
        }

        private void PlayerControlsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            playerControls.SetInputStrings(leftKeyInput.Text,
                rightKeyInput.Text, rotateKeyInput.Text, dropKeyInput.Text);

            playerControls.Save();
        }

        private void defaultButton_Click(object sender, EventArgs e)
        {
            leftKeyInput.Text = PlayerControls.DefaultLeft;
            rightKeyInput.Text = PlayerControls.DefaultRight;
            rotateKeyInput.Text = PlayerControls.DefaultRotate;
            dropKeyInput.Text = PlayerControls.DefaultDrop;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
