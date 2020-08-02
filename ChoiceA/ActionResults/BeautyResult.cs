using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace ChoiceA.ActionResults
{
    public class BeautyResult : IActionResult
    {
        public string InnerHtml { set; get; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            string html = $@"
        <!DOCTYPE html><html><head>
        <title>Главная страница</title>
        <meta charset=utf-8 />
        </head> <body> {InnerHtml} </body></html>";

            var bytes = Encoding.UTF8.GetBytes(html);
            await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
