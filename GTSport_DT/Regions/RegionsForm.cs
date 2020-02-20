using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTSport_DT.Regions
{
    /// <summary>The form for the regions.</summary>
    /// <seealso cref="System.Windows.Forms.Form"/>
    public partial class RegionsForm : Form
    {
        private NpgsqlConnection con;
        private RegionsService regionsService;

        private Region workingRegion = new Region();

        /// <summary>Initializes a new instance of the <see cref="RegionsForm"/> class.</summary>
        public RegionsForm()
        {
            InitializeComponent();
        }

        /// <summary>Initializes a new instance of the <see cref="RegionsForm"/> class.</summary>
        /// <param name="con">The con.</param>
        /// <exception cref="ArgumentNullException">con</exception>
        public RegionsForm(NpgsqlConnection con)
        {
            this.con = con ?? throw new ArgumentNullException(nameof(con));

            regionsService = new RegionsService(con);

            InitializeComponent();

            // add test data to work with. AddTestData();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateList();
            SetButtons();
        }

        private void AddTestData()
        {
            Region region = new Region("", "AMERICA");
            regionsService.Save(ref region);

            region = new Region("", "EUROPE");
            regionsService.Save(ref region);

            region = new Region("", "ASIA-PACIFIC");
            regionsService.Save(ref region);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetToWorkingRegion();

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

        private void SetToWorkingRegion()
        {
            txtDescription.Text = workingRegion.Description;
        }

        private void tvRegions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workingRegion = (Region)tvRegions.SelectedNode.Tag;

            SetToWorkingRegion();

            SetButtons();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

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

        private void UpdateOtherForms()
        {
            GTSportForm workingParentForm = (GTSportForm)this.ParentForm;

            workingParentForm.UpdateRegionsOnForms();
        }

        private void UpdateWorkingRegion()
        {
            workingRegion.Description = txtDescription.Text;
        }
    }
}