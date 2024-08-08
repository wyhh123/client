using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.common.utils
{
    //图片UI
    public class CreateResUtils : MonoBehaviour
    {
        private RectTransform m_rectTransform;

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
            // 设置图片的宽度和高度，例如，将宽度设置为1538，高度设置为750
            //m_rectTransform.sizeDelta = new Vector2(1538f, 750f);
            //m_rectTransform.anchoredPosition = new Vector2(450, -200);
        }


        public void SetSprite(string name)
        {
            Image imgPic = gameObject.GetComponent<Image>();
            if (imgPic == null)
            {
                imgPic = gameObject.AddComponent<Image>();
            }
            imgPic.sprite = Resources.Load<Sprite>(name);
            imgPic.SetNativeSize();
            imgPic.type = Image.Type.Sliced;
        }
        public void SetWhiteSprite()
        {
            Image imgPic = gameObject.GetComponent<Image>();
            if (imgPic == null)
            {
                imgPic = gameObject.AddComponent<Image>();
            }
            // 创建一个白色的Sprite
            Sprite whiteSprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
            imgPic.sprite = whiteSprite;
        }

        public void SetTransform(float temp_x, float temp_y)
        {
            m_rectTransform.anchoredPosition = new Vector2(temp_x, temp_y);
        }

        public RectTransform rectTransform
        {
            get
            {
                return m_rectTransform;
            }
        }

    }

}
