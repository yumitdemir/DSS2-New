using System.Security.Claims;
using System.Text;
using Forum.Application.Dto;
using Forum.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Forum.Web.UI.Controllers;

[Authorize(Roles = "Admin, User")]
[Route("[controller]/[action]")]
public class CommentController : Controller
{
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5038/") };

    // GET: Comment
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/Comment");
        var content = await response.Content.ReadAsStringAsync();
        var comments = JsonConvert.DeserializeObject<List<CommentDetailDto>>(content);

        if (comments == null)
        {
            comments = new List<CommentDetailDto>();
        }

        return View(comments);
    }

    // GET: Comment/Create
    public IActionResult Create(int id)
    {
        ViewBag.TopicId = id;
        return View();
    }

    // POST: Comment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCommentDto comment)
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
        var comment = JsonConvert.DeserializeObject<UpdateCommentDto>(content);

        return View(comment);
    }

    // POST: Comment/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateCommentDto comment)
    {
        // check if the user is the owner of the comment check updateCommentdto for the user id

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
        var comment = JsonConvert.DeserializeObject<CommentDetailDto>(content);

        return View(comment);
    }

    // GET: Comment/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.GetAsync($"api/Comment/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var comment = JsonConvert.DeserializeObject<CommentDetailDto>(content);

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