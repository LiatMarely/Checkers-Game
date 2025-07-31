namespace Ex05_Liat_207918608.FormUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton6X6 = new System.Windows.Forms.RadioButton();
            this.radioButton8X8 = new System.Windows.Forms.RadioButton();
            this.radioButton10X10 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkboxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = global::Ex05_Liat_207918608.Properties.Resources.Beige1;
            this.label1.Location = new System.Drawing.Point(191, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            // 
            // radioButton6X6
            // 
            this.radioButton6X6.AutoSize = true;
            this.radioButton6X6.BackgroundImage = global::Ex05_Liat_207918608.Properties.Resources.Beige1;
            this.radioButton6X6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButton6X6.Location = new System.Drawing.Point(246, 220);
            this.radioButton6X6.Name = "radioButton6X6";
            this.radioButton6X6.Size = new System.Drawing.Size(69, 24);
            this.radioButton6X6.TabIndex = 1;
            this.radioButton6X6.TabStop = true;
            this.radioButton6X6.Text = "6 X 6";
            this.radioButton6X6.UseVisualStyleBackColor = true;
            // 
            // radioButton8X8
            // 
            this.radioButton8X8.AutoSize = true;
            this.radioButton8X8.BackgroundImage = global::Ex05_Liat_207918608.Properties.Resources.Beige1;
            this.radioButton8X8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButton8X8.Location = new System.Drawing.Point(361, 220);
            this.radioButton8X8.Name = "radioButton8X8";
            this.radioButton8X8.Size = new System.Drawing.Size(69, 24);
            this.radioButton8X8.TabIndex = 2;
            this.radioButton8X8.TabStop = true;
            this.radioButton8X8.Text = "8 X 8";
            this.radioButton8X8.UseVisualStyleBackColor = true;
            // 
            // radioButton10X10
            // 
            this.radioButton10X10.AutoSize = true;
            this.radioButton10X10.BackgroundImage = global::Ex05_Liat_207918608.Properties.Resources.Beige1;
            this.radioButton10X10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButton10X10.Location = new System.Drawing.Point(473, 220);
            this.radioButton10X10.Name = "radioButton10X10";
            this.radioButton10X10.Size = new System.Drawing.Size(87, 24);
            this.radioButton10X10.TabIndex = 3;
            this.radioButton10X10.TabStop = true;
            this.radioButton10X10.Text = "10 X 10";
            this.radioButton10X10.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = global::Ex05_Liat_207918608.Properties.Resources.Beige1;
            this.label2.Location = new System.Drawing.Point(191, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft PhagsPa", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer1.Image = global::Ex05_Liat_207918608.Properties.Resources.Beige1;
            this.labelPlayer1.Location = new System.Drawing.Point(241, 290);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(85, 26);
            this.labelPlayer1.TabIndex = 5;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkboxPlayer2
            // 
            this.checkboxPlayer2.AutoSize = true;
            this.checkboxPlayer2.BackgroundImage = global::Ex05_Liat_207918608.Properties.Resources.Beige1;
            this.checkboxPlayer2.Font = new System.Drawing.Font("Microsoft PhagsPa", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkboxPlayer2.Location = new System.Drawing.Point(236, 332);
            this.checkboxPlayer2.Name = "checkboxPlayer2";
            this.checkboxPlayer2.Size = new System.Drawing.Size(104, 30);
            this.checkboxPlayer2.TabIndex = 7;
            this.checkboxPlayer2.Text = "Plater 2:";
            this.checkboxPlayer2.UseVisualStyleBackColor = true;
            this.checkboxPlayer2.CheckedChanged += new System.EventHandler(this.checkboxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Location = new System.Drawing.Point(346, 338);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.ReadOnly = true;
            this.textBoxPlayer2.Size = new System.Drawing.Size(188, 22);
            this.textBoxPlayer2.TabIndex = 8;
            this.textBoxPlayer2.Text = "[Computer]";
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(346, 290);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(188, 22);
            this.textBoxPlayer1.TabIndex = 9;
            // 
            // buttonDone
            // 
            this.buttonDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonDone.Location = new System.Drawing.Point(394, 384);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(140, 44);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = false;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Ex05_Liat_207918608.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(767, 483);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.checkboxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton10X10);
            this.Controls.Add(this.radioButton8X8);
            this.Controls.Add(this.radioButton6X6);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton6X6;
        private System.Windows.Forms.RadioButton radioButton8X8;
        private System.Windows.Forms.RadioButton radioButton10X10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkboxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.Button buttonDone;
    }
}
