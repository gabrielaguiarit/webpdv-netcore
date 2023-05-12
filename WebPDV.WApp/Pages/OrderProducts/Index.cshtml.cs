using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebPDV.Domain.Entities;
using WebPDV.WApp.Data;

namespace WebPDV.WApp.Pages.OrderProducts
{
    public class IndexModel : PageModel
    {
        private readonly WebPDV.WApp.Data.OrderDbContext _context;

        public IndexModel(WebPDV.WApp.Data.OrderDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                    .Include(x => x.OrderItems).ThenInclude(x => x.Product)
                .Include(o => o.Customer).OrderBy(x => x.OrderId).ToListAsync();
            }
        }
    }
}
