using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesHandlers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace RazorPagesHandlers.Pages.Categories
{
    public class IndexParamsRouteModel : PageModel
    {
        private readonly SimpleDbContext _dbContext;

        public IndexParamsRouteModel(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Category> Categories { get; private set; }

        public async Task OnGetAsync()
        {
            Categories = await _dbContext.Categories.ToListAsync();
        }

        public async Task OnPostSearchAsync(string query)
        {
            Categories = await _dbContext
                .Categories
                .AsNoTracking()
                .Where(c => !string.IsNullOrEmpty(c.Description) && c.Description.Contains(query))
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}