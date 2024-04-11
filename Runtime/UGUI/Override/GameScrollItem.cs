using UnityEngine;

namespace IG.Runtime.Extension.UGUI{
    public abstract class GameScrollItem : MonoBehaviour{
        protected object _Obj;

        public void SetData(IGameScrollItemData data){
            bool isRepeatRefresh = _Obj == data;
            _Obj = data;
            SetData(isRepeatRefresh);
        }

        protected abstract void  SetData(bool isRepeatRefresh);
        public abstract    float GetWidth();
        public abstract    float GetHeight();
    }

    /// <summary>
    /// Scroll through the list item data source interface.
    /// </summary>
    public interface IGameScrollItemData{
    }
}