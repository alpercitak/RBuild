using System;

namespace RBuild.Classes
{
    public class AppMain
    {
        #region "Enum"

        private enum enum_Mode
        {
            INSTALL,
            UNINSTALL,
            SHOWHELP,
            DEFAULT
        }//enum

        #endregion

        #region "Constants"

        private const string CMD_INSTALL = "-i";
        private const string CMD_UNINSTALL = "-u";
        private const string CMD_SHOWHELP = "-h";

        private const string HELP_STRING = "Help        :        -h\n";
        private const string INSTALL_STRING = "Install     :        -i\n";
        private const string UNINSTALL_STRING = "Uninstall   :        -u\n";

        #endregion

        #region "Methods"

        public static void Main(string[] prm_arrArgs)
        {
            enum_Mode eMode = enum_Mode.DEFAULT;

            if (prm_arrArgs.Length > 0)
                eMode = GetModeByCommand(prm_arrArgs[0]);

            switch (eMode)
            {
                case enum_Mode.DEFAULT: Console.WriteLine(HELP_STRING); break;
                case enum_Mode.INSTALL: Registration.Install(); break;
                case enum_Mode.SHOWHELP: Console.WriteLine(HELP_STRING + INSTALL_STRING + UNINSTALL_STRING); break;
                case enum_Mode.UNINSTALL: Registration.Uninstall(); break;
            }//switch
        }//void

        private static enum_Mode GetModeByCommand(string prm_strCommand)
        {
            switch (prm_strCommand)
            {
                case CMD_INSTALL: return enum_Mode.INSTALL;
                case CMD_UNINSTALL: return enum_Mode.UNINSTALL;
                case CMD_SHOWHELP: return enum_Mode.SHOWHELP;
                default: return enum_Mode.DEFAULT;
            }//switch
        }//function

        #endregion
    }//class
}//namespace
