using AutoMapper;
using System;
using System.Collections.Generic;

namespace Plus.AutoMapper
{
    /// <summary>
    /// PlusAutoMapperConfiguration
    /// </summary>
    public class PlusAutoMapperConfiguration : IPlusAutoMapperConfiguration
    {
        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public bool UseStaticMapper { get; set; }

        public PlusAutoMapperConfiguration()
        {
            UseStaticMapper = true;
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}