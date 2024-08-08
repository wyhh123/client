
using com.beiyou.snake.gameclient.engine;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



namespace com.beiyou.snake.gameclient.ui
{
    //蛇头UI及事件
    public class SnakeUI : MonoBehaviour
    {
        //获取joystick中的摇杆对象 snakebodymove中的蛇对象 gamepane的canvas对象
        public GameObject touching;
        public GameObject SNAKE;
        public GameObject canvas;

        //计算蛇头偏转的角度和弧度
        private float angle = 0;
        private float radian = 0;

        //不操作时蛇头的最小速度和摇杆提供的附加速度
        public float minV = 2.3f;
        public float touchingV = 0.012f;
        
        //蛇头图像对象图片
        private GameObject headPic;

        //蛇皮肤
        public int snakeIndex;
        private void Awake()
        {
            //初始化蛇
            CreateSnake();
            
        }

        // 随机蛇出生位置
        private Vector2 Randxy()
        {
            float x = Random.Range(100, 1000);
            float y = Random.Range(100, 600);
            return new Vector2(x,y);
        }

        private void CreateSnake()
        {
            //分别获取对应对象
            SNAKE = GameObject.Find("bodys");//蛇身对象控制蛇身移动
            touching = GameObject.Find("joystickTouch");//摇杆对象获取摇杆操作
            canvas = GameObject.Find("Canvas");//UI画布调用GamePane的方法

            snakeIndex = canvas.GetComponent<GamePane>().selfUid;
            //设置初始位置大小
            this.transform.position = Randxy();
                //new Vector3(100, 100, 0);
            this.transform.localScale = new Vector3(1, 1, 0);

            //图片初始化 设置父对象 添加图片组件并设置图片 设置位置大小
            headPic = new GameObject("headpic");
            headPic.transform.SetParent(this.transform);
            headPic.AddComponent<Image>();
            headPic.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/skin_"+(snakeIndex%18+1)+"_head");
            headPic.transform.localScale = new Vector3(0.3f, 0.3f, 0);
            headPic.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            //蛇头添加刚体碰撞箱等组件并进行基础设置
            this.gameObject.AddComponent<RectTransform>();
            this.gameObject.AddComponent<Rigidbody2D>();
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().radius = 13;
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

            //只有蛇头添加玩家标签
            this.gameObject.tag = "Player";
        }

        private void FixedUpdate()
        {
            //蛇每帧移动
            Move();

        }

        //蛇运动
        private void Move()
        {
            //移动
            //获取摇杆偏移量向量
            Vector2 vec = touching.GetComponent<RectTransform>().anchoredPosition;
            //摇杆偏移向量标准化得到最小速度
            Vector2 minVec = vec.normalized;
            //蛇头根据速度每帧位置移动
            this.transform.position = new Vector3(
                //蛇头本身某方向位置 + 摇杆某方向偏移大小*摇杆控制速度的参数 + 最小速度产生的某方向移动*控制最小速度的参数
                this.transform.position.x + vec.x * touchingV + minVec.x * minV,
                this.transform.position.y + vec.y * touchingV + minVec.y * minV,
                0
                );


            //转头
            if (vec.x == 0)//蛇头方向计算
            {
                //x为0维持原状
            }
            else
            {
                //摇杆偏移方向
                radian = Mathf.Atan((vec.y / vec.x));//蛇头方向计算 弧度
                angle = radian / Mathf.PI * 180;//弧度转角度

                if (vec.x < 0)//摇杆偏移在x轴下方时 数学关系修正
                {
                    angle = angle + 180;//方向修正 
                }

            }

            //蛇头子对象图片的旋转 直接转至蛇头朝向
            headPic.transform.eulerAngles = new Vector3(
                //2d游戏其他方向夹角为0
                0,
                0,
                angle - 90
                //计算得角度为和正x轴夹角 eulerAngles z值为和正y轴夹角
                );
        }

        //蛇生长
        private void Grow()
        {
           //创建蛇身小段初始化 设置父对象 添加蛇身UI 加入gamePane的蛇身列中
            GameObject segment = new GameObject("segment");
            segment.transform.SetParent(SNAKE.transform);
            segment.AddComponent<SnakeBodyUI>();
            canvas.GetComponent<GamePane>().snakeBodys.Add(segment);
        }

        // 死与新生
        private async void Die()
        {
            //GamePane中释放蛇全部组件的方法
            canvas.GetComponent<GamePane>().SnakeDeath();

            // 1.5秒推迟
            await Task.Delay(1500);

            //GamePane中创建蛇全部组件的方法
            //仅在游戏进行中重新生成
            if(GamePane.gameStageFlag == 1) 
            {
                canvas.GetComponent<GamePane>().MakeSnake();
            }
            
        }


            
        
        //碰撞检测
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Food")//碰到食物
            {

                //蛇身生长
                Grow();


            }
            else if (other.tag == "Obs")//碰到墙壁
            {

                //死与新生
                Die();
            }
            
        }


    }

}
