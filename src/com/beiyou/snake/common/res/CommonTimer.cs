using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace com.beiyou.snake.common.res
{
    public class CommonTimer : MonoBehaviour
    {
        private bool repeatAlways = false;//�Ƿ�һֱѭ��
        private bool repeat = true;//�Ƿ����ѭ��ִ��(�����ʶ��ͨ��ѭ��������ȷ��)

        private Coroutine timerCoroutine;

        private UnityAction timerHandler;//��ʱ�������ص�
        private UnityAction timerCompleteHandler;//��ʱ�������ص�
        private float delay;
        private long repeatCount;

        private bool running = false;//�Ƿ�������ʱ��(�����ʶ���Ǽ�ʱ���Ŀ���)
        public bool Running { get => running; set => running = value; }

        /// <summary>
        /// ����Э�̷���
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="repeatCount"></param>
        public void InitTimer(float delay, long repeatCount = -1)
        {
            this.delay = delay;
            this.repeatCount = repeatCount;

            this.running = false;//Ĭ�ϲ�������ʱ��
            this.repeat = true;//Ĭ������ִ��һ��

            //���ѭ��������-1.��һֱѭ��
            if (repeatCount == -1)
            {
                repeatAlways = true;
            }
            else
            {
                repeatAlways = false;
            }
        }

        /// <summary>
        /// ��Ӽ�ʱ�������¼�
        /// </summary>
        /// <param name="callback"></param>
        public void AddTimerEventListener(UnityAction timerHandler)
        {
            this.timerHandler = timerHandler;
        }

        /// <summary>
        /// ��Ӽ�ʱ������¼�
        /// </summary>
        /// <param name="timerCompleteHandler"></param>
        public void AddTimerCompleteEventListener(UnityAction timerCompleteHandler)
        {
            this.timerCompleteHandler = timerCompleteHandler;
        }

        /// <summary>
        /// ����timerЭ��
        /// </summary>
        /// <returns></returns>
        IEnumerator RunTimer()
        {
            //�Ƿ�ִ��
            while (running && repeat)
            {
                //�ӳ�ָ����ʱ��
                yield return new WaitForSeconds(delay);

                //ִ�м�ʱ����������
                if (timerHandler != null)
                {
                    timerHandler.Invoke();
                }

                //����Ƿ����ѭ��
                if (repeatAlways == true)//һֱѭ��
                {
                    repeat = true;
                }
                else//ѭ��ָ���Ĵ���
                {
                    repeatCount--;
                    if (repeatCount <= 0)
                    {
                        //ֹͣѭ��
                        repeat = false;

                        //ִ�м�ʱ����������
                        if (timerCompleteHandler != null)
                        {
                            timerCompleteHandler.Invoke();

                        }
                    }
                    else
                    {
                        //����ѭ��
                        repeat = true;
                    }
                }
            }

        }

        /// <summary>
        /// ����timer
        /// </summary>
        public void StartTimer()
        {
            //����Э�̣�Э���Ѿ�����������timer��δִ�У������������⣬�����д���ȶ��
            running = true;
            timerCoroutine = StartCoroutine(RunTimer());
        }

        /// <summary>
        /// ֹͣtimer
        /// </summary>
        public void StopTimer()
        {
            running = false;
            StopCoroutine(timerCoroutine);
        }

        /// <summary>
        /// ����timer��ֹͣЭ��
        /// </summary>
        public void DestoryTimer()
        {
            running = false;
            repeat = false;
            repeatAlways = false;
            StopCoroutine(timerCoroutine);
        }


    }
}