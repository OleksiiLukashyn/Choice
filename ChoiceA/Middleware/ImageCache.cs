using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Builder;

namespace ChoiceA.Middleware
{
    public class ImageCache
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly string _path;

        public ImageCache(RequestDelegate next, IWebHostEnvironment env, string path)
        {
            _next = next;
            _env = env;
            _path = path;
        }

        // собственно RequestDelegate
        //
        public async Task Invoke(HttpContext context)
        {
            var name = context.Request.Path.Value.Split("/").Last();     // like 123.png
            if (name.EndsWith(".png"))
            {
                string pathToImage = Path.Combine(_env.WebRootPath, _path, name);

                using (FileStream f = File.Open(pathToImage, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[f.Length];
                    f.Read(buffer, 0, (int)f.Length);

                    context.Response.ContentType = "image/png";
                    context.Response.ContentLength = f.Length;
                    await context.Response.Body.WriteAsync(buffer, 0, (int)f.Length);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }

    static class ImageCacheExtentions
    {
        public static IApplicationBuilder UseImageCache(this IApplicationBuilder app, string path)
        {
            return app.UseMiddleware<ImageCache>(path);
        }
    }
}
