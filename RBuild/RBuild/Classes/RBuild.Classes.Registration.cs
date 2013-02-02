using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace RBuild.Classes
{
    public class Registration
    {
        #region "Constants"

        private const string PATH_EXPLORER = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer";
        private const string PATH_APPROVED = "Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved";
        private const string PATH_RBUILD = "*\\ShellEx\\ContextMenuHandlers\\RBuild";

        #endregion

        #region "Methods"

        [ComRegisterFunction()]
        public static void Install()
        {
            Assembly objAssembly = Assembly.GetExecutingAssembly();
            RegistrationServices objRegServices = new RegistrationServices();

            try
            {
                objRegServices.RegisterAssembly(objAssembly, AssemblyRegistrationFlags.SetCodeBase);

                RegistryKey objRK = Registry.CurrentUser.OpenSubKey(PATH_EXPLORER, true);
                objRK.SetValue("DesktopProcess", 1);
                objRK.Close();

                objRK = Registry.LocalMachine.OpenSubKey(PATH_APPROVED, true);
                objRK.SetValue(BuildHandler.CLSID, "RBuild");
                objRK.Close();

                objRK = Registry.ClassesRoot.CreateSubKey(PATH_RBUILD);
                objRK.SetValue(string.Empty, BuildHandler.CLSID);
                objRK.Close();
            }//try
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }//catch
        }//void

        public static void Uninstall()
        {
            Assembly objAssembly = Assembly.GetExecutingAssembly();
            RegistrationServices objRegServices = new RegistrationServices();

            try
            {
                objRegServices.UnregisterAssembly(objAssembly);

                RegistryKey objRK = Registry.LocalMachine.OpenSubKey(PATH_APPROVED, true);
                objRK.DeleteValue(BuildHandler.CLSID);
                objRK.Close();

                Registry.ClassesRoot.DeleteSubKey(PATH_RBUILD);
            }//try
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }//catch
        }//void

        #endregion
    }//class
}//namespace
