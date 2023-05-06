using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.Helpers
{
    public class ClaimsHelper
    {
        public static string ProfilePictureClaim { get;} = "http://VideoShare/claims/ProfilePicturePath";
        public static string? getClaimValue(AuthenticationState context, string typeName) {
            string? claimValue = context.User.Claims.FirstOrDefault(x => x.Type.Equals(typeName, StringComparison.OrdinalIgnoreCase))?.Value;
            //If value is whitespace, null should be returned because claim has no value.
            //Then, coalescing the return value will allow individual components to respond to a null claim.
            return String.IsNullOrWhiteSpace(claimValue) ? null : claimValue;
        }
        public static int getUserID(AuthenticationState context)
        {
            try
            {
                string? userIDString = context.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
                int userID = Convert.ToInt32(userIDString);
                return userID;
            }
            catch {
                return 0;
            }
        }
    }
}
