using System;
using AutoMapper;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.AutoMapper
{
    public class AutoMapperBootstrapper : IBootstrapper<MapperConfiguration>
    {
        private readonly Action<IMapperConfigurationExpression> _action;

        public AutoMapperBootstrapper(Action<IMapperConfigurationExpression> action)
        {
            _action = action;
        }

        public void Run()
        {
            var config = new MapperConfiguration(_action);
            Result = config;
        }

        public MapperConfiguration Result { get; private set; }
    }
}