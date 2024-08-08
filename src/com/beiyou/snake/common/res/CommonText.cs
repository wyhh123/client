using com.beiyou.snake.gameclient.ui;
using UnityEngine;
using UnityEngine.UI;

namespace com.beiyou.snake.common.res
{
    //�ı�UI
    public class CommonText : MonoBehaviour
    {
        private RectTransform m_rectTransform;//����ռ�����
        private Text m_TextComponent;//�ı�

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

        [System.Obsolete]
        private void Awake()
        {
            //��������ظ����
            if (gameObject.GetComponent<CanvasRenderer>()==null)
            {
                gameObject.AddComponent<CanvasRenderer>();//CanvasRenderer���������Ⱦ UI Ԫ�ص�ͼ�α�ʾ
            }

            if (gameObject.GetComponent<RectTransform>() == null)
            {
                m_rectTransform = gameObject.AddComponent<RectTransform>();
            }
            else { m_rectTransform = gameObject.GetComponent<RectTransform>(); }

            
            // �������ĵ�Ϊ���Ͻ�
            m_rectTransform.pivot = new Vector2(0, 1);
            // ����ê��Ϊ���Ͻ�
            m_rectTransform.anchorMin = new Vector2(0, 1);
            m_rectTransform.anchorMax = new Vector2(0, 1);

            Image img;

            //ͼƬ���
            if (gameObject.GetComponent<Image>() == null)
            {
                img = gameObject.AddComponent<Image>();
            }
            else {  img = gameObject.GetComponent<Image>(); }
            //������ͼƬ���ܿɽ���
            img.color = new Vector4(0, 0, 0, 0);//����ͼƬ���ɼ�


            //�ı����
            GameObject textObj = new("Text");
            textObj.transform.parent = m_rectTransform;
            m_TextComponent = textObj.AddComponent<Text>();   //��GameObject�������text���
            m_TextComponent.font = Font.CreateDynamicFontFromOSFont("Arial", 24);
            m_TextComponent.fontSize = 24;  //��������Ϊ24����
            m_TextComponent.text = "";  //������ʾ����
            m_TextComponent.alignment = TextAnchor.MiddleLeft;  //��ˮƽ�ʹ�ֱ���ԣ�������ʾMiddleCenter��ʾ���Ķ���MiddleLef��ʾ���������
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


        }

    }
}
