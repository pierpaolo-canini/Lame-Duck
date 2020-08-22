using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace LameDuck
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("8F6AB6D4-FF2D-48D1-999B-45D491B5225C")]

    /*
    public class mySettings
    {
        public XYZ storedbboxMIN { get; set; }
        public XYZ storedbboxMAX { get; set; }
    }
    public static class mySettingsSchema
    {
        readonly static Guid schemaGuid = new Guid("{D18A6D82-5A57-4E8B-91FE-5BC1BA56BD5F}");

        public static Schema GetSchema()
        {

            Schema schema = Schema.Lookup(schemaGuid);
            if (schema != null) return schema;

            SchemaBuilder schemaBuilder = new SchemaBuilder(schemaGuid);
            schemaBuilder.SetSchemaName("myMINMAXSettings");
            schemaBuilder.AddSimpleField("storedbboxMIN", typeof(XYZ));
            schemaBuilder.AddSimpleField("storedbboxMAX", typeof(XYZ));

            return schemaBuilder.Finish();
        }
    }
    */
    public partial class ToggleSectionBoxTool
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
            ToggleSectionBox(commandData.Application.ActiveUIDocument);
            return Result.Succeeded;
        }

        #region Revit Macros generated code
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Module_Startup);
            this.Shutdown += new System.EventHandler(Module_Shutdown);
        }
        #endregion
        public void ToggleSectionBox(UIDocument uidoc)
        {
            Document doc = uidoc.Document;
            Element activeView = uidoc.ActiveView as Element;

            if (doc.ActiveView.GetType().ToString() == "Autodesk.Revit.DB.View3D")
            {
                try
                {
                    if ((doc.ActiveView as View3D).IsSectionBoxActive)
                    {
                        /*
                        try
                        {
                            Transaction dataStorage = new Transaction(doc, "Data Storage");
                            dataStorage.Start();
                            TaskDialog.Show("h", "1");
                            DataStorage pointsStorage = DataStorage.Create(doc);
                            Entity entity = new Entity(mySettingsSchema.GetSchema());
                            TaskDialog.Show("h", "2");
                            entity.Set("storedbboxMIN", ((activeView as View3D).GetSectionBox().Min) as XYZ);
                            entity.Set("storedbboxMAX", ((activeView as View3D).GetSectionBox().Max) as XYZ);
                            TaskDialog.Show("h", "3");
                            pointsStorage.SetEntity(entity);
                            dataStorage.Commit();
                        }
                        catch
                        {
                            TaskDialog.Show("h", "h");
                        }
                        */
                        Transaction tSbboxOff = new Transaction(doc, "Toggle Section Box Off");
                        tSbboxOff.Start();
                        (activeView as View3D).IsSectionBoxActive = false;
                        tSbboxOff.Commit();
                    }
                    else
                    {
                        Transaction tSbboxOn = new Transaction(doc, "Toggle Section Box On");
                        tSbboxOn.Start();
                        (activeView as View3D).IsSectionBoxActive = true;
                        tSbboxOn.Commit();
                    }
                }
                catch
                {

                }
            }
            else
            {
                TaskDialog.Show("Hey!", "That's not a 3d View");
            }
        }
    }
}