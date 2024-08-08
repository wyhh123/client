using com.beiyou.snake.gameclient.engine;

using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.UI;



namespace com.beiyou.snake.gameclient.ui
{
    //联机模式蛇头UI
    public class SnakeOnlieUI : MonoBehaviour
    {
        //大部分内容与蛇UI相同
        public GameObject touching;
        public GameObject SNAKE;
        public GameObject canvas;
        private float angle = 0;
        private float radian = 0;
        public float minV = 2.3f;
        public float touchingV = 0.012f;
        private GameObject headpic;
        public int snakeIndex;
        private int length = 0;
        private int initlength = 0;
        //初始化
        private void Awake()
        {

            CreateSnake();

        }

        //随机位置生成
        private Vector2 Randxy()
        {
            float x = Random.Range(100, 1000);
            float y = Random.Range(100, 600);
            return new Vector2(x, y);
        }

        private void CreateSnake()
        {
            SNAKE = GameObject.Find("bodys");
            touching = GameObject.Find("joystickTouch");
            canvas = GameObject.Find("Canvas");
            //获取其他脚本内的对象 
            this.initlength = canvas.GetComponent<GamePane>().initlength;
            this.length = initlength;
            snakeIndex = canvas.GetComponent<GamePane>().selfUid;
            this.transform.position = Randxy();
            this.transform.localScale = new Vector3(1, 1, 0);


            headpic = new GameObject("headpic");
            headpic.transform.SetParent(this.transform);
            headpic.AddComponent<Image>();
            headpic.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/skin_" + (snakeIndex % 18 + 1) + "_head");
            headpic.transform.localScale = new Vector3(0.3f, 0.3f, 0);
            headpic.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            this.gameObject.AddComponent<RectTransform>();
            this.gameObject.AddComponent<Rigidbody2D>();
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().radius = 13;
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

            this.gameObject.tag = "Player";
        }

        private void FixedUpdate()
        {
            Move();
            canvas.GetComponent<GameLogic>().SendSnakeMsg(angle,
                this.transform.position.x,
                this.transform.position.y,
                this.length);
            //GameLogic发送蛇信息的方法 传入角度和位置和蛇长度 每帧发送
        }



        private void Move()
        {
            //玩家蛇的移动同单人模式
            Vector2 vec = touching.GetComponent<RectTransform>().anchoredPosition;
            Vector2 minVec = vec.normalized;
            this.transform.position = new Vector3(
                this.transform.position.x + vec.x * touchingV + minVec.x * minV,
                this.transform.position.y + vec.y * touchingV + minVec.y * minV,
                0
                );

            if (vec.x == 0)//蛇头方向计算
            {

            }
            else
            {
                radian = Mathf.Atan((vec.y / vec.x));//蛇头方向计算 弧度
                angle = radian / Mathf.PI * 180;//弧度转角度

                if (vec.x < 0)
                {
                    angle = angle + 180;//方向修正
                }

            }

            headpic.transform.eulerAngles = new Vector3(
                0,
                0,
                angle - 90
                );
        }

        private void Grow()
        {
            this.length++;
            //玩家蛇成长 长度增加1 分数加10
            canvas.GetComponent<GamePane>().RenewScoreUI(1,10,0,0);
            //增加的蛇小段 加入蛇身列中
            GameObject segment = new GameObject("segment");
            segment.transform.SetParent(SNAKE.transform);
            segment.AddComponent<SnakeBodyUI>();
            canvas.GetComponent<GamePane>().snakeBodys.Add(segment);
        }

        private async void Die()
        {
            this.length = 0;
            
            //玩家蛇死亡 长度归位最初长度 分数减100 死亡计数增加 发送死亡信息
            canvas.GetComponent<GamePane>().RenewScoreUI(-999, -100, 0, 0);
            canvas.GetComponent<GamePane>().deadCount++;
            canvas.GetComponent<GameLogic>().SendSnakeDeath();
            canvas.GetComponent<GamePane>().OnlineSnakeDeath();//销毁联网蛇
            
            //推迟3s复活
            await Task.Delay(3000);


            //生成联网蛇 仅在游戏进行中
            if (GamePane.gameStageFlag == 1)
            {
                canvas.GetComponent<GamePane>().MakeOnlineSnake();
            }
            this.length = this.initlength;
        }

        //碰撞检测
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Food")//碰到食物
            {

                //蛇身生长
                Grow();

            }
            else if (other.tag == "Obs")//碰到墙壁或对手
            {

                //重置游戏
                Die();
            }
            else if(other.tag == "DeadBody")//蛇尸体特殊食物
            {
                //仅成长
                Grow();
            }
        }


    }

}
