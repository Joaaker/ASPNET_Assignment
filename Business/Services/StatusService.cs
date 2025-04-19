using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;
using System.Diagnostics;
using System.Linq.Expressions;

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

    public async Task<IResponseResult> GetStatusByExpressionAsync(Expression<Func<StatusEntity, bool>> expression)
    {
        try
        {
            var status = await _statusRepository.GetModelAsync(expression);

            return ResponseResult<Status>.Ok(status);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving client");
        }
    }
}