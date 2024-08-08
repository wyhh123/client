using UnityEngine;
using UnityEngine.UI;
using com.beiyou.snake.gameclient.engine;

namespace com.beiyou.snake.gameclient.ui
{
    //����ʳ��UI 
    public class FoodOnlineUI : MonoBehaviour
    {
        public GameObject canvas;

        //ʳ���ʼ��
        private void Awake()
        {
            canvas = GameObject.Find("Canvas");

            //ʳ���ȡͼ�������������ͼƬ
            this.gameObject.AddComponent<Image>();
            

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

        //���������ʳ��ͬ������ xyΪ���������ݵ����λ�� zΪ���������ݵ����ʳ����ɫ
        public void Set(int x,int y,int z)
        {
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/node" + z);
            this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }


        //��ײ���
        private void OnTriggerEnter2D(Collider2D other)
        {
            //���������
            if (other.tag == "Player")
            {
                //����ʳ�ﱻ�Ե���Ϣ
                canvas.GetComponent<GameLogic>().SendFoodEatMsg(this.name);
                //�����ô�Ϊ����ҳ�
                GamePane.playerEatFlag = 1;

            }

        }

    }
}
