using System.Net.Http;

namespace ArcheProjectWebApp.Helper
{
    public class StudentApi
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("http://localhost:50629");
            return client;
        }
    }
}
