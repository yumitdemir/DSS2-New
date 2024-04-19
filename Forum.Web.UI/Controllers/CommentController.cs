using System.Text;
using Forum.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Forum.Web.UI.Controllers;

[Authorize(Roles = "Admin, User")]
[Route("[controller]/[action]")]
public class CommentController : Controller
{
    private readonly HttpClient _httpClient;

    public CommentController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5038/");
    }

    // GET: Comment
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/Comment");
        var content = await response.Content.ReadAsStringAsync();
        var comments = JsonConvert.DeserializeObject<List<Comment>>(content);

        return View(comments);
    }

    // GET: Comment/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Comment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Comment comment)
    {
        var content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/Comment", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(comment);
    }

    // GET: Comment/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"api/Comment/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var comment = JsonConvert.DeserializeObject<Comment>(content);

        return View(comment);
    }

    // POST: Comment/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Comment comment)
    {
        var content = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/Comment/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(comment);
    }

    // GET: Comment/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"api/Comment/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var comment = JsonConvert.DeserializeObject<Comment>(content);

        return View(comment);
    }

    // GET: Comment/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.GetAsync($"api/Comment/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var comment = JsonConvert.DeserializeObject<Comment>(content);

        return View(comment);
    }

    // POST: Comment/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Comment/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}

