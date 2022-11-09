using UnityEditor;

namespace TFPlay.BuildTools.Builder
{
    public class AndroidBuildConfig : BaseBuildConfig
    {
        public bool buildAppBundle;
        public bool buildAndRun;
        public ScriptingImplementation scriptingBeckend;

        public AndroidBuildConfig(bool developmentBuild, bool buildAndRun, bool buildAppBundle, ScriptingImplementation scriptingBeckend) : base(developmentBuild)
        {
            this.buildAndRun = buildAndRun;
            this.buildAppBundle = buildAppBundle;
            this.scriptingBeckend = scriptingBeckend;
        }
    }
}

