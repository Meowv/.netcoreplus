using AutoMapper;

namespace Plus.AutoMapper
{
    /// <summary>
    /// IAutoMapRegistrar
    /// </summary>
    public interface IAutoMapRegistrar
    {
        void RegisterMaps(IMapperConfigurationExpression config);
    }
}