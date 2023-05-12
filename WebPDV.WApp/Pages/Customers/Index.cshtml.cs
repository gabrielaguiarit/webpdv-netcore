using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebPDV.Domain.Entities;
using WebPDV.WApp.Data;

namespace WebPDV.WApp.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly WebPDV.WApp.Data.OrderDbContext _context;

        public IndexModel(WebPDV.WApp.Data.OrderDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customers != null)
            {
                Customer = await _context.Customers.ToListAsync();
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
