using AutoMapper;
using System.Linq;
using System;
using MTFS.Business.Bootstrapper.MapProfiles;

namespace MTFS.Business.Bootstrapper.BootstrapTask
{
    public  static class ConfigureAutoMapper  
    { 
        public static  void Execute()
        {
            var profiles = typeof(BusinessProfile).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            foreach (var profile in profiles)
            {
                Mapper.Initialize(x =>
                {
                    x.AddProfile(Activator.CreateInstance(profile) as Profile);
                });
            } 

        } 
    }
}