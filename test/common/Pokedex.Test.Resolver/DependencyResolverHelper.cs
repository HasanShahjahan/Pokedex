using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Test.Resolver
{
    public class DependencyResolverHelper
    {
        private readonly IWebHost _webHost;

        public DependencyResolverHelper(IWebHost WebHost) => _webHost = WebHost;

        public T GetService<T>()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var scopedService = services.GetRequiredService<T>();
                    return scopedService;
                }
                catch (Exception e)
                {
                    throw e;
                }
            };
        }
    }
}
