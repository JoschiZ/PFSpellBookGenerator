using System.ComponentModel;
using SpellBookGenerator.Pages;

namespace SpellBookGenerator;

public class LoadingService
{
    public bool IsVisible { get; set; }
    public string Message { get; set; } = "";

    public LoadingOverlayProvider? Provider { get; set; }
    public bool SteppedLoading { get; set; }
    public double CurrentSteppedValue { get; set; }
    public double MaxSteps { get; set; }

    private Task UpdateUi()
    {
        if (Provider is null)
        {
            throw new Exception("OverlayProvider Not Registered");
        }
        
        return Provider.Update();
    }

    public async Task<Func<string, Task>> StartSteppedLoading(int overallSteps, string initialMessage)
    {
        SteppedLoading = true;
        CurrentSteppedValue = 0;
        IsVisible = true;
        MaxSteps = overallSteps;
        Message = initialMessage;
        await UpdateUi();
        return async (string message) =>
        {
            Message = message;
            CurrentSteppedValue += 1;
            await UpdateUi();
        };
    }

    public async Task FinishLoading()
    {
        IsVisible = false;
        SteppedLoading = false;
        CurrentSteppedValue = 0;
        Message = "";
        await UpdateUi();
    }
    
    public async Task<TReturn> ShowAsync<TReturn>(string message, Func<Task<TReturn>> func)
    {
        Message = message;
        IsVisible = true;
        await UpdateUi();

        var result = await func();

        IsVisible = false;
        await UpdateUi();
        return result;
    }

    public async Task ShowAsync(string message, Func<Task> func)
    {
        Message = message;
        IsVisible = true;
        await UpdateUi();
        
        await func();

        IsVisible = false;
        await UpdateUi();
    }

}