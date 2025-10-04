using System.ComponentModel;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Parentee_BE.AI.Plugins.PluginDto;
using Parentee_BE.BLL.Services.Interfaces;

namespace Parentee_BE.AI.Plugins;

public class ChildPlugin
{
    private readonly IChildService _childService;
    private readonly IMapper _mapper;
    private readonly ILogger<ChildPlugin> _logger;
    public ChildPlugin(IChildService childService, IMapper mapper, ILogger<ChildPlugin> logger)
    {
        _childService = childService;
        _mapper = mapper;
        _logger = logger;
    }

    [KernelFunction("get_children_today")]
    [Description("Get today's data for a child including measurement, feeding, diaper changes, and sleeps.")]
    [return: Description("The child's daily data including measurement, feedings, sleeps, and diaper changes.")]
    public async Task<GetChildTodayForAiResponse> GetChildrenToday(
        [Description("The unique identifier (GUID) of the child.")] Guid childId)
    {
        try
        {
            _logger.LogInformation("Get children today for {ChildId}", childId);
            var childEntity = await _childService.GetChildTodayById(childId);

            if (childEntity == null)
            {
                _logger.LogWarning("Child with ID {ChildId} not found.", childId);
                return null; // or throw a custom exception
            }

            return _mapper.Map<GetChildTodayForAiResponse>(childEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting children today!");
            throw;
        }
    }


}