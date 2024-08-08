
using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //��ʬ�������ʳ�� ���Ժ󲻻���������
    public class DeadBodyUI : MonoBehaviour
    {
        public GameObject canvas;
        
        //ʳ���ʼ��
        private void Awake()
        {
            // ��ȡCanvas
            canvas = GameObject.Find("Canvas");

            //ʳ���ȡͼ�������������ͼƬ
            this.gameObject.AddComponent<Image>();
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/node" + randi());

            //ʳ���Сλ������
            //this.gameObject.AddComponent<RectTransform>();
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

            //ʳ�������ײ�䲢����
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            this.gameObject.GetComponent<CircleCollider2D>().radius = 8;

            //ʳ���ǩ
            this.gameObject.tag = "Food";

        }



        //���ʳ��node��� ֻ����ʳ����ɫ
        public int randi()
        {
            int i = Random.Range(1, 13);
            return i;
        }

        //��ײ���
        private void OnTriggerEnter2D(Collider2D other)
        {
            //���������
            if (other.tag == "Player")
            {
                Destroy(this.gameObject);
                 
            }
            //������
            else if (other.tag =="Obs")
            {
                //�������ڼ����Ƿ�UI�ĸ���
                canvas.GetComponent<GamePane>().RenewScoreUI(0, 0, 1, 10);
                Destroy(this.gameObject);
            }
        }

    }
}
