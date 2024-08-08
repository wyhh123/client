using com.beiyou.snake.common.res;
using com.beiyou.snake.common.utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //������λͷ������
    public class Role : MonoBehaviour
    {
        private RectTransform m_rectTransform;

        private GameObject headImageUIObject = null;// ͷ��

        private GameObject seatNumTextObject;// ��λ��


        private void Awake()
        {
            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // �������ĵ�Ϊ���Ͻ�
            m_rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            headImageUIObject = new GameObject("headImageUIObject");
            headImageUIObject.transform.SetParent(gameObject.transform);
            headImageUIObject.AddComponent<HeadImageUI>();


            seatNumTextObject = new("seatNumTextObject");
            seatNumTextObject.transform.SetParent(gameObject.transform);
            CommonText seatNumText = seatNumTextObject.AddComponent<CommonText>();   //��GameObject����������
            seatNumText.rectTransform.anchoredPosition = new Vector2(0, 20);
            seatNumText.rectTransform.sizeDelta = new Vector2(100, 20);
            seatNumText.TextComponent.fontSize = 20;
            seatNumText.TextComponent.color = Color.green;
            seatNumText.TextComponent.text = "";
        }

        public RectTransform rectTransform
        {
            get
            {
                return m_rectTransform;
            }
        }


        public void RemoveUserUserAvater()
        {
            Component componentToRemove = headImageUIObject.GetComponent<HeadImageUI>();
            if (componentToRemove != null)
            {

                Destroy(componentToRemove);


            }
        }

        public void ShowUserAvater(int userId)
        {
            this.RemoveUserUserAvater();
            int i = userId % 18 + 1;
            //string url = Appconst.SERVER_URL + "/web/resources/images/ffb" + userId + ".jpg";
            string url = "E:\\Sprites\\skin_"+i+"_head.png";

            HeadImageUI headImageUI = headImageUIObject.GetComponent<HeadImageUI>();
            if (headImageUI == null)
            {
                headImageUI = headImageUIObject.AddComponent<HeadImageUI>();
            }
            headImageUI.ShowPlayerHeadImage(url);

        }

        public void SetSeatNumText(int chairId)
        {
            if (chairId != -1)
            {
                seatNumTextObject.GetComponent<CommonText>().TextComponent.text = "" + chairId;
            }
            else
            {
                seatNumTextObject.GetComponent<CommonText>().TextComponent.text = "";
            }
        }
        public string GetSeatNumText()
        {
            return seatNumTextObject.GetComponent<CommonText>().TextComponent.text;
        }


        //ͷ�����¼�
        public void AddClickAvatarPicEventListener(UnityAction<GameObject> eventHandler)
        {
            headImageUIObject.GetComponent<HeadImageUI>().AddBtnEventListener(eventHandler);
        }


    }


}
