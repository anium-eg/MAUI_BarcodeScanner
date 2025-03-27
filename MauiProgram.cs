using BarcodeScanning;
using CommunityToolkit.Maui;
using MAUI_BarcodeScanner.Components;
using MAUI_BarcodeScanner.Services;
using MAUI_BarcodeScanner.ViewModels;
using MAUI_BarcodeScanner.Views;
using Microsoft.Extensions.Logging;
using The49.Maui.BottomSheet;
using ZXing.Net.Maui.Controls;

namespace MAUI_BarcodeScanner;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>().UseBarcodeScanning()
			.UseMauiCommunityToolkit()
			.UseBottomSheet()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddSingleton<HttpClient>();

		builder.Services.AddSingleton<Datastore>();
		builder.Services.AddSingleton<Inventory>();

		builder.Services.AddTransient<CartViewModel>();
		builder.Services.AddTransient<ScannerCameraViewModel>();
		builder.Services.AddTransient<ItemDetailsViewModel>();

		builder.Services.AddTransient<ItemDetailSheet>();

		#if DEBUG
			builder.Logging.AddDebug();
		#endif

		return builder.Build();
	}
}
