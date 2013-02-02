using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RBuild.Classes
{
    #region "Enum"

    public enum MIIM : uint
    {
        STATE = 0x00000001,
        ID = 0x00000002,
        SUBMENU = 0x00000004,
        CHECKMARKS = 0x00000008,
        TYPE = 0x00000010,
        DATA = 0x00000020,
        STRING = 0x00000040,
        BITMAP = 0x00000080,
        FTYPE = 0x00000100
    }//enum

    public enum MF : uint
    {
        INSERT = 0x00000000,
        CHANGE = 0x00000080,
        APPEND = 0x00000100,
        DELETE = 0x00000200,
        REMOVE = 0x00001000,
        BYCOMMAND = 0x00000000,
        BYPOSITION = 0x00000400,
        SEPARATOR = 0x00000800,
        ENABLED = 0x00000000,
        GRAYED = 0x00000001,
        DISABLED = 0x00000002,
        UNCHECKED = 0x00000000,
        CHECKED = 0x00000008,
        USECHECKBITMAPS = 0x00000200,
        STRING = 0x00000000,
        BITMAP = 0x00000004,
        OWNERDRAW = 0x00000100,
        POPUP = 0x00000010,
        MENUBARBREAK = 0x00000020,
        MENUBREAK = 0x00000040,
        UNHILITE = 0x00000000,
        HILITE = 0x00000080,
        DEFAULT = 0x00001000,
        SYSMENU = 0x00002000,
        HELP = 0x00004000,
        RIGHTJUSTIFY = 0x00004000,
        MOUSESELECT = 0x00008000
    }//enum

    public enum CLIPFORMAT : uint
    {
        CF_TEXT = 1,
        CF_BITMAP = 2,
        CF_METAFILEPICT = 3,
        CF_SYLK = 4,
        CF_DIF = 5,
        CF_TIFF = 6,
        CF_OEMTEXT = 7,
        CF_DIB = 8,
        CF_PALETTE = 9,
        CF_PENDATA = 10,
        CF_RIFF = 11,
        CF_WAVE = 12,
        CF_UNICODETEXT = 13,
        CF_ENHMETAFILE = 14,
        CF_HDROP = 15,
        CF_LOCALE = 16,
        CF_MAX = 17,

        CF_OWNERDISPLAY = 0x0080,
        CF_DSPTEXT = 0x0081,
        CF_DSPBITMAP = 0x0082,
        CF_DSPMETAFILEPICT = 0x0083,
        CF_DSPENHMETAFILE = 0x008E,

        CF_PRIVATEFIRST = 0x0200,
        CF_PRIVATELAST = 0x02FF,

        CF_GDIOBJFIRST = 0x0300,
        CF_GDIOBJLAST = 0x03FF
    }//enum

    // GetCommandString uFlags
    public enum GCS : uint
    {
        VERBA = 0x00000000,     // canonical verb
        HELPTEXTA = 0x00000001,     // help text (for status bar)
        VALIDATEA = 0x00000002,     // validate command exists
        VERBW = 0x00000004,     // canonical verb (unicode)
        HELPTEXTW = 0x00000005,     // help text (unicode version)
        VALIDATEW = 0x00000006,     // validate command exists (unicode)
        UNICODE = 0x00000004,     // for bit testing - Unicode string
        VERB = GCS.VERBA,
        HELPTEXT = GCS.HELPTEXTA,
        VALIDATE = GCS.VALIDATEA
    }//enum

    #endregion

    #region "Structs"

    [StructLayout(LayoutKind.Sequential)]
    public struct MENUITEMINFO
    {
        public uint cbSize;
        public uint fMask;
        public uint fType;
        public uint fState;
        public int wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public IntPtr dwItemData;
        public string dwTypeData;
        public uint cch;
        public IntPtr hbmpItem;
    }//struct

    #endregion

    #region "Interfaces"

    [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), GuidAttribute("000214e8-0000-0000-c000-000000000046")]
    public interface IShellExtInit
    {
        [PreserveSig()]
        int Initialize(IntPtr pidlFolder, IntPtr lpdobj, uint /*HKEY*/ hKeyProgID);
    }//interface

    [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), GuidAttribute("000214e4-0000-0000-c000-000000000046")]
    public interface IContextMenu
    {
        [PreserveSig()]
        int QueryContextMenu(IntPtr hmenu, uint iMenu, int idCmdFirst, int idCmdLast, uint uFlags);
        [PreserveSig()]
        void InvokeCommand(IntPtr pici);
        [PreserveSig()]
        void GetCommandString(int idcmd, uint uflags, int reserved, StringBuilder commandstring, int cch);
    }//interface

    #endregion
}//namespace
