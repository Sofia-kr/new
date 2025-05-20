namespace ОБЗД
{
    partial class EditDoslidnyk
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
            this.txtSerVyd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWhere = new System.Windows.Forms.TextBox();
            this.txtSetVik = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSetName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSerVyd
            // 
            this.txtSerVyd.Location = new System.Drawing.Point(445, 134);
            this.txtSerVyd.Name = "txtSerVyd";
            this.txtSerVyd.Size = new System.Drawing.Size(100, 22);
            this.txtSerVyd.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(361, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "SET (Введіть нове значення для прізвища дослідника)";
            // 
            // txtWhere
            // 
            this.txtWhere.Location = new System.Drawing.Point(378, 43);
            this.txtWhere.Name = "txtWhere";
            this.txtWhere.Size = new System.Drawing.Size(167, 22);
            this.txtWhere.TabIndex = 17;
            // 
            // txtSetVik
            // 
            this.txtSetVik.Location = new System.Drawing.Point(445, 87);
            this.txtSetVik.Name = "txtSetVik";
            this.txtSetVik.Size = new System.Drawing.Size(100, 22);
            this.txtSetVik.TabIndex = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(289, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(162, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(89, 232);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(126, 23);
            this.btnReplace.TabIndex = 14;
            this.btnReplace.Text = "Змінити";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "WHERE (ID дослідника =):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "SET (Введіть нове значення для Ім\'я дослідника)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(387, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "SET (Введіть нове значення для Місця роботи дослідника)";
            // 
            // txtSetName
            // 
            this.txtSetName.Location = new System.Drawing.Point(445, 171);
            this.txtSetName.Name = "txtSetName";
            this.txtSetName.Size = new System.Drawing.Size(100, 22);
            this.txtSetName.TabIndex = 21;
            // 
            // EditDoslidnyk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 286);
            this.Controls.Add(this.txtSetName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSerVyd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWhere);
            this.Controls.Add(this.txtSetVik);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditDoslidnyk";
            this.Text = "EditDoslidnyk";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSerVyd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWhere;
        private System.Windows.Forms.TextBox txtSetVik;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSetName;
    }
}