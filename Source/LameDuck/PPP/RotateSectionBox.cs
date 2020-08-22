using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace LameDuck
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("7C87F687-AE39-4ADE-9914-3C08D92C8478")]

    public partial class RotateSectionBoxTool
    {
        public EventHandler Shutdown { get; private set; }
        public EventHandler Startup { get; private set; }

        private void Module_Startup(object sender, EventArgs e)
        {

        }

        private void Module_Shutdown(object sender, EventArgs e)
        {

        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RotateSectionBox(commandData.Application.ActiveUIDocument);
            return Result.Succeeded;
        }



        #region Revit Macros generated code
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Module_Startup);
            this.Shutdown += new System.EventHandler(Module_Shutdown);
        }
        #endregion
        public Result RotateSectionBox(UIDocument uidoc)
        {
            if(uidoc.ActiveView.GetType().ToString() == "Autodesk.Revit.DB.View3D")
            {
                if ((uidoc.ActiveView as View3D).IsSectionBoxActive)
                {
                    RotateSectionBoxForm myForm1 = new RotateSectionBoxForm();
                    myForm1.doc = uidoc.Document;
                    myForm1.Show();
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
            return Result.Succeeded;
        }
    }
}