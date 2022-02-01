using Demo.Services;
using Prism;
using Prism.Ioc;
using FileAccessService = Demo.UWP.Services.FileAccessService;

namespace Demo.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new Demo.App(new UwpInitializer()));
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterSingleton<IFileAccessService, FileAccessService>();
        }
    }
}
