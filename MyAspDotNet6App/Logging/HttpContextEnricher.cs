using Serilog.Core;
using Serilog.Events;

namespace MyAspDotNet6App.Logging;

/// <summary>
/// HttpContext情報出力用のSerilogカスタムエンリッチャー
/// </summary>
public class HttpContextEnricher : ILogEventEnricher
{
    /// <summary>
    /// HttpContextアクセス用クラス
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="httpContextAccessor">HttpContextアクセス用クラス</param>
    public HttpContextEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        if (!(_httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false))
        {
            return;
        }
        var userName = _httpContextAccessor.HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type.EndsWith("/name"))?.Value.ToString();
        var userNameProperty = factory.CreateProperty("UserName", userName);
        logEvent.AddPropertyIfAbsent(userNameProperty);
        var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
        var ipAddressProperty = factory.CreateProperty("IpAddress", string.IsNullOrWhiteSpace(ipAddress) ? "UnknownAddress" : ipAddress);
        logEvent.AddPropertyIfAbsent(ipAddressProperty);
    }
}
