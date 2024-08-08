using com.beiyou.snake.common.res;
using com.beiyou.snake.gameclient.entity;
using com.beiyou.snake.gameclient.socketdata;
using com.beiyou.snake.gameclient.ui;
using System;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System.Security.Claims;
using UnityEngine.UIElements;

namespace com.beiyou.snake.gameclient.engine
{
    public class GameLogic : MonoBehaviour
    {
        //��Ϸ������
        private GamePane gamePane;
        //�û��� ���� uid
        private string userName = "";
        private string passWord = "";
        private int uid = 0;
        //�ͻ��� ip��ַ �˿�
        private ClientPeer client = null;
        private string ip = "192.168.1.136";
        private int port = 8201;

        //�û��� �û�
        public UserTableEntity userTableEntity = null;
        public OneUserEntity myUserEntity = null;


        private void Awake()
        {
            //�û����û��ĳ�ʼ��
            this.userTableEntity = new UserTableEntity();
            this.myUserEntity = new OneUserEntity();    
        }

        //�ͻ��˷���������
        public void StartGameConnect(string userName, string passWord)
        {
            this.userName = userName;
            this.passWord = passWord;

            StartConnonectSocket();
        }

        //��ʼ����socket
        private void StartConnonectSocket()
        {
            client = new ClientPeer(ip, port);
            client.Connect();

            SendLoginInfo();
        }

        //���͵�¼��Ϣ
        private void SendLoginInfo()
        {
            string sendXml = SendXmlHelper.BuildUserLoginXml(this.userName, this.passWord);
            client.Send(sendXml);
        }

        //��¼�ɹ���⴦��
        private void CheckSuccess(string success,string userId)
        {
            if (success == "1")
            {
                uid = int.Parse(userId);
                this.myUserEntity.SetUid(uid);
                Debug.Log("��¼�ɹ�");
                SendAutoSitInfoXml();
                //��¼�ɹ������Զ�������Ϣ
                //��ʼ����Ϸ����UI ������Ϣ ��Ϊ������ť��ӵ���¼�����
                gamePane = gameObject.AddComponent<GamePane>();
                gamePane.SetUserInfo(this.userName,this.uid);
                gamePane.AddSoloPlayBtnEventListener(ClickSoloPlayBtn);
                gamePane.AddClickChairEventListener(ClickChair);
                gamePane.AddClickRankListBtnEventListener(ClickRankList);
                gamePane.AddReturnBtnEventListener(ClickReturn);
            }

        }

        //��ص�¼��Ϣ
        private void YiDiLoginMsg(string userId)
        {
            Debug.Log(userId + "��ص�¼");
        }

        //�����Զ�������Ϣ
        private void SendAutoSitInfoXml()
        {
            string sendXml = SendXmlHelper.BuildAutoSitInfoXml("" + this.myUserEntity.GetUid());
            client.Send(sendXml);
        }

        //�Զ���������
        private void AutoSitInfoToClient(string allSitUserInfo, string sitType)
        {
            string[] allSitUserInfoArr = allSitUserInfo.Split(":");
            if (allSitUserInfoArr != null && allSitUserInfoArr.Length > 0)
            {
                for (int i = 0; i < allSitUserInfoArr.Length; i++)
                {
                    string oneSitUserInfo = allSitUserInfoArr[i];
                    if (oneSitUserInfo != null && oneSitUserInfo != "")
                    {
                        string[] oneSitUserInfoArr = oneSitUserInfo.Split("_");
                        int chairId = int.Parse(oneSitUserInfoArr[0]);
                        if (oneSitUserInfoArr[1] != "")
                        {
                            int userId = int.Parse(oneSitUserInfoArr[1]);
                            string userName = oneSitUserInfoArr[2];
                            // ������ֵ
                            userTableEntity.SetOneUserEntity(chairId, userId, userName);

                            if (userId > 0)
                            {
                                if (userId == myUserEntity.GetUid())
                                {
                                    myUserEntity.SetChairId(chairId);
                                }
                                int myChairId = myUserEntity.GetChairId();
                                //ShowUserAvater(myChairId, chairId, userId, userName);

                            }
                            else
                            {
                                //this.RemoveUserUserAvater(chairId, userId, userName);
                            }
                        }
                    }
                }
            }
        }

        //���Լ�������
        private void SendJoinTable()
        {
            string sendXml = SendXmlHelper.BuildJoinTableXml(uid + "");
            client.Send(sendXml);
        }

        //�����û�׼����Ϣ
        //private void SendUserReadyInfoToClient(string chairId, string ready)
        //{

        //}

        //��������û���Ϣ
        private void OtherUserInfo(string eUserName,string eUserId)
        {
            gamePane.SetEnemyInfo(eUserName, eUserId);
        }


        //�ɹ�ƥ�������Ϸ
        private void PlaySuccess(string success) {
            if(success == "1")
            {
                gamePane.DelWaitEnemy();
                gamePane.OnlineMode();
            }
        }

        //�ڷ����������ʳ��λ�ô�����ʳ�� 
        private void FoodPosition(string position)
        {
            gamePane.OnlineMakeFood(position);
        }

        //ԭ��ʳ�ﱻ�Ժ���ʳ����Ϣ
        public void SendFoodEatMsg(string name)
        {
            string sendXml = SendXmlHelper.BuildFoodEatXml(name);
            client.Send(sendXml);
        }

        //���շ�������ʳ�����ƺ���λ����Ϣ�� ʳ��λ�ñ任
        private void FoodTrans(string name,string x,string y)
        {
            gamePane.FoodTransform(name, x, y);
        }

