using UnityEngine;

namespace IG.Runtime.Extension.UGUI{
    public static class UIExtension{
        public static RectTransform UIRect(this Transform tr){
            return tr as RectTransform;
        }

        public static RectTransform UIRect(this GameObject obj){
            return obj.transform as RectTransform;
        }

        public static RectTransform UIRect(this MonoBehaviour mono){
            return mono.transform as RectTransform;
        }
    }
}