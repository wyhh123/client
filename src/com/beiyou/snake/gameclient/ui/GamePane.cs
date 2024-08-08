using com.beiyou.snake.common.res;
using com.beiyou.snake.common.utils;
using com.beiyou.snake.gameclient.engine;
using com.beiyou.snake.gameclient.entity;
using com.beiyou.snake.gameclient.socketdata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


namespace com.beiyou.snake.gameclient.ui
{
    public class GamePane : MonoBehaviour
    {
        //大厅组件
        private GameObject bgPic;//匹配界面背景
        private GameObject title;//匹配界面标题
        private GameObject vsPic;//vs图片
        private List<GameObject> chairIdList = null;// 座位号列表
        private List<GameObject> avatarList = null;// 头像
        private GameObject soloPlayBtn;//单人模式进入按钮
        private GameObject rankListBtn;//排行榜按钮
        private GameObject returnBtn;//返回按钮
        private GameObject waitEnemy;//等待对手
        private GameObject rankList;//排行榜
        

        //游戏页面组件
        private GameObject gmBg;//游戏界面背景
        private GameObject foods;//食物
        private GameObject snakeSelf;//玩家蛇
        public List<GameObject> snakeBodys = null;//蛇身列
        private GameObject snakeBody;//蛇身
        private GameObject segment;//蛇身小段
        private List<GameObject> snakeBodysOther = null;//其他蛇蛇身列
        private GameObject snakeOther;//其他蛇对象
        private GameObject snakeOtherHead;//其他蛇的头
        private GameObject joystick;//游戏摇杆
        private GameObject walls;//墙
        private GameObject soloQuitBtn;//单人模式退出按钮
        private GameObject countDown;//倒计时UI
        private GameObject score;//分数面板UI
        private GameObject jiesuan;//结算页面

        //游戏常数
        public int initlength = 1;//初始长度
        public int foodNum = 20;//固定食物数
        public string restTime = "60";//剩余时间
        //玩家和对手的数据 死亡数 长度 分数 用户名 uid
        public int deadCount = 0;
        public int selfLength = 1;
        public int selfScore = 0;
        public string selfName = "";
        public int selfUid = -1;
        public int enemyDeadCount = 0;
        public int enemyLength = 1;
        public int enemyScore = 0;
        public string enemyName = "";
        public int enemyUid = -1;

        //游戏标志
        public static int gameStageFlag = 0;//判断游戏是否结束
        public static int playerEatFlag = 0;//判断联机模式食物是由玩家还是对手吃



