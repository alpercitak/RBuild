using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.Win32;
using RBuild.Forms;

namespace RBuild.Classes
{
    [Guid("972F4E5F-CE5B-495E-9E72-6E9F3E63E088"), ComVisible(true)]
    public class BuildHandler : IContextMenu, IShellExtInit
    {
        #region "Enum"

        private enum enum_Version
        {
            NA,
            V2003,
            V2005,
            V2008,
            V2010
        }//enum

        #endregion

        #region "Interop"

        [DllImport("user32")]
        private static extern int InsertMenuItem([In] IntPtr hmenu, [In] uint uposition, [In] uint uflags, [In] ref MENUITEMINFO mii);

        [DllImport("shell32")]
        private static extern uint DragQueryFile(IntPtr hDrop, uint iFile, [Out] StringBuilder buffer, int cch);

        #endregion

        #region "Constants"

        public const string CLSID = "{972F4E5F-CE5B-495E-9E72-6E9F3E63E088}";

        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        private const string V2003 = "visual studio 2003";
        private const string V2005 = "visual studio 2005";
        private const string V2008 = "visual studio 2008";
        private const string V2010 = "visual studio 2010";

        #endregion

        #region "Variables"

        private IDataObject _objDataObject = null;
        private IntPtr _inthDrop = IntPtr.Zero;
        private frmResult objFrmResult = new frmResult();

        #endregion

        #region "Methods"

        private bool CheckExtension()
        {
            StringBuilder objSB = new StringBuilder(1024);

            DragQueryFile(_inthDrop, 0, objSB, objSB.Capacity + 1);
            string strFile = objSB.ToString();

            return Path.GetExtension(strFile).Equals(".sln");
        }//function

