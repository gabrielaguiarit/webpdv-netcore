using WebPDV.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPDV.WApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly WebPDV.WApp.Data.OrderDbContext _context;

        public CreateModel(WebPDV.WApp.Data.OrderDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Product.Orders = new List<OrderProduct>();

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
