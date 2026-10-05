using Hospital_Management_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UnreadNotificationsViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public UnreadNotificationsViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(string id, string userType)
    {
        var unreadCount = await _context.Notifications
            .CountAsync(n =>
                !n.IsRead &&
                (
                    (n.ReceiverId == id && n.ReceiverType == userType) ||
                    (userType == "Staff" && n.ReceiverType == "AllStaff")
                ));

        return View(unreadCount);
    }
}