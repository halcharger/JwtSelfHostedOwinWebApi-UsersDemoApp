using FluentAssertions;
using NUnit.Framework;
using service.Domain;
using service.Handlers;

namespace Tests
{
    [TestFixture]
    public class JwtTests
    {
        [Test]
        public void ShouldCorrectlyEncodeThenDecodeUserIntoJwt()
        {
            var user = new User
            {
                Id = 1,
                Name = "Allen",
                Username = "allenf",
                Email = "allen.firth@gmail.com",
                Role = "IT"
            };

            var token = JwtAuthenticationMessageHandler.CreateJWT(user);
            var decodedUser = JwtAuthenticationMessageHandler.GetUserFromJWT(token);

            decodedUser.Id.ShouldBeEquivalentTo(user.Id);
        }
    }
}