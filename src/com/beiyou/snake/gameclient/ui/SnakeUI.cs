
using com.beiyou.snake.gameclient.engine;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



namespace com.beiyou.snake.gameclient.ui
{
    //��ͷUI���¼�
    public class SnakeUI : MonoBehaviour
    {
        //��ȡjoystick�е�ҡ�˶��� snakebodymove�е��߶��� gamepane��canvas����
        public GameObject touching;
        public GameObject SNAKE;
        public GameObject canvas;

        //������ͷƫת�ĽǶȺͻ���
        private float angle = 0;
        private float radian = 0;

        //������ʱ��ͷ����С�ٶȺ�ҡ���ṩ�ĸ����ٶ�
        public float minV = 2.3f;
        public float touchingV = 0.012f;
        
        //��ͷͼ�����ͼƬ
        private GameObject headPic;

        //��Ƥ��
        public int snakeIndex;
        private void Awake()
        {
            //��ʼ����
            CreateSnake();
            
        }

        // ����߳���λ��
        private Vector2 Randxy()
        {
            float x = Random.Range(100, 1000);
            float y = Random.Range(100, 600);
            return new Vector2(x,y);
        }

        private void CreateSnake()
        {
            //�ֱ��ȡ��Ӧ����
            SNAKE = GameObject.Find("bodys");//���������������ƶ�
            touching = GameObject.Find("joystickTouch");//ҡ�˶����ȡҡ�˲���
            canvas = GameObject.Find("Canvas");//UI��������GamePane�ķ���

            snakeIndex = canvas.GetComponent<GamePane>().selfUid;
            //���ó�ʼλ�ô�С
            this.transform.position = Randxy();
                //new Vector3(100, 100, 0);
            this.transform.localScale = new Vector3(1, 1, 0);

            //ͼƬ��ʼ�� ���ø����� ���ͼƬ���������ͼƬ ����λ�ô�С
            headPic = new GameObject("headpic");
            headPic.transform.SetParent(this.transform);
            headPic.AddComponent<Image>();
            headPic.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/skin_"+(snakeIndex%18+1)+"_head");
            headPic.transform.localScale = new Vector3(0.3f, 0.3f, 0);
            headPic.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            //��ͷ��Ӹ�����ײ�����������л�������
            this.gameObject.AddComponent<RectTransform>();
            this.gameObject.AddComponent<Rigidbody2D>();
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().radius = 13;
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

            //ֻ����ͷ�����ұ�ǩ
            this.gameObject.tag = "Player";
        }

        private void FixedUpdate()
        {
            //��ÿ֡�ƶ�
            Move();

        }

        //���˶�
        private void Move()
        {
            //�ƶ�
            //��ȡҡ��ƫ��������
            Vector2 vec = touching.GetComponent<RectTransform>().anchoredPosition;
            //ҡ��ƫ��������׼���õ���С�ٶ�
            Vector2 minVec = vec.normalized;
            //��ͷ�����ٶ�ÿ֡λ���ƶ�
            this.transform.position = new Vector3(
                //��ͷ����ĳ����λ�� + ҡ��ĳ����ƫ�ƴ�С*ҡ�˿����ٶȵĲ��� + ��С�ٶȲ�����ĳ�����ƶ�*������С�ٶȵĲ���
                this.transform.position.x + vec.x * touchingV + minVec.x * minV,
                this.transform.position.y + vec.y * touchingV + minVec.y * minV,
                0
                );


            //תͷ
            if (vec.x == 0)//��ͷ�������
            {
                //xΪ0ά��ԭ״
            }
            else
            {
                //ҡ��ƫ�Ʒ���
                radian = Mathf.Atan((vec.y / vec.x));//��ͷ������� ����
                angle = radian / Mathf.PI * 180;//����ת�Ƕ�

                if (vec.x < 0)//ҡ��ƫ����x���·�ʱ ��ѧ��ϵ����
                {
                    angle = angle + 180;//�������� 
                }

            }

            //��ͷ�Ӷ���ͼƬ����ת ֱ��ת����ͷ����
            headPic.transform.eulerAngles = new Vector3(
                //2d��Ϸ��������н�Ϊ0
                0,
                0,
                angle - 90
                //����ýǶ�Ϊ����x��н� eulerAngles zֵΪ����y��н�
                );
        }

        //������
        private void Grow()
        {
           //��������С�γ�ʼ�� ���ø����� �������UI ����gamePane����������
            GameObject segment = new GameObject("segment");
            segment.transform.SetParent(SNAKE.transform);
            segment.AddComponent<SnakeBodyUI>();
            canvas.GetComponent<GamePane>().snakeBodys.Add(segment);
        }

        // ��������
        private async void Die()
        {
            //GamePane���ͷ���ȫ������ķ���
            canvas.GetComponent<GamePane>().SnakeDeath();

            // 1.5���Ƴ�
            await Task.Delay(1500);

            //GamePane�д�����ȫ������ķ���
            //������Ϸ��������������
            if(GamePane.gameStageFlag == 1) 
            {
                canvas.GetComponent<GamePane>().MakeSnake();
            }
            
        }


            
        
        //��ײ���
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Food")//����ʳ��
            {

                //��������
                Grow();


            }
            else if (other.tag == "Obs")//����ǽ��
            {

                //��������
                Die();
            }
            
        }


    }

}
