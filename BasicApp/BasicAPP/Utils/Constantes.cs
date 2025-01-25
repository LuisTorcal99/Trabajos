using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BasicAPP.Utils
{
    public static class Constantes
    {
        public const string BASE_LOCAL_DIRECTORY = "./FILES";
        public const string JSON_FILTER = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

        public const string BASE_URL = "https://localhost:7000/api/";
        public const string LOGIN_PATH = "users/login";
        public const string USERS_PATH = "users";
        public const string REGISTER_PATH = "users/register";
        public const string VOLANTES_PATH = "Volantes";

        public const string ERROR_TYC = "Debes aceptar los términos y condiciones";
        public const string ERROR_PASSWORDEQUALS = "Contraseñas distintas";
        public const string ERROR_CAMPOSNULL = "Tienes que rellenar todos los campos";
        public const string CAMBIOS_CONTRASEÑA = "Cambio de contraseña";

        public const string ROLE_REGISTRER_ADMIN = "Admin";
        public const string ROLE_REGISTRER_USER = "user";
    }
}
