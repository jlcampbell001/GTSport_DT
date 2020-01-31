using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTSport_DT.Regions
{
    public partial class RegionsForm : Form
    {
        private NpgsqlConnection con;
        private RegionsService regionsService;

        private Region workingRegion = new Region();

        public RegionsForm()
        {
            InitializeComponent();
        }

        public RegionsForm(NpgsqlConnection con)
        {
            this.con = con ?? throw new ArgumentNullException(nameof(con));

            regionsService = new RegionsService(con);

            InitializeComponent();

            // add test data to work with.
            // AddTestData();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateList();
            SetButtons();
        }

        private void tvRegions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workingRegion = (Region)tvRegions.SelectedNode.Tag;

            SetToWorkingRegion();

            SetButtons();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateWorkingRegion();

            try
            {
                regionsService.Save(ref workingRegion);

                UpdateList();

                SetSelected(workingRegion.PrimaryKey);

                UpdateOtherForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            txtDescription.Focus();
            SetButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (workingRegion.PrimaryKey != null)
            {
                if (MessageBox.Show("Do you wish to delete region " + workingRegion.Description + "?", "Delete Region", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        regionsService.Delete(workingRegion.PrimaryKey);

                        UpdateList();

                        if (tvRegions.Nodes.Count > 0)
                        {
                            tvRegions.SelectedNode = tvRegions.Nodes[0];
                        }
                        else
                        {
                            workingRegion = new Region();
                            SetToWorkingRegion();
                        }

                        UpdateOtherForms();

                        tvRegions.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a region to delete first.", "Error");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            workingRegion = new Region();

            SetToWorkingRegion();

            txtDescription.Focus();

            SetButtons();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetToWorkingRegion();

            txtDescription.Focus();

            SetButtons();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        // ********************************************************************************
        /// <summary>
        /// Update the tree list.
        /// </summary>
        // ********************************************************************************
        private void UpdateList()
        {
            List<Region> regions = regionsService.GetList(true);

            tvRegions.BeginUpdate();
            tvRegions.Nodes.Clear();

            foreach (Region region in regions)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = region;
                treeNode.Text = region.Description;

                tvRegions.Nodes.Add(treeNode);
            }

            tvRegions.EndUpdate();
        }

        // ********************************************************************************
        /// <summary>
        /// Some test data to work with.
        /// </summary>
        // ********************************************************************************
        private void AddTestData()
        {
            Region region = new Region("", "AMERICA");
            regionsService.Save(ref region);

            region = new Region("", "EUROPE");
            regionsService.Save(ref region);

            region = new Region("", "ASIA-PACIFIC");
            regionsService.Save(ref region);
        }

        // ********************************************************************************
        /// <summary>
        /// Set the fields to the values in the work entity.
        /// </summary>
        // ********************************************************************************
        private void SetToWorkingRegion()
        {
            txtDescription.Text = workingRegion.Description;
        }

        // ********************************************************************************
        /// <summary>
        /// Set the working entity to the values of the fields.
        /// </summary>
        // ********************************************************************************
        private void UpdateWorkingRegion()
        {
            workingRegion.Description = txtDescription.Text;
        }

        // ********************************************************************************
        /// <summary>
        /// Sets the tree views selected region to the region with the passed primary key.
        /// </summary>
        /// <param name="primaryKey"></param>
        // ********************************************************************************
        private void SetSelected(string primaryKey)
        {
            if (!String.IsNullOrWhiteSpace(primaryKey))
            {
                for (int i = 0; i < tvRegions.Nodes.Count; i++)
                {
                    Region region = (Region)tvRegions.Nodes[i].Tag;
                    if (region.PrimaryKey == primaryKey)
                    {
                        tvRegions.SelectedNode = tvRegions.Nodes[i];
                    }
                }
            }
        }

        // ********************************************************************************
        /// <summary>
        /// Enabled / Disable the buttons.
        /// </summary>
        // ********************************************************************************
        private void SetButtons()
        {
            if (workingRegion.Description != txtDescription.Text)
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

            }

            if (String.IsNullOrWhiteSpace(workingRegion.PrimaryKey))
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        private void UpdateOtherForms()
        {
            GTSportForm workingParentForm = (GTSportForm)this.ParentForm;

            workingParentForm.UpdateRegionsOnForms();
        }
    }
}
