using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.common.res
{
    //按钮UI
    public class CommonButton : MonoBehaviour
    {
        private RectTransform m_rectTransform;//对象空间属性
        private Button button;

        private void Awake()
        {
            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // 设置中心点为左上角
            m_rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            //图片组件
            Image img = gameObject.AddComponent<Image>();//必须有图片才能可交互
            img.sprite = Resources.Load<Sprite>("UISprite");
            img.rectTransform.sizeDelta = new Vector2(100, 50);
        }

        public RectTransform rectTransform//供外部调用的空间对象
        {
            get
            {
                return m_rectTransform;
            }
        }

        public void SetButtonLabel(string name)
        {
            button = gameObject.AddComponent<Button>();

            // 添加按钮文本
            GameObject buttonTextGameObject = new GameObject("Text");
            buttonTextGameObject.transform.parent = m_rectTransform;
            Text buttonText = buttonTextGameObject.AddComponent<Text>();
            buttonText.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            buttonText.fontSize = 24;  //设置字体为28号字
            buttonText.text = "";  //设置显示文字
            buttonText.alignment = TextAnchor.MiddleCenter;  //（水平和垂直属性）居中显示MiddleCenter表示中心对齐MiddleLef表示居中左对齐
            buttonText.alignByGeometry = false;  // true 表示文本按照几何形状对齐。这意味着文本的几何边界（由字符的轮廓形状确定）会影响文本的对齐。这样做可以确保字符之间的空白部分也被考虑在内。
            buttonText.fontStyle = FontStyle.Normal; //Bold表示粗体,Italic表示斜体,Normal表示正常,BoldAndItalic表示粗体+斜体
            buttonText.lineSpacing = 1f;   //lineSpacing表示行间距,例如1.5表示是原行间距的1.5倍
            buttonText.supportRichText = false;  //设置文本为富文本,表示接收可以使用类似 <color>、<b> 和 <size> 标记
            buttonText.horizontalOverflow = HorizontalWrapMode.Overflow; //水平溢出 Overflow表示单行显示Wrap表示多行显示
            buttonText.verticalOverflow = VerticalWrapMode.Truncate; // 垂直溢出Truncate表示垂直方向溢出将被截断不显示
            buttonText.resizeTextForBestFit = false;  //"Best Fit" 是一个用于控制文本自动调整字体大小以适应文本框大小的功能。当启用 "BestFit" 选项时，Text 组件会尽量调整文本的字体大小，以使得文本在给定的文本框内完全可见。
            buttonText.color = Color.red;  //设置文本颜色RGBA值
            buttonText.material = null;//设置材质为null
            buttonText.raycastTarget = false;  //是否进行射线检测 对于 Text、Image、RawImage 等 UI 元素，它们的基类是 Graphic，因此都有 raycastTarget 属性。
            buttonText.raycastPadding = new Vector4(0f, 0f, 0f, 0f);  //设置射线检测范围 当raycastTarget为true时生效,否则无效
            buttonText.maskable = true;  //是否相应遮罩,false则显示不受遮罩影响

            buttonText.rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            buttonText.rectTransform.anchorMin = new Vector2(0, 1);
            buttonText.rectTransform.anchorMax = new Vector2(0, 1);
            buttonText.rectTransform.sizeDelta = new Vector2(100, 50);
            buttonText.rectTransform.anchoredPosition = new Vector2(0, 0);


            buttonText.text = name;
        }

        public void AddBtnEventListener(UnityAction<GameObject> eventHandler)
        {
            button.onClick.AddListener(delegate {
                eventHandler(button.gameObject);
            });
        }

    }

}