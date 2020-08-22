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
    [Autodesk.Revit.DB.Macros.AddInId("2864B5A6-EE3E-41CE-9CD9-82B788EC0CA9")]

    public partial class TotalSectionBoxTool
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
            TotalSectionBox(commandData.Application.ActiveUIDocument);
            return Result.Succeeded;
        }



        #region Revit Macros generated code
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Module_Startup);
            this.Shutdown += new System.EventHandler(Module_Shutdown);
        }
        #endregion
        public void TotalSectionBox(UIDocument uidoc)
        {
            Document doc = uidoc.Document;
            Element activeView = uidoc.ActiveView as Element;
            IList<Element> RVTLinkInstances = new FilteredElementCollector(doc).OfClass(typeof(RevitLinkInstance)).ToElements().ToList<Element>();
            var doclist = new List<Document>();

            for (int i = 0; i < RVTLinkInstances.Count; i++)
            {
                RevitLinkInstance l1 = RVTLinkInstances[i] as RevitLinkInstance;
                Document d1 = l1.GetLinkDocument();
                doclist.Add(d1);
            }

            try
            {
                Reference pickedElement = uidoc.Selection.PickObject(ObjectType.LinkedElement);
                ElementId lEleID = pickedElement.LinkedElementId;

                /* debug task dialog
                TaskDialog kk = new TaskDialog("kjk");
                kk.MainContent = doc.ActiveView.UniqueId+ "\n"+ uidoc.ActiveView.UniqueId;
                kk.Show();
                */

                if (activeView.GetType().ToString() != "Autodesk.Revit.DB.View3D")
                {
                    var dddTypes = new List<ViewFamilyType>();
                    FilteredElementCollector collector = new FilteredElementCollector(doc);
                    ICollection<Element> viewTypesInDoc = collector.OfClass(typeof(ViewFamilyType)).ToElements();
                    foreach (ViewFamilyType v in viewTypesInDoc)
                    {
                        if (v.ViewFamily == ViewFamily.ThreeDimensional)
                        {
                            dddTypes.Add(v);
                        }
                    }
                    Transaction newDDD = new Transaction(doc, "newDDD");
                    newDDD.Start();
                    View3D new3D = View3D.CreateIsometric(doc, dddTypes.First<ViewFamilyType>().Id);
                    newDDD.Commit();
                    uidoc.ActiveView = new3D;
                    activeView = new3D;
                }

                Transaction newSectionBox = new Transaction(doc, "newSectionBox");
                newSectionBox.Start();
                foreach(Document lDoc in doclist)
                {
                    if (lDoc.IsLinked)
                    {
                        try
                        {
                            BoundingBoxXYZ lEleBbox = lDoc.GetElement(lEleID).get_BoundingBox(activeView as View3D);
                            (activeView as View3D).SetSectionBox(lEleBbox);
                            newSectionBox.Commit();
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}