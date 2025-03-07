using MAUI_BarcodeScanner.Services;
using MAUI_BarcodeScanner.Views;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace MAUI_BarcodeScanner;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.UseBarcodeReader()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
    

		DependencyService.Register<MockDataStore>();
		DependencyService.Register<Inventory>();

		#if DEBUG
			builder.Logging.AddDebug();
		#endif

		return builder.Build();
	}
}
