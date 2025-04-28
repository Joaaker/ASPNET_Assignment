using System.Diagnostics;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Dtos;
using Domain.Models;

namespace Business.Services;

public class NotificationService(INotificationRepository notificationRepository, INotificationDismissRepository notificationDismissRepository, IMemberService memberService) : INotificationService
{
    private readonly INotificationDismissRepository _notificationDismissRepository = notificationDismissRepository;
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    private readonly IMemberService _memberService = memberService;
    public async Task<IResponseResult<NotificationEntity>> AddNotificationAsync(NotificationDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Image))
            {
                switch (dto.NotificationTypeId)
                {
                    case 1:
                        dto.Image = "https://aspnetassignment.blob.core.windows.net/images/1d0e95a8-e947-4877-8857-c15de4e55a87.svg";
                        break;

                    case 2:
                        dto.Image = "https://aspnetassignment.blob.core.windows.net/images/b069ca09-4f72-4fdf-a5d3-e5b26c3aca5b.svg";
                        break;
                }
            }
            var notificationEntity = NotificationFactory.CreateEntity(dto);

            await _notificationRepository.BeginTransactionAsync();

            await _notificationRepository.AddAsync(notificationEntity);
            var saveResult = await _notificationRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving NotificationEntity");

            await _notificationRepository.CommitTransactionAsync();
            return ResponseResult<NotificationEntity>.Ok(notificationEntity);
        }
        catch (Exception ex)
        {
            await _notificationRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult<NotificationEntity>.Error($"Error creating notification: {ex.Message}");
        }
    }

    public async Task<IResponseResult<IEnumerable<NotificationEntity>>> GetNotificationsAsync(string userId)
    {
        var memberResult = await _memberService.GetMemberByExpressionAsync(x => x.Id == userId);
        var member = ((ResponseResult<Member>)memberResult).Data;
        var entities = await _notificationRepository.GetNotificationsByUserId(userId);

        if (member!.RoleName == "User")
        {
            var userNotifications = entities.Where(n => n.NotificationTargetGroupId == 1).ToList();
            return ResponseResult<IEnumerable<NotificationEntity>>.Ok(userNotifications);
        }
        return ResponseResult<IEnumerable<NotificationEntity>>.Ok(entities);
    }

    public async Task<IResponseResult> DismissNotificationAsync(string notificationId, string userId)
    {
        try
        {
            var alreadyDismissed = await _notificationDismissRepository.AlreadyExistsAsync(x => x.NotificationId == notificationId && x.UserId == userId);
            if (alreadyDismissed)
                throw new Exception("Already dismissed");

            var dismissed = NotificationDismissFactory.CreateEntity(notificationId, userId);
            await _notificationDismissRepository.BeginTransactionAsync();

            await _notificationDismissRepository.AddAsync(dismissed);
            var saveResult = await _notificationDismissRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving NotificationDismissEntity");

            await _notificationDismissRepository.CommitTransactionAsync();
            return ResponseResult.Ok();    
        }
        catch (Exception ex)
        {
            await _notificationDismissRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error :: {ex.Message}");
        }
    }
}
