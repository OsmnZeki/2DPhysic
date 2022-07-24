using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Lib
{
    public static class DrawDebug
    {
        public class DalakDebugHelper : MonoBehaviour
        {
            public struct TextInfo
            {
                public string text;
                public int fontSize;
                public Vector3 pos;
                public Color color;
            }
            public readonly List<TextInfo> textInfos = new List<TextInfo>();

            readonly GUIStyle style = new GUIStyle();
#if UNITY_EDITOR

            int resetDrawCall = 10;
            void Update()
            {
                resetDrawCall--;
                if (resetDrawCall <= 0)
                {
                    textInfos.Clear();
                }
            }
            void OnDrawGizmos()
            {
                resetDrawCall = 10;
                for (int i = 0; i < textInfos.Count; i++)
                {
                    var text = textInfos[i];
                    style.normal.textColor = text.color;
                    style.fontSize = text.fontSize;
                    style.fontStyle = FontStyle.Bold;
                    UnityEditor.Handles.Label(text.pos, text.text, style);
                }
                if (!UnityEditor.EditorApplication.isPaused)
                {
                    textInfos.Clear();
                }
            }
#endif
        }
        
        static DalakDebugHelper helper;

        static DalakDebugHelper Helper
        {
            get
            {
                if (helper == null)
                {
                    helper = new GameObject("Dalak Debug Helper").AddComponent<DalakDebugHelper>();
                    GameObject.DontDestroyOnLoad(helper.gameObject);
                }

                return helper;
            }
        }
        
        public static void DrawText(string text, Vector3 pos, Color color, int fontSize = 16)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }

            Helper.textInfos.Add(new DalakDebugHelper.TextInfo()
            {
                text = text,
                color = color,
                pos = pos,
                fontSize = fontSize
            });
#endif

        }
        
        public static void DrawCircle(Vector2 center, float radius, Color color)
        {
            int nStep = 20;
            float perAngle = (float)360/nStep;
            
            var startPoint =new Vector2(center.x, center.y - radius);
            
            var oldPos = startPoint;
            
            for (int i = 0; i < nStep; i++)
            {
                var rotated = Quaternion.AngleAxis(i * perAngle, Vector3.forward);

                var currentPos = (Vector3)center + rotated * Vector2.down * radius;
                Debug.DrawLine(oldPos,currentPos,color);
                oldPos = currentPos;
            }
            
            Debug.DrawLine(oldPos,startPoint,color);
        }
        
    }
}


