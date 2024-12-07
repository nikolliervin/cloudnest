using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CloudNest.Api.Helpers
{
    public class UserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            if (_httpContextAccessor == null)
            {
                throw new InvalidOperationException("HttpContextAccessor is not set. Make sure to call SetHttpContextAccessor in Startup.");
            }

            var userClaims = _httpContextAccessor.HttpContext?.User?.Claims;
            var userIdClaim = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                return Guid.Parse(userIdClaim.Value);
            }

            throw new UnauthorizedAccessException("User is not authenticated.");
        }
    }
}