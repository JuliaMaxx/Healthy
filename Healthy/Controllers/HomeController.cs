using Healthy.Models;
using Html;
using ILogger;
using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;
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
    public class HtmlHeader2 : BaseHtmlElement
    {
        private string _content;

        public HtmlHeader2(string content)
        {
            _content = content;
        }

        public override string Render()
        {
            return $"<h2>{_content}</h2>";
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

    public class HtmlImage : BaseHtmlElement
    {
        private string Src { get; }
        private string Alt { get; }

        public HtmlImage(string src, string alt)
        {
            Src = src;
            Alt = alt;
        }

        public override string Render() => $"<img src='{Src}' alt='{Alt}' />";
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
                    new HtmlHeader("Welcome to Get Healthy"),
                    new HtmlParagraph("<a href='' class='saveButton btn-primary'>Try now!</a>"),
    
                    // Additional header for the welcome page
                    new HtmlHeader2("Your Journey to Health Starts Here"),
    
                    // Paragraph with a brief introduction
                    new HtmlParagraph("Discover a new way to improve your health and wellbeing with perso   nalized tips and resources."),
    
                    // Image placeholder for a screenshot
                    new HtmlImage("path/to/screenshot1.png", "Screenshot of the health dashboard"),
    
                    // Paragraph with lorem ipsum text for description
                    new HtmlParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."),
    
                    // Image placeholder for another screenshot
                    new HtmlImage("path/to/screenshot2.png", "Screenshot of the tracking features"),
    
                    // Another paragraph with lorem ipsum text for description
                    new HtmlParagraph("Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."),

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
