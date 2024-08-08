using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.common.res
{
    //������ı�UI
    public class CommonInputText : MonoBehaviour
    {
        private RectTransform m_rectTransform;//����ռ�����
        private Text m_TextComponent;//�ı�
        private Text m_Placeholder;//��ʾ

        public RectTransform rectTransform//���ⲿ���õĿռ����
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

        public Text Placeholder
        {
            get
            {
                return m_Placeholder;
            }
        }

        [System.Obsolete]
        private void Awake()
        {
            gameObject.AddComponent<CanvasRenderer>();//CanvasRenderer���������Ⱦ UI Ԫ�ص�ͼ�α�ʾ

            m_rectTransform = gameObject.AddComponent<RectTransform>();
            // �������ĵ�Ϊ���Ͻ�
            m_rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            //ͼƬ���
            Image img = gameObject.AddComponent<Image>();//������ͼƬ���ܿɽ���
            img.color = new Vector4(0, 0, 0, 0);//����ͼƬ���ɼ�

            //��ʾ�ı����
            GameObject m_PlaceholderObj = new("Placeholder");
            m_PlaceholderObj.transform.parent = m_rectTransform;
            m_Placeholder = m_PlaceholderObj.AddComponent<Text>();
            m_Placeholder.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            m_Placeholder.fontSize = 24;
            m_Placeholder.color = new Color(0.62f, 0.63f, 0.65f, 0.75f);
            m_Placeholder.text = "������...";
            m_Placeholder.alignment = TextAnchor.MiddleCenter;
            m_Placeholder.alignByGeometry = false;
            m_Placeholder.fontStyle = FontStyle.Normal;
            m_Placeholder.lineSpacing = 1f;
            m_Placeholder.supportRichText = false;  //�����ı�Ϊ���ı�,��ʾ���տ���ʹ������ <color>��<b> �� <size> ���
            m_Placeholder.horizontalOverflow = HorizontalWrapMode.Overflow; //ˮƽ��� Overflow��ʾ������ʾWrap��ʾ������ʾ
            m_Placeholder.verticalOverflow = VerticalWrapMode.Truncate; // ��ֱ���Truncate��ʾ��ֱ������������ضϲ���ʾ
            m_Placeholder.resizeTextForBestFit = false;  //"Best Fit" ��һ�����ڿ����ı��Զ����������С����Ӧ�ı����С�Ĺ��ܡ������� "BestFit" ѡ��ʱ��Text ����ᾡ�������ı��������С����ʹ���ı��ڸ������ı�������ȫ�ɼ���
            m_Placeholder.material = null;//���ò���Ϊnull
            m_Placeholder.raycastTarget = false;  //�Ƿ�������߼�� ���� Text��Image��RawImage �� UI Ԫ�أ����ǵĻ����� Graphic����˶��� raycastTarget ���ԡ�
            m_Placeholder.raycastPadding = new Vector4(0f, 0f, 0f, 0f);  //�������߼�ⷶΧ ��raycastTargetΪtrueʱ��Ч,������Ч
            m_Placeholder.maskable = true;  //�Ƿ���Ӧ����,false����ʾ��������Ӱ��
                                            ////����Ϊstretchģʽ
                                            //m_Placeholder.rectTransform.anchorMin = Vector2.zero;// �������Ͻ�ê��
                                            //m_Placeholder.rectTransform.anchorMax = Vector2.one;
                                            //m_Placeholder.rectTransform.offsetMin = Vector2.zero;// ����ƫ��Ϊ(0, 0)��ʾ���Ͻǣ�(1, 1)��ʾ���½�
                                            //m_Placeholder.rectTransform.offsetMax = Vector2.zero;
                                            // �������ĵ�Ϊ���Ͻ�
            m_Placeholder.rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_Placeholder.rectTransform.anchorMin = new Vector2(0, 1);
            m_Placeholder.rectTransform.anchorMax = new Vector2(0, 1);
            m_Placeholder.rectTransform.sizeDelta = new Vector2(354, 60);

            //�ı����
            GameObject textObj = new("Text");
            textObj.transform.parent = m_rectTransform;
            m_TextComponent = textObj.AddComponent<Text>();   //��GameObject�������text���
            m_TextComponent.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            m_TextComponent.fontSize = 24;  //��������Ϊ28����
            m_TextComponent.text = "";  //������ʾ����
            m_TextComponent.alignment = TextAnchor.MiddleCenter;  //��ˮƽ�ʹ�ֱ���ԣ�������ʾMiddleCenter��ʾ���Ķ���MiddleLef��ʾ���������
            m_TextComponent.alignByGeometry = false;  // true ��ʾ�ı����ռ�����״���롣����ζ���ı��ļ��α߽磨���ַ���������״ȷ������Ӱ���ı��Ķ��롣����������ȷ���ַ�֮��Ŀհײ���Ҳ���������ڡ�
            m_TextComponent.fontStyle = FontStyle.Normal; //Bold��ʾ����,Italic��ʾб��,Normal��ʾ����,BoldAndItalic��ʾ����+б��
            m_TextComponent.lineSpacing = 1f;   //lineSpacing��ʾ�м��,����1.5��ʾ��ԭ�м���1.5��
            m_TextComponent.supportRichText = false;  //�����ı�Ϊ���ı�,��ʾ���տ���ʹ������ <color>��<b> �� <size> ���
            m_TextComponent.horizontalOverflow = HorizontalWrapMode.Overflow; //ˮƽ��� Overflow��ʾ������ʾWrap��ʾ������ʾ
            m_TextComponent.verticalOverflow = VerticalWrapMode.Truncate; // ��ֱ���Truncate��ʾ��ֱ������������ضϲ���ʾ
            m_TextComponent.resizeTextForBestFit = false;  //"Best Fit" ��һ�����ڿ����ı��Զ����������С����Ӧ�ı����С�Ĺ��ܡ������� "BestFit" ѡ��ʱ��Text ����ᾡ�������ı��������С����ʹ���ı��ڸ������ı�������ȫ�ɼ���
            m_TextComponent.color = Color.white;  //�����ı���ɫRGBAֵ
            m_TextComponent.material = null;//���ò���Ϊnull
            m_TextComponent.raycastTarget = false;  //�Ƿ�������߼�� ���� Text��Image��RawImage �� UI Ԫ�أ����ǵĻ����� Graphic����˶��� raycastTarget ���ԡ�
            m_TextComponent.raycastPadding = new Vector4(0f, 0f, 0f, 0f);  //�������߼�ⷶΧ ��raycastTargetΪtrueʱ��Ч,������Ч
            m_TextComponent.maskable = true;  //�Ƿ���Ӧ����,false����ʾ��������Ӱ��
                                              ////����Ϊstretchģʽ
                                              //m_TextComponent.rectTransform.anchorMin = Vector2.zero;// �������Ͻ�ê��
                                              //m_TextComponent.rectTransform.anchorMax = Vector2.one;
                                              //m_TextComponent.rectTransform.offsetMin = Vector2.zero;// ����ƫ��Ϊ(0, 0)��ʾ���Ͻǣ�(1, 1)��ʾ���½�
                                              //m_TextComponent.rectTransform.offsetMax = Vector2.zero;
                                              // �������ĵ�Ϊ���Ͻ�
            m_TextComponent.rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_TextComponent.rectTransform.anchorMin = new Vector2(0, 1);
            m_TextComponent.rectTransform.anchorMax = new Vector2(0, 1);
            m_TextComponent.rectTransform.sizeDelta = new Vector2(354, 60);

            //���InputField���
            InputField inputField = gameObject.AddComponent<InputField>();
            //inputField.transition = Selectable.Transition.None;
            inputField.targetGraphic = img;
            inputField.textComponent = m_TextComponent;
            inputField.placeholder = m_Placeholder;
            inputField.onValueChange.AddListener(HandleInputFieldValueChange);//����仯
            inputField.onEndEdit.AddListener(HandleInputEndEdit);//�������
            inputField.onSubmit.AddListener(HandleInputSubmit);//�ύ���룬����س�
        }

        // onValueChange�¼��Ĵ�����
        private void HandleInputFieldValueChange(string newValue)
        {
            //Debug.Log("����仯��" + newValue);

            // ������ִ������Ҫ�Ĳ����������������UIԪ�ء����������
        }

        private void HandleInputEndEdit(string newValue)
        {
            //Debug.Log("������ɣ�" + newValue);
        }

        private void HandleInputSubmit(string newValue)
        {
            //Debug.Log("�����ύ��" + newValue);
        }
    }
}
