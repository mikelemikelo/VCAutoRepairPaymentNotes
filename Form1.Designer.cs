
namespace VCAutoRepairPaymentNotes
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
            this.checkBody = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.monto = new System.Windows.Forms.TextBox();
            this.middleSummary = new System.Windows.Forms.Label();
            this.bottomSummary = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.memo = new System.Windows.Forms.TextBox();
            this.resetFormButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.checkDateTextBox = new System.Windows.Forms.TextBox();
            this.optionalInternalNotesField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBody
            // 
            this.checkBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBody.Location = new System.Drawing.Point(12, 41);
            this.checkBody.Name = "checkBody";
            this.checkBody.Size = new System.Drawing.Size(595, 35);
            this.checkBody.TabIndex = 0;
            this.checkBody.TextChanged += new System.EventHandler(this.refreshSummaryDueToTextChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pay to the order of";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(613, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "$";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // monto
            // 
            this.monto.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monto.Location = new System.Drawing.Point(654, 41);
            this.monto.Name = "monto";
            this.monto.Size = new System.Drawing.Size(211, 38);
            this.monto.TabIndex = 1;
            this.monto.Leave += new System.EventHandler(this.monto_Leave);
            // 
            // middleSummary
            // 
            this.middleSummary.AutoSize = true;
            this.middleSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.middleSummary.Location = new System.Drawing.Point(12, 296);
            this.middleSummary.Name = "middleSummary";
            this.middleSummary.Size = new System.Drawing.Size(2, 15);
            this.middleSummary.TabIndex = 5;
            // 
            // bottomSummary
            // 
            this.bottomSummary.AutoSize = true;
            this.bottomSummary.Location = new System.Drawing.Point(23, 419);
            this.bottomSummary.Name = "bottomSummary";
            this.bottomSummary.Size = new System.Drawing.Size(0, 13);
            this.bottomSummary.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(737, 189);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 60);
            this.button1.TabIndex = 4;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.printButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Memo:";
            // 
            // memo
            // 
            this.memo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memo.Location = new System.Drawing.Point(12, 101);
            this.memo.Multiline = true;
            this.memo.Name = "memo";
            this.memo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.memo.Size = new System.Drawing.Size(853, 60);
            this.memo.TabIndex = 2;
            this.memo.TextChanged += new System.EventHandler(this.refreshSummaryDueToTextChange);
            // 
            // resetFormButton
            // 
            this.resetFormButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.resetFormButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetFormButton.Location = new System.Drawing.Point(12, 419);
            this.resetFormButton.Name = "resetFormButton";
            this.resetFormButton.Size = new System.Drawing.Size(127, 34);
            this.resetFormButton.TabIndex = 5;
            this.resetFormButton.Text = "Create a new one";
            this.resetFormButton.UseVisualStyleBackColor = false;
            this.resetFormButton.Click += new System.EventHandler(this.resetFormButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Summary:";
            // 
            // checkDateTextBox
            // 
            this.checkDateTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkDateTextBox.Location = new System.Drawing.Point(654, 3);
            this.checkDateTextBox.Name = "checkDateTextBox";
            this.checkDateTextBox.Size = new System.Drawing.Size(211, 29);
            this.checkDateTextBox.TabIndex = 12;
            this.checkDateTextBox.TextChanged += new System.EventHandler(this.refreshSummaryDueToTextChange);
            // 
            // optionalInternalNotesField
            // 
            this.optionalInternalNotesField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionalInternalNotesField.Location = new System.Drawing.Point(12, 189);
            this.optionalInternalNotesField.Multiline = true;
            this.optionalInternalNotesField.Name = "optionalInternalNotesField";
            this.optionalInternalNotesField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.optionalInternalNotesField.Size = new System.Drawing.Size(694, 60);
            this.optionalInternalNotesField.TabIndex = 3;
            this.optionalInternalNotesField.TextChanged += new System.EventHandler(this.refreshSummaryDueToTextChange);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(422, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "**Optional - Internal notes (These notes will only be included in the summary if " +
    "not empty)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(818, 464);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "V1.0.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 486);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.optionalInternalNotesField);
            this.Controls.Add(this.checkDateTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.resetFormButton);
            this.Controls.Add(this.memo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bottomSummary);
            this.Controls.Add(this.middleSummary);
            this.Controls.Add(this.monto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBody);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "A C Auto Repair";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox checkBody;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox monto;
        private System.Windows.Forms.Label middleSummary;
        private System.Windows.Forms.Label bottomSummary;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox memo;
        private System.Windows.Forms.Button resetFormButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox checkDateTextBox;
        private System.Windows.Forms.TextBox optionalInternalNotesField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

