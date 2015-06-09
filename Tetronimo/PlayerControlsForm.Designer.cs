namespace Tetromino
{
    partial class PlayerControlsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.defaultButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.dropKeyInput = new System.Windows.Forms.ComboBox();
            this.rotateKeyInput = new System.Windows.Forms.ComboBox();
            this.rightKeyInput = new System.Windows.Forms.ComboBox();
            this.leftKeyInput = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.defaultButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.dropKeyInput);
            this.panel1.Controls.Add(this.rotateKeyInput);
            this.panel1.Controls.Add(this.rightKeyInput);
            this.panel1.Controls.Add(this.leftKeyInput);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 167);
            this.panel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(93, 133);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // defaultButton
            // 
            this.defaultButton.Location = new System.Drawing.Point(174, 133);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(75, 23);
            this.defaultButton.TabIndex = 6;
            this.defaultButton.Text = "Default";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 133);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // dropKeyInput
            // 
            this.dropKeyInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropKeyInput.FormattingEnabled = true;
            this.dropKeyInput.Items.AddRange(new object[] {
            "`",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0",
            "[",
            "]",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            ",",
            ".",
            "/",
            ";",
            "\'",
            "*",
            "-",
            "=",
            "Tab",
            "Caps Lock",
            "Up Arrow",
            "Down Arrow",
            "Left Arrow",
            "Right Arrow",
            "Space",
            "Backspace",
            "Enter"});
            this.dropKeyInput.Location = new System.Drawing.Point(139, 91);
            this.dropKeyInput.Name = "dropKeyInput";
            this.dropKeyInput.Size = new System.Drawing.Size(85, 21);
            this.dropKeyInput.TabIndex = 3;
            // 
            // rotateKeyInput
            // 
            this.rotateKeyInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rotateKeyInput.FormattingEnabled = true;
            this.rotateKeyInput.Items.AddRange(new object[] {
            "`",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0",
            "[",
            "]",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            ",",
            ".",
            "/",
            ";",
            "\'",
            "*",
            "-",
            "=",
            "Tab",
            "Caps Lock",
            "Up Arrow",
            "Down Arrow",
            "Left Arrow",
            "Right Arrow",
            "Space",
            "Backspace",
            "Enter"});
            this.rotateKeyInput.Location = new System.Drawing.Point(139, 64);
            this.rotateKeyInput.Name = "rotateKeyInput";
            this.rotateKeyInput.Size = new System.Drawing.Size(85, 21);
            this.rotateKeyInput.TabIndex = 2;
            // 
            // rightKeyInput
            // 
            this.rightKeyInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rightKeyInput.FormattingEnabled = true;
            this.rightKeyInput.Items.AddRange(new object[] {
            "`",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0",
            "[",
            "]",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            ",",
            ".",
            "/",
            ";",
            "\'",
            "*",
            "-",
            "=",
            "Tab",
            "Caps Lock",
            "Up Arrow",
            "Down Arrow",
            "Left Arrow",
            "Right Arrow",
            "Space",
            "Backspace",
            "Enter"});
            this.rightKeyInput.Location = new System.Drawing.Point(139, 37);
            this.rightKeyInput.Name = "rightKeyInput";
            this.rightKeyInput.Size = new System.Drawing.Size(85, 21);
            this.rightKeyInput.TabIndex = 1;
            // 
            // leftKeyInput
            // 
            this.leftKeyInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftKeyInput.FormattingEnabled = true;
            this.leftKeyInput.Items.AddRange(new object[] {
            "`",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0",
            "[",
            "]",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            ",",
            ".",
            "/",
            ";",
            "\'",
            "*",
            "-",
            "=",
            "Tab",
            "Caps Lock",
            "Up Arrow",
            "Down Arrow",
            "Left Arrow",
            "Right Arrow",
            "Space",
            "Backspace",
            "Enter"});
            this.leftKeyInput.Location = new System.Drawing.Point(139, 10);
            this.leftKeyInput.Name = "leftKeyInput";
            this.leftKeyInput.Size = new System.Drawing.Size(85, 21);
            this.leftKeyInput.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Drop";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Rotate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Move Right";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Move Left";
            // 
            // PlayerControlsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(261, 167);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayerControlsForm";
            this.Text = "Player Controls";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerControlsForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox rightKeyInput;
        private System.Windows.Forms.ComboBox leftKeyInput;
        private System.Windows.Forms.Button defaultButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox dropKeyInput;
        private System.Windows.Forms.ComboBox rotateKeyInput;
        private System.Windows.Forms.Button cancelButton;
    }
}