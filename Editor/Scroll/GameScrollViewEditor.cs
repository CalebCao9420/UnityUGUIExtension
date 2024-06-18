#if UNITY_EDITOR
using IG.Runtime.Extension.UGUI;
using UnityEditor;

namespace IG.Editor.Extension.UGUI{
    [CustomEditor(typeof(GameScrollView), true)]
    public class GameScrollViewEditor : UnityEditor.Editor{
    }
}
#endif