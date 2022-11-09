using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TFPlay.BuildIncrementor
{
    [CustomEditor(typeof(BuildVersionSettings))]
    public class BuildVersionSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var buildVersionSettings = (BuildVersionSettings)target;
            GUILayout.Space(12);
            if (GUILayout.Button("Equalize Build Version", GUILayout.Height(40)))
            {
                buildVersionSettings.EqualizeBuildVersion();
            }
        }
    }
}