        private void Awake()
        {


            //大厅背景图片设置
            bgPic = new GameObject("GameBg");
            bgPic.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils = bgPic.AddComponent<CreateResUtils>();
            createResUtils.SetSprite("index/Image/index_ConfirmFormBGPic");
            ////createResUtils.SetTransform(1538, 0);
            createResUtils.rectTransform.anchoredPosition = new Vector2(0, 0);
            createResUtils.rectTransform.sizeDelta = new Vector2(1372f, 770f);
            //bgPic.transform.Rotate(Vector3.forward, 0f);

            //大厅标题设置
            title = new GameObject("title");
            title.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils1 = title.AddComponent<CreateResUtils>();
            createResUtils1.SetSprite("gameclient/sprites/Sprites/game_name_icon");
            ////createResUtils.SetTransform(1538, 0);
            createResUtils1.rectTransform.anchoredPosition = new Vector2(400, 0);
            createResUtils1.rectTransform.sizeDelta = new Vector2(600f, 135f);

            //vs图片
            vsPic = new GameObject("vsPic");
            vsPic.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils2 = vsPic.AddComponent<CreateResUtils>();
            createResUtils2.SetSprite("gameclient/vs");
            createResUtils2.rectTransform.anchoredPosition = new Vector2(315,-260);
            createResUtils2.rectTransform.sizeDelta = new Vector2(100,100);

            //座位列
            chairIdList = new List<GameObject>();
            for (int i = 0; i < 2; i++)
            {
                //座位创建 设置父对象 添加组件设置属性
                GameObject tempGameObject = new GameObject("Chair" + i);
                tempGameObject.transform.SetParent(gameObject.transform);

                tempGameObject.AddComponent<GameChairIdUI>();
                tempGameObject.GetComponent<GameChairIdUI>().SetChairIdNum(i);

                chairIdList.Add(tempGameObject);
            }

            //头像列
            avatarList = new List<GameObject>();
            for (int i = 0; i < 2; i++)
            {
                //头像创建 设置父对象 添加组件
                GameObject tempGameObject = new GameObject("Avatar" + i);
                tempGameObject.transform.SetParent(gameObject.transform);
                tempGameObject.AddComponent<Role>();
                avatarList.Add(tempGameObject);
            }


            //单人模式按钮
            soloPlayBtn = new GameObject("sPBtn");
            soloPlayBtn.transform.SetParent(gameObject.transform);
            CommonButton commonBtn_spbtn = soloPlayBtn.AddComponent<CommonButton>();
            commonBtn_spbtn.SetButtonLabel("单人练习");
            commonBtn_spbtn.rectTransform.anchoredPosition = new Vector2(1200, -40);

            //排行榜按钮
            rankListBtn = new GameObject("rBtn");
            rankListBtn.transform.SetParent(gameObject.transform);
            rankListBtn.AddComponent<ThreeStatusCommonBtn>();
            ThreeStatusCommonBtn threeStatusCommonBtn = rankListBtn.GetComponent<ThreeStatusCommonBtn>();//按钮点击图形变换脚本
            if (threeStatusCommonBtn != null)
            {
                string normalStr = "gameclient/sprites/Sprites/home_rank_bt_normal";
                string pressedStr = "gameclient/sprites/Sprites/home_rank_bt_press";
                string disableStr = "gameclient/sprites/Sprites/home_rank_bt_press";
                threeStatusCommonBtn.setThreeSprite(normalStr, pressedStr, disableStr);
                threeStatusCommonBtn.rectTransform.anchoredPosition = new Vector2(200, -550);
            }



            //返回按钮
            returnBtn = new GameObject("reBtn");
            returnBtn.transform.SetParent(gameObject.transform);
            returnBtn.AddComponent<ThreeStatusCommonBtn>();
            ThreeStatusCommonBtn threeStatusCommonBtn1 = returnBtn.GetComponent<ThreeStatusCommonBtn>();
            if (threeStatusCommonBtn1 != null)
            {
                string normalStr1 = "gameclient/sprites/Sprites/back_icon_normal";
                string pressedStr1 = "gameclient/sprites/Sprites/back_icon_press";
                string disableStr1 = "gameclient/sprites/Sprites/back_icon_press";
                threeStatusCommonBtn1.setThreeSprite(normalStr1, pressedStr1, disableStr1);
                threeStatusCommonBtn1.rectTransform.anchoredPosition = new Vector2(1000, -550);
            }


            //等待对手标志
            waitEnemy = new GameObject("wE");
            waitEnemy.transform.SetParent(gameObject.transform);
            CommonText commonText = waitEnemy.AddComponent<CommonText>();

            commonText.rectTransform.anchoredPosition = new Vector2(-90, 30);
            commonText.GetComponent<CommonText>().TextComponent.fontSize = 40;
            commonText.GetComponent<CommonText>().TextComponent.text = "正在等待对手。。。";
            commonText.GetComponent<CommonText>().TextComponent.color = Color.black;
            waitEnemy.SetActive(false);

            //排行榜
            rankList = new GameObject("rankList");
            rankList.transform.SetParent(gameObject.transform);
            rankList.AddComponent<RankListUI>();
            rankList.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30);
            rankList.GetComponent<RankListUI>().AddOnCloseBtnEventListener(HideRankList);
            rankList.SetActive(false);

            //初始化
            InitGame();

        }



        // 初始化大厅界面
        public void InitGame()
        {
            //设置座位
            chairIdList[0].GetComponent<GameChairIdUI>().rectTransform.anchoredPosition = new Vector2(200, -350);
            chairIdList[1].GetComponent<GameChairIdUI>().rectTransform.anchoredPosition = new Vector2(450, -350);


        }

        //根据点击添加图像
        public void AddHeadPic(int chairId,int uid)
        {
            Vector2 anchoredPosition = chairIdList[chairId].GetComponent<GameChairIdUI>().rectTransform.anchoredPosition;
            Vector2 tempVector2 = new Vector2(-25, 122);

            avatarList[chairId].GetComponent<Role>().rectTransform.anchoredPosition = anchoredPosition + tempVector2;
            avatarList[chairId].GetComponent<Role>().ShowUserAvater(uid);
            avatarList[chairId].GetComponent<Role>().SetSeatNumText(chairId);
        }

