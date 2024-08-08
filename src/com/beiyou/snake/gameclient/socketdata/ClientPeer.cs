using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;


namespace com.beiyou.snake.gameclient.socketdata
{
    //�ͻ���socket
    public class ClientPeer
    {
        //����socket ip port
        private Socket socket;
        private string ip;
        private int port;

        //��������ʼ�� ������Ϣ����Ϣ����
        private byte[] receiveBuffer = new byte[1024];
        private StringBuilder stringBuffer = new StringBuilder();
        public Queue<string> SocketMsgQueue = new Queue<string>();

        //����socket
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

        //����socket
        public void Connect()
        {
            try
            {
                Debug.Log("��ʼ����socket");
                socket.Connect(ip, port);
                Debug.Log("���ӷ������ɹ�");

                StartReceive();
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        //��ʼ����
        private void StartReceive()
        {
            if(socket == null && socket.Connected == false)
            {
                Debug.LogError("����ʧ�ܣ��޷���������");
                return;
            }

            socket.BeginReceive(receiveBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, socket);
        }

        //��������
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);//���������첽��������
                byte[] tmpByteArray = new byte[length];
                Buffer.BlockCopy(receiveBuffer, 0, tmpByteArray, 0, length);//�������ݿ�

                string tmpStr = Encoding.Default.GetString(tmpByteArray, 0, length);//�����յ�������תΪstring

                ProcessReceive(tmpStr);//���������汾������

                StartReceive();//��ʼ�첽�����´�����
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        //��ȡ���ݴ���
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

        //���������������
        public void Send(string data)
        {
            try
            {
                if (socket == null || socket.Connected == false)
                {
                    //Debug.LogError("û�����ӳɹ����޷���������");
                    return;
                }
                //Debug.Log("socket������Ϣ��" + data);
                socket.Send(Encoding.Default.GetBytes(data + "\n"));
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        //�Ͽ�����
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
