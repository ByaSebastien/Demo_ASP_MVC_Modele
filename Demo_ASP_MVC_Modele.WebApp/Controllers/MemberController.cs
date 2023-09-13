using BLL;
using GUI;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_MVC_Modele.WebApp.Controllers
{
    public class MemberController : Controller
    {
        private IMemberService _Service;
        private SessionManager _Session;
        private IGameService _GameService;

        public MemberController(IMemberService service, SessionManager session, IGameService gameService)
        {
            _Service = service;
            _Session = session;
            _GameService = gameService;
        }
        public IActionResult Register()
        {
            return View(new MemberForm());
        }
        [HttpPost]
        public IActionResult Register([FromForm] MemberForm memberForm)
        {
            if (!ModelState.IsValid)
                return View(memberForm);
            try
            {
                _Service.Register(memberForm.ToMember().ToBLL());
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData["Error"] = true;
                return View(memberForm);
            }
        }
        public IActionResult Login()
        {
            return View(new MemberConnection());
        }
        [HttpPost]
        public IActionResult Login([FromForm] MemberConnection memberConnection)
        {
            if (!ModelState.IsValid)
                return View(memberConnection);
            else
            {
                MemberSession member = _Service.Login(memberConnection.Pseudo, memberConnection.Password).ToMemberSession();
                if (member is null)
                {
                    ViewData["Error"] = true;
                    return View(memberConnection);
                }
                else
                {
                    _Session.CurrentMember = member;
                    return RedirectToAction("Index", "Game");
                }
            }
        }
        [AuthRequired]
        public IActionResult Logout()
        {
            _Session.Logout();
            return RedirectToAction("Index", "Home");
        }
        [AuthRequired]
        public IActionResult Detail()
        {
            MemberDetail member = _Session.CurrentMember.ToMemberDetail(_GameService.GetFavoriteByMemberId(_Session.CurrentMember.Id)); 
            return View(member);
        }
    }
}
