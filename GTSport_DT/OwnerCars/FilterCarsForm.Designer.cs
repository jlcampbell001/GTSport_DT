namespace GTSport_DT.OwnerCars
{
    partial class FilterCarsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCategoryFrom = new System.Windows.Forms.ComboBox();
            this.cmbCategoryTo = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.nudYearFrom = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudYearTo = new System.Windows.Forms.NumericUpDown();
            this.nudMaxPowerFrom = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudMaxPowerTo = new System.Windows.Forms.NumericUpDown();
            this.cmbDrivetrain = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbManufacturer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRegion = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudYearFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYearTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPowerFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPowerTo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Category:";
            // 
            // cmbCategoryFrom
            // 
            this.cmbCategoryFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFrom.FormattingEnabled = true;
            this.cmbCategoryFrom.Location = new System.Drawing.Point(73, 12);
            this.cmbCategoryFrom.Name = "cmbCategoryFrom";
            this.cmbCategoryFrom.Size = new System.Drawing.Size(50, 21);
            this.cmbCategoryFrom.TabIndex = 27;
            // 
            // cmbCategoryTo
            // 
            this.cmbCategoryTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryTo.FormattingEnabled = true;
            this.cmbCategoryTo.Location = new System.Drawing.Point(129, 12);
            this.cmbCategoryTo.Name = "cmbCategoryTo";
            this.cmbCategoryTo.Size = new System.Drawing.Size(50, 21);
            this.cmbCategoryTo.TabIndex = 28;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 208);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 29;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(177, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // nudYearFrom
            // 
            this.nudYearFrom.Location = new System.Drawing.Point(70, 39);
            this.nudYearFrom.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.nudYearFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudYearFrom.Name = "nudYearFrom";
            this.nudYearFrom.Size = new System.Drawing.Size(50, 20);
            this.nudYearFrom.TabIndex = 32;
            this.nudYearFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudYearFrom.Enter += new System.EventHandler(this.nudYearFrom_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Year:";
            // 
            // nudYearTo
            // 
            this.nudYearTo.Location = new System.Drawing.Point(126, 39);
            this.nudYearTo.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.nudYearTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudYearTo.Name = "nudYearTo";
            this.nudYearTo.Size = new System.Drawing.Size(50, 20);
            this.nudYearTo.TabIndex = 33;
            this.nudYearTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudYearTo.Enter += new System.EventHandler(this.nudYearTo_Enter);
            // 
            // nudMaxPowerFrom
            // 
            this.nudMaxPowerFrom.Location = new System.Drawing.Point(73, 65);
            this.nudMaxPowerFrom.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMaxPowerFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudMaxPowerFrom.Name = "nudMaxPowerFrom";
            this.nudMaxPowerFrom.Size = new System.Drawing.Size(50, 20);
            this.nudMaxPowerFrom.TabIndex = 35;
            this.nudMaxPowerFrom.Enter += new System.EventHandler(this.nudMaxPower_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Power: Max:";
            // 
            // nudMaxPowerTo
            // 
            this.nudMaxPowerTo.Location = new System.Drawing.Point(129, 65);
            this.nudMaxPowerTo.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMaxPowerTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudMaxPowerTo.Name = "nudMaxPowerTo";
            this.nudMaxPowerTo.Size = new System.Drawing.Size(50, 20);
            this.nudMaxPowerTo.TabIndex = 36;
            this.nudMaxPowerTo.Enter += new System.EventHandler(this.nudMaxPowerTo_Enter);
            // 
            // cmbDrivetrain
            // 
            this.cmbDrivetrain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrivetrain.FormattingEnabled = true;
            this.cmbDrivetrain.Location = new System.Drawing.Point(70, 91);
            this.cmbDrivetrain.Name = "cmbDrivetrain";
            this.cmbDrivetrain.Size = new System.Drawing.Size(50, 21);
            this.cmbDrivetrain.TabIndex = 42;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Drivetrain:";
            // 
            // cmbManufacturer
            // 
            this.cmbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturer.FormattingEnabled = true;
            this.cmbManufacturer.Location = new System.Drawing.Point(77, 118);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new System.Drawing.Size(175, 21);
            this.cmbManufacturer.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Manufacturer:";
            // 
            // cmbCountry
            // 
            this.cmbCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(70, 145);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(181, 21);
            this.cmbCountry.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Countries:";
            // 
            // cmbRegion
            // 
            this.cmbRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegion.FormattingEnabled = true;
            this.cmbRegion.Location = new System.Drawing.Point(70, 172);
            this.cmbRegion.Name = "cmbRegion";
            this.cmbRegion.Size = new System.Drawing.Size(182, 21);
            this.cmbRegion.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Region:";
            // 
            // FilterCarsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 245);
            this.Controls.Add(this.cmbRegion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbCountry);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbManufacturer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDrivetrain);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.nudMaxPowerTo);
            this.Controls.Add(this.nudMaxPowerFrom);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudYearTo);
            this.Controls.Add(this.nudYearFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbCategoryTo);
            this.Controls.Add(this.cmbCategoryFrom);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterCarsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Car Filter";
            ((System.ComponentModel.ISupportInitialize)(this.nudYearFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYearTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPowerFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPowerTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCategoryFrom;
        private System.Windows.Forms.ComboBox cmbCategoryTo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nudYearFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudYearTo;
        private System.Windows.Forms.NumericUpDown nudMaxPowerFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudMaxPowerTo;
        private System.Windows.Forms.ComboBox cmbDrivetrain;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbManufacturer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRegion;
        private System.Windows.Forms.Label label5;
    }
}