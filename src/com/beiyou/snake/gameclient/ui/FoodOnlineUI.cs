using UnityEngine;
using UnityEngine.UI;
using com.beiyou.snake.gameclient.engine;

namespace com.beiyou.snake.gameclient.ui
{
    //联网食物UI 
    public class FoodOnlineUI : MonoBehaviour
    {
        public GameObject canvas;

        //食物初始化
        private void Awake()
        {
            canvas = GameObject.Find("Canvas");

            //食物获取图像组件并随机添加图片
            this.gameObject.AddComponent<Image>();
            

            //食物大小位置设置
            //this.gameObject.AddComponent<RectTransform>();
            
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

            //食物添加碰撞箱并设置
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            this.gameObject.GetComponent<CircleCollider2D>().radius = 8;

            //食物标签
            this.gameObject.tag = "Food";

        }

        //联网情况下食物同步设置 xy为服务器传递的随机位置 z为服务器传递的随机食物颜色
        public void Set(int x,int y,int z)
        {
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/node" + z);
            this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }


        //碰撞检测
        private void OnTriggerEnter2D(Collider2D other)
        {
            //对象是玩家
            if (other.tag == "Player")
            {
                //发送食物被吃的信息
                canvas.GetComponent<GameLogic>().SendFoodEatMsg(this.name);
                //表明该次为本玩家吃
                GamePane.playerEatFlag = 1;

            }

        }

    }
}
