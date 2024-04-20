using System.Text;
using Forum.Application.Dto;
using Forum.Domain.Models;
using Forum.Infrastructure;
using Forum.Web.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Forum.Web.UI.Controllers;

[Authorize(Roles = "Admin, User")]
[Route("[controller]/[action]")]
public class TopicController : Controller
{
    private readonly HttpClient _httpClient;

    public TopicController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5038/");
    }

    // GET: Topic
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/Topic");
        var content = await response.Content.ReadAsStringAsync();
        var topics = JsonConvert.DeserializeObject<List<TopicDetailDto>>(content);

        return View(topics);
    }

    // GET: Topic/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Topic/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTopicDto topic)
    {
        var content = new StringContent(JsonConvert.SerializeObject(topic), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Topic", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(topic);
    }

    // GET: Topic/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"api/Topic/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var topic = JsonConvert.DeserializeObject<Forum.Infrastructure.TopicDetailDto>(content);

        var updateTopicDto = new UpdateTopicDto
        {
            Subject = topic!.Subject,
            Status = topic.Status,
            Likes = topic.Likes,
            CreatorId = topic.CreatorId
        };

        return View(updateTopicDto);
    }

    // POST: Topic/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateTopicDto topic)
    {
        var content = new StringContent(JsonConvert.SerializeObject(topic), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/Topic/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(topic);
    }

    // GET: Topic/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"api/Topic/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var topic = JsonConvert.DeserializeObject<TopicDetailDto>(content);

        return View(topic);
    }

    // GET: Topic/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.GetAsync($"api/Topic/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var topic = JsonConvert.DeserializeObject<TopicDetailDto>(content);

        return View(topic);
    }

    // POST: Topic/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Topic/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}