        //�����ߵĽǶȺ�λ�� ������Ϣ
        public void SendSnakeMsg(float angle,float x,float y,int length)
        {
            string sendXml = SendXmlHelper.BuildSnakeMsgXml(uid+"", angle, x,y,length);
            client.Send(sendXml);
        }

        //���ն����ߵĽǶȺ�λ�ó�����Ϣ������������
        private void SnakeMsg(string userId, string angle, string x,string y,string length)
        {
            if (userId != this.uid + "")//ȷ��Ϊ������
            {
                gamePane.MakeEnemy(float.Parse(angle),float.Parse(x),float.Parse(y),int.Parse(length));
            }
        }

        //������������Ϣ
        public void SendSnakeDeath()
        {
            string sendXml = SendXmlHelper.BuildSnakeDeathXml(uid + "");
            client.Send(sendXml);
        }

        //���ն�����������Ϣ�����ж�������������
        private void SnakeDeath(string userId)
        {
            if(userId != this.uid +"")
            {
                gamePane.SnakeOtherDeath();
            }
        }

        //���ͽ�����Ϣ
        public void SendJiesuanMsg(int score)
        {
            string sendXml = SendXmlHelper.BuildJiesuanMsgXml(uid+"", score);
            client.Send(sendXml);
        }



        //�����λ�¼�
        private void ClickChair(GameObject arg0)
        {
            //������ͼ���������Ϣ ��ʾ�ȴ�������ʾ ������λ���� ���ͷ��
            SendJoinTable();
            gamePane.AddWaitEnemy();
            GameChairIdUI chair = arg0.GetComponent<GameChairIdUI>();
            
            gamePane.AddHeadPic(chair.GetChairIdNum(),uid);

        }

        //������а�
        private void ClickRankList(GameObject arg0) 
        {
            //����������а�����������Ϣ ����ʾ���а�
            string sendXml = SendXmlHelper.BuildRankListDataRequestXml(uid+"",0);
            client.Send(sendXml);
            gamePane.ShowRankList();
        }

        //���а񻬵��ײ�
        public void RankListToBottom(int num)
        {
            //���а񻬵��ײ���������������Ϣ ���������� 
            string sendXml = SendXmlHelper.BuildRankListDataRequestXml(uid + "", num-20);
            client.Send(sendXml);
            
        }

        //��ʾ���а�����
        public void ShowRankData(String str)
        {
            gamePane.RankListData(str);
        }

        //������ذ�ť
        private void ClickReturn(GameObject arg0)
        {
            //��������û�������Ϣ ���ص�¼ҳ�� �������UI���� �ر��Ͽͻ��˵�ͨ��
            string sendXml = SendXmlHelper.BuildUserReturnXml();
            client.Send(sendXml);

            gamePane.ReturnLogin();
            
            Destroy(gamePane);
            client.Close();

            //Application.Quit();
        }

        //�������ģʽ�¼� ���뵥��ģʽ
        private void ClickSoloPlayBtn(GameObject arg0)
        {
            gamePane.EnterSoloGameScene();
        }

        //ʣ����Ϸʱ����Ϣ���� 
        private void GameTimeToClient(string gameTime)
        {
            gamePane.CountDownChange(gameTime);//���ĵ���ʱ
        }

        //���շ��͵��ͻ��˵���Ϸ������Ϣ
        private void GameOverToClient()
        {
            
            Debug.Log("��Ϸ����");
            gamePane.GameOver();//������Ϸ����
        }




        private void Update()
        {
            //ʵʱ������Ϣ������Ϣ
            while (client != null && client.SocketMsgQueue.Count > 0)
            {
                string msg = client.SocketMsgQueue.Dequeue();
                //Debug.Log("���յ���������socket��Ϣ��" + msg);
                List<string> tempList = XmlDataHelper.DataHelper(msg);
                //�����յ���Ϣͷ�����в�ͬ����
                if (tempList != null)
                {
                    switch (tempList[0])
                    {
                        case "LoginSuccess":
                            this.CheckSuccess(tempList[1], tempList[2]);
                            break;
                        case "YiDiLoginMsg":
                            this.YiDiLoginMsg(tempList[1]);
                            break;
                        case "AutoSitInfo":
                            this.AutoSitInfoToClient(tempList[1], tempList[2]);
                            break;
                        case "RankList":
                            this.ShowRankData(tempList[1]);
                            break;
                        //case "SendUserReadyInfoToClient":
                        //    this.SendUserReadyInfoToClient(tempList[1], tempList[2]);
                        //    break;
                        case "OtherUserInfo":
                            this.OtherUserInfo(tempList[1], tempList[2]);
                            break;
                        case "PlaySuccess":
                            this.PlaySuccess(tempList[1]);
                            break;

                        case "FoodPosition":
                            this.FoodPosition(tempList[1]);
                            break;
                        case "FoodTrans":
                            this.FoodTrans(tempList[1], tempList[2], tempList[3]);
                            break;
                        case "SnakeMsg":
                            this.SnakeMsg(tempList[1], tempList[2], tempList[3], tempList[4], tempList[5]);
                            break;
                        case "SnakeDeath":
                            this.SnakeDeath(tempList[1]);
                            break;
                        case "GameTimeToClient":
                            this.GameTimeToClient(tempList[1]);
                            break;
                        case "GameOverToClient":
                            this.GameOverToClient();
                            break;
                        default:
                            break;
                    }
                }

            }
        }


    }
    
   
}
