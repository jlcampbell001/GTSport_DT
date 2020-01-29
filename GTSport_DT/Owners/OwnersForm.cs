using GTSport_DT.General.KeySequence;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GTSport_DT.Owners
{
    public partial class OwnersForm : Form
    {
        private OwnersService ownersService;

        private Owner workingOwner = new Owner();

        public OwnersForm()
        {
            InitializeComponent();
        }

        public OwnersForm(OwnersService ownersService)
        {
            this.ownersService = ownersService ?? throw new ArgumentNullException(nameof(ownersService));

            // add some data.
            // AddTestData();



            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateList();
            SetButtons();
        }


        // ********************************************************************************
        /// <summary>
        /// Update the tree list.
        /// </summary>
        // ********************************************************************************
        private void UpdateList()
        {
            // make sure there is a default owner set.
            ownersService.GetDefaultOwner();

            List<Owner> owners = ownersService.GetList(true);

            tvOwners.BeginUpdate();
            tvOwners.Nodes.Clear();

            foreach (Owner owner in owners)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = owner;
                treeNode.Text = owner.OwnerName;

                if (owner.DefaultOwner)
                {
                    treeNode.ForeColor = Color.Blue;
                    Font font = new Font(tvOwners.Font, FontStyle.Bold);
                    treeNode.NodeFont = font;
                }

                tvOwners.Nodes.Add(treeNode);
            }

            tvOwners.EndUpdate();
        }

        private void AddTestData()
        {
            ownersService.GetDefaultOwner();

            Owner owner = new Owner("", "Jonathan", false);
            ownersService.Save(ref owner);

            owner = new Owner("", "Ernest", false);
            ownersService.Save(ref owner);

            owner = new Owner("", "Jacquetta", false);
            ownersService.Save(ref owner);

            owner = new Owner("", "Temple", false);
            ownersService.Save(ref owner);

            owner = new Owner("", "Flint", false);
            ownersService.Save(ref owner);

            owner = new Owner("", "Gaynor", false);
            ownersService.Save(ref owner);
        }

        // ********************************************************************************
        /// <summary>
        /// Set up the working owner with the selected owner from the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ********************************************************************************
        private void tvOwners_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workingOwner = (Owner)tvOwners.SelectedNode.Tag;

            SetToWorkingOwner();

            SetButtons();
        }

        // ********************************************************************************
        /// <summary>
        /// Set the user entry fields with the data from the working owner object.
        /// </summary>
        // ********************************************************************************
        private void SetToWorkingOwner()
        {
            txtOwnerName.Text = workingOwner.OwnerName;

            chkDefaultOwner.Checked = workingOwner.DefaultOwner;
        }

        // ********************************************************************************
        /// <summary>
        /// Set the working owner object with data entered by the user.
        /// </summary>
        // ********************************************************************************
        private void UpdateWorkingOwner()
        {
            workingOwner.OwnerName = txtOwnerName.Text;
            workingOwner.DefaultOwner = chkDefaultOwner.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateWorkingOwner();

            try
            {
                ownersService.Save(ref workingOwner);

                UpdateList();

                UpdateCurrentOwnerInParent(workingOwner);

                SetSelectedOwner(workingOwner.PrimaryKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            txtOwnerName.Focus();
            SetButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (workingOwner.PrimaryKey != null)
            {
                if (MessageBox.Show("Do you wish to delete owner " + workingOwner.OwnerName + "?", "Delete Owner", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        ownersService.Delete(workingOwner.PrimaryKey);

                        UpdateList();

                        UpdateCurrentOwnerInParent(workingOwner, deleted: true);

                        tvOwners.SelectedNode = tvOwners.Nodes[0];

                        tvOwners.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select an owner to delete first.", "Error");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            workingOwner = new Owner();

            SetToWorkingOwner();

            txtOwnerName.Focus();

            SetButtons();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetToWorkingOwner();

            txtOwnerName.Focus();

            SetButtons();
        }

        // ********************************************************************************
        /// <summary>
        /// Enabled / Disable the buttons.
        /// </summary>
        // ********************************************************************************
        private void SetButtons()
        {
            if (workingOwner.OwnerName != txtOwnerName.Text || workingOwner.DefaultOwner != chkDefaultOwner.Checked)
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnSetCurrentOwner.Enabled = false;
            }
            else
            {
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnSetCurrentOwner.Enabled = true;
            }

            if (String.IsNullOrWhiteSpace(workingOwner.PrimaryKey))
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        private void txtOwnerName_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void chkDefaultOwner_CheckedChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        // ********************************************************************************
        /// <summary>
        /// Updates the current owner information on the main form if there are changes done to the owner or a new owner is picked.
        /// <para>If the current owner was deleted the current owner will be set to the default owner.</para>
        /// </summary>
        /// <param name="owner">The owner that was updated.</param>
        /// <param name="deleted">If the owner was deleted.</param>
        /// <param name="force">Forces the current owner to be changed to the passed owner.</param>
        // ********************************************************************************
        private void UpdateCurrentOwnerInParent(Owner owner, Boolean deleted = false, Boolean force = false)
        {
            GTSportForm workingParentForm = (GTSportForm)this.ParentForm;

            if (force || workingParentForm.currentOwner.PrimaryKey == owner.PrimaryKey)
            {
                if (deleted)
                {
                    // get the new default owner.
                    workingParentForm.SetCurrentOwner(ownersService.GetDefaultOwner());
                } else
                {
                    // update any changes to the current owner.
                    workingParentForm.SetCurrentOwner(owner);
                }
            }
        }

        private void btnSetCurrentOwner_Click(object sender, EventArgs e)
        {
            UpdateCurrentOwnerInParent(workingOwner, force: true);
        }

        // ********************************************************************************
        /// <summary>
        /// Sets the tree views selected owner to the owner with the passed primary key.
        /// </summary>
        /// <param name="primaryKey"></param>
        // ********************************************************************************
        private void SetSelectedOwner(string primaryKey)
        {
            if (!String.IsNullOrWhiteSpace(primaryKey))
            {
                for (int i = 0; i < tvOwners.Nodes.Count; i++)
                {
                    Owner owner = (Owner)tvOwners.Nodes[i].Tag;
                    if (owner.PrimaryKey == primaryKey)
                    {
                        tvOwners.SelectedNode = tvOwners.Nodes[i];
                    }
                }
            }
        }
    }
}
