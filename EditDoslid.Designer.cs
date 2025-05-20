namespace ОБЗД
{
    partial class EditDoslid
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
            this.txtSetStatus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWhere = new System.Windows.Forms.TextBox();
            this.txtSetData = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSetStatus
            // 
            this.txtSetStatus.Location = new System.Drawing.Point(481, 141);
            this.txtSetStatus.Name = "txtSetStatus";
            this.txtSetStatus.Size = new System.Drawing.Size(100, 22);
            this.txtSetStatus.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(362, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "SET (Введіть нове значення для статусу дослідження)";
            // 
            // txtWhere
            // 
            this.txtWhere.Location = new System.Drawing.Point(414, 53);
            this.txtWhere.Name = "txtWhere";
            this.txtWhere.Size = new System.Drawing.Size(167, 22);
            this.txtWhere.TabIndex = 17;
            // 
            // txtSetData
            // 
            this.txtSetData.Location = new System.Drawing.Point(481, 100);
            this.txtSetData.Name = "txtSetData";
            this.txtSetData.Size = new System.Drawing.Size(100, 22);
            this.txtSetData.TabIndex = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(307, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(162, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(107, 194);
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
            this.label2.Location = new System.Drawing.Point(38, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "WHERE (ID Досліда=):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(428, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "SET (Введіть нове значення для дати дослідження РРРР-ММ-ДД)";
            // 
            // EditDoslid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 244);
            this.Controls.Add(this.txtSetStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWhere);
            this.Controls.Add(this.txtSetData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditDoslid";
            this.Text = "EditDoslid";
            this.Load += new System.EventHandler(this.EditDoslid_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSetStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWhere;
        private System.Windows.Forms.TextBox txtSetData;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}