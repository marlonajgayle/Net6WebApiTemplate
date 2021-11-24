namespace Net6WebApiTemplate.Api.Routes.Version1
{
    public static class ApiRoutes
    {
        public const string Domain = "api";
        public const string Version = "v{version:apiVersion}";
        public const string Base = Domain + "/" + Version;

        public static class Auth
        {
            public const string SignIn = Base + "/signin";
        }

        public static class Client
        {
            public const string Create = Base + "/clients";
            public const string Get = Base + "/clients/{id}";
            public const string GetAll = Base + "/clients";
            public const string Update = Base + "/clients/{id}";
            public const string Delete = Base + "/clients/{id}";
        }
    }
}