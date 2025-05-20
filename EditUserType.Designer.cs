namespace ОБЗД
{
    partial class EditUserType
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
            this.btnEditTypeUser = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdNameUser = new System.Windows.Forms.ComboBox();
            this.cmdTypeUser = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnEditTypeUser
            // 
            this.btnEditTypeUser.Location = new System.Drawing.Point(52, 142);
            this.btnEditTypeUser.Name = "btnEditTypeUser";
            this.btnEditTypeUser.Size = new System.Drawing.Size(103, 29);
            this.btnEditTypeUser.TabIndex = 0;
            this.btnEditTypeUser.Text = "Змінити";
            this.btnEditTypeUser.UseVisualStyleBackColor = true;
            this.btnEditTypeUser.Click += new System.EventHandler(this.btnEditTypeUser_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(222, 142);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 29);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Вихід";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Користувач";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Тип доступу";
            // 
            // cmdNameUser
            // 
            this.cmdNameUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdNameUser.FormattingEnabled = true;
            this.cmdNameUser.Location = new System.Drawing.Point(208, 44);
            this.cmdNameUser.Name = "cmdNameUser";
            this.cmdNameUser.Size = new System.Drawing.Size(121, 24);
            this.cmdNameUser.TabIndex = 4;
            // 
            // cmdTypeUser
            // 
            this.cmdTypeUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdTypeUser.FormattingEnabled = true;
            this.cmdTypeUser.Location = new System.Drawing.Point(208, 87);
            this.cmdTypeUser.Name = "cmdTypeUser";
            this.cmdTypeUser.Size = new System.Drawing.Size(121, 24);
            this.cmdTypeUser.TabIndex = 5;
            // 
            // EditUserType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 233);
            this.Controls.Add(this.cmdTypeUser);
            this.Controls.Add(this.cmdNameUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEditTypeUser);
            this.Name = "EditUserType";
            this.Text = "EditUserPassword";
            this.Load += new System.EventHandler(this.EditUserType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditTypeUser;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmdNameUser;
        private System.Windows.Forms.ComboBox cmdTypeUser;
    }
}