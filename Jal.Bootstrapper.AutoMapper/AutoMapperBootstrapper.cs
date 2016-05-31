using AutoMapper;
using AutoMapper.Data;
using AutoMapper.Mappers;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.AutoMapper
{
    public class AutoMapperBootstrapper : IBootstrapper<bool>
    {
        private readonly Profile[] _profiles;

        public AutoMapperBootstrapper(Profile[] profiles)
        {
            _profiles = profiles;
        }

        public void Configure()
        {
            var profiles = _profiles;

            Mapper.Initialize(a =>
                              {
                                  foreach (var profile in profiles)
                                  {
                                      a.AddProfile(profile);
                                  }
                                  MapperRegistry.Mappers.Add(new DataReaderMapper { YieldReturnEnabled = true });
                              });
            Result = true;
        }

        public bool Result { get; private set; }
    }
}