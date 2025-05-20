namespace ОБЗД
{
    partial class AddNewUser
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
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTypeUser = new System.Windows.Forms.TextBox();
            this.txtPasswordUser = new System.Windows.Forms.TextBox();
            this.txtPassword2User = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.Location = new System.Drawing.Point(42, 224);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(100, 28);
            this.btnAddNewUser.TabIndex = 0;
            this.btnAddNewUser.Text = "Додати";
            this.btnAddNewUser.UseVisualStyleBackColor = true;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Користувач";
            // 
            // txtNameUser
            // 
            this.txtNameUser.Location = new System.Drawing.Point(187, 41);
            this.txtNameUser.Name = "txtNameUser";
            this.txtNameUser.Size = new System.Drawing.Size(100, 22);
            this.txtNameUser.TabIndex = 2;
            this.txtNameUser.Leave += new System.EventHandler(this.txtNameUser_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Тип доступу";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Підтвердження паролю";
            // 
            // txtTypeUser
            // 
            this.txtTypeUser.Location = new System.Drawing.Point(187, 80);
            this.txtTypeUser.Name = "txtTypeUser";
            this.txtTypeUser.Size = new System.Drawing.Size(100, 22);
            this.txtTypeUser.TabIndex = 6;
            this.txtTypeUser.TextChanged += new System.EventHandler(this.txtTypeUser_TextChanged);
            this.txtTypeUser.Leave += new System.EventHandler(this.txtTypeUser_Leave);
            // 
            // txtPasswordUser
            // 
            this.txtPasswordUser.Location = new System.Drawing.Point(187, 127);
            this.txtPasswordUser.Name = "txtPasswordUser";
            this.txtPasswordUser.Size = new System.Drawing.Size(100, 22);
            this.txtPasswordUser.TabIndex = 7;
            this.txtPasswordUser.UseSystemPasswordChar = true;
            // 
            // txtPassword2User
            // 
            this.txtPassword2User.Location = new System.Drawing.Point(187, 165);
            this.txtPassword2User.Name = "txtPassword2User";
            this.txtPassword2User.Size = new System.Drawing.Size(100, 22);
            this.txtPassword2User.TabIndex = 8;
            this.txtPassword2User.UseSystemPasswordChar = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(187, 224);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Вихід";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 290);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtPassword2User);
            this.Controls.Add(this.txtPasswordUser);
            this.Controls.Add(this.txtTypeUser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNameUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddNewUser);
            this.Name = "AddNewUser";
            this.Text = "AddNewUser";
            this.Load += new System.EventHandler(this.AddNewUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddNewUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTypeUser;
        private System.Windows.Forms.TextBox txtPasswordUser;
        private System.Windows.Forms.TextBox txtPassword2User;
        private System.Windows.Forms.Button btnExit;
    }
}