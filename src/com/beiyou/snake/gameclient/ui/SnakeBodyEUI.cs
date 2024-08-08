using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    // 对方蛇身体的初始化
    public class SnakeBodyEUI : MonoBehaviour
    {
        public int bodyIndex;

        private void Awake()
        {
            bodyIndex = this.transform.GetComponentInParent<GamePane>().enemyUid;

            //蛇身大小 位置（不可见位置） 碰撞箱 图片
            this.gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0);

            this.gameObject.AddComponent<RectTransform>();

            this.transform.position = new Vector2(-10,-10);

            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            this.gameObject.GetComponent<CircleCollider2D>().radius = 50;

            this.gameObject.AddComponent<Image>();
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("gameclient/sprites/Sprites/skin_"+(bodyIndex%18+1)+"_body");

        }


    }

}