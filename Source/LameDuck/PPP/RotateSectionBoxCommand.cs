using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.UI.Selection;

namespace LameDuck
{
    /// <summary>
    /// Description of Class2.
    /// </summary>

    public class selectedParameters
    {
        public int Angle { get; set; }
        public int PreviousAngle { get; set; }
        public bool actived { get; set; }
    }

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class RotateSectionBoxIE : IExternalEventHandler
    {
        public selectedParameters myselectedAngle { get; set; }
        public void Execute(UIApplication uiapp)
        {
            Document doc = uiapp.ActiveUIDocument.Document;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Element activeView = uidoc.ActiveView as Element;
            if ((activeView as View3D).IsSectionBoxActive)
            {
                if (!(activeView as View3D).IsSectionBoxActive)
                {
                    Autodesk.Revit.UI.TaskDialog.Show("Hey!", "Section box is not active");
                    return;
                }

                BoundingBoxXYZ box = (activeView as View3D).GetSectionBox();
                XYZ axis = new XYZ(0, 0, 1);
                XYZ midPoint = new XYZ((box.Min.X + box.Max.X) / 2, (box.Min.Y + box.Max.Y)/ 2, 0);

                try
                {
                    Transaction bboxRot = new Transaction(doc, "bboxRot");
                    bboxRot.Start();
                    Transform rotateRestore = Transform.CreateRotationAtPoint(axis, Math.PI / 180 * (-myselectedAngle.PreviousAngle), midPoint);
                    box.Transform = box.Transform.Multiply(rotateRestore);
                    Transform rotate = Transform.CreateRotationAtPoint(axis, Math.PI / 180 * (myselectedAngle.Angle), midPoint);
                    box.Transform = box.Transform.Multiply(rotate);
                    (activeView as View3D).SetSectionBox(box);
                    bboxRot.Commit();
                }
                catch
                {

                }
            }
        }
        public string GetName()
        {
            return "External Event Example";
        }

        public static implicit operator RotateSectionBoxIE(Squarefy v)
        {
            throw new NotImplementedException();
        }
    }

    public class Squarefy : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            Document doc = uiapp.ActiveUIDocument.Document;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Element activeView = uidoc.ActiveView as Element;

            BoundingBoxXYZ bbox = (activeView as View3D).GetSectionBox();

            var minXY = new XYZ(bbox.Min.X, bbox.Min.Y, 0);
            var HXY = new XYZ(bbox.Min.X, bbox.Max.Y, 0);
            var LXY = new XYZ(bbox.Max.X, bbox.Min.Y, 0);

            var H = minXY.DistanceTo(HXY);
            var L = minXY.DistanceTo(LXY);

            if(Math.Round(H,4)==Math.Round(L,4))
            {
                Autodesk.Revit.UI.TaskDialog.Show("Hey!", "Already Squarefied!");
            }
            else
            {
                try
                {
                    Transaction bboxRot = new Transaction(doc, "Squarefy");
                    if (L > H)
                    {
                        bboxRot.Start();
                        XYZ vecY = new XYZ(0, 1 * ((L - H) / 2), 0);
                        Transform traslYMIN = Transform.CreateTranslation(vecY.Negate());
                        Transform traslYMAX = Transform.CreateTranslation(vecY);

                        bbox.Max = traslYMAX.OfPoint(bbox.Max);
                        bbox.Min = traslYMIN.OfPoint(bbox.Min);

                        (activeView as View3D).SetSectionBox(bbox);

                        uidoc.RefreshActiveView();

                        bboxRot.Commit();
                    }
                    else
                    {
                        bboxRot.Start();
                        XYZ vecX = new XYZ((H - L) / 2, 0, 0);
                        Transform traslXMIN = Transform.CreateTranslation(vecX.Negate());
                        Transform traslXMAX = Transform.CreateTranslation(vecX);

                        bbox.Max = traslXMAX.OfPoint(bbox.Max);
                        bbox.Min = traslXMIN.OfPoint(bbox.Min);

                        (activeView as View3D).SetSectionBox(bbox);

                        uidoc.RefreshActiveView();

                        bboxRot.Commit();
                    }
                }
                catch
                {

                }
            }

        }
        public string GetName()
        {
            return "External Event Example";
        }
    }

    public class OrientToFace : IExternalEventHandler
    {
        public selectedParameters myselectedAngle { get; set; }

        public void Execute(UIApplication uiapp)
        {
            Document doc = uiapp.ActiveUIDocument.Document;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Element activeView = uidoc.ActiveView as Element;

            try
            {
                Reference pickedFace = uidoc.Selection.PickObject(ObjectType.Face);

                double famInstanceRot = 0;

                if ((doc.GetElement(pickedFace.ElementId).Location as LocationPoint) != null)
                {
                    famInstanceRot = (doc.GetElement(pickedFace.ElementId).Location as LocationPoint).Rotation;
                }

                //based on the PyRevit method

                GeometryObject faceGeo = doc.GetElement(pickedFace).GetGeometryObjectFromReference(pickedFace);

                XYZ faceNorm = (faceGeo as Face).ComputeNormal(new UV(0,0));

                BoundingBoxXYZ bbox = (activeView as View3D).GetSectionBox();

                XYZ normal = bbox.Transform.get_Basis(0).Normalize();
                double angle = faceNorm.AngleTo(normal);
                XYZ axis = new XYZ(0, 0, 1);
                XYZ midPoint = new XYZ((bbox.Min.X + bbox.Max.X) / 2, (bbox.Min.Y + bbox.Max.Y) / 2, 0);

                if (faceNorm.X < 0)
                {
                    Transform rotate = Transform.CreateRotationAtPoint(axis, Math.PI - (angle + famInstanceRot), midPoint);
                    bbox.Transform = bbox.Transform.Multiply(rotate);
                }
                else
                {
                    Transform rotate = Transform.CreateRotationAtPoint(axis, (angle + famInstanceRot), midPoint);
                    bbox.Transform = bbox.Transform.Multiply(rotate);
                }
                Transaction bboxRot = new Transaction(doc, "bboxRot");
                bboxRot.Start();
                (activeView as View3D).SetSectionBox(bbox);
                bboxRot.Commit();
            }
            catch
            {

            }
        }
         public string GetName()
        {
            return "External Event Example";
        }
    }
}
