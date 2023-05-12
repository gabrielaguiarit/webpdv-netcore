using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebPDV.Domain.Entities;
using System.Text;

namespace WebPDV.WApp.Pages.OrderProducts
{
    public class DetailsModel : PageModel
    {
        private readonly WebPDV.WApp.Data.OrderDbContext _context;

        public DetailsModel(WebPDV.WApp.Data.OrderDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            using MailMessage mail = new();
            mail.From = new MailAddress("g.craguiar@gmail.com");
            mail.To.Add("g.craguiar@gmail.com");
            mail.Subject = "Pedido de venda";
            mail.Body = "Detalhes do pedido:\n" + GetOrderDetails(order);

            using SmtpClient smtp = new("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("5e1a5747caf4d9", "89d24bef6285c9"),
                EnableSsl = true
            };
            await smtp.SendMailAsync(mail);

            return RedirectToPage("./Index");
        }

        private string GetOrderDetails(Order order)
        {
            StringBuilder sb = new();
            sb.AppendLine("Detalhes do Pedido:");
            sb.AppendLine("-------------------");
            sb.AppendLine($"Data do Pedido: {order.OrderDate.ToString("dd/MM/yyyy")}");
            sb.AppendLine($"Cliente: {order.Customer.Name}");
            sb.AppendLine("Itens:");

            foreach (var item in order.OrderItems)
            {
                sb.AppendLine($"- {item.Quantity} x {item.Product.Name} (valor unitário: R$ {item.Product.Price}, total: R$ {item.Quantity * item.Product.Price})");
            }

            sb.AppendLine($"Total do Pedido: R$ {order.OrderItems.Sum(item => item.Quantity * item.Product.Price)}");

            return sb.ToString();
        }
    }
}
