using Microsoft.Extensions.Hosting;
using System.Windows;

namespace WPF_App_Starter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().Build();
        }
    }
}
