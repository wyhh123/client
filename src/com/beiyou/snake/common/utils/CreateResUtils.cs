using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.common.utils
{
    //ͼƬUI
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
            // �������ĵ�Ϊ���Ͻ�
            m_rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);
            // ����ͼƬ�Ŀ�Ⱥ͸߶ȣ����磬���������Ϊ1538���߶�����Ϊ750
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
            // ����һ����ɫ��Sprite
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
