using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Data;
using AutoMapper.Mappers;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.AutoMapper
{
    public class AutoMapperBootstrapper : IBootstrapper<bool>
    {
        private readonly Assembly[] _profileSourceAssemblies;

        public AutoMapperBootstrapper(Assembly[] profileSourceAssemblies)
        {
            _profileSourceAssemblies = profileSourceAssemblies;
        }

        public void Configure()
        {
            var profiles = GetInstancesOf<Profile>(_profileSourceAssemblies);

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

        public T[] GetInstancesOf<T>(Assembly[] assemblies)
        {
            var type = typeof(T);
            var instances = new List<T>();
            foreach (var assembly in assemblies)
            {
                var assemblyInstance = (
                    assembly.GetTypes()
                    .Where(t => type.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null)
                    .Select(Activator.CreateInstance)
                    .Cast<T>()
                    ).ToArray();
                instances.AddRange(assemblyInstance);
            }
            return instances.ToArray();
        }
    }
}