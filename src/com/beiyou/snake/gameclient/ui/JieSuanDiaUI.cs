using com.beiyou.snake.common.res;
using com.beiyou.snake.common.ui;
using com.beiyou.snake.common.utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using Spine.Unity;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace com.beiyou.snake.gameclient.ui
{

    //结算页面
    public class JieSuanDiaUI : MonoBehaviour
    {
        private RectTransform m_rectTransform;
        //结算页面的大背景 文本背景 标题 结算文本 返回按钮
        private GameObject bgPic = null;
        private GameObject diaBg = null;
        private GameObject titleText = null;
        private GameObject jiesuanText = null;
        private GameObject returnBtn = null;

        //结算声音和动画
        private GameObject anime = null;
        private AudioSource audioSource = null;

        private void Awake()
        {
            this.gameObject.AddComponent<AudioSource>();
            audioSource = this.gameObject.GetComponent<AudioSource>();

            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // 设置中心点为左上角
            m_rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            //各个组件的初始化和设置
            bgPic = new GameObject("bgPic");
            TransparentShelterUI transparentShelterUI = bgPic.AddComponent<TransparentShelterUI>();
            bgPic.transform.SetParent(gameObject.transform);
            transparentShelterUI.InitUI(common.utils.Appconst.STAGE_W, common.utils.Appconst.STAGE_H);
            transparentShelterUI.SetAlpha(0.5f);

            diaBg = new GameObject("diaBgObject");
            diaBg.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils = diaBg.AddComponent<CreateResUtils>();
            createResUtils.SetWhiteSprite();
            createResUtils.rectTransform.sizeDelta = new Vector2(600, 600);
            createResUtils.rectTransform.anchoredPosition = new Vector2(480, -80);

            titleText = new("titleTextObject");
            titleText.transform.SetParent(gameObject.transform);
            CommonText uiText = titleText.AddComponent<CommonText>();   //给GameObject对象添加组件
            uiText.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(250, -20);
            uiText.rectTransform.sizeDelta = new Vector2(400, 30);
            uiText.TextComponent.color = Color.black;
            uiText.TextComponent.fontSize = 30;
            uiText.TextComponent.text = "游戏结束";

            jiesuanText = new("jiesuanTextObject");
            jiesuanText.transform.SetParent(gameObject.transform);
            CommonText commonText_jiesuanText = jiesuanText.AddComponent<CommonText>();   //给GameObject对象添加组件
            commonText_jiesuanText.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(20, -100);
            commonText_jiesuanText.rectTransform.sizeDelta = new Vector2(500, 500);
            commonText_jiesuanText.TextComponent.color = Color.black;
            commonText_jiesuanText.TextComponent.fontSize = 30;
            commonText_jiesuanText.TextComponent.text = "";
            commonText_jiesuanText.TextComponent.verticalOverflow = VerticalWrapMode.Overflow ;


            returnBtn = new GameObject("returnBtn");
            returnBtn.transform.SetParent(gameObject.transform);
            CommonButton commonBtn = returnBtn.AddComponent<CommonButton>();
            commonBtn.SetButtonLabel("返回");
            commonBtn.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(10, -10);

            anime = new GameObject("anime");
            anime.transform.SetParent(gameObject.transform);
            anime.AddComponent<SkeletonGraphic>();
            anime.GetComponent<SkeletonGraphic>().skeletonDataAsset = Resources.Load<SkeletonDataAsset>("Gauge/Gauge_SkeletonData");


        }
        public RectTransform rectTransform
        {
            get
            {
                return m_rectTransform;
            }
        }

        //某一方胜利的文本设置
        public void SetJieSuanText(string username, int score, int deathcount)
        {
            string tempStr = "获胜玩家：" + username + "\n分数：" + score+"\n本局死亡次数："+deathcount;

            this.jiesuanText.GetComponent<CommonText>().TextComponent.text = tempStr;

        }

        //平局的文本设置
        public void SetTieJieSuanText()
        {
            string tempStr = "旗鼓相当的对手！";

            this.jiesuanText.GetComponent<CommonText>().TextComponent.text = tempStr;
        }

        //播放胜利音乐和动画
        public void PlayVicVocal()
        {
            audioSource.clip = Resources.Load<AudioClip>("sounds/victory");
            audioSource.Play();
            anime.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "Fill", true);
            anime.GetComponent<SkeletonGraphic>().timeScale = 2f;
        }

        //播放未胜利音乐和动画
        public void PlayNoVicVocal()
        {
            audioSource.clip = Resources.Load<AudioClip>("sounds/lose");
            audioSource.Play();

            anime.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "Fill", true);
            anime.GetComponent<SkeletonGraphic>().timeScale = 0.5f;
        }

        //添加关闭按钮事件监听
        public void AddOnCloseBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            returnBtn.GetComponent<CommonButton>().AddBtnEventListener(eventHandler);
        }


    }
}
