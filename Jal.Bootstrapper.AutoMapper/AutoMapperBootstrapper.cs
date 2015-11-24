using AutoMapper;
using AutoMapper.Data;
using AutoMapper.Mappers;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.AutoMapper
{
    public class AutoMapperBootstrapper : IBootstrapper<bool>
    {
        public void Configure()
        {
            var assemblies = AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("Map");
            var profiles = AssemblyFinder.Impl.AssemblyFinder.Current.GetInstancesOf<Profile>(assemblies);
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