
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
    [Autodesk.Revit.DB.Macros.AddInId("2F895F0B-17A2-4E82-A7FE-8E0128B4AECB")]

    public partial class AboutTool
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
            About(commandData.Application.ActiveUIDocument);
            return Result.Succeeded;
        }



        #region Revit Macros generated code
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Module_Startup);
            this.Shutdown += new System.EventHandler(Module_Shutdown);
        }
        #endregion
        public void About(UIDocument uidoc)
        {

            TaskDialog about = new TaskDialog("About...");
            about.MainContent = "Lame Duck - 2020";
            about.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Made by Pierpaolo Canini");
            about.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Lame Duck GitHub");
            about.MainInstruction = "Hi!";

            TaskDialogResult tdResult = about.Show();

            bool Yes = true;

            if (TaskDialogResult.CommandLink1 == tdResult)
            {
                Yes = true;

            }
            else if (TaskDialogResult.CommandLink2 == tdResult)
            {
                Yes = false;
            }
            else
            {
                return;
            }

            if (Yes)
            {
                System.Diagnostics.Process.Start("https://www.linkedin.com/in/pierpaolocanini/");
            }
            if (!Yes)
            {
                System.Diagnostics.Process.Start("https://github.com/pierpaolo-canini/");
            }
        }
    }
}