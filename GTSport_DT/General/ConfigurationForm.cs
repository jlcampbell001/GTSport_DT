using System;
using System.Windows.Forms;
using static GTSport_DT.General.EncryptStrings;

namespace GTSport_DT.General
{
    /// <summary>The form to allow the user to update the database configuration.</summary>
    /// <seealso cref="System.Windows.Forms.Form"/>
    public partial class ConfigurationForm : Form
    {
        private ConnectionConfiguration connectionConfiguration = new ConnectionConfiguration();

        /// <summary>Initializes a new instance of the <see cref="ConfigurationForm"/> class.</summary>
        public ConfigurationForm()
        {
            InitializeComponent();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            connectionConfiguration.LoadConnectionConfig();

            SetToConfiguration();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateConfiguration();

            connectionConfiguration.SaveConnectionConfig();

            this.DialogResult = DialogResult.OK;

            this.Close();
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
    }
}