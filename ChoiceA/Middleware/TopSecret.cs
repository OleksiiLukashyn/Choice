using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Builder;

namespace ChoiceA.Middleware
{
    public class TopSecret
    {
        private const string SecretFilePath = "";
        private const string SecretFileName = "secret.secret";
        private const string RequestPath = "secret";

        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public TopSecret(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Split("/").Last() == RequestPath &&
                context.User.Identity.IsAuthenticated) {
                string pathToFile = Path.Combine(_env.WebRootPath, SecretFilePath, SecretFileName);
                await context.Response.SendFileAsync(pathToFile);
            }
            else
            {
                await _next(context);
            }
        }
    }

    static class TopSecretExtentions
    {
        public static IApplicationBuilder UseTopSecret(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TopSecret>();
        }
    }
}
