namespace Plus.Dependency
{
    public interface IDependencyRegistrar
    {
        void RegisterAssembly(IRegistrationContext context);
    }
}