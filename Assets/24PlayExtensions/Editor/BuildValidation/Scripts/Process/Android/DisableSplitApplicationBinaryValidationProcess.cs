using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TFPlay.BuildValidation
{
    public class DisableSplitApplicationBinaryValidationProcess : BaseAndroidValidationProcess
    {
        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking SplitApplicationBinary");
            if (PlayerSettings.Android.useAPKExpansionFiles)
            {
                OnValidationProcessFailed("Disable SplitApplicationBinary flag while building apk");
            }
        }
    }
}
