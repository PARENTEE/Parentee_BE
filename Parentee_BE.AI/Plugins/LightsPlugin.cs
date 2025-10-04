using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Parentee_BE.AI.Plugins.PluginDto;

namespace Parentee_BE.AI.Plugins;

public class LightsPlugin
{
    
    private readonly  List<LightModel> _lights = new()
    {
        new LightModel { Id = 1, Name = "Table Lamp", IsOn = false, Brightness = Brightness.Medium, Color = "#FFFFFF" },
        new LightModel { Id = 2, Name = "Porch light", IsOn = false, Brightness = Brightness.High, Color = "#FF0000" },
        new LightModel { Id = 3, Name = "Chandelier", IsOn = true, Brightness = Brightness.Low, Color = "#FFFF00" }
    };

    [KernelFunction("get_lights")]
    [Description("Gets a list of lights and their current state")]
    public async Task<List<LightModel>> GetLightsAsync()
    {
        return _lights;
    }

    [KernelFunction("change_state")]
    [Description("Changes the state of the light")]
    public async Task<LightModel?> ChangeStateAsync(LightModel changeState)
    {
        // Find the light to change
        var light = _lights.FirstOrDefault(l => l.Id == changeState.Id);

        // If the light does not exist, return null
        if (light == null)
        {
            return null;
        }

        // Update the light state
        light.IsOn = changeState.IsOn;
        light.Brightness = changeState.Brightness;
        light.Color = changeState.Color;

        return light;
    }
}