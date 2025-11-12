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
    
    [KernelFunction("get_children_status")]
    [Description("Get a specific data for a child including feeding, diaper changes, and sleeps.")]
    [return: Description("The child's daily data including measurement, feedings, sleeps, and diaper changes.")]
    public async Task<GetChildTodayForAiResponse> GetChildrenStatus(
        [Description("The unique identifier (GUID) of the parent user.")] Guid userId,
        [Description("The name of the child.")]
        string childName,
        [Description("The date that user want to search (optional).")]
        DateTime? date = null)
    {
        try
        {
            // ✅ Nếu user không truyền date, mặc định là hôm nay UTC
            var searchDate = date?.Date ?? DateTime.UtcNow.Date;

            var childEntity = await _childService.GetChildStatus(userId, searchDate, childName);

            if (childEntity == null)
            {
                _logger.LogWarning("Child not found.");
                return null; // hoặc throw custom exception
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