namespace Updater
{
    internal static class Program
    {
        public static string UPDATE_ZIP_PATH = "";
        public static string EXE_PATH = "";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // handle input args
            if (args.Length != 2) return; // dont bother as it was called wrong
            UPDATE_ZIP_PATH = args[0];
            EXE_PATH = args[1];

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Updater());

        }
    }
}