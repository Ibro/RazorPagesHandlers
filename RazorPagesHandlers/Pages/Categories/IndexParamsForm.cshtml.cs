using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesHandlers.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RazorPagesHandlers.Pages.Categories
{
    public class IndexParamsFormModel : PageModel
    {
        private readonly SimpleDbContext _dbContext;

        public IndexParamsFormModel(SimpleDbContext dbContext)
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
    }
}