using UnityEditor;
using UnityEngine;

namespace litefeel.AlignTools
{
    public static class Settings
    {
        private const string AdjustPositionByKeyboardKey = "litefeel.AlignTools.AdjustPositionByKeyboard";
        private const string ShowRulerKey = "litefeel.AlignTools.ShowRuler";
        private const string RulerLineColorKey = "litefeel.AlignTools.RulerLineColor";


        [InitializeOnLoadMethod]
        private static void Init()
        {
            _AdjustPositionByKeyboard = EditorPrefs.GetBool(AdjustPositionByKeyboardKey, false);
            _ShowRuler = EditorPrefs.GetBool(ShowRulerKey, false);

            var ruleLineColorStr = EditorPrefs.GetString(RulerLineColorKey, null);
            var ruleLineColor = Color.white;
            if (!ColorUtility.TryParseHtmlString(ruleLineColorStr, out ruleLineColor))
                ruleLineColor = Color.white;
            _RulerLineColor = ruleLineColor;
        }

        private static bool _AdjustPositionByKeyboard;
        public static bool AdjustPositionByKeyboard
        {
            get { return _AdjustPositionByKeyboard; }
            set
            {
                if (value != _AdjustPositionByKeyboard)
                {
                    _AdjustPositionByKeyboard = value;
                    EditorPrefs.SetBool(AdjustPositionByKeyboardKey, value);
                }
            }
        }

        private static bool _ShowRuler;
        public static bool ShowRuler
        {
            get { return _ShowRuler; }
            set
            {
                if (value != _ShowRuler)
                {
                    _ShowRuler = value;
                    EditorPrefs.SetBool(ShowRulerKey, value);
                }
            }
        }

        private static Color _RulerLineColor;
        public static Color RulerLineColor
        {
            get { return _RulerLineColor; }
            set
            {
                if (value != _RulerLineColor)
                {
                    _RulerLineColor = value;
                    EditorPrefs.SetString(RulerLineColorKey, "#" + ColorUtility.ToHtmlStringRGBA(value));
                }
            }
        }
    }
}


