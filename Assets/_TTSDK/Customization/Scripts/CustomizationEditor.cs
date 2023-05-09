#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.XR;
using UnityEngine;

namespace TT.Customization
{
    public class CustomizationEditor : EditorWindow
    {
        [Serializable]
        private class CustomizationWrapper
        {
            public List<Customization> customizationDataList;
        }

        private Customization customizationData;
        private List<Customization> customizationList = new List<Customization>();
        private Vector2 scrollPos;

        GameObject character;

        bool load;

        [MenuItem("TT/Customization")]
        public static void ShowWindow()
        {
            var window = GetWindow<CustomizationEditor>();
            window.titleContent = new GUIContent("Customization System");
            window.minSize = new Vector2(2000, 700);
        }
        private void OnGUI()
        {
            if (customizationData == null)
                customizationData = new Customization();
            if (customizationList == null)
                customizationList = new List<Customization>();

            if (!load)
                LoadCustom();

            if (!character)
            {
                character = Instantiate(Resources.Load<GameObject>("CustomCharacter"));
                GameObject.FindObjectOfType<CustomizationManager>().character = character.GetComponent<Custom>().customCharacter;
            }

            if (GUILayout.Button("New Customization"))
            {
                customizationList.Add(customizationData);
                customizationData = new Customization();

                SaveCustom();
            }

            EditorGUILayout.Space(25);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            for (int i = 0; i < customizationList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(900));

                EditorGUILayout.LabelField("", GUILayout.Width(10));

                EditorGUILayout.BeginVertical();
                customizationList[i].skinColor = EditorGUILayout.ColorField("Skin Color", customizationList[i].skinColor, GUILayout.Width(250));
                customizationList[i].hairColor = EditorGUILayout.ColorField("Hair Color", customizationList[i].hairColor, GUILayout.Width(250));
                customizationList[i].beardColor = EditorGUILayout.ColorField("Beeard Color", customizationList[i].beardColor, GUILayout.Width(250));
                customizationList[i].tshirtColor = EditorGUILayout.ColorField("T-shirt Color", customizationList[i].tshirtColor, GUILayout.Width(250));
                customizationList[i].pantColor = EditorGUILayout.ColorField("Pant Color", customizationList[i].pantColor, GUILayout.Width(250));
                customizationList[i].shoeColor = EditorGUILayout.ColorField("Shoe Color", customizationList[i].shoeColor, GUILayout.Width(250));
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                customizationList[i].hair = EditorGUILayout.Toggle("Hair", customizationList[i].hair, GUILayout.Width(250));
                customizationList[i].tshirt = EditorGUILayout.Toggle("T-shirt", customizationList[i].tshirt, GUILayout.Width(250));
                customizationList[i].belt = EditorGUILayout.Toggle("Belt", customizationList[i].belt, GUILayout.Width(250));
                customizationList[i].beard1 = EditorGUILayout.Toggle("Beard 1", customizationList[i].beard1, GUILayout.Width(250));
                customizationList[i].beard2 = EditorGUILayout.Toggle("Beard 2", customizationList[i].beard2, GUILayout.Width(250));
                customizationList[i].beard3 = EditorGUILayout.Toggle("Beard 3", customizationList[i].beard3, GUILayout.Width(250));
                customizationList[i].mustache1 = EditorGUILayout.Toggle("Mustache 1", customizationList[i].mustache1, GUILayout.Width(250));
                customizationList[i].mustache2 = EditorGUILayout.Toggle("Mustache 2", customizationList[i].mustache2, GUILayout.Width(250));
                customizationList[i].mustache3 = EditorGUILayout.Toggle("Mustache 3", customizationList[i].mustache3, GUILayout.Width(250));
                EditorGUILayout.EndVertical();

                EditorGUILayout.LabelField("", GUILayout.Width(10));

                EditorGUILayout.BeginVertical();
                customizationList[i].leftEarning1 = EditorGUILayout.Toggle("Left Earning 1", customizationList[i].leftEarning1, GUILayout.Width(250));
                customizationList[i].leftEarning2 = EditorGUILayout.Toggle("Left Earning 2", customizationList[i].leftEarning2, GUILayout.Width(250));
                customizationList[i].leftEarning3 = EditorGUILayout.Toggle("Left Earning 3", customizationList[i].leftEarning3, GUILayout.Width(250));
                customizationList[i].rightEarning1 = EditorGUILayout.Toggle("Right Earning 1", customizationList[i].rightEarning1, GUILayout.Width(250));
                customizationList[i].rightEarning2 = EditorGUILayout.Toggle("Right Earning 2", customizationList[i].rightEarning2, GUILayout.Width(250));
                customizationList[i].rightEarning3 = EditorGUILayout.Toggle("Right Earning 3", customizationList[i].rightEarning3, GUILayout.Width(250));
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();

                if (i != 0)
                {
                    if (GUILayout.Button("Delete", GUILayout.Width(900)))
                    {
                        customizationList.RemoveAt(i);

                        SaveCustom();
                    }
                }

                if (GUILayout.Button("Apply", GUILayout.Width(900)))
                {
                    SaveCustom();

                    GameObject.FindObjectOfType<CustomizationManager>().customizationDataList = customizationList;
                    GameObject.FindObjectOfType<CustomizationManager>().Apply(customizationList[i]);
                }

            }
            EditorGUILayout.EndVertical();

            GUI.DrawTexture(new Rect(500, scrollPos.y - 50, position.width, position.height - 20), character.GetComponent<Camera>().targetTexture, ScaleMode.ScaleToFit);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

        public void SaveCustom()
        {
            CustomizationWrapper customizationWrapper = new CustomizationWrapper();
            customizationWrapper.customizationDataList = customizationList;
            string jsonData = JsonUtility.ToJson(customizationWrapper);
            File.WriteAllText(Application.dataPath + "/_TTSDK/Customization/customization.json", jsonData);
        }

        public void LoadCustom()
        {
            if (File.Exists(Application.dataPath + "/_TTSDK/Customization/customization.json"))
            {
                string jsonData = File.ReadAllText(Application.dataPath + "/_TTSDK/Customization//customization.json");
                CustomizationWrapper customizationWrapper = JsonUtility.FromJson<CustomizationWrapper>(jsonData);
                customizationList = customizationWrapper.customizationDataList;
            }

            load = true;
        }

        private void OnDisable()
        {
            DestroyImmediate(character.gameObject);
        }
    }
}
#endif