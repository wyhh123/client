using com.beiyou.snake.gameclient.engine;

using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.UI;



namespace com.beiyou.snake.gameclient.ui
{
    //����ģʽ��ͷUI
    public class SnakeOnlieUI : MonoBehaviour
    {
        //�󲿷���������UI��ͬ
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
        //��ʼ��
        private void Awake()
        {

            CreateSnake();

        }

        //���λ������
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
            //��ȡ�����ű��ڵĶ��� 
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
            //GameLogic��������Ϣ�ķ��� ����ǶȺ�λ�ú��߳��� ÿ֡����
        }



        private void Move()
        {
            //����ߵ��ƶ�ͬ����ģʽ
            Vector2 vec = touching.GetComponent<RectTransform>().anchoredPosition;
            Vector2 minVec = vec.normalized;
            this.transform.position = new Vector3(
                this.transform.position.x + vec.x * touchingV + minVec.x * minV,
                this.transform.position.y + vec.y * touchingV + minVec.y * minV,
                0
                );

            if (vec.x == 0)//��ͷ�������
            {

            }
            else
            {
                radian = Mathf.Atan((vec.y / vec.x));//��ͷ������� ����
                angle = radian / Mathf.PI * 180;//����ת�Ƕ�

                if (vec.x < 0)
                {
                    angle = angle + 180;//��������
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
            //����߳ɳ� ��������1 ������10
            canvas.GetComponent<GamePane>().RenewScoreUI(1,10,0,0);
            //���ӵ���С�� ������������
            GameObject segment = new GameObject("segment");
            segment.transform.SetParent(SNAKE.transform);
            segment.AddComponent<SnakeBodyUI>();
            canvas.GetComponent<GamePane>().snakeBodys.Add(segment);
        }

        private async void Die()
        {
            this.length = 0;
            
            //��������� ���ȹ�λ������� ������100 ������������ ����������Ϣ
            canvas.GetComponent<GamePane>().RenewScoreUI(-999, -100, 0, 0);
            canvas.GetComponent<GamePane>().deadCount++;
            canvas.GetComponent<GameLogic>().SendSnakeDeath();
            canvas.GetComponent<GamePane>().OnlineSnakeDeath();//����������
            
            //�Ƴ�3s����
            await Task.Delay(3000);


            //���������� ������Ϸ������
            if (GamePane.gameStageFlag == 1)
            {
                canvas.GetComponent<GamePane>().MakeOnlineSnake();
            }
            this.length = this.initlength;
        }

        //��ײ���
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Food")//����ʳ��
            {

                //��������
                Grow();

            }
            else if (other.tag == "Obs")//����ǽ�ڻ����
            {

                //������Ϸ
                Die();
            }
            else if(other.tag == "DeadBody")//��ʬ������ʳ��
            {
                //���ɳ�
                Grow();
            }
        }


    }

}
