using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{

    //摇杆UI实现
    [RequireComponent(typeof(EventTrigger))]
    public class JoystickUI : ScrollRect
    {
        //摇杆背景和摇杆触点
        private GameObject JoystickBg;
        private GameObject JoystickTouch;

        /// <summary>
        /// 拖动差值
        /// </summary>
        public Vector2 offsetValue;

        // 半径 -- 控制拖拽区域
        private float mRadius;

        /// <summary>
        /// 移动中回调
        /// </summary>
        public System.Action<RectTransform> JoystickMoveHandle;
        /// <summary>
        /// 移动结束回调
        /// </summary>
        public System.Action<RectTransform> JoystickEndHandle;

        /// <summary>
        /// 摇杆是否处于可用状态
        /// </summary>
        public bool joyIsCanUse = false;

        protected override void Awake()
        {
            //摇杆背景 初始化
            JoystickBg = new GameObject("joystickBg");
            JoystickBg.transform.SetParent(gameObject.transform);
            JoystickBg.AddComponent<RectTransform>();
            JoystickBg.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0,0);
            JoystickBg.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
            JoystickBg.AddComponent<Image>();
            Image jbg = JoystickBg.GetComponent<Image>();
            jbg.sprite = Resources.Load<Sprite>("index/Image/de3fb5d371d8aa2d0d3fb814c124d4ef");
            JoystickBg.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);

            //摇杆触点 初始化
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

            //触点半径
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

        //初始状况
        protected override void OnEnable()
        {
            joyIsCanUse = false;
            offsetValue = Vector2.zero;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            joyIsCanUse = true;
            //虚拟摇杆移动
            Vector3 contentPosition = this.content.anchoredPosition;

            //超出范围
            if (contentPosition.magnitude > mRadius)
            {
                contentPosition = contentPosition.normalized * mRadius;
                SetContentAnchoredPosition(contentPosition);
            }

        }

        //拖拽检测
        private void FixedUpdate()
        {
            if (joyIsCanUse)
            {
                JoystickMoveHandle?.Invoke(this.content);
                offsetValue = this.content.anchoredPosition3D;
            }
        }

        //拖拽结束
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            joyIsCanUse = false;
            offsetValue = Vector2.zero;
            JoystickEndHandle?.Invoke(this.content);
        }

        // 随手落下设置摇杆位置
        private void OnPointerDown(PointerEventData eventData)
        {
            Vector2 LocalPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(),
                eventData.position, eventData.pressEventCamera, out LocalPosition);
            this.viewport.localPosition = LocalPosition;
        }

        // 抬起还原位置
        private void OnPointerUp(PointerEventData eventData)
        {
            this.viewport.anchoredPosition3D = Vector3.zero;
        }



    }
}

