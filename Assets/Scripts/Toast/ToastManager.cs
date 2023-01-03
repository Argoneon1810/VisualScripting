using UnityEngine;

namespace Toast
{
    public class ToastManager : MonoBehaviour
    {
        public static ToastManager Instance;
        public GameObject toastPrefab;
        public Canvas toastCanvas;
        public AnimationCurve toastShowHideCurve;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public Toast MakeText(string text, float length = Toast.LENGTH_SHORT, ShowHideMode mode = ShowHideMode.Fade)
        {
            Toast toast = Instantiate(toastPrefab, toastCanvas.transform).GetComponent<Toast>();
            toast.text = text;
            toast.length = length;
            toast.toastShowHideCurve = toastShowHideCurve;
            toast.showHideMode = mode;
            return toast;
        }
    }
}