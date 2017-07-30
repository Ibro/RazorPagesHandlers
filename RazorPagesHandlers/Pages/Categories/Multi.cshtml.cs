using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesHandlers.Data;
using System.Threading.Tasks;

namespace RazorPagesHandlers.Pages.Categories
{
    public class MultiModel : PageModel
    {
        private readonly SimpleDbContext _dbContext;

        public MultiModel(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _dbContext.Categories.FindAsync(id);

            if (Category == null)
            {
                return RedirectToPage("Categories");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Categories.Add(Category);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}