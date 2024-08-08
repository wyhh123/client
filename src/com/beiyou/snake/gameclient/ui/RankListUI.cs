using com.beiyou.snake.common.res;
using com.beiyou.snake.common.ui;
using com.beiyou.snake.common.utils;
using com.beiyou.snake.gameclient.engine;
using com.beiyou.snake.gameclient.ui;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



namespace com.beiyou.snake.gameclient.ui
{
    //���а�UI
    public class RankListUI : MonoBehaviour
    {
        public GameObject canvas;
        
        private RectTransform m_rectTransform;
        //���а񱳾� ���� ��ͷ ���ذ�ť
        private GameObject bgPic = null;
        private GameObject diaBg = null;
        private GameObject titleText = null;
        private GameObject tableTitleText = null;
        private GameObject returnBtn = null;

        //scrollview������ͼ���
        public GameObject rankList = null;
        private GameObject viewport = null;
        private GameObject content = null;

        //Ԥ��rank����;�������
        private GameObject rankItem = null;
        private GameObject rank = null;
        private GameObject avatar = null;
        private GameObject uid = null;
        private GameObject uname = null;
        private GameObject score = null;
        private GameObject gamecount = null;
        private GameObject wingamecount = null;

        
        public ScrollRect scrollRect; // ����Scroll View���
        //public GameObject theContent; // ��������
        //public GameObject itemPrefab; // �����������Ԥ����
        
        //�������rankItem����
        public List<GameObject> items = null;

        //������ȡ��������
        private GameObject overLimit=null;

        private void Awake()
        {
            canvas = GameObject.Find("Canvas");//UI��������GamePane�ķ���

            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // �������ĵ�Ϊ���Ͻ�
            m_rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            //������̨����ui����
            bgPic = new GameObject("bgPic");
            TransparentShelterUI transparentShelterUI = bgPic.AddComponent<TransparentShelterUI>();
            bgPic.transform.SetParent(gameObject.transform);
            transparentShelterUI.InitUI(common.utils.Appconst.STAGE_W, common.utils.Appconst.STAGE_H);
            transparentShelterUI.SetAlpha(0.5f);
            //���а�ҳ�汳��ui
            diaBg = new GameObject("diaBgObject");
            diaBg.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils = diaBg.AddComponent<CreateResUtils>();
            createResUtils.SetWhiteSprite();
            createResUtils.rectTransform.sizeDelta = new Vector2(1400, 700);
            createResUtils.rectTransform.anchoredPosition = new Vector2(0, 0);
            
            //�����ı�����
            titleText = new("titleTextObject");
            titleText.transform.SetParent(gameObject.transform);
            CommonText uiText = titleText.AddComponent<CommonText>();   //��GameObject����������
            uiText.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(650, -20);
            uiText.rectTransform.sizeDelta = new Vector2(400, 30);
            uiText.TextComponent.color = Color.black;
            uiText.TextComponent.fontSize = 30;
            uiText.TextComponent.text = "RANK";

            //��ͷ�ı�����
            tableTitleText = new("tabletitle");
            tableTitleText.transform.SetParent(gameObject.transform);
            CommonText commonText = tableTitleText.AddComponent<CommonText>();
            commonText.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(155, -60);
            commonText.rectTransform.sizeDelta = new Vector2(400, 30);
            commonText.TextComponent.color = Color.black;
            commonText.TextComponent.fontSize = 30;
            commonText.TextComponent.text = "����              ͷ��              id             �û���         �ܾ���          ��ʤ��        �ܻ���";

            //���ذ�ť����
            returnBtn = new GameObject("returnBtn");
            returnBtn.transform.SetParent(gameObject.transform);
            CommonButton commonBtn = returnBtn.AddComponent<CommonButton>();
            commonBtn.SetButtonLabel("����");
            commonBtn.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(10, -10);


            //���а������������ ��ʼ�� ���ø����� ���ô�Сλ�� 
            rankList = new GameObject("rankList");
            rankList.transform.SetParent(gameObject.transform);
            rankList.AddComponent<RectTransform>();
            rankList.GetComponent<RectTransform>().anchoredPosition = new Vector2(650, -335);
            rankList.GetComponent<RectTransform>().sizeDelta = new Vector2(1300, 550);
            //rankList.AddComponent<Image>();
            //rankList.GetComponent<Image>().color = Color.gray;
            //���а����scrollrect��� ��ֹˮƽ����
            ScrollRect scrollRect = rankList.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            //scrollview ��ͼ���� ���ô�С ����ͼƬ�������
            viewport = new GameObject("viewport");
            viewport.transform.SetParent(rankList.transform);
            viewport.AddComponent<RectTransform>();
            viewport.GetComponent<RectTransform>().sizeDelta = new Vector2(1300, 550);
            viewport.AddComponent<Image>();
            viewport.AddComponent<Mask>();
            //scrollview ���ݲ��� ����λ�ÿ�ȣ��߶ȸ������ݶ��ٶ�̬���ã� ���gridlayoutgroup���������content�϶�������
            content = new GameObject("content");
            content.transform.SetParent(viewport.transform);
            content.AddComponent<RectTransform>();
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(1300,100);
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -800);
            content.AddComponent<GridLayoutGroup>();
            content.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1300, 100);
            content.GetComponent<GridLayoutGroup>().padding.top = 10;
            content.AddComponent<ContentSizeFitter>();
            //����ͼ��������ӵ�ranklist��scrollrect�����
            rankList.GetComponent<ScrollRect>().viewport = viewport.GetComponent<RectTransform>();
            rankList.GetComponent<ScrollRect>().content = content.GetComponent<RectTransform>();
            //ÿ�����ݵ�Ԥ��������� ��ʼ�� ���ø����� �������������Ӷ���ֲ� ���ͼƬ���ô�С��ɫ
            rankItem = new GameObject("line");
            rankItem.transform.SetParent(content.transform);
            rankItem.AddComponent<GridLayoutGroup>();
            rankItem.GetComponent<GridLayoutGroup>().cellSize = new Vector2(165, 100);
            rankItem.GetComponent<GridLayoutGroup>().padding.left = 90;
            rankItem.AddComponent<ContentSizeFitter>();
            //rankItem.AddComponent<Image>();      
            //rankItem.AddComponent<RectTransform>();
            rankItem.GetComponent<RectTransform>().sizeDelta = new Vector2(1300, 100);
            //rankItem.GetComponent<Image>().color = Color.grey;

