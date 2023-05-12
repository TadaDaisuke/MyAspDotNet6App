using Serilog.Core;
using Serilog.Events;

namespace MyAspDotNet6App.Logging;

public class UserNameEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserNameEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        if (!(_httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false))
        {
            return;
        }
        var userName = _httpContextAccessor.HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type.EndsWith("/name"))?.Value.ToString();
        var userNameProperty = factory.CreateProperty("UserName", userName);
        logEvent.AddPropertyIfAbsent(userNameProperty);
    }
}
