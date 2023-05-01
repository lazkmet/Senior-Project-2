using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.Helpers
{
    public class ClaimsHelper
    {
        public static string? getClaimValue(AuthenticationState context, string typeName) {
            string? claimValue = context.User.Claims.FirstOrDefault(x => x.Type.Equals(typeName, StringComparison.OrdinalIgnoreCase))?.Value;
            return claimValue;
        }
    }
}
