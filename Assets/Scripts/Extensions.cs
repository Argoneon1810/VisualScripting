using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace EaseOfUse
{
    namespace CanvasScale
    {
        public class CanvasScale
        {
            static Vector2 half = new Vector2(.5f, .5f);

            public static float GetWidthMatchingScaler(CanvasScaler scaler) => Mathf.Lerp(
                scaler.referenceResolution.x,
                scaler.referenceResolution.y * Camera.main.aspect,
                scaler.matchWidthOrHeight
            );

            public static float GetHeightMatchingScaler(CanvasScaler scaler) => Mathf.Lerp(
                scaler.referenceResolution.x / Camera.main.aspect,
                scaler.referenceResolution.y,
                scaler.matchWidthOrHeight
            );

            public static Vector2 GetMousePositionInCanvas(Vector2 mousePos, Canvas targetCanvas)
            {
                CanvasScaler scaler = targetCanvas.GetComponent<CanvasScaler>();

                float scaledCanvasWidth = GetWidthMatchingScaler(scaler);
                float scaledCanvasHeight = GetHeightMatchingScaler(scaler);

                mousePos.x = mousePos.x / Screen.width * scaledCanvasWidth - scaledCanvasWidth / 2;
                mousePos.y = mousePos.y / Screen.height * scaledCanvasHeight - scaledCanvasHeight / 2;

                return mousePos;
            }
            public static Vector3 GetCenterAnchoredPosition(Vector2 pos, Vector2 anchorMin, Vector2 anchorMax, Canvas targetCanvas)
            {
                if (anchorMin == half && anchorMax == half)
                    return pos;

                CanvasScaler scaler = targetCanvas.GetComponent<CanvasScaler>();

                float scaledCanvasWidth = GetWidthMatchingScaler(scaler);
                float scaledCanvasHeight = GetHeightMatchingScaler(scaler);

                var trueCenter = anchorMin + (anchorMax - anchorMin) / 2;
                var offset = half - trueCenter;

                return pos - new Vector2(offset.x * scaledCanvasWidth, offset.y * scaledCanvasHeight);
            }
        }
    }
    namespace VectorCalculation
    {
        public static class Extensions
        {
            public static float Cross(this Vector2 pointA, Vector2 pointB)
                => (pointA.x * pointB.y) - (pointB.x * pointA.y);

            public static Vector2 Tangent(this Vector2 normalizedDir)
                => Vector3.Cross(Vector3.forward, normalizedDir);
            public static Vector3 Tangent(this Vector3 normalizedDir, Vector3 baseAxis)
                => Vector3.Cross(baseAxis, normalizedDir);

            public static bool IsLeftOf(this Vector2 pointA, Vector2 pointB) =>
                (pointB - pointA).normalized.Cross(Vector2.up) < 0;
            public static bool IsLeftOf(this Vector3 pointA, Vector3 pointB, Vector3 baseAxis)
                => (pointB - pointA).normalized.Tangent(baseAxis).GetNonZeroElement() < 0;

            public static float GetNonZeroElement(this Vector3 target)
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (i == 0 && target.x != 0)
                        return target.x;
                    if (i == 1 && target.y != 0)
                        return target.y;
                    if (i == 2 && target.z != 0)
                        return target.z;
                }
                return 0;
            }

            public static Vector2 ToVector2XY(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);

            public static RectTransform rt(this Transform transform) => transform as RectTransform;
        }

        public class VectorCalculation
        {
            public static bool Similar(Vector3 left, Vector3 right, float threshold)
            {
                if ((left - right).sqrMagnitude <= (threshold * threshold)) return true;
                return false;
            }

            public static float SignedAngle(Vector2 pointA, Vector2 pointB)
            {
                return pointA.IsLeftOf(pointB) ? -UnsignedAngle(pointA, pointB) : UnsignedAngle(pointA, pointB);
            }

            public static float SignedAngle(Vector3 pointA, Vector3 pointB, Vector3 baseAxis)
            {
                return pointA.IsLeftOf(pointB, baseAxis) ? -UnsignedAngle(pointA, pointB, baseAxis) : UnsignedAngle(pointA, pointB, baseAxis);
            }

            public static float UnsignedAngle(Vector2 pointA, Vector2 pointB)
            {
                return Mathf.Acos(Vector2.Dot((pointB - pointA).normalized, Vector2.up)) * Mathf.Rad2Deg;
            }

            public static float UnsignedAngle(Vector3 pointA, Vector3 pointB, Vector3 baseAxis)
            {
                return Mathf.Acos(Vector3.Dot((pointB - pointA).normalized, baseAxis)) * Mathf.Rad2Deg;
            }
        }
    }
    namespace Console
    {
        public class Console
        {
            public static void ClearLog()
            {
                var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
                var type = assembly.GetType("UnityEditor.LogEntries");
                var method = type.GetMethod("Clear");
                method.Invoke(new object(), null);
            }
        }
    }
}