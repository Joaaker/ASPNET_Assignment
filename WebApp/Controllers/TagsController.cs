using System;
using Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class TagsController(DataContext context) : Controller
{
    private readonly DataContext _context = context;

    //[HttpGet]
    //public async Task<IActionResult> SearchTags(string term)
    //{
    //    if (string.IsNullOrWhiteSpace(term))
    //        return Json(new List<object>());

    //    var tags = await _context.Tags
    //        .Where(x => x.TagName.Contains(term))
    //        .ToListAsync();

    //    return Json(tags);
    //}
}