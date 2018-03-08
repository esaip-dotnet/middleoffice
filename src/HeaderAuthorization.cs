using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Primitives;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MiddleOffice
{
    // Cette classe permet de gérer l'identification à l'API
    public class HeaderAuthorization
    {
        // Fonction permettant de vérifier l'authentification
        public static Tuple<bool, string> FailFastCheckAuthorization(HttpContext context)
        {
            // Vérifiction du header Authorization
            StringValues authorizationToken;
            context.Request.Headers.TryGetValue("Authorization", out authorizationToken);
            if (StringValues.IsNullOrEmpty(authorizationToken)) return new Tuple<bool, string>(false, null);            
            if (authorizationToken.Count != 1) return new Tuple<bool, string>(false, null);

            // Récupération username et password à partir du Basic authentication
            string contenuHeadAuth = authorizationToken.First();
            Console.WriteLine(contenuHeadAuth);
            if (!contenuHeadAuth.StartsWith("Basic ")) return new Tuple<bool, string>(false, null);
            string tabCoupleIdentifBase64 = contenuHeadAuth.Substring(6);
            byte[] tabCoupleIdentif = Convert.FromBase64String(tabCoupleIdentifBase64);
            string coupleIdentif = Encoding.UTF8.GetString(tabCoupleIdentif);
            string[] tupleUserNamePassword = coupleIdentif.Split(":");
            string userName = tupleUserNamePassword[0];
            string password = tupleUserNamePassword[1];

            // Vérification couple username et password
            if (userName.Length == 0 || password.Length == 0) return new Tuple<bool, string>(false, userName);
            if (!userName.Equals("admin") || !password.Equals("salvia")) return new Tuple<bool, string>(false, userName);

            return new Tuple<bool, string>(true, userName);
        }
    }
}