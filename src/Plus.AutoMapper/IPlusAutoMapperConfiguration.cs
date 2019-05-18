using AutoMapper;
using System;
using System.Collections.Generic;

namespace Plus.AutoMapper
{
    /// <summary>
    /// IPlusAutoMapperConfiguration
    /// </summary>
    public interface IPlusAutoMapperConfiguration
    {
        List<Action<IMapperConfigurationExpression>> Configurators { get; }

        /// <summary>
        /// Use static <see cref="Mapper.Instance"/>.
        /// Default: true.
        /// </summary>
        bool UseStaticMapper { get; set; }
    }
}