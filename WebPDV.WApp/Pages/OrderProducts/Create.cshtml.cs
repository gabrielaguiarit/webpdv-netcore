using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebPDV.Domain.Entities;
using WebPDV.WApp.Data;

namespace WebPDV.WApp.Pages.OrderProducts
{
    public class CreateModel : PageModel
    {
        private readonly OrderDbContext _context;

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public IList<Product> Products { get; set; } = default!;

        [BindProperty]
        public List<OrderProduct> OrderItems { get; set; } = new List<OrderProduct>();

        [BindProperty]
        public string SearchTerm { get; set; }

        public CreateModel(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            Products = await _context.Products.ToListAsync();

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Get the serialized OrderItems from the form and deserialize it
            var orderItemsJson = Request.Form["OrderItems"];
            var orderItems = JsonConvert.DeserializeObject<List<OrderItem>>(orderItemsJson);

            var orderId = Guid.NewGuid();
            var lastOrder = await _context.Orders.OrderBy(x => x.OrderId).LastOrDefaultAsync();

            var order = new Order
            {
                Id = orderId,
                OrderId = lastOrder.OrderId + 1,
                OrderDate = Order.OrderDate,
                CustomerId = Order.CustomerId,
                OrderItems = new List<OrderProduct>()
            };

            foreach (var item in orderItems)
            {
                if (item.ProductQuantity > 0)
                {
                    order.OrderItems.Add(new OrderProduct
                    {
                        ProductId = item.ProductId,
                        Price = item.ProductPrice,
                        Quantity = item.ProductQuantity
                    });
                }
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