        private void DoBuild(string prm_strFile)
        {
            try
            {
                enum_Version eVersion = GetVSVersion(prm_strFile);

                string strSuffix = string.Empty;

                switch (eVersion)
                {
                    case enum_Version.V2003: strSuffix = "7.1"; break;
                    case enum_Version.V2005: strSuffix = "8.0"; break;
                    case enum_Version.V2008: strSuffix = "9.0"; break;
                    case enum_Version.V2010: strSuffix = "10.0"; break;
                }//switch

                if (eVersion == enum_Version.NA)
                {
                    System.Windows.Forms.MessageBox.Show("Could not retrieve version");
                    return;
                }//if

                string strIdePath = GetIdePath(strSuffix);

                if (string.IsNullOrEmpty(strIdePath))
                {
                    System.Windows.Forms.MessageBox.Show("Could not retrieve vs install path");
                    return;
                }//if

                Process objProcess = new Process();
                objProcess.StartInfo.FileName = Path.Combine(strIdePath, "devenv.com");
                objProcess.StartInfo.Arguments = string.Format("\"{0}\" /build debug", prm_strFile);
                objProcess.StartInfo.UseShellExecute = false;
                objProcess.StartInfo.RedirectStandardOutput = true;
                objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                objProcess.StartInfo.CreateNoWindow = true;
                objProcess.Start();

                string strResult = objProcess.StandardOutput.ReadToEnd();

                objProcess.WaitForExit();

                objFrmResult.Result = strResult;
                objFrmResult.Show();
            }//try
            catch (Exception Ex)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("{0}\r\n{1}", Ex.Message, Ex.InnerException.Message));
            }//catch
        }//void

        private string GetIdePath(string prm_strSuffix)
        {
            string strPrefix = Environment.Is64BitOperatingSystem ? "SOFTWARE\\Wow6432Node\\Microsoft\\VisualStudio" : "SOFTWARE\\Microsoft\\VisualStudio";

            return Registry.LocalMachine.OpenSubKey(string.Format("{0}\\{1}", strPrefix, prm_strSuffix), false).GetValue("InstallDir") as string;
        }//function

        private enum_Version GetVSVersion(string prm_strFile)
        {
            try
            {
                string strContent = string.Empty;

                using (StreamReader objSR = new StreamReader(prm_strFile))
                    strContent = objSR.ReadToEnd().ToLower();

                if (string.IsNullOrEmpty(strContent)) return enum_Version.NA;

                if (strContent.IndexOf(V2003) >= 0) return enum_Version.V2003;
                if (strContent.IndexOf(V2005) >= 0) return enum_Version.V2005;
                if (strContent.IndexOf(V2008) >= 0) return enum_Version.V2008;
                if (strContent.IndexOf(V2010) >= 0) return enum_Version.V2010;

                return enum_Version.NA;
            }//try
            catch (Exception)
            {
                return enum_Version.NA;
            }//catch
        }//function

        #endregion

        #region "IContextMenu Members"

        int IContextMenu.QueryContextMenu(IntPtr hmenu, uint iMenu, int idCmdFirst, int idCmdLast, uint uFlags)
        {
            int id = (int)iMenu;

            if (!CheckExtension()) return id;

            MENUITEMINFO sep = new MENUITEMINFO();
            sep.cbSize = (uint)Marshal.SizeOf(sep);
            sep.fMask = (uint)MIIM.TYPE;
            sep.fType = (uint)MF.SEPARATOR;

            InsertMenuItem(hmenu, iMenu, 1, ref sep);

            id++;

            MENUITEMINFO mii = new MENUITEMINFO();
            mii.cbSize = (uint)Marshal.SizeOf(mii);
            mii.fMask = (uint)(MIIM.STRING | MIIM.FTYPE | MIIM.ID | MIIM.STATE);
            mii.wID = idCmdFirst + id;
            mii.fType = (uint)MF.STRING;
            mii.dwTypeData = "RBuild";
            mii.fState = (uint)MF.ENABLED;

            InsertMenuItem(hmenu, iMenu + 1, 1, ref mii);

            id++;

            sep = new MENUITEMINFO();
            sep.cbSize = (uint)Marshal.SizeOf(sep);
            sep.fMask = (uint)MIIM.TYPE;
            sep.fType = (uint)MF.SEPARATOR;

            InsertMenuItem(hmenu, iMenu + 2, 1, ref sep);

            id++;

            return id;
        }//function

        void IContextMenu.InvokeCommand(IntPtr pici)
        {
            try
            {
                StringBuilder objSB = new StringBuilder(1024);

                DragQueryFile(_inthDrop, 0, objSB, objSB.Capacity + 1);
                string strFile = objSB.ToString();

                DoBuild(strFile);
            }//try
            catch (Exception Ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + Ex.ToString(), "Error in RBuild");
            }//catch
        }//void

        void IContextMenu.GetCommandString(int idcmd, uint uflags, int reserved, StringBuilder commandstring, int cch)
        {
            switch (uflags)
            {
                case (uint)GCS.VERB:
                    commandstring = new StringBuilder("RBuild".Substring(1, cch - 1));
                    break;
                case (uint)GCS.HELPTEXT:
                    commandstring = new StringBuilder("Builds VS Solutions".Substring(1, cch - 1));
                    break;
                case (uint)GCS.VALIDATE:
                    break;
            }//switch
        }//void

        #endregion

        #region "IShellExtInit Members"

        int IShellExtInit.Initialize(IntPtr pidl, IntPtr lpdobj, uint hKeyProgID)
        {
            try
            {
                _objDataObject = null;
                if (lpdobj != (IntPtr)0)
                {
                    _objDataObject = (IDataObject)Marshal.GetObjectForIUnknown(lpdobj);
                    FORMATETC fmt = new FORMATETC();
                    fmt.cfFormat = (int)CLIPFORMAT.CF_HDROP;
                    fmt.ptd = IntPtr.Zero;
                    fmt.dwAspect = DVASPECT.DVASPECT_CONTENT;
                    fmt.lindex = -1;
                    fmt.tymed = TYMED.TYMED_HGLOBAL;
                    STGMEDIUM medium = new STGMEDIUM();
                    _objDataObject.GetData(ref fmt, out medium);
                    _inthDrop = medium.unionmember;
                }//if
            }//try
            catch (Exception) { }//catch

            return 0;
        }//function

        #endregion
    }//class
}//namespace
