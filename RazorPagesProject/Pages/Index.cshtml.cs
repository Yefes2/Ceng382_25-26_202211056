using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorPagesProject.Models;
using System.Collections.Generic;
using System.Linq;


namespace RazorPagesProject.Pages
{
    public class IndexModel : PageModel
    {
        private static List<ClassInformationModel> AllClasses = new(); // source data

        public List<ClassInformationTable> DisplayClasses { get; set; } = new();
        public string FilterKeyword { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        private const int PageSize = 10;

        public void OnGet(string filterKeyword, int currentPage = 1)
        {
            FilterKeyword = filterKeyword;
            CurrentPage = currentPage;

            if (AllClasses.Count == 0)
            {
                // Populate synthetic data only once
                for (int i = 1; i <= 100; i++)
                {
                    AllClasses.Add(new ClassInformationModel
                    {
                        ClassName = $"Sample Class {i}",
                        StudentCount = i % 30 + 1,
                        Description = $"Description for class {i}"
                    });
                }
            }

            // Apply filtering (if any)
            var filtered = string.IsNullOrWhiteSpace(filterKeyword)
                ? AllClasses
                : AllClasses.Where(c => c.ClassName.Contains(filterKeyword, System.StringComparison.OrdinalIgnoreCase)).ToList();

            // Calculate total pages
            TotalPages = (int)System.Math.Ceiling((double)filtered.Count / PageSize);

            // Paginate the result
            var paged = filtered
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Map to table model
            DisplayClasses = paged.Select(c => new ClassInformationTable
            {
                Id = c.Id,
                ClassName = c.ClassName,
                StudentCount = c.StudentCount,
                Description = c.Description
            }).ToList();
        }
    }
}