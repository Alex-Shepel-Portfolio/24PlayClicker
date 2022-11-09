using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class EnableKeystoreValidationProcess : BaseAndroidValidationProcess
    {
        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking Keystore");
            bool keystoreEnabled = PlayerSettings.Android.useCustomKeystore;
            if (!keystoreEnabled)
            {
                OnValidationProcessFailed("Enable Custom Keystore flag");
            }
        }
    }
}
