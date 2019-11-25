using DeLaTourAndroid.Vista;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DeLaTourAndroid
{
    public partial class App : Application
    {
        public NavigationPage PrincipalPage { get; set; }
        public NavigationPage PinPage { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PrincipalPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
