using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace MauiAppMinhasCompras;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.ConfigureLifecycleEvents(events =>
        {
#if WINDOWS
            events.AddWindows(windows =>
            {
                windows.OnWindowCreated(window =>
                {
                    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                    var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                    appWindow.Resize(new Windows.Graphics.SizeInt32(640, 740));

                    var displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(
                        windowId,
                        Microsoft.UI.Windowing.DisplayAreaFallback.Primary);

                    var centerX = (displayArea.WorkArea.Width - 640) / 2;
                    var centerY = (displayArea.WorkArea.Height - 740) / 2;

                    appWindow.Move(new Windows.Graphics.PointInt32(centerX, centerY));
                });
            });
#endif
        });

        return builder.Build();
    }
}