
using com.beiyou.snake.gameclient.engine;
using com.beiyou.snake.gameclient.socketdata;
using com.beiyou.snake.index.ui;

using UnityEngine;



namespace com.beiyou.snake.index.engine
{
    public class indexEngine : MonoBehaviour
    {
        //目录面板对象 用户名 密码 游戏客户端
        private IndexPane indexPane;
        private string username;
        private string password;

        private GameLogic gameClient;


        //初始化目录面板 添加开始按钮点击事件监听
        private void Awake()
        {
            indexPane = gameObject.AddComponent<IndexPane>();
            indexPane.AddClickStartGameBtnEventListener(ClickStartGameBtnEventHandler);

        }

        //开始按钮点击事件
        private void ClickStartGameBtnEventHandler(GameObject arg0)
        {
            username = indexPane.GetUserNameText();
            password = indexPane.GetPasswdText();

            StartGameClient(username, password);
        }


        //实例化游戏客户端
        private void StartGameClient(string userName, string passWord)
        {
            gameClient = gameObject.AddComponent<GameLogic>();
            gameClient.StartGameConnect(userName, passWord);
        }


    }
}
