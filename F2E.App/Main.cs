using System;
using System.Windows.Forms;
using F2E.App.Forms.Frm;

namespace F2E.App
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            if (FalloutFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                FalloutFolderPathTb.Text = FalloutFolderBrowserDialog.SelectedPath;
            }
        }

        private void FrmEditorBtn_Click(object sender, EventArgs e)
        {
            new FrmEditor().Show(this);
        }
    }
}
