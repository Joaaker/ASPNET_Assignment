using System.Diagnostics;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Domain.Dtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Business.Services;

public class NotificationService(INotificationRepository notificationRepository, INotificationDismissRepository notificationDismissRepository) : INotificationService
{
    private readonly INotificationDismissRepository _notificationDismissRepository = notificationDismissRepository;
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    public async Task<IResponseResult> AddNotificationAsync(NotificationDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Image))
            {
                switch (dto.NotificationTypeId)
                {
                    case 1:
                        dto.Image = "~/Images/Profiles/user-template.svg";
                        break;

                    case 2:
                        dto.Image = "~/Images/Projects/project-template.svg";
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
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _notificationRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error creating project :: {ex.Message}");
        }
    }

    public async Task<IResponseResult<IEnumerable<NotificationEntity>>> GetNotificationsAsync(string userId)
    {
        var entites = await _notificationRepository.GetNotificationsByUserId(userId);
        return ResponseResult<IEnumerable<NotificationEntity>>.Ok(entites);
    }


    public async Task<IResponseResult> DismissNotificationAsync(string notificationId, string userId)
    {
        try
        {
            var alreadyDismissed = await _notificationDismissRepository.AlreadyExistsAsync(x => x.NotificationId == notificationId && x.UserId == userId);
            if (alreadyDismissed == false)
            {
                var dismissed = NotificationDismissFactory.CreateEntity(notificationId, userId);
                await _notificationDismissRepository.BeginTransactionAsync();

                await _notificationDismissRepository.AddAsync(dismissed);
                var saveResult = await _notificationDismissRepository.SaveAsync();
                if (saveResult == false)
                    throw new Exception("Error saving NotificationDismissEntity");

                await _notificationDismissRepository.CommitTransactionAsync();
                return ResponseResult.Ok();
            }
            else
                throw new Exception("Already dismissed");
        }
        catch (Exception ex)
        {
            await _notificationDismissRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error :: {ex.Message}");
        }
    }
}
