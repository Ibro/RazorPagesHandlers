using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesHandlers.Data;

namespace RazorPagesHandlers.Pages
{
    public class MultiPostModel : PageModel
    {

        private readonly SimpleDbContext _dbContext;

        public MultiPostModel(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostFirstAsync()
        {
            return await InsertCategory("First");
        }
      
        public async Task<IActionResult> OnPostSecondAsync()
        {
            return await InsertCategory("Second");
        }

        private async Task<IActionResult> InsertCategory(string name)
        {
            Category.Name = name;
            _dbContext.Categories.Add(Category);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Categories/Index");
        }
    }
}