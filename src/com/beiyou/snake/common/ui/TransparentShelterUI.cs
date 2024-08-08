using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.common.ui
{
    public class TransparentShelterUI : MonoBehaviour
    {
        private Image image = null;
        private Button button = null;

        private void Awake()
        {
            image = gameObject.AddComponent<Image>();
            image.name = "img";
            image.color = new Color(0, 0, 0, 0.5f);
            image.rectTransform.anchoredPosition = Vector2.zero;
            image.rectTransform.sizeDelta = new Vector2(750, 1538);

            button = gameObject.AddComponent<Button>();
        }

        public void InitUI(float widthV = 750, float heightV = 1538, Color colorV = default)
        {
            image.color = (colorV == default(Color)) ? Color.black : colorV; // 使用默认值 Color.black 如果未提供值
            image.rectTransform.sizeDelta = new Vector2(widthV, heightV);//大小
        }

        public void SetAlpha(float aValue)
        {
            image.color = new Color(0, 0, 0, aValue);
        }

        public void ResetSize(float widthValue, float heightValue)
        {
            image.rectTransform.sizeDelta = new Vector2(widthValue, heightValue);
        }

        public void AddTransparentShelterUIEventListener(UnityAction<GameObject> eventHandler)
        {
            button.onClick.AddListener(delegate {
                eventHandler(button.gameObject);
            });
        }


    }
}
