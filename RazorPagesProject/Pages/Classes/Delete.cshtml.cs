using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesProject.Data;
using RazorPagesProject.Models;
using System.Threading.Tasks;

namespace RazorPagesProject.Pages.Classes
{
    public class DeleteModel : PageModel
    {
        private readonly SchoolDbContext _context;
        public DeleteModel(SchoolDbContext context) => _context = context;

        [BindProperty]
        public Class ClassItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ClassItem = await _context.Classes.FindAsync(id);
            if (ClassItem == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var item = await _context.Classes.FindAsync(id);
            if (item != null)
            {
                _context.Classes.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
