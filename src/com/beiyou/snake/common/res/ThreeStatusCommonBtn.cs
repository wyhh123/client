using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.common.res
{
    //点击按钮切换形态脚本
    public class ThreeStatusCommonBtn : MonoBehaviour
    {
        private RectTransform m_rectTransform;

        private Button button;
        private void Awake()
        {
            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // 设置中心点为左上角
            m_rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);
        }

        public void setThreeSprite(string normalStr, string pressedStr, string disableStr)
        {
            Image imgPic = gameObject.AddComponent<Image>();
            imgPic.sprite = Resources.Load<Sprite>(normalStr);
            imgPic.SetNativeSize();
            imgPic.type = Image.Type.Sliced;

            button = gameObject.AddComponent<Button>();
            button.transition = Selectable.Transition.SpriteSwap;

            button.spriteState = new SpriteState
            {
                highlightedSprite = Resources.Load<Sprite>(normalStr),
                pressedSprite = Resources.Load<Sprite>(pressedStr),
                selectedSprite = Resources.Load<Sprite>(normalStr),
                disabledSprite = Resources.Load<Sprite>(disableStr)
            };
            // 更新按钮显示
            //button.targetGraphic.SetAllDirty();
        }

        public RectTransform rectTransform
        {
            get
            {
                return m_rectTransform;
            }
        }

        public void AddBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            button.onClick.AddListener(delegate {
                eventHandler(button.gameObject);
            });
        }


    }

}


