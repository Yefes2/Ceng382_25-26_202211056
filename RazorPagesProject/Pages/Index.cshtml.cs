using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorPagesProject.Models;
using System.Collections.Generic;
using System.Linq;


namespace RazorPagesProject.Pages
{
    public class IndexModel : PageModel
    {
        public List<ClassInformationModel> ClassList { get; set; } = new();

        [BindProperty]
        public ClassInformationModel NewClass { get; set; }

        public bool IsEditMode { get; set; } = false;

        public void OnGet(){}

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ClassList.Add(new ClassInformationModel
            {
                ClassName = NewClass.ClassName,
                StudentCount = NewClass.StudentCount,
                Description = NewClass.Description
            });

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id){
            var classToDelete = ClassList.FirstOrDefault(c => c.Id == id);
            if (classToDelete != null)
            {
                ClassList.Remove(classToDelete);
                
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEditRequest(int id){
            var item = ClassList.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                NewClass = new ClassInformationModel
                {
                    Id = item.Id,
                    ClassName = item.ClassName,
                    StudentCount = item.StudentCount,
                    Description = item.Description
                };
                IsEditMode = true;
            }
            return Page();
        }

        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid)
                return Page();

            var item = ClassList.FirstOrDefault(x => x.Id == NewClass.Id);
            if (item != null)
            {
                item.ClassName = NewClass.ClassName;
                item.StudentCount = NewClass.StudentCount;
                item.Description = NewClass.Description;
            }

            return RedirectToPage();
        }
        
    }
}