            //���rankscrollview�ű� ��ȡ����
            //rankList.AddComponent<RankScrollView>();
            //rankList.GetComponent<RankScrollView>().content = content;
            //rankList.GetComponent<RankScrollView>().itemPrefab = rankItem;
            //scrollRect = rankList.GetComponent<ScrollRect>();

            //�������ݶ����ʼ������
            rank = new GameObject("rank");
            rank.transform.SetParent(rankItem.transform);
            //CommonText rankText = rank.AddComponent<CommonText>();
            //rankText.TextComponent.color = Color.black;
            //rankText.rectTransform.anchoredPosition = rank.GetComponent<RectTransform>().anchoredPosition;
            avatar = new GameObject("avatar");
            avatar.transform.SetParent(rankItem.transform);
            //avatar.AddComponent<HeadImageUI>();
            uid = new GameObject("uid");
            uid.transform.SetParent(rankItem.transform);
            //CommonText uidText = uid.AddComponent<CommonText>();
            //uidText.rectTransform.anchoredPosition = uid.GetComponent<RectTransform>().anchoredPosition;
            //uidText.TextComponent.color = Color.black;
            uname = new GameObject("uname");
            uname.transform.SetParent(rankItem.transform);
            //CommonText unameText = uname.AddComponent<CommonText>();
            //unameText.TextComponent.color = Color.black;
            //unameText.rectTransform.anchoredPosition = uname.GetComponent<RectTransform>().anchoredPosition;
            gamecount = new GameObject("gamecount");
            gamecount.transform.SetParent(rankItem.transform);
            //CommonText gamecountText = gamecount.AddComponent<CommonText>();
            //gamecountText.TextComponent.color = Color.black;
            //gamecountText.rectTransform.anchoredPosition = gamecount.GetComponent<RectTransform>().anchoredPosition;
            wingamecount = new GameObject("wingamecount");
            wingamecount.transform.SetParent(rankItem.transform);
            //CommonText wingamecountText = wingamecount.AddComponent<CommonText>();
            //wingamecountText.TextComponent.color = Color.black;
            //wingamecountText.rectTransform.anchoredPosition = wingamecount.GetComponent<RectTransform>().anchoredPosition;
            score = new GameObject("score");
            score.transform.SetParent(rankItem.transform);
            //CommonText scoreText = score.AddComponent<CommonText>();
            //scoreText.TextComponent.color = Color.black;
            //scoreText.rectTransform.anchoredPosition = score.GetComponent<RectTransform>().anchoredPosition;

