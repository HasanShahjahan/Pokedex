using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Pokedex.Api;
using Pokedex.Security.Handlers;
using Pokedex.Test.Resolver;
using Xunit;

namespace Pokedex.Test.Unit
{
    public class JwtTokenHandlerUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly string accessToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkZW50aXR5IjoiSGFzYW4iLCJuYmYiOjE2MTc5MDU4NjYsImV4cCI6MTYyMDQ5Nzg2NiwiaWF0IjoxNjE3OTA1ODY2fQ.o2rTundIHpSacmWA8hR130GGHtSWH9ufWbURBnSJ6G8";

        public JwtTokenHandlerUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void GenerateAccessToekn()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.GenerateJwtSecurityToken("Hasan");
            Assert.NotEmpty(token);
        }

        [Fact]
        public void PrepareTokenFromAccessToekn()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void VerifyJwtSecurityToken()
        {
            var jwtTokenHandler = _serviceProvider.GetService<IJwtTokenHandler>();
            string token = jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
            var (isVerified, userId) = jwtTokenHandler.VerifyJwtSecurityToken(token);
            Assert.True(isVerified || !string.IsNullOrEmpty(userId));
        }
    }
}
