using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreateExamApp.Models;
using Microsoft.AspNetCore.Identity;
using CreateExamApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CreateExamApp.Controllers
{
    public class HomeController : Controller
    {
        public String html;
        public Uri url;
        public String[] TitleArray = new String[5];
        public String[] LinkArray = new String[5];
        public String[] ContextArray = new String[5];

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IExamRepository _examRepository;

        public HomeController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IExamRepository examRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _examRepository = examRepository;
        }

        public void GetTitleData(String Url, String XPath, int i)
        {
            url = new Uri(Url);

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            html = client.DownloadString(url);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            TitleArray[i] = doc.DocumentNode.SelectSingleNode(XPath).InnerText;
            ViewBag.Collection = TitleArray;
        }

        public void GetLinkData(String Url, String XPath, String tag, int i)
        {
            url = new Uri(Url);

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            html = client.DownloadString(url);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            LinkArray[i] = doc.DocumentNode.SelectSingleNode(XPath).Attributes[tag].Value;
            ViewData["Links"] = LinkArray;
        }

        public void GetContextData(String Url, String XPath, int i)
        {
            url = new Uri(Url);

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            html = client.DownloadString(url);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode node = doc.DocumentNode.SelectSingleNode(XPath);
            if(node != null)
                ContextArray[i] = node.InnerText;
            else
            {
                node = doc.DocumentNode.SelectSingleNode("/html/body/main/div/section/article/p[1]");
                if (node != null)
                    ContextArray[i] = node.InnerText;
                else
                    ContextArray[i] = "Like most parents, I try to limit my kid’s screen time. But screens are so ubiquitous that it’s sometimes hard for me to grasp how thoroughly they’ve infiltrated my kids’ lives.";
            }
            ViewBag.Context = ContextArray;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        
        public IActionResult HomePage()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Option> Options = new List<Option>
                {
                    new Option { Id = "1", option = "A"},
                    new Option { Id = "2", option = "B"},
                    new Option { Id = "3", option = "C"},
                    new Option { Id = "4", option = "D"}
                };
                ViewBag.Options = new SelectList(Options, "Id","option");

                for (int i = 0; i < 5; i++)
                {
                    GetTitleData("https://www.wired.com", "/html/body/div[3]/div/div[3]/div/div/div[2]/div[3]/div[1]/div[1]/div/ul/li["+ (i + 1) +"]/a/div[2]/h5", i);
                    GetLinkData("https://www.wired.com", "/html/body/div[3]/div/div[3]/div/div/div[2]/div[3]/div[1]/div[1]/div/ul/li[" + (i + 1) + "]/a", "href", i);
                }

                for(int i = 0; i < 5; i++)
                {
                    string link = "https://www.wired.com" + LinkArray[i].ToString();
                    GetContextData(link, "/html/body/div[1]/div/main/article/div[2]/div/div[1]/div/div[1]/p[1]", i);
                }
                
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public IActionResult AddExam([FromBody]Exam exam)
        {
            _examRepository.AddExam(exam);
            return RedirectToAction("ExamIndex","Exam");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login obj)
        {
            if (ModelState.IsValid)
            {
                // Girilen kullanıcı adına sahip kullanıcı varse user değişkenine atıyoruz
                var user = await _userManager.FindByNameAsync(obj.username);

                // eğer kullanıcı varsa if içerisine giriyoruz
                if (user != null)
                {
                    // kullanıcı girişi yapıyoruz
                    var result = await _signInManager.PasswordSignInAsync(user, obj.password, false, false);

                    // eğer giriş ilemi başarılıysa anasayfaya yönlendiriyoruz
                    if (result.Succeeded)
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                }
                // böyle bir kullanıcı yoksa geriye hata döndürüyoruz
                // ilk parametre key ikinci parametre value
                ModelState.AddModelError("", "Kullanıcı Bulunamadı.");
                return View(obj);
            }
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Login obj)
        {
            if (ModelState.IsValid)
            {
                // yeni bir kullanıcı nesnesi oluşturuyoruz
                var user = new IdentityUser()
                { UserName = obj.username };
                // oluşturulan kullanıcıyı parola(hash) ile birlikte kayıt ediyoruz
                var result = await _userManager.CreateAsync(user, obj.password);

                // kayıt işlemi başarılı ise Login sayfasına yönlendiriyoruz
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            return View(obj);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
