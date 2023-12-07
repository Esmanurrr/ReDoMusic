using AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReDoMusic.Domain.Identity;
using ReDoMusic.MVC.Models;

namespace ReDoMusic.MVC.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            var registerViewModel = new AuthRegisterViewModel();
            return View(registerViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthRegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
                return View(registerViewModel);

            var userId = Guid.NewGuid();

            var user = new User()
            {
                Id = userId,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Gender = registerViewModel.Gender,
                BirthDate = registerViewModel.BirthDate.Value.ToUniversalTime(),
                UserName = registerViewModel.UserName,
            };

            var registerResult = await _userManager.CreateAsync(user,registerViewModel.Password);

            if (!registerResult.Succeeded)
                throw new Exception("Kayıt oluşturulamadı!");   
            
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            var loginViewModel = new AuthLoginViewModel();

            return View(loginViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> LoginAsync(AuthLoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if(user is null)
                return View(loginViewModel);

            var loginResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password,false,false);

            if(!loginResult.Succeeded)
                return View(loginViewModel);

            return RedirectToAction(nameof(HomeController));
        }
    }
}
