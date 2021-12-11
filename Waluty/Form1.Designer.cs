namespace Waluty
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Currency = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Code = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Mid = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.effectiveDate = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FilterLabel = new System.Windows.Forms.ToolStripLabel();
            this.ShowAllLabels = new System.Windows.Forms.ToolStripLabel();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(93, 551);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(381, 41);
            this.button1.TabIndex = 4;
            this.button1.Text = "Aktualizuj bazę danych";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Currency,
            this.Code,
            this.Mid,
            this.effectiveDate});
            this.dataGridView1.Location = new System.Drawing.Point(12, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(549, 517);
            this.dataGridView1.TabIndex = 5;
            // 
            // Currency
            // 
            this.Currency.DataPropertyName = "Currency";
            this.Currency.HeaderText = "Waluta";
            this.Currency.Name = "Currency";
            this.Currency.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Kod";
            this.Code.Name = "Code";
            this.Code.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Mid
            // 
            this.Mid.DataPropertyName = "Mid";
            this.Mid.HeaderText = "Średni kurs";
            this.Mid.Name = "Mid";
            this.Mid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // effectiveDate
            // 
            this.effectiveDate.DataPropertyName = "effectiveDate";
            this.effectiveDate.HeaderText = "Data";
            this.effectiveDate.Name = "effectiveDate";
            this.effectiveDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilterLabel,
            this.ShowAllLabels});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(573, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FilterLabel
            // 
            this.FilterLabel.Name = "FilterLabel";
            this.FilterLabel.Size = new System.Drawing.Size(0, 22);
            this.FilterLabel.Visible = false;
            // 
            // ShowAllLabels
            // 
            this.ShowAllLabels.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.ShowAllLabels.Name = "ShowAllLabels";
            this.ShowAllLabels.Size = new System.Drawing.Size(90, 22);
            this.ShowAllLabels.Text = "Pokaż wszystkie";
            this.ShowAllLabels.Click += new System.EventHandler(this.ShowAllLabels_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Location = new System.Drawing.Point(480, 551);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 41);
            this.button2.TabIndex = 7;
            this.button2.Text = "Odśwież";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(573, 611);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Okno jak okno";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button button1;
        private DataGridView dataGridView1;
        private ToolStrip toolStrip1;
        private ToolStripLabel FilterLabel;
        private ToolStripLabel ShowAllLabels;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Currency;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Code;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Mid;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn effectiveDate;
        private Button button2;
    }
}