using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace com.beiyou.snake.gameclient.ui
{
    //蛇身移动脚本
    public class SnakeBodyMove : MonoBehaviour
    {
        //蛇身列 蛇头
        public List<GameObject> tSnakeBodys = null;
        public GameObject head;

        private void Awake()
        {
            //初始化
            //head = GameObject.Find("snakehead");
            tSnakeBodys = new List<GameObject>();
        }

        private void FixedUpdate()
        {
            //从后至前 蛇身追前一节蛇身 第一节蛇身追蛇头
            for(int i = tSnakeBodys.Count - 1; i > 0; i--)
            {
                tSnakeBodys[i].transform.position = tSnakeBodys[i - 1].transform.position;  
            }
            tSnakeBodys[0].transform.position = head.transform.position;
            
        }

    }
}
