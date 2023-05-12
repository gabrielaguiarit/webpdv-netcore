using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebPDV.Domain.Entities;
using WebPDV.WApp.Data;

namespace WebPDV.WApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WebPDV.WApp.Data.OrderDbContext _context;

        public IndexModel(WebPDV.WApp.Data.OrderDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products.ToListAsync();
            }
        }
    }
}
