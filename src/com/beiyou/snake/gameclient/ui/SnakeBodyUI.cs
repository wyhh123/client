using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    //蛇身UI设置
    public class SnakeBodyUI : MonoBehaviour
    {
        
        //private GameObject segment;
        public GameObject head;
        //其他脚本内的蛇头对象
        public int bodyIndex;
        private void Awake()
        {
            //获取蛇头对象
            head = GameObject.Find("snakehead");
            bodyIndex = this.transform.GetComponentInParent<GamePane>().selfUid;
            //蛇身大小设置
            this.gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0);
            this.gameObject.AddComponent<RectTransform>();
            //蛇身初始位置为蛇头位置
            this.transform.position = head.transform.position;
            //碰撞箱设置
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            this.gameObject.GetComponent<CircleCollider2D>().radius = 50;
            // 蛇身图设置
            this.gameObject.AddComponent<Image>();
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/skin_"+(bodyIndex%18+1)+"_body");

        }


    }

}