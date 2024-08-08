using com.beiyou.snake.gameclient.ui;
using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.common.res
{
    //文本UI
    public class CommonText : MonoBehaviour
    {
        private RectTransform m_rectTransform;//对象空间属性
        private Text m_TextComponent;//文本

        public RectTransform rectTransform//供外部调用的空间对象
        {
            get
            {
                return m_rectTransform;
            }
        }

        public Text TextComponent
        {
            get
            {
                return m_TextComponent;
            }
        }

        [System.Obsolete]
        private void Awake()
        {
            //避免组件重复添加
            if (gameObject.GetComponent<CanvasRenderer>()==null)
            {
                gameObject.AddComponent<CanvasRenderer>();//CanvasRenderer负责处理和渲染 UI 元素的图形表示
            }

            if (gameObject.GetComponent<RectTransform>() == null)
            {
                m_rectTransform = gameObject.AddComponent<RectTransform>();
            }
            else { m_rectTransform = gameObject.GetComponent<RectTransform>(); }

            
            // 设置中心点为左上角
            m_rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            Image img;

            //图片组件
            if (gameObject.GetComponent<Image>() == null)
            {
                img = gameObject.AddComponent<Image>();
            }
            else {  img = gameObject.GetComponent<Image>(); }
            //必须有图片才能可交互
            img.color = new Vector4(0, 0, 0, 0);//设置图片不可见


            //文本组件
            GameObject textObj = new("Text");
            textObj.transform.parent = m_rectTransform;
            m_TextComponent = textObj.AddComponent<Text>();   //给GameObject对象添加text组件
            m_TextComponent.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            m_TextComponent.fontSize = 24;  //设置字体为24号字
            m_TextComponent.text = "";  //设置显示文字
            m_TextComponent.alignment = TextAnchor.MiddleLeft;  //（水平和垂直属性）居中显示MiddleCenter表示中心对齐MiddleLef表示居中左对齐
            m_TextComponent.alignByGeometry = false;  // true 表示文本按照几何形状对齐。这意味着文本的几何边界（由字符的轮廓形状确定）会影响文本的对齐。这样做可以确保字符之间的空白部分也被考虑在内。
            m_TextComponent.fontStyle = FontStyle.Normal; //Bold表示粗体,Italic表示斜体,Normal表示正常,BoldAndItalic表示粗体+斜体
            m_TextComponent.lineSpacing = 1f;   //lineSpacing表示行间距,例如1.5表示是原行间距的1.5倍
            m_TextComponent.supportRichText = false;  //设置文本为富文本,表示接收可以使用类似 <color>、<b> 和 <size> 标记
            m_TextComponent.horizontalOverflow = HorizontalWrapMode.Overflow; //水平溢出 Overflow表示单行显示Wrap表示多行显示
            m_TextComponent.verticalOverflow = VerticalWrapMode.Truncate; // 垂直溢出Truncate表示垂直方向溢出将被截断不显示
            m_TextComponent.resizeTextForBestFit = false;  //"Best Fit" 是一个用于控制文本自动调整字体大小以适应文本框大小的功能。当启用 "BestFit" 选项时，Text 组件会尽量调整文本的字体大小，以使得文本在给定的文本框内完全可见。
            m_TextComponent.color = Color.white;  //设置文本颜色RGBA值
            m_TextComponent.material = null;//设置材质为null
            m_TextComponent.raycastTarget = false;  //是否进行射线检测 对于 Text、Image、RawImage 等 UI 元素，它们的基类是 Graphic，因此都有 raycastTarget 属性。
            m_TextComponent.raycastPadding = new Vector4(0f, 0f, 0f, 0f);  //设置射线检测范围 当raycastTarget为true时生效,否则无效
            m_TextComponent.maskable = true;  //是否相应遮罩,false则显示不受遮罩影响
                                              ////设置为stretch模式
                                              //m_TextComponent.rectTransform.anchorMin = Vector2.zero;// 设置左上角锚点
                                              //m_TextComponent.rectTransform.anchorMax = Vector2.one;
                                              //m_TextComponent.rectTransform.offsetMin = Vector2.zero;// 设置偏移为(0, 0)表示左上角，(1, 1)表示右下角
                                              //m_TextComponent.rectTransform.offsetMax = Vector2.zero;
                                              // 设置中心点为左上角
            m_TextComponent.rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            m_TextComponent.rectTransform.anchorMin = new Vector2(0, 1);
            m_TextComponent.rectTransform.anchorMax = new Vector2(0, 1);
            m_TextComponent.rectTransform.sizeDelta = new Vector2(354, 60);


        }

    }
}
