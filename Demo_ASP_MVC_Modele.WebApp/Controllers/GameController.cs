using BLL;
using DAL;
using GUI;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_MVC_Modele.WebApp.Controllers
{
    public class GameController : Controller
    {
        private IGameService _Service;
        private SessionManager _Session;
        public GameController(IGameService service, SessionManager session)
        {
            _Service = service;
            _Session = session;
        }

        public IActionResult Index()
        {
            return View(_Service.GetAll().Select(g => g.ToGUI()));
        }
        [AuthRequired]
        public IActionResult Add()
        {
            return View();
        }
        [AuthRequired]
        [HttpPost]
        public IActionResult Add(GameForm gameForm)
        {
            if (gameForm.Nb_Player_Max < gameForm.Nb_Player_Min)
                ModelState.AddModelError("Nombre joueur maximum", "Le nombre de joueur maximum doit être supérieur au nombre de joueur minimum");
            if (!ModelState.IsValid)
                return View(gameForm);
            else
            {
                _Service.Add(gameForm.ToGame().ToBLL());
                return RedirectToAction(nameof(Index));
            }
        }
        [AuthRequired]
        public IActionResult Delete(int id)
        {
            _Service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {
            GUI.Game game = _Service.GetById(id).ToGUI();
            return View(game);
        }
        [AuthRequired]
        public IActionResult Edit(int id)
        {
            GUI.Game game = _Service.GetById(id).ToGUI();
            return View(game);
        }
        [AuthRequired]
        [HttpPost]
        public IActionResult Edit(GUI.Game game)
        {
            if (game.Nb_Player_Max < game.Nb_Player_Min)
                ModelState.AddModelError("Nombre joueur maximum", "Le nombre de joueur maximum doit être supérieur au nombre de joueur minimum");
            if (!ModelState.IsValid)
                return View(game);
            else
            {
                _Service.Update(game.ToBLL());
                return RedirectToAction(nameof(Index));
            }
        }
        [AuthRequired]
        public IActionResult AddFavorite(int id)
        {
            try
            {
                _Service.AddFavoriteToMember(_Session.CurrentMember.Id, id);
            }
            catch (Exception ex)
            {
                TempData["errorFav"] = "Jeu déja en favoris";
            }
            return RedirectToAction("Index");
        }
        [AuthRequired]
        public IActionResult Favorite()
        {
            IEnumerable<GUI.Game> games = _Service.GetFavoriteByMemberId(_Session.CurrentMember.Id).Select(g => g.ToGUI());
            return View(games);
        }
        [AuthRequired]
        public IActionResult DeleteFavorite(int idGame)
        {
            _Service.DeleteFavoriteToMember(_Session.CurrentMember.Id, idGame);
            return RedirectToAction("Favorite");
        }
    }
}
