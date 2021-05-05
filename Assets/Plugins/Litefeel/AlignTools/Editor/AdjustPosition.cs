using UnityEditor;
using UnityEngine;

namespace litefeel.AlignTools
{
    public class AdjustPosition
    {
        internal static void Execute()
        {
            if (!Settings.AdjustPositionByKeyboard) return;

            //Debug.Log("DoKeyboard");
            var evt = Event.current;
            if (null == evt || evt.type != EventType.KeyDown) return;

            int ox = 0, oy = 0;
            switch (evt.keyCode)
            {
                case KeyCode.LeftArrow: ox = -1; break;
                case KeyCode.RightArrow: ox = 1; break;
                case KeyCode.UpArrow: oy = 1; break;
                case KeyCode.DownArrow: oy = -1; break;
            }
            if (ox != 0 || oy != 0)
            {
                evt.Use();
                var offset = new Vector2(ox, oy);
                if (evt.control) offset *= 10;
                foreach (var rt in Utils.GetRectTransforms())
                {
                    Undo.RecordObject(rt, "AdjustPosition");
                    rt.anchoredPosition += offset;
                }
            }
        }
    }
}


