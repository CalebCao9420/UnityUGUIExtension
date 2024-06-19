#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace IG.UGUIExtension{
    public class UGUIExtensionEditor{
        [MenuItem("GameObject/UI工具/UI/UIText", false, 10)]
        public static void CreateText(MenuCommand command){
            GameObject go = new GameObject("Text");
            GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
            var text = go.AddComponent<UIText>();
            // var outline = go.AddComponent<OutLineEx>();
            // text.font                    = AssetDatabase.LoadAssetAtPath<Font>("Assets/Game/Res/LocalRes/Fonts/jf-openhuninn-2.0.ttf");
            text.font                    = UGUIExtensionConfig.Instance.DefaultTextFont;
            text.fontSize                = 20;
            text.alignment               = TextAnchor.MiddleLeft;
            text.text                    = "Text it is using UIText now";
            text.rectTransform.sizeDelta = new Vector2(160, 36);
            text.enableOutline           = true;
            text.outlineCol              = Color.black;
            text.outlineWidth            = 1;
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
#endif