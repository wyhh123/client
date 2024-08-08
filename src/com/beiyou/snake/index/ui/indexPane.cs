using com.beiyou.snake.common.res;
using com.beiyou.snake.common.utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.index.ui
{
    //游戏目录面板UI
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
            // 获取图片的RectTransform组件
            RectTransform rectTransform = bgpic.GetComponent<RectTransform>();
            // 设置中心点为左上角
            rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            // 设置图片的宽度和高度，例如，将宽度设置为1538，高度设置为750
            rectTransform.sizeDelta = new Vector2(1538f, 750f);
            rectTransform.anchoredPosition = new Vector2(0, 0);

            loginTextBGPic = new GameObject("loginTextBGPic");
            loginTextBGPic.transform.SetParent(bgpic.transform);
            CreateResUtils createResUtils = loginTextBGPic.AddComponent<CreateResUtils>();
            createResUtils.SetSprite("index/Image/index_LoginInputTextBGPic");
            createResUtils.rectTransform.anchoredPosition = new Vector2(500, -200);

            //用户名
            usernametTextObj = new("usernametTextObj");
            usernametTextObj.transform.SetParent(loginTextBGPic.transform);
            CommonInputText usernameText = usernametTextObj.AddComponent<CommonInputText>();   //给GameObject对象添加组件
            usernameText.rectTransform.anchoredPosition = new Vector2(180, -10);
            usernameText.rectTransform.sizeDelta = new Vector2(354, 60);
            usernameText.TextComponent.fontSize = 30;
            usernameText.Placeholder.fontSize = 30;
            usernameText.Placeholder.text = "请输入用户名";

            //密码
            passwordTextObj = new("passwordTextObj");
            passwordTextObj.transform.SetParent(loginTextBGPic.transform);
            CommonInputText passwordText = passwordTextObj.AddComponent<CommonInputText>();   //给GameObject对象添加组件
            passwordText.rectTransform.anchoredPosition = new Vector2(180, -90);
            passwordText.rectTransform.sizeDelta = new Vector2(354, 60);
            passwordText.TextComponent.fontSize = 30;
            passwordText.Placeholder.fontSize = 30;
            passwordText.Placeholder.text = "请输入密码";

            //开始游戏按钮
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

        //获取用户名
        public string GetUserNameText()
        {
            return usernametTextObj.GetComponent<CommonInputText>().TextComponent.text;
        }

        //获取密码
        public string GetPasswdText()
        {
            return passwordTextObj.GetComponent<CommonInputText>().TextComponent.text;
        }

        //添加开始按钮点击事件监听
        public void AddClickStartGameBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            startGameBtn.GetComponent<ThreeStatusCommonBtn>().AddBtnEventListener(eventHandler);
        }


    }

}
