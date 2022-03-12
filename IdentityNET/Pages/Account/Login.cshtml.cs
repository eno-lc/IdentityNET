using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace IdentityNET.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //verify credentials - hardcoded
            if (Credential.Username == "admin" && Credential.Password == "password")
            {
                //create the security context
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@admin.com"),
                    new Claim("Department", "HR"),
                    new Claim("Department","Administrator"),
                    new Claim("Department", "Manager")
                    };
                var identity = new ClaimsIdentity(claims, "CookieAuthentication");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuthentication", claimsPrincipal);
                
                return RedirectToPage("/Index");
            }
            return Page();

        }
    }

    public class Credential
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


}
