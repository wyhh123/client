
using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //蛇尸体的特殊食物 被吃后不会重新生成
    public class DeadBodyUI : MonoBehaviour
    {
        public GameObject canvas;
        
        //食物初始化
        private void Awake()
        {
            // 获取Canvas
            canvas = GameObject.Find("Canvas");

            //食物获取图像组件并随机添加图片
            this.gameObject.AddComponent<Image>();
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/node" + randi());

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



        //随机食物node序号 只决定食物颜色
        public int randi()
        {
            int i = Random.Range(1, 13);
            return i;
        }

        //碰撞检测
        private void OnTriggerEnter2D(Collider2D other)
        {
            //对象是玩家
            if (other.tag == "Player")
            {
                Destroy(this.gameObject);
                 
            }
            //对手蛇
            else if (other.tag =="Obs")
            {
                //对手蛇在己方记分UI的更新
                canvas.GetComponent<GamePane>().RenewScoreUI(0, 0, 1, 10);
                Destroy(this.gameObject);
            }
        }

    }
}
