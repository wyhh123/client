using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //����ģʽ��ʳ��UI
    public class FoodUI : MonoBehaviour
    {
        
        //����ģʽ��ʳ���ʼ��
        private void Awake()
        {
            //ʳ���ȡͼ�������������ͼƬ
            this.gameObject.AddComponent<Image>();
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/node"+randi());

            //ʳ���Сλ������
            this.gameObject.GetComponent<RectTransform>().anchoredPosition = randxy();
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);

            //ʳ�������ײ�䲢����
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            this.gameObject.GetComponent<CircleCollider2D>().radius = 8;
            
            //ʳ���ǩ
            this.gameObject.tag = "Food";

        }

        //�����ͼλ��
        public Vector2 randxy()
        {
            float x = Random.Range(20, 1330);
            float y = Random.Range(30, 760);
            return new Vector2(x, y);
        }

        //���ʳ��node���
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
                this.gameObject.GetComponent<RectTransform>().anchoredPosition = randxy();//����ı�ʳ��λ��
            }
        }

    }
}
