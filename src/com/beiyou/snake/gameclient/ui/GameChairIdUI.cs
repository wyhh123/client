using com.beiyou.snake.common.res;
using com.beiyou.snake.common.utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //��Ϸ������λ�ű�
    public class GameChairIdUI : MonoBehaviour
    {
        private RectTransform m_rectTransform;

        private GameObject chairIdBg = null;
        private GameObject decadeText = null;
        private GameObject unitText = null;
        private GameObject avatarBg = null;
        private GameObject nameTextObject = null;
        private int chairIdNum = -1;
        private Button button;
        //���� ��λ����ͼ ʮλ�����ı� ��λ�����ı� ͷ�񱳾� �����ı� ��ť
        private void Awake()
        {
            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // �������ĵ�Ϊ���Ͻ�
            m_rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);
            
            //��λ��������
            chairIdBg = new GameObject("chairIdBg");
            chairIdBg.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils = chairIdBg.AddComponent<CreateResUtils>();
            createResUtils.SetSprite("gameclient/gameclient_GameChairIdBlackBG");
            createResUtils.rectTransform.anchoredPosition = new Vector2(0, 0);
            //createResUtils.rectTransform.sizeDelta = new Vector2(100, 100);
      
            //ͷ�񱳾�����
            avatarBg = new GameObject("avatarBg");
            avatarBg.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils1 = avatarBg.AddComponent<CreateResUtils>();
            createResUtils1.SetSprite("gameclient/wolfvoice_SitDownBtn");
            createResUtils1.rectTransform.anchoredPosition = new Vector2(-25, 120);
            createResUtils1.rectTransform.sizeDelta = new Vector2(100, 100);

            //ʮλ�ı�ͼƬ
            decadeText = new GameObject("decadeText");
            decadeText.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils2 = decadeText.AddComponent<CreateResUtils>();
            createResUtils2.SetSprite("gameclient/gameclient_ChairIdTxt0");
            createResUtils2.rectTransform.anchoredPosition = new Vector2(15, -3);

            //��λ�ı�ͼƬ
            unitText = new GameObject("unitText");
            unitText.transform.SetParent(gameObject.transform);
            CreateResUtils createResUtils3 = unitText.AddComponent<CreateResUtils>();
            createResUtils3.SetSprite("gameclient/gameclient_ChairIdTxt0");
            createResUtils3.rectTransform.anchoredPosition = new Vector2(20, -3);

            //��ť���
            button = gameObject.AddComponent<Button>();
        }

        //recttransform���
        public RectTransform rectTransform
        {
            get
            {
                return m_rectTransform;
            }
        }

        //��λ����ʾ����
        public void RemoveSit()
        {
            chairIdBg.SetActive(false);
            unitText.SetActive(false);
            decadeText.SetActive(false);
        }
        public void ShowSit()
        {
            chairIdBg.SetActive(true);
            nameTextObject.GetComponent<CommonInputText>().TextComponent.text = "";
            SetChairIdNum(this.chairIdNum);
        }

        public void ShowName(string nameText)
        {
            //Debug.Log("showName:" + nameText);
            RemoveSit();
            this.nameTextObject.GetComponent<CommonText>().TextComponent.text = "�û�" + nameText;
        }

        //������λId
        public void SetChairIdNum(int chairIdNum)
        {
            this.chairIdNum = chairIdNum;
            int tempDecade = chairIdNum / 10;
            int tempUnit = chairIdNum % 10;
            //Debug.Log("tempDecade:" + tempDecade);
            //Debug.Log("tempUnit:" + tempUnit);
            if (tempDecade > 0)
            {
                decadeText.GetComponent<CreateResUtils>().SetSprite("gameclient/gameclient_ChairIdTxt" + tempDecade);
                decadeText.SetActive(true);
            }
            else
            {
                decadeText.SetActive(false);
            }
            if (tempUnit >= 0)
            {
                unitText.GetComponent<CreateResUtils>().SetSprite("gameclient/gameclient_ChairIdTxt" + tempUnit);
                unitText.SetActive(true);
            }
            else
            {
                unitText.SetActive(false);
            }
        }

        //��ȡ��λId
        public int GetChairIdNum()
        {
            return this.chairIdNum;
        }

        //���ͷ��ͼƬ����¼�����
        public void AddClickAvatarPicEventListener(UnityAction<GameObject> eventHandler)
        {
            button.onClick.AddListener(delegate
            {
                eventHandler(button.gameObject);
            });
        }


    }
}



