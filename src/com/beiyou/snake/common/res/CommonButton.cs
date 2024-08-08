using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.beiyou.snake.common.res
{
    //��ťUI
    public class CommonButton : MonoBehaviour
    {
        private RectTransform m_rectTransform;//����ռ�����
        private Button button;

        private void Awake()
        {
            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // �������ĵ�Ϊ���Ͻ�
            m_rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            //ͼƬ���
            Image img = gameObject.AddComponent<Image>();//������ͼƬ���ܿɽ���
            img.sprite = Resources.Load<Sprite>("UISprite");
            img.rectTransform.sizeDelta = new Vector2(100, 50);
        }

        public RectTransform rectTransform//���ⲿ���õĿռ����
        {
            get
            {
                return m_rectTransform;
            }
        }

        public void SetButtonLabel(string name)
        {
            button = gameObject.AddComponent<Button>();

            // ��Ӱ�ť�ı�
            GameObject buttonTextGameObject = new GameObject("Text");
            buttonTextGameObject.transform.parent = m_rectTransform;
            Text buttonText = buttonTextGameObject.AddComponent<Text>();
            buttonText.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            buttonText.fontSize = 24;  //��������Ϊ28����
            buttonText.text = "";  //������ʾ����
            buttonText.alignment = TextAnchor.MiddleCenter;  //��ˮƽ�ʹ�ֱ���ԣ�������ʾMiddleCenter��ʾ���Ķ���MiddleLef��ʾ���������
            buttonText.alignByGeometry = false;  // true ��ʾ�ı����ռ�����״���롣����ζ���ı��ļ��α߽磨���ַ���������״ȷ������Ӱ���ı��Ķ��롣����������ȷ���ַ�֮��Ŀհײ���Ҳ���������ڡ�
            buttonText.fontStyle = FontStyle.Normal; //Bold��ʾ����,Italic��ʾб��,Normal��ʾ����,BoldAndItalic��ʾ����+б��
            buttonText.lineSpacing = 1f;   //lineSpacing��ʾ�м��,����1.5��ʾ��ԭ�м���1.5��
            buttonText.supportRichText = false;  //�����ı�Ϊ���ı�,��ʾ���տ���ʹ������ <color>��<b> �� <size> ���
            buttonText.horizontalOverflow = HorizontalWrapMode.Overflow; //ˮƽ��� Overflow��ʾ������ʾWrap��ʾ������ʾ
            buttonText.verticalOverflow = VerticalWrapMode.Truncate; // ��ֱ���Truncate��ʾ��ֱ������������ضϲ���ʾ
            buttonText.resizeTextForBestFit = false;  //"Best Fit" ��һ�����ڿ����ı��Զ����������С����Ӧ�ı����С�Ĺ��ܡ������� "BestFit" ѡ��ʱ��Text ����ᾡ�������ı��������С����ʹ���ı��ڸ������ı�������ȫ�ɼ���
            buttonText.color = Color.red;  //�����ı���ɫRGBAֵ
            buttonText.material = null;//���ò���Ϊnull
            buttonText.raycastTarget = false;  //�Ƿ�������߼�� ���� Text��Image��RawImage �� UI Ԫ�أ����ǵĻ����� Graphic����˶��� raycastTarget ���ԡ�
            buttonText.raycastPadding = new Vector4(0f, 0f, 0f, 0f);  //�������߼�ⷶΧ ��raycastTargetΪtrueʱ��Ч,������Ч
            buttonText.maskable = true;  //�Ƿ���Ӧ����,false����ʾ��������Ӱ��

            buttonText.rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
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