        //显示等待对手标志
        public void AddWaitEnemy()
        {
            waitEnemy.SetActive(true);         
        }

        //移除等待对手标志
        public void DelWaitEnemy()
        {
            waitEnemy.SetActive(false);
        }

        //显示排行榜
        public void ShowRankList()
        {
            rankList.SetActive(true);

            
        }

        //隐藏排行榜
        public void HideRankList(GameObject arg0)
        {
            //rankList.SetActive(false);
            Destroy(rankList);
            //移除老排行榜 重新创建对象
            rankList = new GameObject("rankList");
            rankList.transform.SetParent(gameObject.transform);
            rankList.AddComponent<RankListUI>();
            rankList.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30);
            rankList.GetComponent<RankListUI>().AddOnCloseBtnEventListener(HideRankList);
            rankList.SetActive(false);
        }

        //加载排行榜数据
        public void RankListData(string str)
        {
            //获取排行榜UI组件 调用其中更新数据的方法
            RankListUI view = rankList.GetComponent<RankListUI>();
            int num = view.items.Count-20;
            string[] vecList = str.Split(';');//接受的数据用分别用; _分割得到每组数据
            for (int i = 0; i < vecList.Length; i++)
            {
                if (vecList[i] != "" && vecList[i] != null)
                {
                    string[] arr = vecList[i].Split('_');
                    view.RankData(i+num, arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);

                }
            }
        }

        //返回登陆界面
        public void ReturnLogin()
        {
            //清除掉gamePane上的所有对象
            Destroy(waitEnemy);
            Destroy(returnBtn);
            Destroy(rankListBtn);
            Destroy(rankList);
            Destroy(soloPlayBtn);
            for (int i = 0; i < 2; i++)
            {
                Destroy(chairIdList[i]);
            }
            for (int i = 0; i < 2; i++)
            {
                Destroy(avatarList[i]);
            }
            chairIdList.Clear();
            avatarList.Clear();
            Destroy(vsPic);
            Destroy(bgPic);
            Destroy(title);

        }

        //玩家信息设置
        public void SetUserInfo(string name,int id)
        {
            selfName = name;
            selfUid = id;
        }

        //进入游戏界面 单人
        public void EnterSoloGameScene()
        {
            //游戏开始
            gameStageFlag = 1;

            //游戏背景
            gmBg = new GameObject("gmBg");
            gmBg.transform.SetParent(gameObject.transform);
            //背景图片UI的设置
            CreateResUtils createResUtils2 = gmBg.AddComponent<CreateResUtils>();
            createResUtils2.SetSprite("gameclient/sprites/Sprites/gamegaijin");
            createResUtils2.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            createResUtils2.rectTransform.sizeDelta = new Vector2(1372f, 770f);

            //退出按钮
            soloQuitBtn = new GameObject("sQBtn");
            soloQuitBtn.transform.SetParent(gameObject.transform);
            CommonButton commonBtn_sqbtn = soloQuitBtn.AddComponent<CommonButton>();
            commonBtn_sqbtn.SetButtonLabel("返回");
            commonBtn_sqbtn.rectTransform.anchoredPosition = new Vector2(1200, -40);
            AddSoloQuitBtnEventListener(ClickSoloQuitBtn);

            //游戏摇杆设置 添加摇杆控制脚本
            joystick = new GameObject("joystick");
            joystick.transform.SetParent(gameObject.transform);
            joystick.AddComponent<RectTransform>();
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 270);
            joystick.GetComponent<RectTransform>().anchoredPosition = new Vector3(-485, -245, 0 );
            joystick.AddComponent<JoystickUI>();


            //游戏食物生成 
            //if (!UnityEditorInternal.InternalEditorUtility.tags.Equals("Food"))//稳定添加食物标签
            //{
            //    UnityEditorInternal.InternalEditorUtility.AddTag("Food");
            //}
            //代码添加标签影响打包
            foods = new GameObject("Foods");
            foods.transform.SetParent(gameObject.transform);
            for(int i = 0; i < foodNum; i++) {
                GameObject food = new GameObject("food"+i);
                food.transform.SetParent(foods.transform);
                //添加普通食物UI脚本
                food.AddComponent<FoodUI>();

            }

            //添加墙壁
            //if (!UnityEditorInternal.InternalEditorUtility.tags.Equals("Obs"))//稳定添加障碍标签
            //{
            //    UnityEditorInternal.InternalEditorUtility.AddTag("Obs");
            //}
            //代码添加标签影响打包
            walls = new GameObject("Wall");
            walls.transform.SetParent(gameObject.transform);
            for (int i = 0; i < 4; i++)
            {
                //墙体循环设置 添加碰撞箱
                GameObject wall = new GameObject("wall" + i);
                wall.transform.SetParent(walls.transform);
                wall.AddComponent<RectTransform>();
                wall.AddComponent<BoxCollider2D>();
                wall.GetComponent<BoxCollider2D>().isTrigger = true;
                switch (i)
                {
                    case 0:
                        //下墙体 位置大小
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(787, -32);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(1712, 60);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(1712, 60);
                        break;
                    case 1:
                        //上墙体 位置大小
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(787, 799);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(1712, 60);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(1712, 60);
                        break;
                    case 2:
                        //左墙体 位置大小
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 407);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 843);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(200, 843);
                        break;
                    case 3:
                        //右墙体 位置大小
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(1460, 407);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 843);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(200, 843);
                        break;
                    default:
                        break;
                }
                //障碍标签
                wall.tag = "Obs";
            }

            //玩家蛇设置
            MakeSnake();

        }

        //生成单人玩家蛇
        public void MakeSnake()
        {
            //蛇身体和蛇头的初始化 设置父对象 添加蛇UI组件 (条件判断 游戏开始且对象完全释放)
            if (IsSnakeBodyDestory() && gameStageFlag == 1) { snakeBody = new GameObject("bodys"); }
            snakeBody.transform.SetParent(gameObject.transform);
            if (IsSnakeSelfDestory() && gameStageFlag == 1) { snakeSelf = new GameObject("snakehead"); }
            snakeSelf.transform.SetParent(gameObject.transform);
            snakeSelf.AddComponent<SnakeUI>();

            //蛇身小段初始化 加入蛇身列
            snakeBodys = new List<GameObject>();
            for (int i = 0; i < initlength; i++)
            {
                segment = new GameObject("segment" + i);
                segment.transform.SetParent(snakeBody.transform);
                segment.AddComponent<SnakeBodyUI>();
                snakeBodys.Add(segment);
            }
            //蛇身添加运动脚本 控制蛇身运动
            snakeBody.AddComponent<SnakeBodyMove>();
            snakeBody.GetComponent<SnakeBodyMove>().head = snakeSelf;
            snakeBody.GetComponent<SnakeBodyMove>().tSnakeBodys = snakeBodys;
        }

        //单人玩家蛇释放
        public void SnakeDeath()
        {
            //对于每个蛇身创建食物 销毁蛇身
            for (int i = 0; i < snakeBodys.Count; i++)
            {
                //创建蛇身体食物
                GameObject food = new GameObject("deadfood"+i);
                food.transform.SetParent(foods.transform);
                food.AddComponent<DeadBodyUI>();
                food.GetComponent<RectTransform>().anchoredPosition = snakeBodys[i].GetComponent<RectTransform>().anchoredPosition;
                //销毁蛇身
                Destroy(snakeBodys[i]);
            }

            //释放蛇身蛇头对象 (仅在对象存在情况下)
            snakeBodys.Clear();
            if (!IsSnakeBodyDestory()) { Destroy(snakeBody); }
            if (!IsSnakeSelfDestory()) { Destroy(snakeSelf); }
        }


        //退出单人模式按钮点击事件
        private void ClickSoloQuitBtn(GameObject arg0)
        {
            gameStageFlag = 0;
            //游戏结束

            //释放蛇
            SnakeDeath();
            //释放各个组件
            Destroy(walls);
            Destroy(foods);
            Destroy(joystick);
            Destroy(gmBg);
            Destroy(soloQuitBtn);

        }

        //联机模式
        public void OnlineMode()
        {
            //游戏开始
            gameStageFlag = 1;



            //游戏背景
            gmBg = new GameObject("gmBg");
            gmBg.transform.SetParent(gameObject.transform);

            CreateResUtils createResUtils2 = gmBg.AddComponent<CreateResUtils>();
            createResUtils2.SetSprite("gameclient/sprites/Sprites/gamegaijin");
            createResUtils2.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            createResUtils2.rectTransform.sizeDelta = new Vector2(1372f, 770f);

            //游戏摇杆设置
            joystick = new GameObject("joystick");

            joystick.transform.SetParent(gameObject.transform);
            joystick.AddComponent<RectTransform>();
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 270);
            joystick.GetComponent<RectTransform>().anchoredPosition = new Vector3(-485, -245, 0);
            joystick.AddComponent<JoystickUI>();

            //添加墙壁
            //if (!UnityEditorInternal.InternalEditorUtility.tags.Equals("Obs"))
            //{
            //    UnityEditorInternal.InternalEditorUtility.AddTag("Obs");
            //}
            walls = new GameObject("Wall");
            walls.transform.SetParent(gameObject.transform);
            for (int i = 0; i < 4; i++)
            {
                GameObject wall = new GameObject("wall" + i);
                wall.transform.SetParent(walls.transform);
                wall.AddComponent<RectTransform>();
                wall.AddComponent<BoxCollider2D>();
                wall.GetComponent<BoxCollider2D>().isTrigger = true;
                switch (i)
                {
                    case 0:
                        //下墙体
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(787, -32);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(1712, 60);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(1712, 60);
                        break;
                    case 1:
                        //上墙体
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(787, 799);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(1712, 60);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(1712, 60);
                        break;
                    case 2:
                        //左墙体
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 407);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 843);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(200, 843);
                        break;
                    case 3:
                        //右墙体
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(1460, 407);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 843);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(200, 843);
                        break;
                    default:
                        break;
                }

                wall.tag = "Obs";
            }

            //游戏倒计时UI
            countDown = new GameObject("countdown");
            countDown.transform.SetParent(gameObject.transform);
            CommonText commonText1 = countDown.AddComponent<CommonText>();

            commonText1.rectTransform.anchoredPosition = new Vector2(-90, 350);
            commonText1.GetComponent<CommonText>().TextComponent.fontSize = 30;
            commonText1.GetComponent<CommonText>().TextComponent.text = "距离结束还有："+restTime+" 秒";
            commonText1.GetComponent<CommonText>().TextComponent.color = Color.black;

            //得分面板UI
            score = new GameObject("score");
            score.transform.SetParent(gameObject.transform);
            CommonText commonText2 = score.AddComponent<CommonText>();

            commonText2.rectTransform.anchoredPosition = new Vector2(330, 350);
            commonText2.GetComponent<CommonText>().TextComponent.fontSize = 20;
            commonText2.GetComponent<CommonText>().TextComponent.text = "(You)玩家：" + selfName + " 长度："+selfLength +
                "  得分：" +selfScore + "\n         玩家：" + enemyName + " 长度：" + enemyLength +"  得分：" + enemyScore;
            commonText2.GetComponent<CommonText>().TextComponent.color = Color.black;

            //玩家蛇设置
            MakeOnlineSnake();

            //对手蛇初始化
            snakeBodysOther = new List<GameObject>();//对手蛇身列
            snakeOther = new GameObject("enemy");//对手蛇整体对象
            snakeOther.transform.SetParent(gameObject.transform);
            for(int i = 0; i < 100; i++)
            {
                //初始化每个对手蛇身小段 设置其父对象snakeOther 添加对手蛇身UI脚本 添加标签 加入对手蛇身列
                GameObject snakebodye = new GameObject("enemybody" + i);
                snakebodye.transform.SetParent(snakeOther.transform);
                snakebodye.AddComponent<SnakeBodyEUI>();
                snakebodye.tag = "Obs";
                snakeBodysOther.Add(snakebodye);
            }
            //对手蛇头初始化 设置父对象 添加对手蛇头的控制脚本（进行位置传递以外的固定运动补充）
            snakeOtherHead = new GameObject("enemyhead");
            snakeOtherHead.transform.SetParent(gameObject.transform);
            snakeOtherHead.AddComponent<SnakeHeadEUI>();

            //为对手蛇增加蛇身运动脚本（控制对手蛇身运动）
            if (snakeOther.GetComponent<SnakeBodyMoveE>())
            {
                snakeOther.GetComponent<SnakeBodyMoveE>().head = snakeOtherHead;
                snakeOther.GetComponent<SnakeBodyMoveE>().tSnakeBodys = snakeBodysOther;
            }
            else
            {
                snakeOther.AddComponent<SnakeBodyMoveE>();
                snakeOther.GetComponent<SnakeBodyMoveE>().head = snakeOtherHead;
                snakeOther.GetComponent<SnakeBodyMoveE>().tSnakeBodys = snakeBodysOther;
            }

            //结算页面初始化 添加结算UI脚本
            jiesuan = new GameObject("js");
            jiesuan.transform.SetParent(gameObject.transform);
            
            jiesuan.AddComponent<JieSuanDiaUI>();
            jiesuan.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-78, -24);
            jiesuan.GetComponent<JieSuanDiaUI>().AddOnCloseBtnEventListener(CloseJieSuan);
            jiesuan.SetActive(false);
        }

        //更新分数面板
        public void RenewScoreUI(int x1,int x2,int x3,int x4)// 己方/敌方 长度/分数  数据更新
        {
            //己方长度 己方分数 敌方长度 敌方分数
            selfLength += x1;
            selfScore += x2;
            enemyLength += x3;
            enemyScore += x4;
            //小于最小值时置为最小值
            if (selfLength < initlength) { selfLength = initlength; }
            if (selfScore < 0) { selfScore = 0; }
            if (enemyLength < initlength) { enemyLength = initlength; }
            if (enemyScore < 0) { enemyScore = 0; }
            if (score != null)
            {
                //计分板文本显示
                score.GetComponent<CommonText>().TextComponent.text = "(You)玩家：" + selfName + " 长度：" + selfLength +
        "  得分：" + selfScore + "\n        玩家：" + enemyName + " 长度：" + enemyLength + "  得分：" + enemyScore;
            }
        }

        //生成联网蛇
        public void MakeOnlineSnake()
        {
            //仅在对象不存在情况下创建新对象 蛇身蛇头
            if (IsSnakeBodyDestory()) { snakeBody = new GameObject("bodys"); } 
            snakeBody.transform.SetParent(gameObject.transform);
            if (IsSnakeSelfDestory()) { snakeSelf = new GameObject("snakehead"); }
            snakeSelf.transform.SetParent(gameObject.transform);
            

            snakeBodys = new List<GameObject>();
            for (int i = 0; i < initlength; i++)
            {
                //初始化蛇小段加入蛇身列
                segment = new GameObject("segment" + i);
                segment.transform.SetParent(snakeBody.transform);
                segment.AddComponent<SnakeBodyUI>();
                snakeBodys.Add(segment);
            }
            //为蛇头添加联网蛇UI组件
            if (snakeSelf.GetComponent<SnakeOnlieUI>())
            {

            }
            else
            {
                snakeSelf.AddComponent<SnakeOnlieUI>();
            }
            //为蛇身添加蛇身运动组件
            if (snakeBody.GetComponent<SnakeBodyMove>())
            {
                snakeBody.GetComponent<SnakeBodyMove>().tSnakeBodys = snakeBodys;
                snakeBody.GetComponent<SnakeBodyMove>().head = snakeSelf;
            }
            else
            {
                snakeBody.AddComponent<SnakeBodyMove>();
                snakeBody.GetComponent<SnakeBodyMove>().tSnakeBodys = snakeBodys;
                snakeBody.GetComponent<SnakeBodyMove>().head = snakeSelf;
            }
            
            
        }

        //联网己方蛇死亡
        public void OnlineSnakeDeath()
        {
            //每个蛇身位置生成食物
            for (int i = 0; i < snakeBodys.Count; i++)
            {
                GameObject food = new GameObject("deadfood" + i);
                food.transform.SetParent(foods.transform);
                food.AddComponent<DeadBodyUI>();
                food.GetComponent<RectTransform>().anchoredPosition = snakeBodys[i].GetComponent<RectTransform>().anchoredPosition;

                
            }
            
            if (snakeBody != null) { Destroy(snakeBody.AddComponent<SnakeBodyMove>()); }

            //释放蛇身蛇头对象 (仅在对象存在情况下)
            snakeBodys.Clear();
            if (!IsSnakeBodyDestory()) { Destroy(snakeBody); }
            if (!IsSnakeSelfDestory()) { Destroy(snakeSelf); }
            
        }

        //联机对手蛇移动
        public void MakeEnemy(float angle,float x,float y,int length)
        {
            if(gameStageFlag == 0) { return; }
            //必须是游戏阶段才有后续操作

            //对方蛇头存在 改变为传递信息的位置 调整为传递信息的方向
            if (snakeOtherHead != null) {
                snakeOtherHead.transform.position = new Vector2(x, y);
                snakeOtherHead.transform.eulerAngles = new Vector3(0, 0, angle - 90);
            }
            //敌方蛇身第1段自动更新为敌方蛇头位置（长度不为0时）
            snakeBodysOther[0].transform.position = new Vector2(x, y);
            if (length == 0) { snakeBodysOther[0].transform.position = new Vector2(-10, -10); }
                
            //for(int i = length-1; i >0; i--)
            //{
            //    snakeBodysOther[i].transform.position = snakeBodysOther[i - 1].transform.position;
            //}
            //敌方蛇头通过脚本控制进行信息传递之外的运动补充 更新其角度和存活状态
            snakeOtherHead.GetComponent<SnakeHeadEUI>().angle = angle;
            snakeOtherHead.GetComponent<SnakeHeadEUI>().live = 1;
            //敌方蛇身更新长度状态
            snakeOther.GetComponent<SnakeBodyMoveE>().length = length;
        }



        //联机生成食物
        public void OnlineMakeFood(string position)
        {
            //if (!UnityEditorInternal.InternalEditorUtility.tags.Equals("Food"))
            //{
            //    UnityEditorInternal.InternalEditorUtility.AddTag("Food");
            //}
            //通过split方法处理得到每个食物位置颜色信息
            foods = new GameObject("Foods");
            foods.transform.SetParent(gameObject.transform);
            string[] vecList = position.Split(';');
            for(int i = 0; i < vecList.Length; i++)
            {
                if (vecList[i] != "" && vecList[i]!=null)
                {
                    string []arr = vecList[i].Split(',');
                    int x = int.Parse(arr[0]);
                    int y = int.Parse(arr[1]);
                    int z = int.Parse(arr[2]);
                    GameObject food = new GameObject("food" + i);
                    food.transform.SetParent(foods.transform);
                    //食物添加联网食物UI脚本 调用其方法进行位置颜色设置
                    food.AddComponent<FoodOnlineUI>();
                    food.GetComponent<FoodOnlineUI>().Set(x, y, z);
                    
                }
            }

        }

        //联机调整食物位置
        public void FoodTransform(string name,string x,string y)
        {
            //食物位置调整但不是玩家吃 则对手吃
            if (playerEatFlag == 0) { 
                RenewScoreUI(0, 0, 1, 10);
            }
            else
            {
                //食物位置调整且是玩家吃 将标志调零
                playerEatFlag = 0;
            }
            //联机食物被吃统一调整位置
            GameObject gameObject1 = GameObject.Find(name);
            gameObject1.GetComponent<RectTransform>().anchoredPosition = new Vector2(int.Parse(x),int.Parse(y));
        }

        //联机对手蛇死亡处理
        public void SnakeOtherDeath()
        {
            //计分板更新
            RenewScoreUI(0, 0, -99, -100);
            enemyDeadCount++;
            //对象不为空时进行移位处理（频繁销毁创建影响性能）
            if (snakeOtherHead != null)
            {
                snakeOtherHead.transform.position = new Vector2(-10, -10);
                snakeOtherHead.GetComponent<SnakeHeadEUI>().live = 0;//标记对手蛇状态
            }

            if (snakeBodysOther[0] != null)
            {
                snakeBodysOther[0].transform.position = new Vector3(-10, -10);
            }

            for(int i = 1; i < snakeBodysOther.Count; i++)//从初始到对手蛇数目上限遍历
            {
                if (snakeBodysOther[i].transform.position == new Vector3(-10, -10))
                {
                    break;//蛇身已在规定位置 处理完毕
                }
                else
                {
                    //蛇身未在规定位置 创建蛇死亡食物 蛇身移位
                    GameObject food = new GameObject("deadfood" + i);
                    food.transform.SetParent(foods.transform);
                    food.AddComponent<DeadBodyUI>();
                    food.GetComponent<RectTransform>().anchoredPosition = snakeBodysOther[i].GetComponent<RectTransform>().anchoredPosition;
                    snakeBodysOther[i].transform.position = new Vector3(-10, -10);
                }
            }
            //SnakeBodysOther
        }

        public bool IsSnakeBodyDestory()//判断是否销毁
        {
            return snakeBody==null;
        }

        public bool IsSnakeSelfDestory()//判断是否销毁
        {
            return snakeSelf == null;
        }

        //倒计时更新
        public void CountDownChange(string time)
        {
            restTime = time;
            countDown.GetComponent<CommonText>().TextComponent.text = "倒计时：" + restTime + " 秒";
        }

        //设置对手信息
        public void SetEnemyInfo(string uname,string uid)
        {
            enemyName = uname;
            enemyUid = int.Parse(uid);
        }

        //结算页面关闭
        public void CloseJieSuan(GameObject arg0)
        {
            Destroy(jiesuan);
        }

        //游戏结束 后续完善
        public async void GameOver()
        {
            //游戏结束标志设0
            gameStageFlag = 0;

            //销毁自己和联机对手
            OnlineSnakeDeath();
            for(int i = 0; i < 60; i++)
            {
                Destroy(snakeBodysOther[i]);
            }
            snakeBodysOther.Clear();
            Destroy(snakeOther);
            Destroy(snakeOtherHead);

            //销毁各个组件
            Destroy(countDown);
            Destroy(score);
            Destroy(walls);
            Destroy(foods);
            Destroy(joystick);
            Destroy(gmBg);

            //头像去除
            for (int i = 0; i < 2; i++)
            {

                avatarList[i].GetComponent<Role>().rectTransform.anchoredPosition = new Vector2(-500,-500);
            }

            // 发送游戏分数
            this.GetComponent<GameLogic>().SendJiesuanMsg(selfScore);

            //显示结算画面 播放声音动画
            if (selfScore > enemyScore)//己方获胜
            {
                jiesuan.GetComponent<JieSuanDiaUI>().SetJieSuanText(selfName,selfScore,deadCount);
                jiesuan.GetComponent<JieSuanDiaUI>().PlayVicVocal();
            }
            else if(selfScore == enemyScore)//平局
            {
                jiesuan.GetComponent<JieSuanDiaUI>().SetTieJieSuanText();
                jiesuan.GetComponent<JieSuanDiaUI>().PlayNoVicVocal();
            }
            else//对方获胜
            {
                jiesuan.GetComponent<JieSuanDiaUI>().SetJieSuanText(enemyName, enemyScore, enemyDeadCount);
                jiesuan.GetComponent<JieSuanDiaUI>().PlayNoVicVocal();
            }
            jiesuan.SetActive(true);//结算显示
            RenewScoreUI(-99, -9999, -99, -9999);//计分板重置

            await Task.Delay(10000);//10秒后未关闭自动关闭结算页
            if (jiesuan != null&& gameStageFlag == 0)//必须在结算不为空且下场游戏未开始时才能销毁结算对象
            {
                Destroy(jiesuan);
            }
        }

        //添加座位点击事件监听
        public void AddClickChairEventListener(UnityAction<GameObject> eventHandler)
        {
            for (int i = 0; i < 2; i++)
            {
                chairIdList[i].GetComponent<GameChairIdUI>().AddClickAvatarPicEventListener(eventHandler);
            }
        }

        //添加返回按钮点击事件监听
        public void AddReturnBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            returnBtn.GetComponent<ThreeStatusCommonBtn>().AddBtnEventListener(eventHandler);
        }

        //添加排行榜按钮点击事件监听
        public void AddClickRankListBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            rankListBtn.GetComponent<ThreeStatusCommonBtn>().AddBtnEventListener(eventHandler);
        }

        //添加单人模式按钮点击事件监听
        public void AddSoloPlayBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            soloPlayBtn.GetComponent<CommonButton>().AddBtnEventListener(eventHandler);
        }

        //添加退出单人模式按钮点击事件监听
        public void AddSoloQuitBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            soloQuitBtn.GetComponent<CommonButton>().AddBtnEventListener(eventHandler);
        }



    }

}