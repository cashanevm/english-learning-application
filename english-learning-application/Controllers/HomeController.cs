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
        //Tag tag = new Tag
        //{
        //    ID = 1,
        //    Name = "Tag Name",
        //    Words = new List<Word>()
        //};

        //Context context = new Context
        //{
        //    ID = 1,
        //    Name = "Context Name",
        //    TranslatedWords = new List<TranslatedWord>(),
        //    TranslatedSentences = new List<TranslatedSentence>(),
        //    Sentences = new List<Sentence>(),
        //};

        //Word word = new Word
        //{
        //    ID = 1,
        //    Tags = new List<Tag>(),
        //    DisplayWords = new List<DisplayWord>(),
        //    Sentences = new List<Sentence>(),
        //    TranslatedSentences = new List<TranslatedSentence>(),
        //    TranslatedWords = new List<TranslatedWord>(),
        //    Tests = new List<Test>(),
        //    OriginalWord = "OriginalWord"
        //};

        //Sentence sentence = new Sentence
        //{
        //    ID = 1,
        //    Contexts = new List<Context>(),
        //    DisplaySentences = new List<DisplaySentence>(),
        //    TranslatedSentences = new List<TranslatedSentence>(),
        //    OwnerId = 1,
        //    WordId = word.ID,
        //    Word = word,
        //    OriginalSentence = "OriginalSentence"
        //};

        //DisplaySentence displaySentence = new DisplaySentence
        //{
        //    ID = 1,
        //    SentenceId = sentence.ID,
        //    Sentence = sentence,
        //    Display = "DisplaySentence"
        //};

        //DisplayWord displayWord = new DisplayWord
        //{
        //    ID = 1,
        //    WordId = word.ID,
        //    Word = word,
        //    Display = "DisplayWord"

        //};

        //Language language = new Language
        //{
        //    ID = 1,
        //    Key = "UA",
        //    Tests = new List<Test>(),
        //    TranslatedSentences = new List<TranslatedSentence>(),
        //    TranslatedWords = new List<TranslatedWord>()
        //};


        //TranslatedSentence translatedSentence = new TranslatedSentence
        //{
        //    ID = 1,
        //    Contexts = new List<Context>(),
        //    OwnerId = 1,
        //    WordId = word.ID,
        //    Word = word,
        //    Translation = "TranslatedSentence",
        //    SentenceId = sentence.ID,
        //    Sentence = sentence,
        //    LanguageId = language.ID,
        //    Language = language
        //};

        //SpeechPart speechPart = new SpeechPart
        //{
        //    ID = 1,
        //    Name = "noun"
        //};


        //TranslatedWord translatedWord = new TranslatedWord
        //{
        //    ID = 1,
        //    Contexts = new List<Context>(),
        //    OwnerId = 1,
        //    WordId = word.ID,
        //    Word = word,
        //    Translation = "TranslatedWord",
        //    LanguageId = language.ID,
        //    Language = language,
        //    SpeechPartId = speechPart.ID,
        //    SpeechPart = speechPart

        //};

        //Test test = new Test
        //{
        //    ID = 1,
        //    TimePerWord = 10,
        //    OwnerId = 1,
        //    Options = 4,
        //    Words = new List<Word>(),
        //    LanguageId = language.ID,
        //    Language = language
        //};




        //_applicationDbContext.SpeechParts.Add(speechPart);
        //_applicationDbContext.Contexts.Add(context);
        //_applicationDbContext.Sentences.Add(sentence);
        //_applicationDbContext.Tags.Add(tag);
        //_applicationDbContext.Tests.Add(test);
        //_applicationDbContext.TranslatedSentences.Add(translatedSentence);
        //_applicationDbContext.TranslatedWords.Add(translatedWord);
        //_applicationDbContext.Words.Add(word);
        //_applicationDbContext.DisplayWords.Add(displayWord);
        //_applicationDbContext.DisplaySentences.Add(displaySentence);
        //_applicationDbContext.Languages.Add(language);


        //_applicationDbContext.SaveChanges();


        //tag.Words.Add(word);
        //word.Tags.Add(tag);

        //context.TranslatedWords.Add(translatedWord);
        //context.TranslatedSentences.Add(translatedSentence);
        //context.Sentences.Add(sentence);
        //translatedWord.Contexts.Add(context);

        //translatedSentence.Contexts.Add(context);
        //sentence.Contexts.Add(context);
        //speechPart.TranslatedWords.Add(translatedWord);
        //word.DisplayWords.Add(displayWord);
        //word.Sentences.Add(sentence);
        //word.TranslatedSentences.Add(translatedSentence);
        //word.TranslatedWords.Add(translatedWord);
        //word.Tests.Add(test);
        //test.Words.Add(word);

        //sentence.DisplaySentences.Add(displaySentence);
        //sentence.TranslatedSentences.Add(translatedSentence);

        //language.Tests.Add(test);
        //language.TranslatedSentences.Add(translatedSentence);
        //language.TranslatedWords.Add(translatedWord);

        //_applicationDbContext.Languages.Update(language);
        //_applicationDbContext.DisplaySentences.Update(displaySentence);
        //_applicationDbContext.DisplayWords.Update(displayWord);
        //_applicationDbContext.Words.Update(word);
        //_applicationDbContext.TranslatedWords.Update(translatedWord);
        //_applicationDbContext.TranslatedSentences.Update(translatedSentence);
        //_applicationDbContext.Tests.Update(test);
        //_applicationDbContext.Tags.Update(tag);
        //_applicationDbContext.Sentences.Update(sentence);
        //_applicationDbContext.Contexts.Update(context);
        //_applicationDbContext.SpeechParts.Update(speechPart);

        //_applicationDbContext.SaveChanges();

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

