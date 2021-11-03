namespace Net6WebApiTemplate.Api.Routes.Version1
{
    public static class ApiRoutes
    {
        public const string Domain = "api";
        public const string Version = "v1";//"v{version:apiVersion}";
        public const string Base = Domain + "/" + Version;

        public static class Client
        {
            public const string Create = Base + "/clients";
        }
    }
}