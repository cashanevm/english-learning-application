using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using english_learning_application.Models;
using english_learning_application.Data;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace english_learning_application.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _applicationDbContext;

    public HomeController(
        ILogger<HomeController> logger,
        ApplicationDbContext applicationDbContext
        )
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }

    public IActionResult Index()
    {

        ViewBag.SpeechParts = _applicationDbContext.SpeechParts
            .Include(l => l.TranslatedWords)
            .ToList();

        ViewBag.Languages = _applicationDbContext.Languages
            .Include(l => l.Tests)
            .Include(l => l.TranslatedSentences)
            .Include(l => l.TranslatedWords)
            .ToList();

        ViewBag.Sentences = _applicationDbContext.Sentences
            .Include(l => l.Contexts)
            .Include(l => l.DisplaySentences)
            .Include(l => l.TranslatedSentences)
            .Include(l => l.Word)
            .ToList();

        ViewBag.Tags = _applicationDbContext.Tags
            .Include(l => l.Words)
            .ToList();

        ViewBag.Tests = _applicationDbContext.Tests
            .Include(l => l.Words)
            .Include(l => l.Language)
            .ToList();

        ViewBag.TranslatedSentences = _applicationDbContext.TranslatedSentences
            .Include(l => l.Contexts)
            .Include(l => l.Word)
            .Include(l => l.Sentence)
            .Include(l => l.Language)
            .ToList();

        ViewBag.TranslatedWords = _applicationDbContext.TranslatedWords
            .Include(l => l.Contexts)
            .Include(l => l.Word)
            .Include(l => l.Language)
             .Include(l => l.SpeechPart)
            .ToList();

        ViewBag.Words = _applicationDbContext.Words
            .Include(l => l.Tags)
            .Include(l => l.DisplayWords)
            .Include(l => l.Sentences)
            .Include(l => l.TranslatedSentences)
            .Include(l => l.TranslatedWords)
            .Include(l => l.Tests)
            .ToList();

        ViewBag.DisplayWords = _applicationDbContext.DisplayWords
            .Include(l => l.Word)
            .ToList();

        ViewBag.DisplaySentences = _applicationDbContext.DisplaySentences
            .Include(l => l.Sentence)
            .ToList();

        ViewBag.Contexts = _applicationDbContext.Contexts
            .Include(l => l.TranslatedWords)
            .Include(l => l.TranslatedSentences)
            .Include(l => l.Sentences)
            .ToList();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

