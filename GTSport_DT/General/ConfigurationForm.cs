using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GTSport_DT.General.EncryptStrings;

namespace GTSport_DT.General
{
    public partial class ConfigurationForm : Form
    {
        ConnectionConfiguration connectionConfiguration = new ConnectionConfiguration();

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            connectionConfiguration.LoadConnectionConfig();

            SetToConfiguration();
        }

        private void SetToConfiguration()
        {
            txtHost.Text = connectionConfiguration.Host;
            txtPort.Text = connectionConfiguration.Port;
            txtDatabase.Text = connectionConfiguration.Database;
            txtUsername.Text = connectionConfiguration.UserName;
            txtPassword.Text = ToInsecureString(connectionConfiguration.Password);
        }

        private void UpdateConfiguration()
        {
            connectionConfiguration.Host = txtHost.Text;
            connectionConfiguration.Port = txtPort.Text;
            connectionConfiguration.Database = txtDatabase.Text;
            connectionConfiguration.UserName = txtUsername.Text;
            connectionConfiguration.Password = ToSecureString(txtPassword.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateConfiguration();

            connectionConfiguration.SaveConnectionConfig();

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
