namespace DevKit.DIoC.Config
{
    public interface IBootConfig
    {
        public bool AutoConfig
        {
            get;
            set;
        }

        public AssemblyConfig[] Assemblies
        {
            get;
            set;
        }
    }
}
