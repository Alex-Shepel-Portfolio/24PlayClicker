namespace TFPlay.BuildTools.Builder
{
    public abstract class BaseBuildConfig
    {
        public bool developmentBuild;

        protected BaseBuildConfig(bool developmentBuild)
        {
            this.developmentBuild = developmentBuild;
        }
    }
}

