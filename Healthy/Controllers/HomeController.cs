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
            return $"<p class='todayDatabase'>{_content}</p>";
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

        public override string Render() => $"<div class'todayDatabase'>'<img src='{Src}' alt='{Alt}' class='homepageImages'/></div>";
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
                    new HtmlParagraph("<br><a href='' class='saveButton btn-primary'>Try now for FREE!</a>"),
    
                    // Additional header for the welcome page
                    new HtmlHeader2("Your Journey to Health Starts Here"),
    
                    // Image placeholder for a screenshot
                    new HtmlImage("../../Assets/todayDatabase.png", "Screenshot of the health dashboard"),
    
                    // Paragraph with a brief introduction
                    new HtmlParagraph("Discover a revolutionary approach to enhancing your health and wellbeing with our comprehensive, personalized tips and resources. Tailored specifically to your unique needs and lifestyle, our guidance will empower you to make informed decisions, adopt healthier habits, and achieve your wellness goals."),
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
