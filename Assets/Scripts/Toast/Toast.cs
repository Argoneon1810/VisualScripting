using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Toast
{
    public enum ShowHideMode
    {
        Fade,
        Transform,
    }

    public class Toast : MonoBehaviour
    {
        public const float LENGTH_SHORT = 2;
        public const float LENGTH_LONG = 5;

        RectTransform mRectTransform;
        Image mask;
        Vector2 originalPosition;
        Color originalColorMask;
        Color originalColorText;
        Color transparentColorMask;
        Color transparentColorText;

        private float showSpeed = 1;

        public float length;
        public string text;
        public AnimationCurve toastShowHideCurve;
        public TextMeshProUGUI textMeshProUGUI;

        public ShowHideMode showHideMode;

        private void Awake()
        {
            mRectTransform = transform as RectTransform;

            originalPosition = mRectTransform.anchoredPosition;

            mask = GetComponent<Image>();

            originalColorMask = mask.color;
            originalColorText = textMeshProUGUI.color;
            transparentColorMask = originalColorMask - new Color(0, 0, 0, 1);
            transparentColorText = originalColorText - new Color(0, 0, 0, 1);
        }

        public void Show()
        {
            textMeshProUGUI.text = text;
            StartCoroutine(ShowCoroutine());
        }

        IEnumerator ShowCoroutine()
        {
            float t = 0;
            if (showHideMode == ShowHideMode.Transform)
            {
                Vector2 targetPosition = originalPosition + new Vector2(0, 200);
                while (t < 1)
                {
                    t += Time.deltaTime / showSpeed;
                    mRectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, toastShowHideCurve.Evaluate(t));
                    yield return null;
                }
            }
            else if (showHideMode == ShowHideMode.Fade)
            {
                originalPosition = mRectTransform.anchoredPosition += new Vector2(0, 200);
                while (t < 1)
                {
                    t += Time.deltaTime / showSpeed;
                    mask.color = Color.Lerp(transparentColorMask, originalColorMask, toastShowHideCurve.Evaluate(t));
                    textMeshProUGUI.color = Color.Lerp(transparentColorText, originalColorText, toastShowHideCurve.Evaluate(t));
                    yield return null;
                }
            }
            yield return new WaitForSeconds(length);
            StartCoroutine(HideCoroutine());
        }

        IEnumerator HideCoroutine()
        {
            float t = 0;
            if (showHideMode == ShowHideMode.Transform)
            {
                Vector2 fromPosition = mRectTransform.anchoredPosition;
                while (t < 1)
                {
                    t += Time.deltaTime / showSpeed;
                    mRectTransform.anchoredPosition = Vector2.Lerp(fromPosition, originalPosition, toastShowHideCurve.Evaluate(t));
                    yield return null;
                }
            }
            else if (showHideMode == ShowHideMode.Fade)
            {
                while (t < 1)
                {
                    t += Time.deltaTime / showSpeed;
                    mask.color = Color.Lerp(originalColorMask, transparentColorMask, toastShowHideCurve.Evaluate(t));
                    textMeshProUGUI.color = Color.Lerp(originalColorText, transparentColorText, toastShowHideCurve.Evaluate(t));
                    yield return null;
                }
            }
            Destroy(gameObject);
        }
    }
}