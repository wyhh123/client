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
        //游戏面板对象
        private GamePane gamePane;
        //用户名 密码 uid
        private string userName = "";
        private string passWord = "";
        private int uid = 0;
        //客户端 ip地址 端口
        private ClientPeer client = null;
        private string ip = "192.168.1.136";
        private int port = 8201;

        //用户桌 用户
        public UserTableEntity userTableEntity = null;
        public OneUserEntity myUserEntity = null;


        private void Awake()
        {
            //用户桌用户的初始化
            this.userTableEntity = new UserTableEntity();
            this.myUserEntity = new OneUserEntity();    
        }

        //客户端服务器连接
        public void StartGameConnect(string userName, string passWord)
        {
            this.userName = userName;
            this.passWord = passWord;

            StartConnonectSocket();
        }

        //开始连接socket
        private void StartConnonectSocket()
        {
            client = new ClientPeer(ip, port);
            client.Connect();

            SendLoginInfo();
        }

        //发送登录信息
        private void SendLoginInfo()
        {
            string sendXml = SendXmlHelper.BuildUserLoginXml(this.userName, this.passWord);
            client.Send(sendXml);
        }

        //登录成功检测处理
        private void CheckSuccess(string success,string userId)
        {
            if (success == "1")
            {
                uid = int.Parse(userId);
                this.myUserEntity.SetUid(uid);
                Debug.Log("登录成功");
                SendAutoSitInfoXml();
                //登录成功发送自动入座信息
                //初始化游戏大厅UI 设置信息 并为各个按钮添加点击事件监听
                gamePane = gameObject.AddComponent<GamePane>();
                gamePane.SetUserInfo(this.userName,this.uid);
                gamePane.AddSoloPlayBtnEventListener(ClickSoloPlayBtn);
                gamePane.AddClickChairEventListener(ClickChair);
                gamePane.AddClickRankListBtnEventListener(ClickRankList);
                gamePane.AddReturnBtnEventListener(ClickReturn);
            }

        }

        //异地登录信息
        private void YiDiLoginMsg(string userId)
        {
            Debug.Log(userId + "异地登录");
        }

        //发送自动入座信息
        private void SendAutoSitInfoXml()
        {
            string sendXml = SendXmlHelper.BuildAutoSitInfoXml("" + this.myUserEntity.GetUid());
            client.Send(sendXml);
        }

        //自动入座处理
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
                            // 设置数值
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

        //尝试加入桌子
        private void SendJoinTable()
        {
            string sendXml = SendXmlHelper.BuildJoinTableXml(uid + "");
            client.Send(sendXml);
        }

        //其他用户准备信息
        //private void SendUserReadyInfoToClient(string chairId, string ready)
        //{

        //}

        //获得其他用户信息
        private void OtherUserInfo(string eUserName,string eUserId)
        {
            gamePane.SetEnemyInfo(eUserName, eUserId);
        }


        //成功匹配进入游戏
        private void PlaySuccess(string success) {
            if(success == "1")
            {
                gamePane.DelWaitEnemy();
                gamePane.OnlineMode();
            }
        }

        //在服务器传输的食物位置处生成食物 
        private void FoodPosition(string position)
        {
            gamePane.OnlineMakeFood(position);
        }

        //原生食物被吃后发送食物信息
        public void SendFoodEatMsg(string name)
        {
            string sendXml = SendXmlHelper.BuildFoodEatXml(name);
            client.Send(sendXml);
        }

        //接收服务器的食物名称和新位置信息后 食物位置变换
        private void FoodTrans(string name,string x,string y)
        {
            gamePane.FoodTransform(name, x, y);
        }

        //发送蛇的角度和位置 长度信息
        public void SendSnakeMsg(float angle,float x,float y,int length)
        {
            string sendXml = SendXmlHelper.BuildSnakeMsgXml(uid+"", angle, x,y,length);
            client.Send(sendXml);
        }

        //接收对手蛇的角度和位置长度信息并调整对手蛇
        private void SnakeMsg(string userId, string angle, string x,string y,string length)
        {
            if (userId != this.uid + "")//确保为对手蛇
            {
                gamePane.MakeEnemy(float.Parse(angle),float.Parse(x),float.Parse(y),int.Parse(length));
            }
        }

        //发送蛇死亡信息
        public void SendSnakeDeath()
        {
            string sendXml = SendXmlHelper.BuildSnakeDeathXml(uid + "");
            client.Send(sendXml);
        }

        //接收对手蛇死亡信息并进行对手蛇死亡处理
        private void SnakeDeath(string userId)
        {
            if(userId != this.uid +"")
            {
                gamePane.SnakeOtherDeath();
            }
        }

        //发送结算信息
        public void SendJiesuanMsg(int score)
        {
            string sendXml = SendXmlHelper.BuildJiesuanMsgXml(uid+"", score);
            client.Send(sendXml);
        }



        //点击座位事件
        private void ClickChair(GameObject arg0)
        {
            //点击后发送加入桌子信息 显示等待对手提示 创建座位对象 添加头像
            SendJoinTable();
            gamePane.AddWaitEnemy();
            GameChairIdUI chair = arg0.GetComponent<GameChairIdUI>();
            
            gamePane.AddHeadPic(chair.GetChairIdNum(),uid);

        }

        //点击排行榜
        private void ClickRankList(GameObject arg0) 
        {
            //点击后发送排行榜数据请求信息 并显示排行榜
            string sendXml = SendXmlHelper.BuildRankListDataRequestXml(uid+"",0);
            client.Send(sendXml);
            gamePane.ShowRankList();
        }

        //排行榜滑到底部
        public void RankListToBottom(int num)
        {
            //排行榜滑倒底部发送数据请求信息 请求新数据 
            string sendXml = SendXmlHelper.BuildRankListDataRequestXml(uid + "", num-20);
            client.Send(sendXml);
            
        }

        //显示排行榜数据
        public void ShowRankData(String str)
        {
            gamePane.RankListData(str);
        }

        //点击返回按钮
        private void ClickReturn(GameObject arg0)
        {
            //点击后发送用户返回信息 返回登录页面 处理大厅UI对象 关闭老客户端的通信
            string sendXml = SendXmlHelper.BuildUserReturnXml();
            client.Send(sendXml);

            gamePane.ReturnLogin();
            
            Destroy(gamePane);
            client.Close();

            //Application.Quit();
        }

        //点击单人模式事件 进入单人模式
        private void ClickSoloPlayBtn(GameObject arg0)
        {
            gamePane.EnterSoloGameScene();
        }

        //剩余游戏时间信息接收 
        private void GameTimeToClient(string gameTime)
        {
            gamePane.CountDownChange(gameTime);//更改倒计时
        }

        //接收发送到客户端的游戏结束信息
        private void GameOverToClient()
        {
            
            Debug.Log("游戏结束");
            gamePane.GameOver();//处理游戏结束
        }




        private void Update()
        {
            //实时处理消息队列信息
            while (client != null && client.SocketMsgQueue.Count > 0)
            {
                string msg = client.SocketMsgQueue.Dequeue();
                //Debug.Log("接收到服务器的socket消息：" + msg);
                List<string> tempList = XmlDataHelper.DataHelper(msg);
                //根据收到消息头部进行不同处理
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
