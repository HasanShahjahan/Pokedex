using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pokedex.Api;
using Pokedex.Test.Resolver;
using Pokedex.Validator;
using System;
using Xunit;

namespace Pokedex.Test.Unit
{
    public class PayloadValidatorUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly string accessToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkZW50aXR5IjoiSGFzYW4iLCJuYmYiOjE2MTc5MDU4NjYsImV4cCI6MTYyMDQ5Nzg2NiwiaWF0IjoxNjE3OTA1ODY2fQ.o2rTundIHpSacmWA8hR130GGHtSWH9ufWbURBnSJ6G8";

        public PayloadValidatorUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void EmptyName()
        {
            var payloadValidator = _serviceProvider.GetService<IValidator>();
            var (statusCode, errorResult) = payloadValidator.PayloadValidator(accessToken, string.Empty);
            Assert.True(statusCode != StatusCodes.Status200OK);
        }

        [Fact]
        public void NotEmptyName()
        {
            var payloadValidator = _serviceProvider.GetService<IValidator>();
            var (statusCode, errorResult) = payloadValidator.PayloadValidator(accessToken, "Hasan");
            Assert.True(statusCode == StatusCodes.Status200OK);
        }
    }
}
