using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;


namespace LameDuckRibbon
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("6A459916-7B64-45EF-A35B-7FE11AC82B21")]
    public partial class LameDuckRibbonApp : IExternalApplication
    {
        public EventHandler Startup { get; private set; }
        public EventHandler Shutdown { get; private set; }

        public Result OnShutdown(UIControlledApplication a)
        { return Result.Succeeded; }

        public Result OnStartup(UIControlledApplication a)
        {
            string TabName = "Lame Duck";
            string path = Assembly.GetExecutingAssembly().Location;

            String exeConfigPath = Path.GetDirectoryName(path) + "\\LameDuckRibbon.dll";

            //Tab
            a.CreateRibbonTab(TabName);

            //Panels
            RibbonPanel PPPRSectionBox = a.CreateRibbonPanel(TabName, "Section Box");
            RibbonPanel PPPRCredits = a.CreateRibbonPanel(TabName, "Credits");

            // Buttons Data
            PushButtonData About = new PushButtonData("ABT", "About...", exeConfigPath, "LameDuckRibbon.InvokeABOUT");
            PushButtonData TotalSectionBox = new PushButtonData("TSB", "Linked Elements\nSection Box", exeConfigPath, "LameDuckRibbon.InvokeTSB");
            PushButtonData RotateSectionBox = new PushButtonData("RSB", "Rotate\nSection Box", exeConfigPath, "LameDuckRibbon.InvokeRSB");
            PushButtonData ToggleSectionBox = new PushButtonData("ToSB", "Toggle\nSection Box", exeConfigPath, "LameDuckRibbon.InvokeToSB");

            //Images and ToolTips


            //About.LargeImage = new BitmapImage(new Uri("pack://application:,,,/LameDuckRibbon;component/Resources/about_ico.png"));

            TotalSectionBox.LargeImage = new BitmapImage(new Uri("pack://application:,,,/LameDuckRibbon;component/Resources/LiElSb_ico.png"));
            TotalSectionBox.ToolTip = "Select your element and push the button";

            RotateSectionBox.LargeImage = new BitmapImage(new Uri("pack://application:,,,/LameDuckRibbon;component/Resources/RoSb_ico.png"));
            RotateSectionBox.ToolTip = "Move the slider to live rotate section box";

            ToggleSectionBox.LargeImage = new BitmapImage(new Uri("pack://application:,,,/LameDuckRibbon;component/Resources/ToSb_ico.png"));
            ToggleSectionBox.ToolTip = "Turn active view section box on/off";
            
            About.LargeImage = new BitmapImage(new Uri("pack://application:,,,/LameDuckRibbon;component/Resources/about_LD_ico.png"));
            About.ToolTip = "About...";

            //add to Ribbon

            RibbonItem Button_TotalSectionBox = PPPRSectionBox.AddItem(TotalSectionBox) as PushButton;
            RibbonItem Button_RotateSectionBox = PPPRSectionBox.AddItem(RotateSectionBox) as PushButton;
            RibbonItem Button_toggleSectionBox = PPPRSectionBox.AddItem(ToggleSectionBox) as PushButton;
            RibbonItem Button3_About = PPPRCredits.AddItem(About) as PushButton;

            return Result.Succeeded;
        }

        private void Module_Startup(object sender, EventArgs e)
        {

        }

        private void Module_Shutdown(object sender, EventArgs e)
        {

        }

        #region Revit Macros generated code
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Module_Startup);
            this.Shutdown += new System.EventHandler(Module_Shutdown);
        }
        #endregion
    }

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class InvokeABOUT : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            String exeConfigPath = Path.GetDirectoryName(path) + "\\LameDuck.dll";
            String exeConfigPath2 = Path.GetDirectoryName(path) + "\\";

            string strCommandName = "AboutTool";

            byte[] assemblyBytes = File.ReadAllBytes(exeConfigPath);

            Assembly objAssembly = Assembly.Load(assemblyBytes);
            IEnumerable<Type> myIEnumerableType = GetTypesSafely(objAssembly);
            foreach (Type objType in myIEnumerableType)
            {
                if (objType.IsClass)
                {
                    if (objType.Name.ToLower() == strCommandName.ToLower())
                    {
                        object ibaseObject = Activator.CreateInstance(objType);
                        object[] arguments = new object[] { commandData, exeConfigPath2, elements };
                        object result = null;

                        result = objType.InvokeMember("Execute", BindingFlags.Default | BindingFlags.InvokeMethod, null, ibaseObject, arguments);

                        break;
                    }
                }
            }
            return Result.Succeeded;

        }
        private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }
    }

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class InvokeTSB : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            String exeConfigPath = Path.GetDirectoryName(path) + "\\LameDuck.dll";
            String exeConfigPath2 = Path.GetDirectoryName(path) + "\\";

            string strCommandName = "TotalSectionBoxTool";

            byte[] assemblyBytes = File.ReadAllBytes(exeConfigPath);

            Assembly objAssembly = Assembly.Load(assemblyBytes);
            IEnumerable<Type> myIEnumerableType = GetTypesSafely(objAssembly);
            foreach (Type objType in myIEnumerableType)
            {
                if (objType.IsClass)
                {
                    if (objType.Name.ToLower() == strCommandName.ToLower())
                    {
                        object ibaseObject = Activator.CreateInstance(objType);
                        object[] arguments = new object[] { commandData, exeConfigPath2, elements };
                        object result = null;

                        result = objType.InvokeMember("Execute", BindingFlags.Default | BindingFlags.InvokeMethod, null, ibaseObject, arguments);

                        break;
                    }
                }
            }
            return Result.Succeeded;

        }
        private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }
    }

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class InvokeRSB : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            String exeConfigPath = Path.GetDirectoryName(path) + "\\LameDuck.dll";
            String exeConfigPath2 = Path.GetDirectoryName(path) + "\\";

            string strCommandName = "RotateSectionBoxTool";

            byte[] assemblyBytes = File.ReadAllBytes(exeConfigPath);

            Assembly objAssembly = Assembly.Load(assemblyBytes);
            IEnumerable<Type> myIEnumerableType = GetTypesSafely(objAssembly);
            foreach (Type objType in myIEnumerableType)
            {
                if (objType.IsClass)
                {
                    if (objType.Name.ToLower() == strCommandName.ToLower())
                    {
                        object ibaseObject = Activator.CreateInstance(objType);
                        object[] arguments = new object[] { commandData, exeConfigPath2, elements };
                        object result = null;

                        result = objType.InvokeMember("Execute", BindingFlags.Default | BindingFlags.InvokeMethod, null, ibaseObject, arguments);

                        break;
                    }
                }
            }
            return Result.Succeeded;

        }
        private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }
    }

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class InvokeToSB : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            String exeConfigPath = Path.GetDirectoryName(path) + "\\LameDuck.dll";
            String exeConfigPath2 = Path.GetDirectoryName(path) + "\\";

            string strCommandName = "ToggleSectionBoxTool";

            byte[] assemblyBytes = File.ReadAllBytes(exeConfigPath);

            Assembly objAssembly = Assembly.Load(assemblyBytes);
            IEnumerable<Type> myIEnumerableType = GetTypesSafely(objAssembly);
            foreach (Type objType in myIEnumerableType)
            {
                if (objType.IsClass)
                {
                    if (objType.Name.ToLower() == strCommandName.ToLower())
                    {
                        object ibaseObject = Activator.CreateInstance(objType);
                        object[] arguments = new object[] { commandData, exeConfigPath2, elements };
                        object result = null;

                        result = objType.InvokeMember("Execute", BindingFlags.Default | BindingFlags.InvokeMethod, null, ibaseObject, arguments);

                        break;
                    }
                }
            }
            return Result.Succeeded;

        }
        private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }
    }

}