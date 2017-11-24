using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Framework.Pipe
{
    /// <summary>
    /// 通讯接收端的回调委托
    /// </summary>
    /// <param name="data">接收到的消息</param>
    public delegate void ServerPipeDelegate(object data);

    /// <summary>
    /// pipe通讯 server端
    /// </summary>
    public class PipeServer
    {

        private NamedPipeServerStream pipeServer = null;
        private string pipeName = null;
        private PipeDirection pipeDirection;
        private int maxServiceCount = 0;
        private string callBackStr = null;
        /// <summary>
        /// 通讯接收端的回调委托实例
        /// </summary>
        private ServerPipeDelegate serverPipeDelegate = null;
        /// <summary>
        /// 接收消息后启动线程处理回调
        /// </summary>
        private Thread threadPro;


        /// <summary>
        /// 线程处理
        /// </summary>
        public void ThreadDo(object callBackStr)
        {
            try
            {
                pipeServer = new NamedPipeServerStream(pipeName, pipeDirection, maxServiceCount);
                if (pipeServer == null)
                    return;

                pipeServer.WaitForConnection();
                StreamReader sr = new StreamReader(pipeServer);
                string data = sr.ReadLine();
                sr.Close();
        
                if (serverPipeDelegate != null)
                    serverPipeDelegate.BeginInvoke(data, null, null);
            }
            catch (Exception)
            {
                throw;

            } 
        }

        /// <summary>
        /// 创建管道server 异步
        /// </summary>
        /// <param name="_PipeName">管道名</param>
        ///  /// <param name="callBackStr">需要返回的数据</param>
        /// <param name="PipeServerPro">回调函数</param>
        /// <returns>是否创建成功</returns>
        public bool CreatePipeQueue(string pipeName,ServerPipeDelegate serverPipeDelegate)
        {
            this.pipeName = pipeName;
            this.pipeDirection = PipeDirection.InOut;
            this.maxServiceCount = 250;
            this.serverPipeDelegate = serverPipeDelegate;

            threadPro = new Thread(ThreadDo);
            threadPro.Start(callBackStr);
            return true;


        }

        /// <summary>
        /// 创建管道server  同步
        /// </summary>
        /// <param name="pipeName">管道名</param>
        /// <param name="callBackStr">需要返回的数据</param>
        /// <returns>接收到的数据</returns>
        public string CreatePipeQueue(string pipeName, string callBackStr)
        {
            this.pipeName = pipeName;
            this.pipeDirection = PipeDirection.InOut;
            this.maxServiceCount = 250;
            this.callBackStr = callBackStr;
            try
            {
                if (serverPipeDelegate == null)
                {
                    return recv();
                }
                else
                    return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string recv()
        {
            try
            {
                pipeServer = new NamedPipeServerStream(pipeName, pipeDirection, maxServiceCount);
                if (pipeServer == null)
                    return "";

                pipeServer.WaitForConnection();
                StreamReader sr = new StreamReader(pipeServer);
                StreamWriter sw = new StreamWriter(pipeServer);
                sw.AutoFlush = true;

                string data = sr.ReadLine();
                sw.WriteLine(callBackStr);

                pipeServer.Disconnect();
                return data;
            }
            catch (IOException e)
            {
                throw new Exception("pipe server read or write error!"+e.Message);

            }
            catch (Exception e)
            {
                throw e;
            }
         
        }
    }

}
