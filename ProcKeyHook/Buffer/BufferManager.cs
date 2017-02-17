using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Buffer
{

    public class BufferManager
    {
        #region Events

        public delegate void FullBufferEventHandler(string dumpedBuffer);

        /// <summary>
        /// Event when the buffer is full
        /// </summary>
        public event FullBufferEventHandler FullBuffer;

        #endregion

        #region Members

        private const int MAX_STREAM = 65536;
        
        private StringBuilder currentStream = null;
        
        #endregion

        #region Constructor

        private BufferManager()
        {
            //Create the StreamWriter
            currentStream = new StringBuilder(MAX_STREAM);
            currentStream.Length = 0;
        }

        #endregion

        #region Singletone Members

        private static object myLock = new object();
        private static BufferManager mySingleton = null;

        public static BufferManager GetInstance()
        {
            lock (myLock)
            {
                if (mySingleton == null)
                { 
                    mySingleton = new BufferManager();
                }
                return mySingleton;
            }
        }

        #endregion

        #region Private Memebers
       
        #endregion

        #region Event Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pKey"></param>
        private void FullBufferEvent(string dumpedBuffer)
        {
            if (FullBuffer != null)
            {
                FullBuffer(dumpedBuffer);
            }
        }

        #endregion

        #region Public Members
        
        /// <summary>
        /// Write into the current buffer or change if the current is full
        /// </summary>
        /// <param name="pText"></param>
        public void Write(String pText)
        {
            lock (myLock)
            {
                //Check if the buffer is full
                if ((currentStream.Length + pText.Length) >= MAX_STREAM)
                {
                    //Get the current buffer state
                    string dumpedBuffer = currentStream.ToString();
                    // Initialize the new buffer
                    currentStream.Length = 0;
                    //Fire Buffer with the old state
                    FullBufferEvent(dumpedBuffer);
                }

                //Write in current buffer
                currentStream.Append(pText);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bufferState"></param>
        /// <returns></returns>
        public string DumpBuffer()
        {
            lock (myLock)
            {
                //Copy the buffer into a new string
                String contentstream = currentStream.ToString();
                //Delete the content of the buffer
                currentStream.Length = 0;
                //Return the result
                return contentstream;
            }
        }

        #endregion
    }
}
