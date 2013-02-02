using System;
using System.Windows.Forms;

namespace RBuild.Forms
{
    public partial class frmResult : Form
    {
        #region "Constructors"

        public frmResult()
        {
            InitializeComponent();
        }//constructor

        #endregion

        #region "Properties"

        public string Result { get; set; }//property

        #endregion

        #region "Events"

        private void frmResult_Load(object sender, EventArgs e)
        {
            this.Width = Screen.GetWorkingArea(this).Width;
            this.Top = this.Left = 0;
            tx_Result.Text = this.Result;
        }//void

        #endregion
    }//class
}//namespace
