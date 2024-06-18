using UnityEngine;

namespace IG.UGUIExtension{
    public interface ILanguageManager{
        SystemLanguage CurrenLang{ get; set; }
        string         GetLangText(string key);
    }
}