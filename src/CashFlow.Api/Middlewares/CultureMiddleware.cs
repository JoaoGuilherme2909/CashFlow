using System.Globalization;

namespace CashFlow.Api.Middlewares;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestedCulture = httpContext.Request.Headers.AcceptLanguage.FirstOrDefault(); 

        var cultureInfo = new CultureInfo("en");

        if (!string.IsNullOrWhiteSpace(requestedCulture) && supportedLanguages.Exists(l => l.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(httpContext);
    }
}
