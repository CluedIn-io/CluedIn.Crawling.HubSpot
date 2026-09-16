using RestSharp;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    // RestSharp is a different major version per CluedIn generation (106.x on 4.7/4.8's net6.0 vs
    // 114.x on 5.0's net10.0): the Method enum members are uppercase (Method.GET) on 106.x and
    // PascalCase (Method.Get) on 114.x. Centralized here instead of repeating #if at every call
    // site across HubSpotClient and the Mesh processors.
    public static class HubSpotRestMethod
    {
#if CLUEDIN_V50
        public const Method Get = Method.Get;
        public const Method Post = Method.Post;
        public const Method Put = Method.Put;
        public const Method Delete = Method.Delete;
#else
        public const Method Get = Method.GET;
        public const Method Post = Method.POST;
        public const Method Put = Method.PUT;
        public const Method Delete = Method.DELETE;
#endif
    }
}
