using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.IO;

namespace Buffer
{
    public class FileManager
    {
        #region Constants

        private const string DEFAULT_PATH = "C:\\keys.txt";
        private const int TIMER_INTERVAL = 60000;

        #endregion

        #region Members

        private Timer timer = null;
        private string filePath = null;
        private static object myLock = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFilePath"></param>
        public FileManager(string pFilePath)
        {
            //Set the Path for the file to generate
            filePath = pFilePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFilePath"></param>
        public FileManager()
            : this(DEFAULT_PATH)
        {
           
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (timer == null)
            {
                //Create the timer.
                timer = new System.Timers.Timer(TIMER_INTERVAL);
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.Start();

                //Srubscribe to the buffer Manager envents
                BufferManager.GetInstance().FullBuffer += new BufferManager.FullBufferEventHandler(buffer_FullBuffer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if (timer != null)
            {
                //Detach from the Timer Tick
                timer.Elapsed -= new ElapsedEventHandler(timer_Elapsed);
                timer.Stop();
                timer.Dispose();
                timer = null;
                //Force write into file
                ForceWrite(BufferManager.GetInstance().DumpBuffer());
                //Unsubscribe to the events
                BufferManager.GetInstance().FullBuffer += new BufferManager.FullBufferEventHandler(buffer_FullBuffer);
            }
        }

        #endregion

        #region Event Methods

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Force Write
            ForceWrite(BufferManager.GetInstance().DumpBuffer());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bufferState"></param>
        private void buffer_FullBuffer(string dumpedBuffer)
        {
            //Force Write
            ForceWrite(dumpedBuffer);
        }

        #endregion

        #region Private Memebers

        /// <summary>
        /// 
        /// </summary>
        private void ForceWrite(string content)
        {
            lock (myLock)
            {
                try
                {
                    //Get the zbuffer and write into a file
                    StreamWriter writer = new StreamWriter(filePath, true);
                    if (writer != null)
                    {
                        //Write the content of the buffer
                        writer.Write(content);
                        //Close handle
                        writer.Close();
                    }
                }
                catch (Exception ex)
                {
                    //Error opening the file
                    System.Console.Out.WriteLine(ex.Message);
                }
            }
        }
        
        #endregion

    }
}
