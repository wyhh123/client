
using UnityEngine;

using UnityEngine.UI;



namespace com.beiyou.snake.gameclient.ui
{
    //敌方蛇头UI
    public class SnakeHeadEUI : MonoBehaviour
    {

        //敌方蛇头图片对象
        private GameObject headpic;
        public int snakeIndex;

        public float minV = 2.3f;
        public float angle = 0;
        public float radian = 0;
        public int live = 1;

        private void Awake()
        {
            //敌方蛇头初始化位置大小
            this.transform.position = new Vector2(-10,-10);
            this.transform.localScale = new Vector3(1, 1, 0);

            snakeIndex = this.transform.GetComponentInParent<GamePane>().enemyUid;
            //敌方蛇头设置图片和图片的属性 
            headpic = new GameObject("headpic");
            headpic.transform.SetParent(this.transform);
            headpic.AddComponent<Image>();
            headpic.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/skin_"+(snakeIndex%18+1)+"_head");

            headpic.transform.localScale = new Vector3(0.3f, 0.3f, 0);
            headpic.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            //敌方蛇头刚体 碰撞箱 标签填加和属性设置
            this.gameObject.AddComponent<RectTransform>();
            this.gameObject.AddComponent<Rigidbody2D>();
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().radius = 13;
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

            this.gameObject.tag = "Obs";

        }

        //蛇头的补充移动（未更新新位置时每帧按照原方向移动）
        private void FixedUpdate()
        {
            if (live == 1)
            {
                Move();
            }
            
        }

        private void Move()
        {
            //敌方蛇头角度 计算得到运动方向
            radian = (angle-180) * Mathf.PI / 180;
            float x = Mathf.Sin(radian);
            float y = Mathf.Cos(radian);
            if (x * y > 0)
            {
                x *= -1;
                y *= -1;
            }
            this.transform.position = new Vector3(
                this.transform.position.x  + x * minV,
                this.transform.position.y  + y * minV,
                0
                );


        }

    }

}
