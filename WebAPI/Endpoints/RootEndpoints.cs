using Microsoft.Extensions.Primitives;

namespace Endpoints;

public static class RootEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/", async (HttpContext ctx) =>
        {
            // Set the content type as html
            ctx.Response.Headers.ContentType = new StringValues("text/html; charset=UTF-8");
            await ctx.Response.SendFileAsync("wwwroot/index.html");
        });
    }
}
