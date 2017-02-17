using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KeyProc
{
    static class Program
    {
        private static Main main = null;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Exited Events
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            // Creating main class 
            main = new Main(true);
            //Start the application
            Application.Run(main);
        }

        /// <summary>
        /// Event to manage unmnaged exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            //Exit the application
            if (main != null)
                main.Stop();
        }

        /// <summary>
        /// Called when an unhandled exception appears.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="System.UnhandledExceptionEventArgs" /> instance containing the event data.
        /// </param>
        private static void OnCurrentDomainUnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            //Exit the application
            if (main != null)
                main.Stop();
            //Exit with code
            System.Environment.Exit(0);
        }
    }
}
