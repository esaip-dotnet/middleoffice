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
    //Classe permettant de s'identifier
    //On récupere à partir du JSON la partie authentification
    //On a une chaine de type Basic bG9naW46cGFzc3dvcmQ=
    //on retire le "Basic " pour n'avoir plus que la partie bG9naW46cGFzc3dvcmQ=
    //on décode cette partie avec la base 64
    //on optient login:password
    //on peut alors parser pour avoir le login et le mot de passe séparé.
    public class HeaderAuthorization
    {
        public static Tuple<bool, string> FailFastCheckAuthorization(HttpContext context)
        {
            StringValues authorizationToken;
            context.Request.Headers.TryGetValue("Authorization", out authorizationToken);
            if (StringValues.IsNullOrEmpty(authorizationToken)) return new Tuple<bool, string>(false, null);            
            if (authorizationToken.Count != 1) return new Tuple<bool, string>(false, null);

            string contenuHeadAuth = authorizationToken.First();
            Console.WriteLine(contenuHeadAuth);
            if (!contenuHeadAuth.StartsWith("Basic ")) return new Tuple<bool, string>(false, null);
            string tabCoupleIdentifBase64 = contenuHeadAuth.Substring(6);
            byte[] tabCoupleIdentif = Convert.FromBase64String(tabCoupleIdentifBase64);
            string coupleIdentif = Encoding.UTF8.GetString(tabCoupleIdentif);
            string[] tupleUserNamePassword = coupleIdentif.Split(":");
            string userName = tupleUserNamePassword[0];
            string password = tupleUserNamePassword[1];

            if (userName.Length == 0 || password.Length == 0) return new Tuple<bool, string>(false, userName);
            if (!userName.Equals("admin") || !password.Equals("salvia")) return new Tuple<bool, string>(false, userName);

            return new Tuple<bool, string>(true, userName);
        }
    }
}