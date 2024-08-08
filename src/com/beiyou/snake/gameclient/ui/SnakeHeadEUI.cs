
using UnityEngine;

using UnityEngine.UI;



namespace com.beiyou.snake.gameclient.ui
{
    //�з���ͷUI
    public class SnakeHeadEUI : MonoBehaviour
    {

        //�з���ͷͼƬ����
        private GameObject headpic;
        public int snakeIndex;

        public float minV = 2.3f;
        public float angle = 0;
        public float radian = 0;
        public int live = 1;

        private void Awake()
        {
            //�з���ͷ��ʼ��λ�ô�С
            this.transform.position = new Vector2(-10,-10);
            this.transform.localScale = new Vector3(1, 1, 0);

            snakeIndex = this.transform.GetComponentInParent<GamePane>().enemyUid;
            //�з���ͷ����ͼƬ��ͼƬ������ 
            headpic = new GameObject("headpic");
            headpic.transform.SetParent(this.transform);
            headpic.AddComponent<Image>();
            headpic.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/skin_"+(snakeIndex%18+1)+"_head");

            headpic.transform.localScale = new Vector3(0.3f, 0.3f, 0);
            headpic.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            //�з���ͷ���� ��ײ�� ��ǩ��Ӻ���������
            this.gameObject.AddComponent<RectTransform>();
            this.gameObject.AddComponent<Rigidbody2D>();
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().radius = 13;
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

            this.gameObject.tag = "Obs";

        }

        //��ͷ�Ĳ����ƶ���δ������λ��ʱÿ֡����ԭ�����ƶ���
        private void FixedUpdate()
        {
            if (live == 1)
            {
                Move();
            }
            
        }

        private void Move()
        {
            //�з���ͷ�Ƕ� ����õ��˶�����
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