            //��������������ʾ����
            overLimit = new GameObject("ol");
            overLimit.transform.SetParent(gameObject.transform);
            overLimit.AddComponent<Image>();
            overLimit.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/limit");
            overLimit.GetComponent<Image>().type = Image.Type.Tiled;
            overLimit.SetActive(false);
        }

        private void Start()
        {
            // ����һ���б����洢������
            items = new List<GameObject>();
            
            //��¡20��Ԥ��item
            for (int i = 0; i < 20; i++)
            {
                // ��¡Ԥ���岢��ӵ�����������
                GameObject item = Instantiate(rankItem, content.transform);
                item.AddComponent<Image>();
                //ǰ������Ӳ�ͬ����
                switch (i){
                    case 0:
                        item.GetComponent<Image>().color = new Color(255,215,0);
                        break;
                    case 1:
                        item.GetComponent<Image>().color = new Color(192, 192, 192);
                        break;
                    case 2:
                        item.GetComponent<Image>().color = Color.cyan;
                        break;
                    default:
                        item.GetComponent<Image>().color = Color.gray;
                        break;
                }
                
                items.Add(item);
            }
            rankItem.SetActive(false);

            // ����Scroll View�Ĺ������ݴ�С
            UpdateContentSize();
        }

        private void UpdateContentSize()
        {
            // �������������������������ͼ�����ݴ�С
            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 
                (items.Count) * rankItem.GetComponent<RectTransform>().sizeDelta.y+50);
        }

        private void Update()
        {
            
            scrollRect = rankList.GetComponent<ScrollRect>();
            if (scrollRect.verticalNormalizedPosition <= 0)// �����ײ�
            {
                if (items.Count < 100)//δ��ȡֵ����
                {
                    rankItem.SetActive(true);//Ԥ���弤��
                    for (int i = 0; i < 20; i++)
                    {
                        
                        // ��¡Ԥ���岢��ӵ�����������
                        GameObject item = Instantiate(rankItem, content.transform);
                        item.AddComponent<Image>();
                        item.GetComponent<Image>().color = Color.gray;
                        items.Add(item);
                    }
                    rankItem.SetActive(false);//Ԥ����ر�
                    UpdateContentSize();
                    scrollRect.verticalNormalizedPosition += 0.12f;
                    canvas.GetComponent<GameLogic>().RankListToBottom(items.Count);//�����µ�������������
                    
                }
                else
                {
                    Debug.Log("����ȡ�����ݳ�����Χ");
                    overLimit.SetActive(true);
                    overLimit.transform.SetParent(content.transform);
                }
            }

        }

        //���а����ݵ�����
        public void RankData(int index, string rank, string uid, string uname, string gamecount, string wingamecount, string score)
        {
            //commontext�ű��������ı�
            CommonText rankText = items[index].transform.GetChild(0).gameObject.AddComponent<CommonText>();
            items[index].transform.GetChild(0).transform.GetChild(0).localPosition = new Vector3(45, -15, 0);
            rankText.TextComponent.color = Color.black;
            rankText.TextComponent.text = rank;
            CommonText uidText = items[index].transform.GetChild(2).gameObject.AddComponent<CommonText>();
            items[index].transform.GetChild(2).transform.GetChild(0).localPosition = new Vector3(45, -15, 0);
            uidText.TextComponent.color = Color.black;
            uidText.TextComponent.text = uid;
            CommonText unameText = items[index].transform.GetChild(3).gameObject.AddComponent<CommonText>();
            items[index].transform.GetChild(3).transform.GetChild(0).localPosition = new Vector3(45, -15, 0);
            unameText.TextComponent.color = Color.black;
            unameText.TextComponent.text = uname;
            CommonText gamecountText = items[index].transform.GetChild(4).gameObject.AddComponent<CommonText>();
            items[index].transform.GetChild(4).transform.GetChild(0).localPosition = new Vector3(45, -15, 0);
            gamecountText.TextComponent.color = Color.black;
            gamecountText.TextComponent.text = gamecount;
            CommonText wingamecountText = items[index].transform.GetChild(5).gameObject.AddComponent<CommonText>();
            items[index].transform.GetChild(5).transform.GetChild(0).localPosition = new Vector3(45, -15, 0);
            wingamecountText.TextComponent.color = Color.black;
            wingamecountText.TextComponent.text = wingamecount;
            CommonText scoreText = items[index].transform.GetChild(6).gameObject.AddComponent<CommonText>();
            items[index].transform.GetChild(6).transform.GetChild(0).localPosition = new Vector3(30, -15, 0);
            scoreText.TextComponent.color = Color.black;
            scoreText.TextComponent.text = score;

            HeadImageUI headImage = items[index].transform.GetChild(1).gameObject.AddComponent<HeadImageUI>();
            headImage.ShowUserAvater(int.Parse(uid));
        }



        public RectTransform rectTransform
        {
            get
            {
                
                return m_rectTransform;
            }
        }




        //��ӹرհ�ť�¼�����
        public void AddOnCloseBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            returnBtn.GetComponent<CommonButton>().AddBtnEventListener(eventHandler);
        }


    }
}
