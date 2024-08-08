
using com.beiyou.snake.gameclient.engine;
using com.beiyou.snake.gameclient.socketdata;
using com.beiyou.snake.index.ui;

using UnityEngine;



namespace com.beiyou.snake.index.engine
{
    public class indexEngine : MonoBehaviour
    {
        //Ŀ¼������ �û��� ���� ��Ϸ�ͻ���
        private IndexPane indexPane;
        private string username;
        private string password;

        private GameLogic gameClient;


        //��ʼ��Ŀ¼��� ��ӿ�ʼ��ť����¼�����
        private void Awake()
        {
            indexPane = gameObject.AddComponent<IndexPane>();
            indexPane.AddClickStartGameBtnEventListener(ClickStartGameBtnEventHandler);

        }

        //��ʼ��ť����¼�
        private void ClickStartGameBtnEventHandler(GameObject arg0)
        {
            username = indexPane.GetUserNameText();
            password = indexPane.GetPasswdText();

            StartGameClient(username, password);
        }


        //ʵ������Ϸ�ͻ���
        private void StartGameClient(string userName, string passWord)
        {
            gameClient = gameObject.AddComponent<GameLogic>();
            gameClient.StartGameConnect(userName, passWord);
        }


    }
}
