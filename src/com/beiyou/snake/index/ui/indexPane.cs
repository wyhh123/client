using com.beiyou.snake.common.res;
using com.beiyou.snake.common.utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.index.ui
{
    //��ϷĿ¼���UI
    public class IndexPane : MonoBehaviour
    {
        private Image bgpic;
        private GameObject loginTextBGPic;
        private GameObject startGameBtn;


        private GameObject usernametTextObj;
        private GameObject passwordTextObj;

        private void Awake()
        {
            bgpic = new GameObject().AddComponent<Image>();
            bgpic.transform.SetParent(gameObject.transform);
            bgpic.sprite = Resources.Load<Sprite>("");
            bgpic.SetNativeSize();
            bgpic.type = Image.Type.Sliced;
            // ��ȡͼƬ��RectTransform���
            RectTransform rectTransform = bgpic.GetComponent<RectTransform>();
            // �������ĵ�Ϊ���Ͻ�
            rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            // ����ͼƬ�Ŀ�Ⱥ͸߶ȣ����磬���������Ϊ1538���߶�����Ϊ750
            rectTransform.sizeDelta = new Vector2(1538f, 750f);
            rectTransform.anchoredPosition = new Vector2(0, 0);

            loginTextBGPic = new GameObject("loginTextBGPic");
            loginTextBGPic.transform.SetParent(bgpic.transform);
            CreateResUtils createResUtils = loginTextBGPic.AddComponent<CreateResUtils>();
            createResUtils.SetSprite("index/Image/index_LoginInputTextBGPic");
            createResUtils.rectTransform.anchoredPosition = new Vector2(500, -200);

            //�û���
            usernametTextObj = new("usernametTextObj");
            usernametTextObj.transform.SetParent(loginTextBGPic.transform);
            CommonInputText usernameText = usernametTextObj.AddComponent<CommonInputText>();   //��GameObject����������
            usernameText.rectTransform.anchoredPosition = new Vector2(180, -10);
            usernameText.rectTransform.sizeDelta = new Vector2(354, 60);
            usernameText.TextComponent.fontSize = 30;
            usernameText.Placeholder.fontSize = 30;
            usernameText.Placeholder.text = "�������û���";

            //����
            passwordTextObj = new("passwordTextObj");
            passwordTextObj.transform.SetParent(loginTextBGPic.transform);
            CommonInputText passwordText = passwordTextObj.AddComponent<CommonInputText>();   //��GameObject����������
            passwordText.rectTransform.anchoredPosition = new Vector2(180, -90);
            passwordText.rectTransform.sizeDelta = new Vector2(354, 60);
            passwordText.TextComponent.fontSize = 30;
            passwordText.Placeholder.fontSize = 30;
            passwordText.Placeholder.text = "����������";

            //��ʼ��Ϸ��ť
            startGameBtn = new GameObject("startGameBtn");
            startGameBtn.transform.SetParent(bgpic.transform);
            startGameBtn.AddComponent<ThreeStatusCommonBtn>();
            ThreeStatusCommonBtn threeStatusCommonBtn = startGameBtn.GetComponent<ThreeStatusCommonBtn>();
            if (threeStatusCommonBtn != null)
            {
                string normalStr = "index/Image/index_StartGameBtnPic_EN";
                string pressedStr = "index/Image/index_StartGameBtnClickPic_EN";
                string disableStr = "index/Image/index_StartGameBtnClickPic_EN";
                threeStatusCommonBtn.setThreeSprite(normalStr, pressedStr, disableStr);
                threeStatusCommonBtn.rectTransform.anchoredPosition = new Vector2(570, -400);
            }


        }

        //��ȡ�û���
        public string GetUserNameText()
        {
            return usernametTextObj.GetComponent<CommonInputText>().TextComponent.text;
        }

        //��ȡ����
        public string GetPasswdText()
        {
            return passwordTextObj.GetComponent<CommonInputText>().TextComponent.text;
        }

        //��ӿ�ʼ��ť����¼�����
        public void AddClickStartGameBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            startGameBtn.GetComponent<ThreeStatusCommonBtn>().AddBtnEventListener(eventHandler);
        }


    }

}
