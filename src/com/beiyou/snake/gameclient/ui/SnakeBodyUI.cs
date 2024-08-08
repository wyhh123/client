using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //����UI����
    public class SnakeBodyUI : MonoBehaviour
    {
        
        //private GameObject segment;
        public GameObject head;
        //�����ű��ڵ���ͷ����
        public int bodyIndex;
        private void Awake()
        {
            //��ȡ��ͷ����
            head = GameObject.Find("snakehead");
            bodyIndex = this.transform.GetComponentInParent<GamePane>().selfUid;
            //�����С����
            this.gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0);
            this.gameObject.AddComponent<RectTransform>();
            //�����ʼλ��Ϊ��ͷλ��
            this.transform.position = head.transform.position;
            //��ײ������
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            this.gameObject.GetComponent<CircleCollider2D>().radius = 50;
            // ����ͼ����
            this.gameObject.AddComponent<Image>();
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/skin_"+(bodyIndex%18+1)+"_body");

        }


    }

}