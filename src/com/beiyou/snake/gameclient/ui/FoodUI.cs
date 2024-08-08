using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //单人模式下食物UI
    public class FoodUI : MonoBehaviour
    {
        
        //单机模式下食物初始化
        private void Awake()
        {
            //食物获取图像组件并随机添加图片
            this.gameObject.AddComponent<Image>();
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/node"+randi());

            //食物大小位置设置
            this.gameObject.GetComponent<RectTransform>().anchoredPosition = randxy();
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

            //食物添加碰撞箱并设置
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            this.gameObject.GetComponent<CircleCollider2D>().radius = 8;
            
            //食物标签
            this.gameObject.tag = "Food";

        }

        //随机地图位置
        public Vector2 randxy()
        {
            float x = Random.Range(20, 1330);
            float y = Random.Range(30, 760);
            return new Vector2(x, y);
        }

        //随机食物node序号
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
                this.gameObject.GetComponent<RectTransform>().anchoredPosition = randxy();//随机改变食物位置
            }
        }

    }
}
