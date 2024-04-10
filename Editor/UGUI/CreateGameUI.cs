﻿using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace IG.Editor.Extension.UGUI{
    public static class CreateGameUI{
        private const string kUILayerName = "UI";

        [MenuItem("GameObject/UI/Game Scroll View")]
        public static void CreateGameScrollView(MenuCommand menuCommand){
            //TODO:需要写一个ReleaseConfig，根据ReleaseConfig的状态set路径，或者直接ReleaseConfig里边就是路径
            string packagesPath = Application.dataPath + "/../Packages/ig.lib.uguiExtension/Res/EditorRes/UGUI/GameScrollView.prefab";
            string localPath    = "Assets/UnityUGUIExtension/Res/EditorRes/UGUI/GameScrollView.prefab";
            string loadPath = String.Empty;
            if (File.Exists(packagesPath)){
                loadPath = packagesPath;
            }
            else{
                loadPath = localPath;
            }

            GameObject gameObject   = AssetDatabase.LoadAssetAtPath<GameObject>(loadPath);
            gameObject      = GameObject.Instantiate(gameObject, Selection.activeTransform);
            gameObject.name = "GameScrollView";
            PlaceUIElementRoot(gameObject, menuCommand);
        }

        private static void PlaceUIElementRoot(GameObject element, MenuCommand menuCommand){
            GameObject parent = menuCommand.context as GameObject;
            if (parent == null || parent.GetComponentInParent<Canvas>() == null){
                parent = GetOrCreateCanvasGameObject();
            }

            string uniqueName = GameObjectUtility.GetUniqueNameForSibling(parent.transform, element.name);
            element.name = uniqueName;
            Undo.RegisterCreatedObjectUndo(element, "Create " + element.name);
            Undo.SetTransformParent(element.transform, parent.transform, "Parent " + element.name);
            GameObjectUtility.SetParentAndAlign(element, parent);
            Selection.activeGameObject = element;
        }

        // Helper function that returns a Canvas GameObject; preferably a parent of the selection, or other existing Canvas.
        static public GameObject GetOrCreateCanvasGameObject(){
            GameObject selectedGo = Selection.activeGameObject;

            // Try to find a gameobject that is the selected GO or one if its parents.
            Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
            if (canvas != null && canvas.gameObject.activeInHierarchy) return canvas.gameObject;

            // No canvas in selection or its parents? Then use just any canvas..
            canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
            if (canvas != null && canvas.gameObject.activeInHierarchy) return canvas.gameObject;

            // No canvas in the scene at all? Then create a new one.
            return CreateNewUI();
        }

        static public GameObject CreateNewUI(){
            // Root for the UI
            var root = new GameObject("Canvas");
            root.layer = LayerMask.NameToLayer(kUILayerName);
            Canvas canvas = root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            root.AddComponent<CanvasScaler>();
            root.AddComponent<GraphicRaycaster>();
            Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

            // if there is no event system add one...
            CreateEventSystem(false);
            return root;
        }

        private static void CreateEventSystem(bool select){ CreateEventSystem(select, null); }

        private static void CreateEventSystem(bool select, GameObject parent){
            var esys = Object.FindObjectOfType<EventSystem>();
            if (esys == null){
                var eventSystem = new GameObject("EventSystem");
                GameObjectUtility.SetParentAndAlign(eventSystem, parent);
                esys = eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
            }

            if (select && esys != null){
                Selection.activeGameObject = esys.gameObject;
            }
        }
    }
}