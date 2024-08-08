using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace com.beiyou.snake.common.res
{
    public class CommonTimer : MonoBehaviour
    {
        private bool repeatAlways = false;//是否一直循环
        private bool repeat = true;//是否继续循环执行(这个标识是通过循环次数来确定)

        private Coroutine timerCoroutine;

        private UnityAction timerHandler;//计时器跳动回调
        private UnityAction timerCompleteHandler;//计时器结束回调
        private float delay;
        private long repeatCount;

        private bool running = false;//是否启动计时器(这个标识就是计时器的开关)
        public bool Running { get => running; set => running = value; }

        /// <summary>
        /// 创建协程方法
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="repeatCount"></param>
        public void InitTimer(float delay, long repeatCount = -1)
        {
            this.delay = delay;
            this.repeatCount = repeatCount;

            this.running = false;//默认不启动计时器
            this.repeat = true;//默认至少执行一次

            //如果循环次数是-1.就一直循环
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
        /// 添加计时器跳动事件
        /// </summary>
        /// <param name="callback"></param>
        public void AddTimerEventListener(UnityAction timerHandler)
        {
            this.timerHandler = timerHandler;
        }

        /// <summary>
        /// 添加计时器完成事件
        /// </summary>
        /// <param name="timerCompleteHandler"></param>
        public void AddTimerCompleteEventListener(UnityAction timerCompleteHandler)
        {
            this.timerCompleteHandler = timerCompleteHandler;
        }

        /// <summary>
        /// 启动timer协程
        /// </summary>
        /// <returns></returns>
        IEnumerator RunTimer()
        {
            //是否执行
            while (running && repeat)
            {
                //延迟指定的时间
                yield return new WaitForSeconds(delay);

                //执行计时器跳动操作
                if (timerHandler != null)
                {
                    timerHandler.Invoke();
                }

                //检查是否继续循环
                if (repeatAlways == true)//一直循环
                {
                    repeat = true;
                }
                else//循环指定的次数
                {
                    repeatCount--;
                    if (repeatCount <= 0)
                    {
                        //停止循环
                        repeat = false;

                        //执行计时器结束操作
                        if (timerCompleteHandler != null)
                        {
                            timerCompleteHandler.Invoke();

                        }
                    }
                    else
                    {
                        //继续循环
                        repeat = true;
                    }
                }
            }

        }

        /// <summary>
        /// 启动timer
        /// </summary>
        public void StartTimer()
        {
            //启动协程（协程已经启动，但是timer并未执行，考虑性能问题，这里有待商榷）
            running = true;
            timerCoroutine = StartCoroutine(RunTimer());
        }

        /// <summary>
        /// 停止timer
        /// </summary>
        public void StopTimer()
        {
            running = false;
            StopCoroutine(timerCoroutine);
        }

        /// <summary>
        /// 销毁timer，停止协程
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