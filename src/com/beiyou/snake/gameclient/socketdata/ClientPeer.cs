using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;


namespace com.beiyou.snake.gameclient.socketdata
{
    //客户端socket
    public class ClientPeer
    {
        //声明socket ip port
        private Socket socket;
        private string ip;
        private int port;

        //声明并初始化 接收信息和消息队列
        private byte[] receiveBuffer = new byte[1024];
        private StringBuilder stringBuffer = new StringBuilder();
        public Queue<string> SocketMsgQueue = new Queue<string>();

        //创建socket
        public ClientPeer(string ip, int port)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.ip = ip;
                this.port = port;
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        //连接socket
        public void Connect()
        {
            try
            {
                Debug.Log("开始连接socket");
                socket.Connect(ip, port);
                Debug.Log("连接服务器成功");

                StartReceive();
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        //开始接收
        private void StartReceive()
        {
            if(socket == null && socket.Connected == false)
            {
                Debug.LogError("连接失败，无法接收数据");
                return;
            }

            socket.BeginReceive(receiveBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, socket);
        }

        //接收数据
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);//结束本次异步接收数据
                byte[] tmpByteArray = new byte[length];
                Buffer.BlockCopy(receiveBuffer, 0, tmpByteArray, 0, length);//拷贝数据块

                string tmpStr = Encoding.Default.GetString(tmpByteArray, 0, length);//将接收到的数据转为string

                ProcessReceive(tmpStr);//解析并缓存本次数据

                StartReceive();//开始异步接收下次数据
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        //获取数据处理
        private void ProcessReceive(string str)
        {
            if(str == null || str == "")
            {
                return;
            }
            StringBuilder sb = this.stringBuffer;
            sb.Append(str);
            int index = sb.ToString().IndexOf("</over>");
            while (index != -1)
            {
                string distr = sb.ToString().Substring(0, index);
                SocketMsgQueue.Enqueue(distr);
                sb.Remove(0, index + 7);
                index = sb.ToString().IndexOf("</over>");
            }
            this.stringBuffer = sb;
        }

        //向服务器发送数据
        public void Send(string data)
        {
            try
            {
                if (socket == null || socket.Connected == false)
                {
                    //Debug.LogError("没有连接成功，无法发送数据");
                    return;
                }
                //Debug.Log("socket发送消息：" + data);
                socket.Send(Encoding.Default.GetBytes(data + "\n"));
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        //断开连接
        public void Close()
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Disconnect(false);
                socket.Close();
                socket = null;

            }
        }

    }


}
