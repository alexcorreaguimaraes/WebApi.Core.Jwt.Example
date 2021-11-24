using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebApi.Core.Jwt.Example.Controllers
{
    public class BaseController : ControllerBase
    {
        [HttpOptions]

        public IActionResult PreflightRoute()
        {
            return NoContent();
        }

   

        protected int IdUsuario
        {
            get
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    int idUsuario;

                    bool resultado = Int32.TryParse(GetClaim(0, identity.Claims), out idUsuario);
                    if (resultado)
                        return idUsuario;
                    else
                        return 0;
                }

                return 0;
            }
        }

        protected string Login
        {
            get
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    return GetClaim(1, identity.Claims);
                }

                return null;
            }
        }

        private static string GetClaim(int itemArray, IEnumerable<Claim> claims)
        {
            int i = 0;
            string retorno = null;
            foreach (Claim item in claims)
            {
                if (itemArray == i)
                    retorno = item.Value.ToString();

                i++;
            }

            return retorno;
        }
    }
}


