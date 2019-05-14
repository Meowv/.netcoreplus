using Plus.Dependency;

namespace Plus
{
    public class PlusStarterOptions
    {
        public IIocManager IocManager { get; set; }

        public PlusStarterOptions()
        {
            IocManager = Dependency.IocManager.Instance;
        }
    }
}