using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.IO;
using System.Threading;

namespace Framework.Pipe
{
    /// <summary>
    /// 进程通讯发送端
    /// </summary>
    public class PipeClient
    {
        private NamedPipeClientStream Client;
        string ServerName = null;
        string PipeName = null;
        PipeDirection _PipeDirection;
       

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ServerName">接收端名字</param>
        /// <param name="PipeName">通道名字</param>
        public PipeClient(string ServerName, string PipeName)
        {
            this.ServerName = ServerName;
            this.PipeName = PipeName;
            this._PipeDirection = PipeDirection.InOut;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns>是否成功启动</returns>
        public bool StartAppPipe()
        {
            Client = new NamedPipeClientStream(ServerName, PipeName, _PipeDirection,PipeOptions.None,System.Security.Principal.TokenImpersonationLevel.Impersonation);
            if (Client == null)
                return false;
            return true;
        }

        /// <summary>
        /// 连接
        /// </summary>
        public bool ConnectAppPipe(int ConnectTimeOut = 300)
        {
            if (Client == null)
                return false;
            try
            {
                Client.Connect(ConnectTimeOut);
                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
        /// <summary>
        /// 发送数据，并接收返回的数据
        /// </summary>
        /// <param name="data">发送的数据</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>接收的数据</returns>
        public string Send(string data)
        {
            if (Client == null)
                return "";
            if (!Client.IsConnected)
            {
                return "";
            }
            try
            {
                StreamWriter sw = new StreamWriter(Client);
                StreamReader sr = new StreamReader(Client);

                sw.AutoFlush = true;
                sw.WriteLine(data);
                string recvdata = sr.ReadLine();
                Client.Close();
                return recvdata;
            }
            catch(Exception e)
            {
                throw e;
            }

        }
        
        /// <summary>
        /// 向通道写
        /// </summary>
        /// <param name="data">写的内容</param>
        private bool Write(string data)
        {
            if (Client == null)
                return false;
            if (!Client.IsConnected)
            {
                return false;
            }
            StreamWriter sw = new StreamWriter(Client);
            sw.WriteLine(data);
            sw.Close();
            return true;
        }

        /// <summary>
        /// 向通道读
        /// </summary>
        /// <returns>读的内容</returns>
        private string Read()
        {
            if (!Client.IsConnected)
            {
                return "";
            }

            StreamReader sr = new StreamReader(Client);
            string recvdata = sr.ReadLine();
            sr.Close();
            return recvdata;

        }
    }
}
