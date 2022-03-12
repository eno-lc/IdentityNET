using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityNET.Pages
{
    [Authorize(Policy = "HRDept")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
