using System;
using System.Drawing;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace LameDuck
{
    public partial class RotateSectionBoxForm : System.Windows.Forms.Form
    {
        public Document doc { get; set; }

        RotateSectionBoxIE myRSBParameter;
        ExternalEvent myRSBActionParameter;

        Squarefy mySQFParameter;
        ExternalEvent mySQFActionParameter;

        OrientToFace myOTFParameter;
        ExternalEvent myOTFActionParameter;

        selectedParameters mySelectedAngle = null;

        int previousValue = 0;

        public RotateSectionBoxForm()
        {
            InitializeComponent();
            myRSBParameter = new RotateSectionBoxIE();
            myRSBActionParameter = ExternalEvent.Create(myRSBParameter);
            mySelectedAngle = new selectedParameters();
            mySelectedAngle.Angle = 0;
            mySelectedAngle.PreviousAngle = 0;
            mySelectedAngle.actived = true;

            mySQFParameter = new Squarefy();
            mySQFActionParameter = ExternalEvent.Create(mySQFParameter);

            myOTFParameter = new OrientToFace();
            myOTFActionParameter = ExternalEvent.Create(myOTFParameter);

            Enabled = mySelectedAngle.actived;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                label4.Text = trackBar1.Value.ToString();
                label6.Text = "";
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            mySelectedAngle.Angle = trackBar1.Value;
            mySelectedAngle.PreviousAngle = previousValue;
            myRSBParameter.myselectedAngle = mySelectedAngle;
            previousValue = trackBar1.Value;
            myRSBActionParameter.Raise();
            label6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mySQFActionParameter.Raise();
            label6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (doc.ActiveView.GetType().ToString() == "Autodesk.Revit.DB.View3D")
            {
                if ((doc.ActiveView as View3D).IsSectionBoxActive)
                {
                    //this.WindowState = FormWindowState.Minimized;
                    myOTFActionParameter.Raise();
                    label6.Text = "Pick a vertical Face";
                }
                else
                {
                    TaskDialog.Show("Hey!", "Section box isn't active");
                }
            }
            else
            {
                TaskDialog.Show("Hey!", "That's not a 3d View");
            }
        }
    }
}
