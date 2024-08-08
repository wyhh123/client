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

    //����ҳ��
    public class JieSuanDiaUI : MonoBehaviour
    {
        private RectTransform m_rectTransform;
        //����ҳ��Ĵ󱳾� �ı����� ���� �����ı� ���ذ�ť
        private GameObject bgPic = null;
        private GameObject diaBg = null;
        private GameObject titleText = null;
        private GameObject jiesuanText = null;
        private GameObject returnBtn = null;

        //���������Ͷ���
        private GameObject anime = null;
        private AudioSource audioSource = null;

        private void Awake()
        {
            this.gameObject.AddComponent<AudioSource>();
            audioSource = this.gameObject.GetComponent<AudioSource>();

            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // �������ĵ�Ϊ���Ͻ�
            m_rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            //��������ĳ�ʼ��������
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
            CommonText uiText = titleText.AddComponent<CommonText>();   //��GameObject����������
            uiText.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(250, -20);
            uiText.rectTransform.sizeDelta = new Vector2(400, 30);
            uiText.TextComponent.color = Color.black;
            uiText.TextComponent.fontSize = 30;
            uiText.TextComponent.text = "��Ϸ����";

            jiesuanText = new("jiesuanTextObject");
            jiesuanText.transform.SetParent(gameObject.transform);
            CommonText commonText_jiesuanText = jiesuanText.AddComponent<CommonText>();   //��GameObject����������
            commonText_jiesuanText.rectTransform.anchoredPosition = createResUtils.rectTransform.anchoredPosition + new Vector2(20, -100);
            commonText_jiesuanText.rectTransform.sizeDelta = new Vector2(500, 500);
            commonText_jiesuanText.TextComponent.color = Color.black;
            commonText_jiesuanText.TextComponent.fontSize = 30;
            commonText_jiesuanText.TextComponent.text = "";
            commonText_jiesuanText.TextComponent.verticalOverflow = VerticalWrapMode.Overflow ;


            returnBtn = new GameObject("returnBtn");
            returnBtn.transform.SetParent(gameObject.transform);
            CommonButton commonBtn = returnBtn.AddComponent<CommonButton>();
            commonBtn.SetButtonLabel("����");
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

        //ĳһ��ʤ�����ı�����
        public void SetJieSuanText(string username, int score, int deathcount)
        {
            string tempStr = "��ʤ��ң�" + username + "\n������" + score+"\n��������������"+deathcount;

            this.jiesuanText.GetComponent<CommonText>().TextComponent.text = tempStr;

        }

        //ƽ�ֵ��ı�����
        public void SetTieJieSuanText()
        {
            string tempStr = "����൱�Ķ��֣�";

            this.jiesuanText.GetComponent<CommonText>().TextComponent.text = tempStr;
        }

        //����ʤ�����ֺͶ���
        public void PlayVicVocal()
        {
            audioSource.clip = Resources.Load<AudioClip>("sounds/victory");
            audioSource.Play();
            anime.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "Fill", true);
            anime.GetComponent<SkeletonGraphic>().timeScale = 2f;
        }

        //����δʤ�����ֺͶ���
        public void PlayNoVicVocal()
        {
            audioSource.clip = Resources.Load<AudioClip>("sounds/lose");
            audioSource.Play();

            anime.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "Fill", true);
            anime.GetComponent<SkeletonGraphic>().timeScale = 0.5f;
        }

        //��ӹرհ�ť�¼�����
        public void AddOnCloseBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            returnBtn.GetComponent<CommonButton>().AddBtnEventListener(eventHandler);
        }


    }
}
