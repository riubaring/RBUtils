using System.Security.Claims;

namespace RBUtils.Extension
{
    public static class ClaimsEx
    {
        private static readonly List<string> _txtAdmins = new() { "Admin", "Admins", "Adminstratrator" };
        private const string _txtPermission = "Permission";

        /// <summary>
        /// To verify whether or not a user has the claim match the permission indicated.
        /// </summary>
        /// <param name="user">Claims Principal to be extended.</param>
        /// <param name="permission">A Permission string to be verified i.e.: create-user, read-user, etc.</param>
        /// <returns>True, if the user has the claim matches the permission indicated. Otherwise, false.</returns>
        public static bool HasPermission(this ClaimsPrincipal user, string permission)
        {
            return user.HasClaim(r => r.Type == _txtPermission && r.Value == permission);
        }

        /// <summary>
        /// To get the user's UserId claim that corresponds to the Id column of ApplicationUser.
        /// </summary>
        /// <param name="user">Claims Principal to be extended.</param>
        /// <returns>Non-zero if the user has the UserId claim. Otherwise, 0.</returns>
        public static int Id(this ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.FirstOrDefault(r => r.Type == "UserId")?.Value ?? "1");
        }

        /// <summary>
        /// To verify whether or not a user is in admin role.
        /// </summary>
        /// <param name="user">Claims Principal to be extended.</param>
        /// <returns>True, if the user is in admin role. Otherwise, false.</returns>
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            foreach (string txtAdmin in _txtAdmins)
            {
                if (user.IsInRole(txtAdmin) || user.IsInRole(txtAdmin.ToLower()) || user.IsInRole(txtAdmin.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// To veriy whether or not a user is a Manager by querying the IsManager claim. IsManager column is set to true in ApplicationUsers.
        /// </summary>
        /// <param name="user">Claims Principal to be extended.</param>
        /// <returns>True, if the user has the IsManager claim set to "true." Otherwise, false.</returns>
        public static bool IsManager(this ClaimsPrincipal user)
        {
            return bool.Parse(user.Claims.FirstOrDefault(r => r.Type == "IsManager")?.Value ?? "false");
        }

        /// <summary>
        /// To get the user's LocationId claim that corresponds to the LocationId column in ApplicationUsers.
        /// </summary>
        /// <param name="user">Claims Principal to be extended.</param>
        /// <returns>Non-zero if the user has the LocationId claim. Otherwise, 0.</returns>
        public static int LocationId(this ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.FirstOrDefault(r => r.Type == "LocationId")?.Value ?? "1");
        }

        /// <summary>
        /// To get the user's RemoteIpAddress claim.
        /// </summary>
        /// <param name="user">Claims Principal to be extended.</param>
        /// <returns>A string value of user's remote ip address.</returns>
        public static string RemoteIpAddress(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(r => r.Type == "RemoteIpAddress")?.Value ?? "";
        }
    }
}
