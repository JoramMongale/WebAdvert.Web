using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webAdvert.web.Models.Accounts;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAdvert.web.Controllers
{
    public class Accounts : Controller
    {

        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;

        public Accounts (SignInManager<CognitoUser> signInManager , UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pool = pool;


        }
        // GET: /<controller>/
  
      public async Task<IActionResult> SignUp()
        {
            var model = new SignUpModel();

           return View(model);
      }





        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _pool.GetUser(model.Email);
                if (user.Status != null)
                {
                    ModelState.AddModelError("UserExits", "user with this email already exits");
                    return View(model);
                }


                user.Attributes.Add(CognitoAttributesConstants.Name,model.Email);
                var createdUser = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false); // user has to created a new password just confirm email addresss
                if (createdUser.Succeeded)
                {

                    RedirectToAction("Confirm");
                }
            }

            return View(model);
        }



        public async Task<IActionResult> Confirm(ConfirmModel model)
        {
            return  View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Confirm_Post(ConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
                if (user== null)
                {
                    ModelState.AddModelError("NotFound", "User with this email address is not found");
                    return View(model);
                }
              //  (_userManager as CognitoUserManager<CognitoUser>).ConfirmSignUpAsync();

                var result = await _userManager.ConfirmEmailAsync(user, model.Code).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    RedirectToAction("index","Home");
                }
                else
                {

                    foreach(var item in result.Errors)
                    {

                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return View(model);
                }

                
            }
            return View(model); 




        }



    }
}
