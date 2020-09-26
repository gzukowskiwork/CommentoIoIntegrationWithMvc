using AutoMapper;
using CommentoIntegrationTest.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using CommentoIntegrationTest.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace CommentoIntegrationTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICreateUrlForSSO _createUrlForSSO;
        private readonly IConfiguration _configuration;

        public AccountController(IMapper mapper,
            UserManager<ApplicationUser> userManager,
            ICreateUrlForSSO createUrlForSSO,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _createUrlForSSO = createUrlForSSO;
            _configuration = configuration;
        }

        #region Registration
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegistration);
            }

            var user = _mapper.Map<ApplicationUser>(userRegistration);
            var result = await _userManager.CreateAsync(user, userRegistration.Password);



            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userRegistration);
            }

            await _userManager.AddToRoleAsync(user, "RegisteredUser");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion

        #region LoginAndSSO


        public IActionResult Login([FromQuery] string token, string hmac)
        {
            HttpContext.Session.SetString("token", token);
            HttpContext.Session.SetString("hmac", hmac);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }

            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            string token = HttpContext.Session.GetString("token");
            string hmac = HttpContext.Session.GetString("hmac");

            if(user!= null && await _userManager.CheckPasswordAsync(user, userLogin.Password))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(identity));

                return RedirectToPage(_createUrlForSSO.DoOperations(user, token, hmac));

                //return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View();
            }
        }

        #endregion
    }
}
