using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;
using System.Diagnostics;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<IResponseResult<IEnumerable<Status>>> GetAllStatusesAsync()
    {
            var statuses = await _statusRepository.GetAllAsync();
            var statusModels = statuses.Select(s => s.MapTo<Status>());

            return ResponseResult<IEnumerable<Status>>.Ok(statusModels);
    }

    public async Task<IResponseResult> GetStatusByNameAsync(string statusName)
    {
        var status = await _statusRepository.GetAsync(x => x.StatusName == statusName);
        var result = status.MapTo<Status>();
        return ResponseResult<Status>.Ok(result);
    }

    public async Task<IResponseResult> GetStatusByIdAsync(int id)
    {
        var status = await _statusRepository.GetAsync(x => x.Id == id);
        var result = status.MapTo<Status>();
        return ResponseResult<Status>.Ok(result);
    }
}