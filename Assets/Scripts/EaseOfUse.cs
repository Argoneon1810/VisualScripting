using System;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace EaseOfUse
{
    public static class Extensions
    {
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

        public static Color WithAlpha(this Color color, float a)
        {
            color.a = a;
            return color;
        }

        public static Vector2 ToVector2XY(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);

        public static Vector3 ToVector3(this Vector2 vector2) => vector2;

        public static RectTransform GetRTOrDefault(this Transform transform) => transform as RectTransform;
    }

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
        public class VectorCalculation
        {
            public static bool Similar(Vector3 left, Vector3 right, float threshold)
                => (left - right).sqrMagnitude <= (threshold * threshold);

            public static float SignedAngle2D(Vector2 pointA, Vector2 pointB)
                => IsBLeftOfA2D(pointA, pointB) ? -UnsignedAngle2D(pointA, pointB) : UnsignedAngle2D(pointA, pointB);
            public static float SignedAngle(Vector3 pointA, Vector3 pointB, Vector3 baseAxis)
                => IsBLeftOfA(pointA, pointB, baseAxis) ? -UnsignedAngle(pointA, pointB, baseAxis) : UnsignedAngle(pointA, pointB, baseAxis);

            public static float UnsignedAngle2D(Vector2 pointA, Vector2 pointB)
                => Mathf.Acos(Vector2.Dot((pointB - pointA).normalized, Vector2.up)) * Mathf.Rad2Deg;
            public static float UnsignedAngle(Vector3 pointA, Vector3 pointB, Vector3 baseAxis)
                => Mathf.Acos(Vector3.Dot((pointB - pointA).normalized, baseAxis)) * Mathf.Rad2Deg;

            public static float Cross2D(Vector2 pointA, Vector2 pointB)
                => (pointA.x * pointB.y) - (pointB.x * pointA.y);

            public static Vector2 Tangent2D(Vector2 normalizedDir)
                => Vector3.Cross(Vector3.forward, normalizedDir);
            public static Vector3 Tangent(Vector3 normalizedDir, Vector3 baseAxis)
                => Vector3.Cross(baseAxis, normalizedDir);

            public static bool IsBLeftOfA2D(Vector2 pointA, Vector2 pointB)
                => Cross2D((pointB - pointA).normalized, Vector2.up) < 0;
            public static bool IsBLeftOfA(Vector3 pointA, Vector3 pointB, Vector3 baseAxis)
                => Tangent((pointB - pointA).normalized, baseAxis).GetNonZeroElement() < 0;
        }
    }
    namespace ConsoleExpansion
    {
        public class ConsoleExpansion
        {
            public static void ClearLog()
            {
                var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
                var type = assembly.GetType("UnityEditor.LogEntries");
                var method = type.GetMethod("Clear");
                method.Invoke(new object(), null);
            }

            public static void Print(params object[] args) => Log(ArgsToString(args), Debug.Log);
            public static void PrintError(params object[] args) => Log(ArgsToString(args), Debug.LogError);

            private static void Log(string s, Action<string> print) => print?.Invoke(s);
            private static string ArgsToString(object[] args)
            {
                StringBuilder sb = new StringBuilder();
                foreach (object arg in args)
                    sb.Append(arg.ToString());
                return sb.ToString();
            }
        }
    }
    namespace BooleanTrigger
    {
        public class BooleanTrigger
        {
            public static bool Trigger(ref bool flag)
            {
                if (flag)
                {
                    flag = false;
                    return true;
                }
                return false;
            }
        }
    }
}