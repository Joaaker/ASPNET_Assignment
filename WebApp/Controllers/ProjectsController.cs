﻿using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Domain.Dtos;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]
public class ProjectsController(IProjectService projectService, IHubContext<NotificationHub> notficationHub, INotificationService notificationService, IClientService clientService, IStatusService statusService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    private readonly IHubContext<NotificationHub> _notificationHub = notficationHub;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IClientService _clientService = clientService;
    private readonly IStatusService _statusService = statusService;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProjectViewModel formData)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

            return BadRequest(new { success = false, errors });
        }

        ProjectRegistrationDto dto = formData;

        var createResult = await _projectService.CreateProjectAsync(dto);
        if (createResult.Success)
        {
            if (await _projectService.GetProjectByExpressionAsync(x => x.Title == formData.Title) is IResponseResult<Project> projectResult && projectResult.Data != null)
            {
                var project = projectResult.Data;
                var message = $"{project.Title} added";
                var notificationEntity = NotificationFactory.CreateDto(1, 2, message, null);

                await _notificationService.AddNotificationAsync(notificationEntity);
            }
            return Ok(new { success = true });
        } else 
            return StatusCode(createResult.StatusCode, new { success = false, message = createResult.ErrorMessage });
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectForEdit(int id)
    {
        var projectResponse = await _projectService.GetProjectByExpressionAsync(x => x.Id == id);
        if (projectResponse is not IResponseResult<Project> projectResult || projectResult.Data == null)
            return NotFound(new { success = false, message = "Project not found." });
        var project = projectResult.Data;

        var statusResponse = await _statusService.GetStatusByExpressionAsync(x => x.StatusName == projectResult.Data.StatusName);
        if (statusResponse is not IResponseResult<Status> statusResult || statusResult.Data == null)
            return NotFound(new { success = false, message = "Status not found." });
        var status = statusResult.Data;

        var clientResponse = await _clientService.GetClientByExpressionAsync(x => x.ClientName == projectResult.Data.ClientName);
        if (clientResponse is not IResponseResult<Client> clientResult || clientResult.Data == null)
            return NotFound(new { success = false, message = "Client not found." });
        var client = clientResult.Data;

        return Json(new { project, status, client});
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProjectViewModel formData)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

            return BadRequest(new { success = false, errors });
        }

       ProjectRegistrationDto dto = formData;

        var updateResult = await _projectService.UpdateProjectAsync(formData.Id, dto);

        if (!updateResult.Success)
            return StatusCode(updateResult.StatusCode, new { success = false, message = updateResult.ErrorMessage });

        return Ok(new { success = true });
    }


    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _projectService.DeleteProjectAsync(id);
        if (!deleteResult.Success)
            return StatusCode(deleteResult.StatusCode, new { success = false, message = deleteResult.ErrorMessage });

        return RedirectToAction("Index");
    }
}