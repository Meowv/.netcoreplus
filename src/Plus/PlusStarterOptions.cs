using Plus.Dependency;

namespace Plus
{
    /// <summary>
    /// PlusStarterOptions
    /// </summary>
    public class PlusStarterOptions
    {
        public IIocManager IocManager { get; set; }

        public PlusStarterOptions()
        {
            IocManager = Dependency.IocManager.Instance;
        }
    }
}