using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{

    //ҡ��UIʵ��
    [RequireComponent(typeof(EventTrigger))]
    public class JoystickUI : ScrollRect
    {
        //ҡ�˱�����ҡ�˴���
        private GameObject JoystickBg;
        private GameObject JoystickTouch;

        /// <summary>
        /// �϶���ֵ
        /// </summary>
        public Vector2 offsetValue;

        // �뾶 -- ������ק����
        private float mRadius;

        /// <summary>
        /// �ƶ��лص�
        /// </summary>
        public System.Action<RectTransform> JoystickMoveHandle;
        /// <summary>
        /// �ƶ������ص�
        /// </summary>
        public System.Action<RectTransform> JoystickEndHandle;

        /// <summary>
        /// ҡ���Ƿ��ڿ���״̬
        /// </summary>
        public bool joyIsCanUse = false;

        protected override void Awake()
        {
            //ҡ�˱��� ��ʼ��
            JoystickBg = new GameObject("joystickBg");
            JoystickBg.transform.SetParent(gameObject.transform);
            JoystickBg.AddComponent<RectTransform>();
            JoystickBg.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0,0);
            JoystickBg.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
            JoystickBg.AddComponent<Image>();
            Image jbg = JoystickBg.GetComponent<Image>();
            jbg.sprite = Resources.Load<Sprite>("index/Image/de3fb5d371d8aa2d0d3fb814c124d4ef");
            JoystickBg.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);

            //ҡ�˴��� ��ʼ��
            JoystickTouch = new GameObject("joystickTouch");
            JoystickTouch.transform.SetParent(JoystickBg.transform);
            JoystickTouch.AddComponent<RectTransform>();
            JoystickTouch.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0,0);
            JoystickTouch.AddComponent<Image>();
            Image jbt = JoystickTouch.GetComponent<Image>();
            jbt.sprite = Resources.Load<Sprite>("index/Image/2202a08d0f0f6e81f467f3bdb07c3217");
            JoystickTouch.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);

        }

        protected override void Start()
        {
            viewport = JoystickBg.GetComponent<RectTransform>();
            content = JoystickTouch.GetComponent<RectTransform>();

            //����뾶
            mRadius = this.content.sizeDelta.x * 0.5f;

            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entryPointerUp = new EventTrigger.Entry();
            entryPointerUp.eventID = EventTriggerType.PointerUp;
            entryPointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
            trigger.triggers.Add(entryPointerUp);

            EventTrigger.Entry entryPointerDown = new EventTrigger.Entry();
            entryPointerDown.eventID = EventTriggerType.PointerDown;
            entryPointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
            trigger.triggers.Add(entryPointerDown);

        }

        //��ʼ״��
        protected override void OnEnable()
        {
            joyIsCanUse = false;
            offsetValue = Vector2.zero;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            joyIsCanUse = true;
            //����ҡ���ƶ�
            Vector3 contentPosition = this.content.anchoredPosition;

            //������Χ
            if (contentPosition.magnitude > mRadius)
            {
                contentPosition = contentPosition.normalized * mRadius;
                SetContentAnchoredPosition(contentPosition);
            }

        }

        //��ק���
        private void FixedUpdate()
        {
            if (joyIsCanUse)
            {
                JoystickMoveHandle?.Invoke(this.content);
                offsetValue = this.content.anchoredPosition3D;
            }
        }

        //��ק����
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            joyIsCanUse = false;
            offsetValue = Vector2.zero;
            JoystickEndHandle?.Invoke(this.content);
        }

        // ������������ҡ��λ��
        private void OnPointerDown(PointerEventData eventData)
        {
            Vector2 LocalPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(),
                eventData.position, eventData.pressEventCamera, out LocalPosition);
            this.viewport.localPosition = LocalPosition;
        }

        // ̧��ԭλ��
        private void OnPointerUp(PointerEventData eventData)
        {
            this.viewport.anchoredPosition3D = Vector3.zero;
        }



    }
}

