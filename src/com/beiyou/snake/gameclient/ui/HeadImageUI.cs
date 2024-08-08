using com.beiyou.snake.common.utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //生成头像的脚本
    public class HeadImageUI : MonoBehaviour
    {
        private RectTransform m_rectTransform;

        //private GameObject defaultHeadImagePic = null;
        private GameObject playerImage = null;

        private Button button;

        private void Awake()
        {
            if (gameObject.GetComponent<RectTransform>() == null)
            {
                m_rectTransform = gameObject.AddComponent<RectTransform>();
            }
            else { m_rectTransform = gameObject.GetComponent<RectTransform>(); }
            
            // 设置中心点为左上角
            m_rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);
            m_rectTransform.anchoredPosition = new Vector2(0, 0);

            //defaultHeadImagePic = new GameObject("defaultHeadImagePic");
            //defaultHeadImagePic.transform.SetParent(gameObject.transform);
            //CreateResUtils createResUtils = defaultHeadImagePic.AddComponent<CreateResUtils>();
            //createResUtils.SetSprite("gameclient/gameclient_TianHeiDefaultHeadImagePicBM");
            //createResUtils.rectTransform.sizeDelta = new Vector2(100, 100);
            //createResUtils.rectTransform.anchoredPosition = new Vector2(0, 0);

            //创建遮罩形状Image
            GameObject maskObject = new GameObject("Mask");
            maskObject.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils1 = maskObject.AddComponent<CreateResUtils>();
            createResUtils1.SetSprite("gameclient/gameclient_TianHeiDefaultHeadImagePicBM");
            createResUtils1.rectTransform.sizeDelta = new Vector2(100, 100);
            createResUtils1.rectTransform.anchoredPosition = new Vector2(0, 0);

            Image maskImage = maskObject.GetComponent<Image>();
            maskImage.transform.SetParent(gameObject.transform, false);
            maskImage.rectTransform.sizeDelta = new Vector2(100, 100); // 设置遮罩大小
            maskImage.color = Color.black; // 设置遮罩颜色，黑色表示完全遮罩
            maskImage.type = Image.Type.Filled;
            maskImage.fillMethod = Image.FillMethod.Radial360; // 设置为圆形遮罩

            playerImage = new GameObject("playerImage");
            playerImage.transform.SetParent(maskObject.transform);
            LoaderBigImageUI loadeBigImageUI = playerImage.AddComponent<LoaderBigImageUI>();
            loadeBigImageUI.rectTransform.anchoredPosition = new Vector2(0, 0);
            loadeBigImageUI.rectTransform.sizeDelta = new Vector2(100, 100);

            //// 将图片添加到遮罩中
            Mask maskComponent = maskObject.AddComponent<Mask>();
            maskComponent.showMaskGraphic = false; // 隐藏遮罩形状Image，只显示遮罩效果
            loadeBigImageUI.rectTransform.SetParent(createResUtils1.rectTransform, false);


            //button = gameObject.AddComponent<Button>();
        }

        public RectTransform rectTransform
        {
            get
            {
                return m_rectTransform;
            }
        }

        public void ShowUserAvater(int userId)
        {
            
            int i = userId % 18 + 1;
            string url = "E:\\Sprites\\skin_" + i + "_head.png";

            ShowPlayerHeadImage(url);

        }

        public void ShowPlayerHeadImage(string url)
        {
            playerImage.GetComponent<LoaderBigImageUI>().ShowBigImage(url);
        }

        public void AddBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            button.onClick.AddListener(delegate
            {
                eventHandler(button.gameObject);
            });
        }
    }
}
