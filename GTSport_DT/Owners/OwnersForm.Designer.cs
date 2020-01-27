namespace GTSport_DT.Owners
{
    partial class OwnersForm
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
            this.trevOwners = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOwnerName = new System.Windows.Forms.TextBox();
            this.chkDefaultOwner = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSetCurrentOwner = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trevOwners
            // 
            this.trevOwners.Dock = System.Windows.Forms.DockStyle.Left;
            this.trevOwners.Location = new System.Drawing.Point(0, 0);
            this.trevOwners.Name = "trevOwners";
            this.trevOwners.Size = new System.Drawing.Size(225, 450);
            this.trevOwners.TabIndex = 0;
            this.trevOwners.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trevOwners_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:  ";
            // 
            // txtOwnerName
            // 
            this.txtOwnerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOwnerName.Location = new System.Drawing.Point(286, 41);
            this.txtOwnerName.Name = "txtOwnerName";
            this.txtOwnerName.Size = new System.Drawing.Size(502, 20);
            this.txtOwnerName.TabIndex = 3;
            this.txtOwnerName.TextChanged += new System.EventHandler(this.txtOwnerName_TextChanged);
            // 
            // chkDefaultOwner
            // 
            this.chkDefaultOwner.AutoSize = true;
            this.chkDefaultOwner.Location = new System.Drawing.Point(240, 76);
            this.chkDefaultOwner.Name = "chkDefaultOwner";
            this.chkDefaultOwner.Size = new System.Drawing.Size(94, 17);
            this.chkDefaultOwner.TabIndex = 4;
            this.chkDefaultOwner.Text = "Default Owner";
            this.chkDefaultOwner.UseVisualStyleBackColor = true;
            this.chkDefaultOwner.CheckedChanged += new System.EventHandler(this.chkDefaultOwner_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(240, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(321, 415);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(632, 415);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "New Owner";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(713, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSetCurrentOwner
            // 
            this.btnSetCurrentOwner.AutoSize = true;
            this.btnSetCurrentOwner.Location = new System.Drawing.Point(231, 12);
            this.btnSetCurrentOwner.Name = "btnSetCurrentOwner";
            this.btnSetCurrentOwner.Size = new System.Drawing.Size(119, 23);
            this.btnSetCurrentOwner.TabIndex = 1;
            this.btnSetCurrentOwner.Text = "Set As Current Owner";
            this.btnSetCurrentOwner.UseVisualStyleBackColor = true;
            this.btnSetCurrentOwner.Click += new System.EventHandler(this.btnSetCurrentOwner_Click);
            // 
            // OwnersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSetCurrentOwner);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkDefaultOwner);
            this.Controls.Add(this.txtOwnerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trevOwners);
            this.Name = "OwnersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Owners";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trevOwners;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOwnerName;
        private System.Windows.Forms.CheckBox chkDefaultOwner;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSetCurrentOwner;
    }
}