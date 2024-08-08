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
        //�������
        private GameObject bgPic;//ƥ����汳��
        private GameObject title;//ƥ��������
        private GameObject vsPic;//vsͼƬ
        private List<GameObject> chairIdList = null;// ��λ���б�
        private List<GameObject> avatarList = null;// ͷ��
        private GameObject soloPlayBtn;//����ģʽ���밴ť
        private GameObject rankListBtn;//���а�ť
        private GameObject returnBtn;//���ذ�ť
        private GameObject waitEnemy;//�ȴ�����
        private GameObject rankList;//���а�
        

        //��Ϸҳ�����
        private GameObject gmBg;//��Ϸ���汳��
        private GameObject foods;//ʳ��
        private GameObject snakeSelf;//�����
        public List<GameObject> snakeBodys = null;//������
        private GameObject snakeBody;//����
        private GameObject segment;//����С��
        private List<GameObject> snakeBodysOther = null;//������������
        private GameObject snakeOther;//�����߶���
        private GameObject snakeOtherHead;//�����ߵ�ͷ
        private GameObject joystick;//��Ϸҡ��
        private GameObject walls;//ǽ
        private GameObject soloQuitBtn;//����ģʽ�˳���ť
        private GameObject countDown;//����ʱUI
        private GameObject score;//�������UI
        private GameObject jiesuan;//����ҳ��

        //��Ϸ����
        public int initlength = 1;//��ʼ����
        public int foodNum = 20;//�̶�ʳ����
        public string restTime = "60";//ʣ��ʱ��
        //��ҺͶ��ֵ����� ������ ���� ���� �û��� uid
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

        //��Ϸ��־
        public static int gameStageFlag = 0;//�ж���Ϸ�Ƿ����
        public static int playerEatFlag = 0;//�ж�����ģʽʳ��������һ��Ƕ��ֳ�



        private void Awake()
        {


            //��������ͼƬ����
            bgPic = new GameObject("GameBg");
            bgPic.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils = bgPic.AddComponent<CreateResUtils>();
            createResUtils.SetSprite("index/Image/index_ConfirmFormBGPic");
            ////createResUtils.SetTransform(1538, 0);
            createResUtils.rectTransform.anchoredPosition = new Vector2(0, 0);
            createResUtils.rectTransform.sizeDelta = new Vector2(1372f, 770f);
            //bgPic.transform.Rotate(Vector3.forward, 0f);

            //������������
            title = new GameObject("title");
            title.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils1 = title.AddComponent<CreateResUtils>();
            createResUtils1.SetSprite("gameclient/sprites/Sprites/game_name_icon");
            ////createResUtils.SetTransform(1538, 0);
            createResUtils1.rectTransform.anchoredPosition = new Vector2(400, 0);
            createResUtils1.rectTransform.sizeDelta = new Vector2(600f, 135f);

            //vsͼƬ
            vsPic = new GameObject("vsPic");
            vsPic.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils2 = vsPic.AddComponent<CreateResUtils>();
            createResUtils2.SetSprite("gameclient/vs");
            createResUtils2.rectTransform.anchoredPosition = new Vector2(315,-260);
            createResUtils2.rectTransform.sizeDelta = new Vector2(100,100);

            //��λ��
            chairIdList = new List<GameObject>();
            for (int i = 0; i < 2; i++)
            {
                //��λ���� ���ø����� ��������������
                GameObject tempGameObject = new GameObject("Chair" + i);
                tempGameObject.transform.SetParent(gameObject.transform);

                tempGameObject.AddComponent<GameChairIdUI>();
                tempGameObject.GetComponent<GameChairIdUI>().SetChairIdNum(i);

                chairIdList.Add(tempGameObject);
            }

            //ͷ����
            avatarList = new List<GameObject>();
            for (int i = 0; i < 2; i++)
            {
                //ͷ�񴴽� ���ø����� ������
                GameObject tempGameObject = new GameObject("Avatar" + i);
                tempGameObject.transform.SetParent(gameObject.transform);
                tempGameObject.AddComponent<Role>();
                avatarList.Add(tempGameObject);
            }


            //����ģʽ��ť
            soloPlayBtn = new GameObject("sPBtn");
            soloPlayBtn.transform.SetParent(gameObject.transform);
            CommonButton commonBtn_spbtn = soloPlayBtn.AddComponent<CommonButton>();
            commonBtn_spbtn.SetButtonLabel("������ϰ");
            commonBtn_spbtn.rectTransform.anchoredPosition = new Vector2(1200, -40);

            //���а�ť
            rankListBtn = new GameObject("rBtn");
            rankListBtn.transform.SetParent(gameObject.transform);
            rankListBtn.AddComponent<ThreeStatusCommonBtn>();
            ThreeStatusCommonBtn threeStatusCommonBtn = rankListBtn.GetComponent<ThreeStatusCommonBtn>();//��ť���ͼ�α任�ű�
            if (threeStatusCommonBtn != null)
            {
                string normalStr = "gameclient/sprites/Sprites/home_rank_bt_normal";
                string pressedStr = "gameclient/sprites/Sprites/home_rank_bt_press";
                string disableStr = "gameclient/sprites/Sprites/home_rank_bt_press";
                threeStatusCommonBtn.setThreeSprite(normalStr, pressedStr, disableStr);
                threeStatusCommonBtn.rectTransform.anchoredPosition = new Vector2(200, -550);
            }



            //���ذ�ť
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


            //�ȴ����ֱ�־
            waitEnemy = new GameObject("wE");
            waitEnemy.transform.SetParent(gameObject.transform);
            CommonText commonText = waitEnemy.AddComponent<CommonText>();

            commonText.rectTransform.anchoredPosition = new Vector2(-90, 30);
            commonText.GetComponent<CommonText>().TextComponent.fontSize = 40;
            commonText.GetComponent<CommonText>().TextComponent.text = "���ڵȴ����֡�����";
            commonText.GetComponent<CommonText>().TextComponent.color = Color.black;
            waitEnemy.SetActive(false);

            //���а�
            rankList = new GameObject("rankList");
            rankList.transform.SetParent(gameObject.transform);
            rankList.AddComponent<RankListUI>();
            rankList.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30);
            rankList.GetComponent<RankListUI>().AddOnCloseBtnEventListener(HideRankList);
            rankList.SetActive(false);

            //��ʼ��
            InitGame();

        }



        // ��ʼ����������
        public void InitGame()
        {
            //������λ
            chairIdList[0].GetComponent<GameChairIdUI>().rectTransform.anchoredPosition = new Vector2(200, -350);
            chairIdList[1].GetComponent<GameChairIdUI>().rectTransform.anchoredPosition = new Vector2(450, -350);


        }

        //���ݵ�����ͼ��
        public void AddHeadPic(int chairId,int uid)
        {
            Vector2 anchoredPosition = chairIdList[chairId].GetComponent<GameChairIdUI>().rectTransform.anchoredPosition;
            Vector2 tempVector2 = new Vector2(-25, 122);

            avatarList[chairId].GetComponent<Role>().rectTransform.anchoredPosition = anchoredPosition + tempVector2;
            avatarList[chairId].GetComponent<Role>().ShowUserAvater(uid);
            avatarList[chairId].GetComponent<Role>().SetSeatNumText(chairId);
        }

        //��ʾ�ȴ����ֱ�־
        public void AddWaitEnemy()
        {
            waitEnemy.SetActive(true);         
        }

        //�Ƴ��ȴ����ֱ�־
        public void DelWaitEnemy()
        {
            waitEnemy.SetActive(false);
        }

        //��ʾ���а�
        public void ShowRankList()
        {
            rankList.SetActive(true);

            
        }

        //�������а�
        public void HideRankList(GameObject arg0)
        {
            //rankList.SetActive(false);
            Destroy(rankList);
            //�Ƴ������а� ���´�������
            rankList = new GameObject("rankList");
            rankList.transform.SetParent(gameObject.transform);
            rankList.AddComponent<RankListUI>();
            rankList.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30);
            rankList.GetComponent<RankListUI>().AddOnCloseBtnEventListener(HideRankList);
            rankList.SetActive(false);
        }

        //�������а�����
        public void RankListData(string str)
        {
            //��ȡ���а�UI��� �������и������ݵķ���
            RankListUI view = rankList.GetComponent<RankListUI>();
            int num = view.items.Count-20;
            string[] vecList = str.Split(';');//���ܵ������÷ֱ���; _�ָ�õ�ÿ������
            for (int i = 0; i < vecList.Length; i++)
            {
                if (vecList[i] != "" && vecList[i] != null)
                {
                    string[] arr = vecList[i].Split('_');
                    view.RankData(i+num, arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);

                }
            }
        }

        //���ص�½����
        public void ReturnLogin()
        {
            //�����gamePane�ϵ����ж���
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

        //�����Ϣ����
        public void SetUserInfo(string name,int id)
        {
            selfName = name;
            selfUid = id;
        }

        //������Ϸ���� ����
        public void EnterSoloGameScene()
        {
            //��Ϸ��ʼ
            gameStageFlag = 1;

            //��Ϸ����
            gmBg = new GameObject("gmBg");
            gmBg.transform.SetParent(gameObject.transform);
            //����ͼƬUI������
            CreateResUtils createResUtils2 = gmBg.AddComponent<CreateResUtils>();
            createResUtils2.SetSprite("gameclient/sprites/Sprites/gamegaijin");
            createResUtils2.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            createResUtils2.rectTransform.sizeDelta = new Vector2(1372f, 770f);

            //�˳���ť
            soloQuitBtn = new GameObject("sQBtn");
            soloQuitBtn.transform.SetParent(gameObject.transform);
            CommonButton commonBtn_sqbtn = soloQuitBtn.AddComponent<CommonButton>();
            commonBtn_sqbtn.SetButtonLabel("����");
            commonBtn_sqbtn.rectTransform.anchoredPosition = new Vector2(1200, -40);
            AddSoloQuitBtnEventListener(ClickSoloQuitBtn);

            //��Ϸҡ������ ���ҡ�˿��ƽű�
            joystick = new GameObject("joystick");
            joystick.transform.SetParent(gameObject.transform);
            joystick.AddComponent<RectTransform>();
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 270);
            joystick.GetComponent<RectTransform>().anchoredPosition = new Vector3(-485, -245, 0 );
            joystick.AddComponent<JoystickUI>();


            //��Ϸʳ������ 
            //if (!UnityEditorInternal.InternalEditorUtility.tags.Equals("Food"))//�ȶ����ʳ���ǩ
            //{
            //    UnityEditorInternal.InternalEditorUtility.AddTag("Food");
            //}
            //������ӱ�ǩӰ����
            foods = new GameObject("Foods");
            foods.transform.SetParent(gameObject.transform);
            for(int i = 0; i < foodNum; i++) {
                GameObject food = new GameObject("food"+i);
                food.transform.SetParent(foods.transform);
                //�����ͨʳ��UI�ű�
                food.AddComponent<FoodUI>();

            }

            //���ǽ��
            //if (!UnityEditorInternal.InternalEditorUtility.tags.Equals("Obs"))//�ȶ�����ϰ���ǩ
            //{
            //    UnityEditorInternal.InternalEditorUtility.AddTag("Obs");
            //}
            //������ӱ�ǩӰ����
            walls = new GameObject("Wall");
            walls.transform.SetParent(gameObject.transform);
            for (int i = 0; i < 4; i++)
            {
                //ǽ��ѭ������ �����ײ��
                GameObject wall = new GameObject("wall" + i);
                wall.transform.SetParent(walls.transform);
                wall.AddComponent<RectTransform>();
                wall.AddComponent<BoxCollider2D>();
                wall.GetComponent<BoxCollider2D>().isTrigger = true;
                switch (i)
                {
                    case 0:
                        //��ǽ�� λ�ô�С
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(787, -32);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(1712, 60);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(1712, 60);
                        break;
                    case 1:
                        //��ǽ�� λ�ô�С
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(787, 799);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(1712, 60);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(1712, 60);
                        break;
                    case 2:
                        //��ǽ�� λ�ô�С
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 407);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 843);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(200, 843);
                        break;
                    case 3:
                        //��ǽ�� λ�ô�С
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(1460, 407);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 843);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(200, 843);
                        break;
                    default:
                        break;
                }
                //�ϰ���ǩ
                wall.tag = "Obs";
            }

            //���������
            MakeSnake();

        }

        //���ɵ��������
        public void MakeSnake()
        {
            //���������ͷ�ĳ�ʼ�� ���ø����� �����UI��� (�����ж� ��Ϸ��ʼ�Ҷ�����ȫ�ͷ�)
            if (IsSnakeBodyDestory() && gameStageFlag == 1) { snakeBody = new GameObject("bodys"); }
            snakeBody.transform.SetParent(gameObject.transform);
            if (IsSnakeSelfDestory() && gameStageFlag == 1) { snakeSelf = new GameObject("snakehead"); }
            snakeSelf.transform.SetParent(gameObject.transform);
            snakeSelf.AddComponent<SnakeUI>();

            //����С�γ�ʼ�� ����������
            snakeBodys = new List<GameObject>();
            for (int i = 0; i < initlength; i++)
            {
                segment = new GameObject("segment" + i);
                segment.transform.SetParent(snakeBody.transform);
                segment.AddComponent<SnakeBodyUI>();
                snakeBodys.Add(segment);
            }
            //��������˶��ű� ���������˶�
            snakeBody.AddComponent<SnakeBodyMove>();
            snakeBody.GetComponent<SnakeBodyMove>().head = snakeSelf;
            snakeBody.GetComponent<SnakeBodyMove>().tSnakeBodys = snakeBodys;
        }

        //����������ͷ�
        public void SnakeDeath()
        {
            //����ÿ��������ʳ�� ��������
            for (int i = 0; i < snakeBodys.Count; i++)
            {
                //����������ʳ��
                GameObject food = new GameObject("deadfood"+i);
                food.transform.SetParent(foods.transform);
                food.AddComponent<DeadBodyUI>();
                food.GetComponent<RectTransform>().anchoredPosition = snakeBodys[i].GetComponent<RectTransform>().anchoredPosition;
                //��������
                Destroy(snakeBodys[i]);
            }

            //�ͷ�������ͷ���� (���ڶ�����������)
            snakeBodys.Clear();
            if (!IsSnakeBodyDestory()) { Destroy(snakeBody); }
            if (!IsSnakeSelfDestory()) { Destroy(snakeSelf); }
        }


        //�˳�����ģʽ��ť����¼�
        private void ClickSoloQuitBtn(GameObject arg0)
        {
            gameStageFlag = 0;
            //��Ϸ����

            //�ͷ���
            SnakeDeath();
            //�ͷŸ������
            Destroy(walls);
            Destroy(foods);
            Destroy(joystick);
            Destroy(gmBg);
            Destroy(soloQuitBtn);

        }

        //����ģʽ
        public void OnlineMode()
        {
            //��Ϸ��ʼ
            gameStageFlag = 1;



            //��Ϸ����
            gmBg = new GameObject("gmBg");
            gmBg.transform.SetParent(gameObject.transform);

            CreateResUtils createResUtils2 = gmBg.AddComponent<CreateResUtils>();
            createResUtils2.SetSprite("gameclient/sprites/Sprites/gamegaijin");
            createResUtils2.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            createResUtils2.rectTransform.sizeDelta = new Vector2(1372f, 770f);

            //��Ϸҡ������
            joystick = new GameObject("joystick");

            joystick.transform.SetParent(gameObject.transform);
            joystick.AddComponent<RectTransform>();
            joystick.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 270);
            joystick.GetComponent<RectTransform>().anchoredPosition = new Vector3(-485, -245, 0);
            joystick.AddComponent<JoystickUI>();

            //���ǽ��
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
                        //��ǽ��
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(787, -32);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(1712, 60);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(1712, 60);
                        break;
                    case 1:
                        //��ǽ��
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(787, 799);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(1712, 60);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(1712, 60);
                        break;
                    case 2:
                        //��ǽ��
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 407);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 843);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(200, 843);
                        break;
                    case 3:
                        //��ǽ��
                        wall.GetComponent<RectTransform>().anchoredPosition = new Vector2(1460, 407);
                        wall.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 843);
                        wall.GetComponent<BoxCollider2D>().size = new Vector2(200, 843);
                        break;
                    default:
                        break;
                }

                wall.tag = "Obs";
            }

            //��Ϸ����ʱUI
            countDown = new GameObject("countdown");
            countDown.transform.SetParent(gameObject.transform);
            CommonText commonText1 = countDown.AddComponent<CommonText>();

            commonText1.rectTransform.anchoredPosition = new Vector2(-90, 350);
            commonText1.GetComponent<CommonText>().TextComponent.fontSize = 30;
            commonText1.GetComponent<CommonText>().TextComponent.text = "����������У�"+restTime+" ��";
            commonText1.GetComponent<CommonText>().TextComponent.color = Color.black;

            //�÷����UI
            score = new GameObject("score");
            score.transform.SetParent(gameObject.transform);
            CommonText commonText2 = score.AddComponent<CommonText>();

            commonText2.rectTransform.anchoredPosition = new Vector2(330, 350);
            commonText2.GetComponent<CommonText>().TextComponent.fontSize = 20;
            commonText2.GetComponent<CommonText>().TextComponent.text = "(You)��ң�" + selfName + " ���ȣ�"+selfLength +
                "  �÷֣�" +selfScore + "\n         ��ң�" + enemyName + " ���ȣ�" + enemyLength +"  �÷֣�" + enemyScore;
            commonText2.GetComponent<CommonText>().TextComponent.color = Color.black;

            //���������
            MakeOnlineSnake();

            //�����߳�ʼ��
            snakeBodysOther = new List<GameObject>();//����������
            snakeOther = new GameObject("enemy");//�������������
            snakeOther.transform.SetParent(gameObject.transform);
            for(int i = 0; i < 100; i++)
            {
                //��ʼ��ÿ����������С�� �����丸����snakeOther ��Ӷ�������UI�ű� ��ӱ�ǩ �������������
                GameObject snakebodye = new GameObject("enemybody" + i);
                snakebodye.transform.SetParent(snakeOther.transform);
                snakebodye.AddComponent<SnakeBodyEUI>();
                snakebodye.tag = "Obs";
                snakeBodysOther.Add(snakebodye);
            }
            //������ͷ��ʼ�� ���ø����� ��Ӷ�����ͷ�Ŀ��ƽű�������λ�ô�������Ĺ̶��˶����䣩
            snakeOtherHead = new GameObject("enemyhead");
            snakeOtherHead.transform.SetParent(gameObject.transform);
            snakeOtherHead.AddComponent<SnakeHeadEUI>();

            //Ϊ���������������˶��ű������ƶ��������˶���
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

            //����ҳ���ʼ�� ��ӽ���UI�ű�
            jiesuan = new GameObject("js");
            jiesuan.transform.SetParent(gameObject.transform);
            
            jiesuan.AddComponent<JieSuanDiaUI>();
            jiesuan.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-78, -24);
            jiesuan.GetComponent<JieSuanDiaUI>().AddOnCloseBtnEventListener(CloseJieSuan);
            jiesuan.SetActive(false);
        }

        //���·������
        public void RenewScoreUI(int x1,int x2,int x3,int x4)// ����/�з� ����/����  ���ݸ���
        {
            //�������� �������� �з����� �з�����
            selfLength += x1;
            selfScore += x2;
            enemyLength += x3;
            enemyScore += x4;
            //С����Сֵʱ��Ϊ��Сֵ
            if (selfLength < initlength) { selfLength = initlength; }
            if (selfScore < 0) { selfScore = 0; }
            if (enemyLength < initlength) { enemyLength = initlength; }
            if (enemyScore < 0) { enemyScore = 0; }
            if (score != null)
            {
                //�Ʒְ��ı���ʾ
                score.GetComponent<CommonText>().TextComponent.text = "(You)��ң�" + selfName + " ���ȣ�" + selfLength +
        "  �÷֣�" + selfScore + "\n        ��ң�" + enemyName + " ���ȣ�" + enemyLength + "  �÷֣�" + enemyScore;
            }
        }

        //����������
        public void MakeOnlineSnake()
        {
            //���ڶ��󲻴�������´����¶��� ������ͷ
            if (IsSnakeBodyDestory()) { snakeBody = new GameObject("bodys"); } 
            snakeBody.transform.SetParent(gameObject.transform);
            if (IsSnakeSelfDestory()) { snakeSelf = new GameObject("snakehead"); }
            snakeSelf.transform.SetParent(gameObject.transform);
            

            snakeBodys = new List<GameObject>();
            for (int i = 0; i < initlength; i++)
            {
                //��ʼ����С�μ���������
                segment = new GameObject("segment" + i);
                segment.transform.SetParent(snakeBody.transform);
                segment.AddComponent<SnakeBodyUI>();
                snakeBodys.Add(segment);
            }
            //Ϊ��ͷ���������UI���
            if (snakeSelf.GetComponent<SnakeOnlieUI>())
            {

            }
            else
            {
                snakeSelf.AddComponent<SnakeOnlieUI>();
            }
            //Ϊ������������˶����
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

        //��������������
        public void OnlineSnakeDeath()
        {
            //ÿ������λ������ʳ��
            for (int i = 0; i < snakeBodys.Count; i++)
            {
                GameObject food = new GameObject("deadfood" + i);
                food.transform.SetParent(foods.transform);
                food.AddComponent<DeadBodyUI>();
                food.GetComponent<RectTransform>().anchoredPosition = snakeBodys[i].GetComponent<RectTransform>().anchoredPosition;

                
            }
            
            if (snakeBody != null) { Destroy(snakeBody.AddComponent<SnakeBodyMove>()); }

            //�ͷ�������ͷ���� (���ڶ�����������)
            snakeBodys.Clear();
            if (!IsSnakeBodyDestory()) { Destroy(snakeBody); }
            if (!IsSnakeSelfDestory()) { Destroy(snakeSelf); }
            
        }

        //�����������ƶ�
        public void MakeEnemy(float angle,float x,float y,int length)
        {
            if(gameStageFlag == 0) { return; }
            //��������Ϸ�׶β��к�������

            //�Է���ͷ���� �ı�Ϊ������Ϣ��λ�� ����Ϊ������Ϣ�ķ���
            if (snakeOtherHead != null) {
                snakeOtherHead.transform.position = new Vector2(x, y);
                snakeOtherHead.transform.eulerAngles = new Vector3(0, 0, angle - 90);
            }
            //�з������1���Զ�����Ϊ�з���ͷλ�ã����Ȳ�Ϊ0ʱ��
            snakeBodysOther[0].transform.position = new Vector2(x, y);
            if (length == 0) { snakeBodysOther[0].transform.position = new Vector2(-10, -10); }
                
            //for(int i = length-1; i >0; i--)
            //{
            //    snakeBodysOther[i].transform.position = snakeBodysOther[i - 1].transform.position;
            //}
            //�з���ͷͨ���ű����ƽ�����Ϣ����֮����˶����� ������ǶȺʹ��״̬
            snakeOtherHead.GetComponent<SnakeHeadEUI>().angle = angle;
            snakeOtherHead.GetComponent<SnakeHeadEUI>().live = 1;
            //�з�������³���״̬
            snakeOther.GetComponent<SnakeBodyMoveE>().length = length;
        }



        //��������ʳ��
        public void OnlineMakeFood(string position)
        {
            //if (!UnityEditorInternal.InternalEditorUtility.tags.Equals("Food"))
            //{
            //    UnityEditorInternal.InternalEditorUtility.AddTag("Food");
            //}
            //ͨ��split��������õ�ÿ��ʳ��λ����ɫ��Ϣ
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
                    //ʳ���������ʳ��UI�ű� �����䷽������λ����ɫ����
                    food.AddComponent<FoodOnlineUI>();
                    food.GetComponent<FoodOnlineUI>().Set(x, y, z);
                    
                }
            }

        }

        //��������ʳ��λ��
        public void FoodTransform(string name,string x,string y)
        {
            //ʳ��λ�õ�����������ҳ� ����ֳ�
            if (playerEatFlag == 0) { 
                RenewScoreUI(0, 0, 1, 10);
            }
            else
            {
                //ʳ��λ�õ���������ҳ� ����־����
                playerEatFlag = 0;
            }
            //����ʳ�ﱻ��ͳһ����λ��
            GameObject gameObject1 = GameObject.Find(name);
            gameObject1.GetComponent<RectTransform>().anchoredPosition = new Vector2(int.Parse(x),int.Parse(y));
        }

        //������������������
        public void SnakeOtherDeath()
        {
            //�Ʒְ����
            RenewScoreUI(0, 0, -99, -100);
            enemyDeadCount++;
            //����Ϊ��ʱ������λ����Ƶ�����ٴ���Ӱ�����ܣ�
            if (snakeOtherHead != null)
            {
                snakeOtherHead.transform.position = new Vector2(-10, -10);
                snakeOtherHead.GetComponent<SnakeHeadEUI>().live = 0;//��Ƕ�����״̬
            }

            if (snakeBodysOther[0] != null)
            {
                snakeBodysOther[0].transform.position = new Vector3(-10, -10);
            }

            for(int i = 1; i < snakeBodysOther.Count; i++)//�ӳ�ʼ����������Ŀ���ޱ���
            {
                if (snakeBodysOther[i].transform.position == new Vector3(-10, -10))
                {
                    break;//�������ڹ涨λ�� �������
                }
                else
                {
                    //����δ�ڹ涨λ�� ����������ʳ�� ������λ
                    GameObject food = new GameObject("deadfood" + i);
                    food.transform.SetParent(foods.transform);
                    food.AddComponent<DeadBodyUI>();
                    food.GetComponent<RectTransform>().anchoredPosition = snakeBodysOther[i].GetComponent<RectTransform>().anchoredPosition;
                    snakeBodysOther[i].transform.position = new Vector3(-10, -10);
                }
            }
            //SnakeBodysOther
        }

        public bool IsSnakeBodyDestory()//�ж��Ƿ�����
        {
            return snakeBody==null;
        }

        public bool IsSnakeSelfDestory()//�ж��Ƿ�����
        {
            return snakeSelf == null;
        }

        //����ʱ����
        public void CountDownChange(string time)
        {
            restTime = time;
            countDown.GetComponent<CommonText>().TextComponent.text = "����ʱ��" + restTime + " ��";
        }

        //���ö�����Ϣ
        public void SetEnemyInfo(string uname,string uid)
        {
            enemyName = uname;
            enemyUid = int.Parse(uid);
        }

        //����ҳ��ر�
        public void CloseJieSuan(GameObject arg0)
        {
            Destroy(jiesuan);
        }

        //��Ϸ���� ��������
        public async void GameOver()
        {
            //��Ϸ������־��0
            gameStageFlag = 0;

            //�����Լ�����������
            OnlineSnakeDeath();
            for(int i = 0; i < 60; i++)
            {
                Destroy(snakeBodysOther[i]);
            }
            snakeBodysOther.Clear();
            Destroy(snakeOther);
            Destroy(snakeOtherHead);

            //���ٸ������
            Destroy(countDown);
            Destroy(score);
            Destroy(walls);
            Destroy(foods);
            Destroy(joystick);
            Destroy(gmBg);

            //ͷ��ȥ��
            for (int i = 0; i < 2; i++)
            {

                avatarList[i].GetComponent<Role>().rectTransform.anchoredPosition = new Vector2(-500,-500);
            }

            // ������Ϸ����
            this.GetComponent<GameLogic>().SendJiesuanMsg(selfScore);

            //��ʾ���㻭�� ������������
            if (selfScore > enemyScore)//������ʤ
            {
                jiesuan.GetComponent<JieSuanDiaUI>().SetJieSuanText(selfName,selfScore,deadCount);
                jiesuan.GetComponent<JieSuanDiaUI>().PlayVicVocal();
            }
            else if(selfScore == enemyScore)//ƽ��
            {
                jiesuan.GetComponent<JieSuanDiaUI>().SetTieJieSuanText();
                jiesuan.GetComponent<JieSuanDiaUI>().PlayNoVicVocal();
            }
            else//�Է���ʤ
            {
                jiesuan.GetComponent<JieSuanDiaUI>().SetJieSuanText(enemyName, enemyScore, enemyDeadCount);
                jiesuan.GetComponent<JieSuanDiaUI>().PlayNoVicVocal();
            }
            jiesuan.SetActive(true);//������ʾ
            RenewScoreUI(-99, -9999, -99, -9999);//�Ʒְ�����

            await Task.Delay(10000);//10���δ�ر��Զ��رս���ҳ
            if (jiesuan != null&& gameStageFlag == 0)//�����ڽ��㲻Ϊ�����³���Ϸδ��ʼʱ�������ٽ������
            {
                Destroy(jiesuan);
            }
        }

        //�����λ����¼�����
        public void AddClickChairEventListener(UnityAction<GameObject> eventHandler)
        {
            for (int i = 0; i < 2; i++)
            {
                chairIdList[i].GetComponent<GameChairIdUI>().AddClickAvatarPicEventListener(eventHandler);
            }
        }

        //��ӷ��ذ�ť����¼�����
        public void AddReturnBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            returnBtn.GetComponent<ThreeStatusCommonBtn>().AddBtnEventListener(eventHandler);
        }

        //������а�ť����¼�����
        public void AddClickRankListBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            rankListBtn.GetComponent<ThreeStatusCommonBtn>().AddBtnEventListener(eventHandler);
        }

        //��ӵ���ģʽ��ť����¼�����
        public void AddSoloPlayBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            soloPlayBtn.GetComponent<CommonButton>().AddBtnEventListener(eventHandler);
        }

        //����˳�����ģʽ��ť����¼�����
        public void AddSoloQuitBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            soloQuitBtn.GetComponent<CommonButton>().AddBtnEventListener(eventHandler);
        }



    }

}