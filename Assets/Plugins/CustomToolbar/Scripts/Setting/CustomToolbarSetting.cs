using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
    internal class CustomToolbarSetting : ScriptableObject
    {
        const string SETTING_PATH = "Assets/Settings/CustomToolbarSetting.asset";

        [SerializeReference] internal List<BaseToolbarElement> elements = new List<BaseToolbarElement>();

        internal static CustomToolbarSetting GetOrCreateSetting()
        {
            var setting = AssetDatabase.LoadAssetAtPath<CustomToolbarSetting>(SETTING_PATH);
            if (setting == null)
            {
                setting = ScriptableObject.CreateInstance<CustomToolbarSetting>();
                setting.elements = GetDefaultToolbarElements();

                if (!Directory.Exists("Assets/Settings"))
                {
                    AssetDatabase.CreateFolder("Assets", "Settings");
                    AssetDatabase.SaveAssets();
                }

                AssetDatabase.CreateAsset(setting, SETTING_PATH);
                AssetDatabase.SaveAssets();
            }
            return setting;
        }

        internal static List<BaseToolbarElement> GetDefaultToolbarElements()
        {
            var elements = new List<BaseToolbarElement>()
            {
                new ToolbarSpace(),
                new ToolbarClearPrefs(),

                new ToolbarSides(),

                new ToolbarTimeslider(),
                new ToolbarSpace(),

                new ToolbarRecompile(),
            };
            return elements;
        }

        internal static SerializedObject GetSerializedSetting()
        {
            return new SerializedObject(GetOrCreateSetting());
        }
    }
}