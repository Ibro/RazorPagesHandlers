using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesHandlers.Data;

namespace RazorPagesHandlers.Pages
{
    public class SinglePostModel : PageModel
    {
        private readonly SimpleDbContext _dbContext;

        public SinglePostModel(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostFirstAsync()
        {
            Category.Name = "First";
            _dbContext.Categories.Add(Category);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Categories/Index");
        }
    }
}