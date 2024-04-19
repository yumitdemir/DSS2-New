using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.UI.Controllers;

[Authorize (Roles = "Admin, User")]
public class TopicController : Controller
{
    private readonly HttpClient _httpClient;
    
    public TopicController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5038/"); // Base URL of API
    }
    
    
    
}