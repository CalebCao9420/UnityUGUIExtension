using System;
using System.Collections.Generic;
using IG.Runtime.Log;
using IG.Runtime.Utils;
using UnityEngine;

namespace IG.UGUIExtension{
    public static class LanguageManagerWrap{
        private static ILanguageManager _languageManager;

        public static SystemLanguage CurrenLang{
            get{
                JudgeManagerExist();
                return _languageManager.CurrenLang;
            }
            set{
                JudgeManagerExist();
                _languageManager.CurrenLang = value;
            }
        }

        public static string GetLangText(string key){
            JudgeManagerExist();
            return _languageManager.GetLangText(key);
        }

        public static string EditorGetLangText(string key, SystemLanguage language = SystemLanguage.English){
            if (string.IsNullOrWhiteSpace(key)){
                return string.Empty;
            }

            //TODO:加了配置后修改
            // var map = EditorLoadFile(language);
            // if (map != null)
            // {
            //     if (map.TryGetValue(key, out string value))
            //     {
            //         return value;
            //     }
            // }
            return key;
        }

        private static void JudgeManagerExist(){
            if (_languageManager == null){
                List<System.Type> implementingTypes = ADFUtils.GetImplementingTypes(typeof(ILanguageManager)) as List<System.Type>;
                if (implementingTypes is not{ Count: > 0 }){
                    LogHelper.Log($"没有找到ILanguageManager继承类,加载错误:{implementingTypes}");
                    return;
                }

                _languageManager = Activator.CreateInstance(implementingTypes[0]) as ILanguageManager;
            }
        }
    }
}