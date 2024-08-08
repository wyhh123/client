using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace com.beiyou.snake.gameclient.ui
{
    //�����ƶ��ű�
    public class SnakeBodyMoveE : MonoBehaviour
    {
        //������ ��ͷ
        public List<GameObject> tSnakeBodys = null;
        public GameObject head;
        public int length=1;

        private void Awake()
        {
            //��ʼ��
            //head = GameObject.Find("snakehead");
            tSnakeBodys = new List<GameObject>();
        }

        private void FixedUpdate()
        {
            //�Ӻ���ǰ ����׷ǰһ������ ��һ������׷��ͷ
            for (int i = length - 1; i > 0; i--)
            {
                tSnakeBodys[i].transform.position = tSnakeBodys[i - 1].transform.position;
            }
            tSnakeBodys[0].transform.position = head.transform.position;

        }

    }
}