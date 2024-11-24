namespace Lab_3_PA
{
    partial class Form1
    {
        public System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxRecords;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDropDatabase;
        private System.Windows.Forms.Button btnAddDummyRecords;
        private System.Windows.Forms.TextBox txtNumRecords;
        private System.Windows.Forms.Button btnSearchByKey;

        private void InitializeComponent()
        {
            this.listBoxRecords = new System.Windows.Forms.ListBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDropDatabase = new System.Windows.Forms.Button();
            this.btnAddDummyRecords = new System.Windows.Forms.Button();
            this.txtNumRecords = new System.Windows.Forms.TextBox();
            this.btnSearchByKey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // listBoxRecords
            this.listBoxRecords.FormattingEnabled = true;
            this.listBoxRecords.Location = new System.Drawing.Point(12, 12);
            this.listBoxRecords.Name = "listBoxRecords";
            this.listBoxRecords.Size = new System.Drawing.Size(360, 200);
            this.listBoxRecords.TabIndex = 0;
            
            // txtKey
            this.txtKey.Location = new System.Drawing.Point(12, 230);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(100, 20);
            this.txtKey.TabIndex = 1;
            
            // txtData
            this.txtData.Location = new System.Drawing.Point(118, 230);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(254, 20);
            this.txtData.TabIndex = 2;
            
            // btnAddRecord
            this.btnAddRecord.Location = new System.Drawing.Point(12, 270);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Size = new System.Drawing.Size(75, 23);
            this.btnAddRecord.TabIndex = 3;
            this.btnAddRecord.Text = "Add Record";
            this.btnAddRecord.UseVisualStyleBackColor = true;
            this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
            
            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(93, 270);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit Record";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            
            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(174, 270);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete Record";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            
            // btnDropDatabase
            this.btnDropDatabase.Location = new System.Drawing.Point(255, 270);
            this.btnDropDatabase.Name = "btnDropDatabase";
            this.btnDropDatabase.Size = new System.Drawing.Size(100, 23);
            this.btnDropDatabase.TabIndex = 6;
            this.btnDropDatabase.Text = "Clear Database";
            this.btnDropDatabase.UseVisualStyleBackColor = true;
            this.btnDropDatabase.Click += new System.EventHandler(this.btnDropDatabase_Click);
            
            // btnAddDummyRecords
            this.btnAddDummyRecords.Location = new System.Drawing.Point(12, 300);
            this.btnAddDummyRecords.Name = "btnAddDummyRecords";
            this.btnAddDummyRecords.Size = new System.Drawing.Size(125, 23);
            this.btnAddDummyRecords.TabIndex = 7;
            this.btnAddDummyRecords.Text = "Add Dummy Records";
            this.btnAddDummyRecords.UseVisualStyleBackColor = true;
            this.btnAddDummyRecords.Click += new System.EventHandler(this.btnAddDummyRecords_Click);
            
            // txtNumRecords
            this.txtNumRecords.Location = new System.Drawing.Point(143, 302);
            this.txtNumRecords.Name = "txtNumRecords";
            this.txtNumRecords.Size = new System.Drawing.Size(100, 20);
            this.txtNumRecords.TabIndex = 8;
            
            // btnSearchByKey
            this.btnSearchByKey.Location = new System.Drawing.Point(249, 302);
            this.btnSearchByKey.Name = "btnSearchByKey";
            this.btnSearchByKey.Size = new System.Drawing.Size(75, 23);
            this.btnSearchByKey.TabIndex = 9;
            this.btnSearchByKey.Text = "Search By Key";
            this.btnSearchByKey.UseVisualStyleBackColor = true;
            this.btnSearchByKey.Click += new System.EventHandler(this.btnSearchByKey_Click);
            
            // Form1
            this.ClientSize = new System.Drawing.Size(384, 331);
            this.Controls.Add(this.btnSearchByKey);
            this.Controls.Add(this.txtNumRecords);
            this.Controls.Add(this.btnAddDummyRecords);
            this.Controls.Add(this.btnDropDatabase);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAddRecord);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.listBoxRecords);
            this.Name = "Form1";
            this.Text = "Database Management";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
