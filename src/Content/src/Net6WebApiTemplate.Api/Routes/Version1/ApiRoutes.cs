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

        public static class Product
        {
            public const string Create = Base + "/products";
            public const string Get = Base + "/products/{id}";
            public const string GetAll = Base + "/products";
            public const string Patch = Base + "/products/{id}";
            public const string Delete = Base + "/products/{id}";
        }

        public static class Category
        {
            public const string Create = Base + "/categories";
            public const string Get = Base + "/categories/{id}";
            public const string GetAll = Base + "/categories";
            public const string Patch = Base + "/categories/{id}";
            public const string Delete = Base + "/categories/{id}";
        }
    }
}