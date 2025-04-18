using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorPagesProject.Models;
using RazorPagesProject.Helpers;  
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

            var filtered = string.IsNullOrWhiteSpace(filterKeyword)
                ? AllClasses
                : AllClasses.Where(c => c.ClassName.Contains(filterKeyword, System.StringComparison.OrdinalIgnoreCase)).ToList();

            // Calculate total pages
            TotalPages = (int)System.Math.Ceiling((double)filtered.Count / PageSize);

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

        public IActionResult OnGetExport(string exportMode, string filterKeyword, List<string> selectedColumns)
        {
            var source = (exportMode == "filtered")
                ? GetFilteredData(filterKeyword)
                : AllClasses;

            var tableData = source.Select(c => new ClassInformationTable
            {
                Id = c.Id,
                ClassName = c.ClassName,
                StudentCount = c.StudentCount,
                Description = c.Description
            }).ToList();

            string json = Utils.Instance.ExportToJson(tableData, selectedColumns);
            byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);

            return File(jsonBytes, "application/json", $"export_{exportMode}.json");
        }

        private List<ClassInformationModel> GetFilteredData(string keyword)
        {
            return string.IsNullOrWhiteSpace(keyword)
                ? AllClasses
                : AllClasses.Where(c =>
                    c.ClassName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }
    }
}
