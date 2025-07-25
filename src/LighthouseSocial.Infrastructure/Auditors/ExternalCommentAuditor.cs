using System.Net.Http.Json;
using LighthouseSocial.Domain.Interfaces;

namespace LighthouseSocial.Infrastructure.Auditors;

public class ExternalCommentAuditor(HttpClient httpClient)
    : ICommentAuditor
{
    public async ValueTask<bool> IsTextCleanAsync(string text)
    {
        //todo@buraksenyurt Adres bilgisi runtime sahibi uygulamadan gelmeli
        //todo@buraksenyurt HashiCorp Consul ile Service Discovery entegrasyonu yapılmalı 
        var response = await
            _httpClient.PostAsJsonAsync("http://localhost:5005/moderate"
            , new { comment = text });

        var result = await response.Content.ReadFromJsonAsync<AuditResult>();
        return result?.IsClean ?? true;
    }
}

public class AuditResult
{
    public bool IsClean { get; set; }
}
