using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JWT;
using log4net;
using Newtonsoft.Json;
using service.Domain;

namespace service.Handlers
{
    public class JwtAuthenticationMessageHandler : DelegatingHandler
    {
        private const string BearerScheme = "Bearer";
        private const string SecretKey = "9fdb64ec22e9cda94ad9964d07dd9dd16ee8c41a8e0d4367e1d09d2520ae583cb8357a992edb76a74a4b866243580b6106f6497733641f588f8608f391443138";

        private readonly ILog _logger = LogManager.GetLogger("JwtAuthenticationMessageHandler");

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authHeader = request.Headers.Authorization;
            if (authHeader == null)
            {
                _logger.Info("Missing authorization header");
                return base.SendAsync(request, cancellationToken);
            }

            if (authHeader.Scheme != BearerScheme)
            {
                _logger.InfoFormat(
                    "Authorization header scheme is {0}; needs to {1} to be handled as a JWT.",
                    authHeader.Scheme,
                    BearerScheme);
                return base.SendAsync(request, cancellationToken);
            }

            var tokenString = authHeader.Parameter;

            try
            {
                var user = GetUserFromJWT(tokenString);
                var principal = new ClaimsPrincipal(user.ToClaimsIdentity()); 

                request.GetRequestContext().Principal = principal;

                _logger.DebugFormat("Thread principal set with identity '{0}'", principal.Identity.Name);
            }
            catch (SignatureVerificationException signatureEx)
            {
                _logger.WarnFormat("Invalid JWT token found in Authorization header of request.");
            }
            catch (Exception e)
            {
                _logger.ErrorFormat("Error during JWT validation: {0}", e);
                throw;
            }

            return base.SendAsync(request, cancellationToken);
        }

        public static string CreateJWT(User user)
        {
            return JsonWebToken.Encode(user, SecretKey, JwtHashAlgorithm.HS512);
        }

        public static User GetUserFromJWT(string jwt)
        {
            var jsonPayload = JsonWebToken.Decode(jwt, SecretKey);
            return JsonConvert.DeserializeObject<User>(jsonPayload);
        }

    }
}