using System.ComponentModel;
using SpellBookGenerator.Pages;

namespace SpellBookGenerator;

public class LoadingService
{
    public bool IsVisible { get; set; }
    public string Message { get; set; } = "";

    public LoadingOverlayProvider? Provider { get; set; }

    private Task UpdateUi()
    {
        if (Provider is null)
        {
            throw new Exception("OverlayProvider Not Registered");
        }
        
        return Provider.Update();
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