using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesProject.Data;
using RazorPagesProject.Models;
using System.Threading.Tasks;

namespace RazorPagesProject.Pages.Classes
{
    public class EditModel : PageModel
    {
        private readonly SchoolDbContext _context;
        public EditModel(SchoolDbContext context) => _context = context;

        [BindProperty]
        public Class ClassItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ClassItem = await _context.Classes.FindAsync(id);
            if (ClassItem == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(ClassItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
