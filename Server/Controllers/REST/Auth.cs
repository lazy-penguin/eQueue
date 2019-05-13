using DataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;

namespace Server.Controllers
{
    public static class Auth
    {
        public static int CheckToken(HttpRequestHeaders headers)
        {
            string token = null;
            if (headers.Contains("Authorization"))
            {
                token = headers.GetValues("Authorization").First();
            }

            if (token == null)
                return 0;

            var userId = TokenManager.GetUserId(token);
            return userId;
        }
        public static bool CheckAccess(int userId, int queueId)
        {
            return UserAccessManager.CheckAccess(userId, queueId);
        }
        public static bool CheckPrivilegedAccess(int userId, int queueId)
        {
            return UserAccessManager.CheckPrivilegedAccess(userId, queueId);
        }

    }
}