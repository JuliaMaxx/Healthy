using Healthy.Models;
using Html;
using ILogger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Html
{
    public abstract class BaseHtmlElement
    {
        public abstract string Render();
    }
    public class HtmlHeader : BaseHtmlElement
    {
        private string _content;

        public HtmlHeader(string content)
        {
            _content = content;
        }

        public override string Render()
        {
            return $"<h1>{_content}</h1>";
        }
    }
    public class HtmlParagraph : BaseHtmlElement
    {
        private string _content;

        public HtmlParagraph(string content)
        {
            _content = content;
        }

        public override string Render()
        {
            return $"<p>{_content}</p>";
        }
    }
}

namespace ILogger
{
    public interface ILoggerService
    {
        void Log(string message);
    }

    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}

namespace Singleton
{
    public class SingletonService
    {
        private static SingletonService _instance;
        private static readonly object _lock = new object();

        private SingletonService()
        {
        }

        public static SingletonService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonService();
                    }
                    return _instance;
                }
            }
        }
    }

}

namespace Healthy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoggerService _loggerService;

        public HomeController(ILogger<HomeController> logger, ILoggerService loggerService)
        {
            _logger = logger;
            _loggerService = loggerService;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Entries");
            }
            else
            {
                var elements = new List<BaseHtmlElement>
                {
                    new HtmlHeader("Welcome"),
                    new HtmlParagraph("Learn about <a href='https://learn.microsoft.com/aspnet/core'>building Web apps with ASP.NET Core</a>.")
                };

                _loggerService.Log("Rendering Index page.");

                ViewBag.Elements = elements;
                return View();
            }
        }

        public IActionResult Foods()
        {
            return View();
        }

        public IActionResult History()
        {
            return RedirectToAction("History", "Entries");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
