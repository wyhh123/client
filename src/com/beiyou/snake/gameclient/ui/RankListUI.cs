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
    //排行榜UI
    public class RankListUI : MonoBehaviour
    {
        public GameObject canvas;
        
        private RectTransform m_rectTransform;
        //排行榜背景 标题 表头 返回按钮
        private GameObject bgPic = null;
        private GameObject diaBg = null;
        private GameObject titleText = null;
        private GameObject tableTitleText = null;
        private GameObject returnBtn = null;

        //scrollview滚动视图组件
        public GameObject rankList = null;
        private GameObject viewport = null;
        private GameObject content = null;

        //预置rank组件和具体数据
        private GameObject rankItem = null;
        private GameObject rank = null;
        private GameObject avatar = null;
        private GameObject uid = null;
        private GameObject uname = null;
        private GameObject score = null;
        private GameObject gamecount = null;
        private GameObject wingamecount = null;

        
        public ScrollRect scrollRect; // 引用Scroll View组件
        //public GameObject theContent; // 内容容器
        //public GameObject itemPrefab; // 单个内容项的预制体
        
        //存放所有rankItem的列
        public List<GameObject> items = null;

        //超出获取数据上限
        private GameObject overLimit=null;

        private void Awake()
        {
            canvas = GameObject.Find("Canvas");//UI画布调用GamePane的方法

            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // 设置中心点为左上角
            m_rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            //覆盖舞台背景ui设置
            bgPic = new GameObject("bgPic");
            TransparentShelterUI transparentShelterUI = bgPic.AddComponent<TransparentShelterUI>();
            bgPic.transform.SetParent(gameObject.transform);
            transparentShelterUI.InitUI(common.utils.Appconst.STAGE_W, common.utils.Appconst.STAGE_H);
            transparentShelterUI.SetAlpha(0.5f);
            //排行榜页面背景ui
            diaBg = new GameObject("diaBgObject");
            diaBg.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils = diaBg.AddComponent<CreateResUtils>();
            createResUtils.SetWhiteSprite();
            createResUtils.rectTransform.sizeDelta = new Vector2(1400, 700);
            createResUtils.rectTransform.anchoredPosition = new Vector2(0, 0);
            
            //标题文本设置
            titleText = new("titleTextObject");
            titleText.transform.SetParent(gameObject.transform);
            CommonText uiText = titleText.AddComponent<CommonText>();   //给GameObject对象添加组件
            uiText.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(650, -20);
            uiText.rectTransform.sizeDelta = new Vector2(400, 30);
            uiText.TextComponent.color = Color.black;
            uiText.TextComponent.fontSize = 30;
            uiText.TextComponent.text = "RANK";

            //表头文本设置
            tableTitleText = new("tabletitle");
            tableTitleText.transform.SetParent(gameObject.transform);
            CommonText commonText = tableTitleText.AddComponent<CommonText>();
            commonText.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(155, -60);
            commonText.rectTransform.sizeDelta = new Vector2(400, 30);
            commonText.TextComponent.color = Color.black;
            commonText.TextComponent.fontSize = 30;
            commonText.TextComponent.text = "排名              头像              id             用户名         总局数          总胜场        总积分";

            //返回按钮设置
            returnBtn = new GameObject("returnBtn");
            returnBtn.transform.SetParent(gameObject.transform);
            CommonButton commonBtn = returnBtn.AddComponent<CommonButton>();
            commonBtn.SetButtonLabel("返回");
            commonBtn.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(10, -10);


            //排行榜具体内容设置 初始化 设置父对象 设置大小位置 
            rankList = new GameObject("rankList");
            rankList.transform.SetParent(gameObject.transform);
            rankList.AddComponent<RectTransform>();
            rankList.GetComponent<RectTransform>().anchoredPosition = new Vector2(650, -335);
            rankList.GetComponent<RectTransform>().sizeDelta = new Vector2(1300, 550);
            //rankList.AddComponent<Image>();
            //rankList.GetComponent<Image>().color = Color.gray;
            //排行榜添加scrollrect组件 禁止水平滑动
            ScrollRect scrollRect = rankList.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            //scrollview 视图部分 设置大小 增加图片遮罩组件
            viewport = new GameObject("viewport");
            viewport.transform.SetParent(rankList.transform);
            viewport.AddComponent<RectTransform>();
            viewport.GetComponent<RectTransform>().sizeDelta = new Vector2(1300, 550);
            viewport.AddComponent<Image>();
            viewport.AddComponent<Mask>();
            //scrollview 内容部分 设置位置宽度（高度根据内容多少动态设置） 添加gridlayoutgroup等组件控制content上对象排列
            content = new GameObject("content");
            content.transform.SetParent(viewport.transform);
            content.AddComponent<RectTransform>();
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(1300,100);
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -800);
            content.AddComponent<GridLayoutGroup>();
            content.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1300, 100);
            content.GetComponent<GridLayoutGroup>().padding.top = 10;
            content.AddComponent<ContentSizeFitter>();
            //将视图和内容添加到ranklist的scrollrect组件中
            rankList.GetComponent<ScrollRect>().viewport = viewport.GetComponent<RectTransform>();
            rankList.GetComponent<ScrollRect>().content = content.GetComponent<RectTransform>();
            //每行数据的预置物件设置 初始化 设置父对象 添加组件控制其子对象分布 添加图片设置大小颜色
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

            //添加rankscrollview脚本 （取消）
            //rankList.AddComponent<RankScrollView>();
            //rankList.GetComponent<RankScrollView>().content = content;
            //rankList.GetComponent<RankScrollView>().itemPrefab = rankItem;
            //scrollRect = rankList.GetComponent<ScrollRect>();

            //具体数据对象初始化设置
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

            //超出数据限制提示对象
            overLimit = new GameObject("ol");
            overLimit.transform.SetParent(gameObject.transform);
            overLimit.AddComponent<Image>();
            overLimit.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/limit");
            overLimit.GetComponent<Image>().type = Image.Type.Tiled;
            overLimit.SetActive(false);
        }

        private void Start()
        {
            // 创建一个列表来存储内容项
            items = new List<GameObject>();
            
            //克隆20个预置item
            for (int i = 0; i < 20; i++)
            {
                // 克隆预制体并添加到内容容器中
                GameObject item = Instantiate(rankItem, content.transform);
                item.AddComponent<Image>();
                //前三名添加不同背景
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

            // 调整Scroll View的滚动内容大小
            UpdateContentSize();
        }

        private void UpdateContentSize()
        {
            // 根据内容项的数量调整滚动视图的内容大小
            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 
                (items.Count) * rankItem.GetComponent<RectTransform>().sizeDelta.y+50);
        }

        private void Update()
        {
            
            scrollRect = rankList.GetComponent<ScrollRect>();
            if (scrollRect.verticalNormalizedPosition <= 0)// 滑到底部
            {
                if (items.Count < 100)//未到取值上限
                {
                    rankItem.SetActive(true);//预制体激活
                    for (int i = 0; i < 20; i++)
                    {
                        
                        // 克隆预制体并添加到内容容器中
                        GameObject item = Instantiate(rankItem, content.transform);
                        item.AddComponent<Image>();
                        item.GetComponent<Image>().color = Color.gray;
                        items.Add(item);
                    }
                    rankItem.SetActive(false);//预制体关闭
                    UpdateContentSize();
                    scrollRect.verticalNormalizedPosition += 0.12f;
                    canvas.GetComponent<GameLogic>().RankListToBottom(items.Count);//发送新的排名数据请求
                    
                }
                else
                {
                    Debug.Log("您获取的数据超出范围");
                    overLimit.SetActive(true);
                    overLimit.transform.SetParent(content.transform);
                }
            }

        }

        //排行榜数据的设置
        public void RankData(int index, string rank, string uid, string uname, string gamecount, string wingamecount, string score)
        {
            //commontext脚本设置其文本
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




        //添加关闭按钮事件监听
        public void AddOnCloseBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            returnBtn.GetComponent<CommonButton>().AddBtnEventListener(eventHandler);
        }


    }
}
