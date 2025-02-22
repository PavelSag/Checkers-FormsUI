
namespace Checkers
{
    partial class GameSettingsForm
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
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButton6By6 = new System.Windows.Forms.RadioButton();
            this.radioButton8By8 = new System.Windows.Forms.RadioButton();
            this.radioButton10By10 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.groupBoxOfBoardSizeRadioButtons = new System.Windows.Forms.GroupBox();
            this.groupBoxOfBoardSizeRadioButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(29, 21);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(81, 17);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // radioButton6By6
            // 
            this.radioButton6By6.AutoSize = true;
            this.radioButton6By6.Location = new System.Drawing.Point(19, 21);
            this.radioButton6By6.Name = "radioButton6By6";
            this.radioButton6By6.Size = new System.Drawing.Size(59, 21);
            this.radioButton6By6.TabIndex = 1;
            this.radioButton6By6.TabStop = true;
            this.radioButton6By6.Text = "6 x 6";
            this.radioButton6By6.UseVisualStyleBackColor = true;
            // 
            // radioButton8By8
            // 
            this.radioButton8By8.AutoSize = true;
            this.radioButton8By8.Location = new System.Drawing.Point(96, 21);
            this.radioButton8By8.Name = "radioButton8By8";
            this.radioButton8By8.Size = new System.Drawing.Size(59, 21);
            this.radioButton8By8.TabIndex = 2;
            this.radioButton8By8.TabStop = true;
            this.radioButton8By8.Text = "8 x 8";
            this.radioButton8By8.UseVisualStyleBackColor = true;
            // 
            // radioButton10By10
            // 
            this.radioButton10By10.AutoSize = true;
            this.radioButton10By10.Location = new System.Drawing.Point(176, 21);
            this.radioButton10By10.Name = "radioButton10By10";
            this.radioButton10By10.Size = new System.Drawing.Size(75, 21);
            this.radioButton10By10.TabIndex = 3;
            this.radioButton10By10.TabStop = true;
            this.radioButton10By10.Text = "10 x 10";
            this.radioButton10By10.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Players:";
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(152, 144);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(137, 22);
            this.textBoxPlayer1Name.TabIndex = 5;
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(48, 144);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(64, 17);
            this.labelPlayer1.TabIndex = 6;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(51, 181);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(82, 21);
            this.checkBoxPlayer2.TabIndex = 8;
            this.checkBoxPlayer2.Text = "Player2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(152, 181);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(137, 22);
            this.textBoxPlayer2Name.TabIndex = 9;
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(214, 224);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // groupBoxOfBoardSizeRadioButtons
            // 
            this.groupBoxOfBoardSizeRadioButtons.Controls.Add(this.radioButton6By6);
            this.groupBoxOfBoardSizeRadioButtons.Controls.Add(this.radioButton8By8);
            this.groupBoxOfBoardSizeRadioButtons.Controls.Add(this.radioButton10By10);
            this.groupBoxOfBoardSizeRadioButtons.Location = new System.Drawing.Point(32, 41);
            this.groupBoxOfBoardSizeRadioButtons.Name = "groupBoxOfBoardSizeRadioButtons";
            this.groupBoxOfBoardSizeRadioButtons.Size = new System.Drawing.Size(257, 57);
            this.groupBoxOfBoardSizeRadioButtons.TabIndex = 11;
            this.groupBoxOfBoardSizeRadioButtons.TabStop = false;
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 274);
            this.Controls.Add(this.groupBoxOfBoardSizeRadioButtons);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.GameSettingsForm_Load);
            this.groupBoxOfBoardSizeRadioButtons.ResumeLayout(false);
            this.groupBoxOfBoardSizeRadioButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButton6By6;
        private System.Windows.Forms.RadioButton radioButton8By8;
        private System.Windows.Forms.RadioButton radioButton10By10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.GroupBox groupBoxOfBoardSizeRadioButtons;
    }
}

