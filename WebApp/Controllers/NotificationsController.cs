using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController(IHubContext<NotficationHub> notficationHub, INotificationService notificationService) : ControllerBase
{
    private readonly IHubContext<NotficationHub> _notficationHub = notficationHub;
    private readonly INotificationService _notificationService = notificationService;


}
