using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace com.beiyou.snake.gameclient.ui
{
    public class LoaderBigImageUI : MonoBehaviour
    {

        private RectTransform m_rectTransform;
        private GameObject img;

        private void Awake()
        {
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
            img = new GameObject("loaderBigImageUI");
            img.transform.SetParent(gameObject.transform);
            img.AddComponent<RawImage>();
            RectTransform n_rectTransform = img.GetComponent<RectTransform>();
            // 设置中心点为左上角
            n_rectTransform.pivot = new Vector2(0, 1);
            // 设置锚点为左上角
            n_rectTransform.anchorMin = new Vector2(0, 1);
            n_rectTransform.anchorMax = new Vector2(0, 1);
            n_rectTransform.sizeDelta = new Vector2(100, 100);
        }
        public RectTransform rectTransform
        {
            get
            {
                return m_rectTransform;
            }
        }
        public void ShowBigImage(string url)
        {
            StartCoroutine(LoadImageFromURL(url));
        }

        IEnumerator LoadImageFromURL(string url)
        {
            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // 图片加载成功
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                img.GetComponent<RawImage>().texture = texture;


            }
            else
            {
                // 加载失败，处理错误
                Debug.LogError("Failed to load image: " + www.error);
            }
        }


    }
}
