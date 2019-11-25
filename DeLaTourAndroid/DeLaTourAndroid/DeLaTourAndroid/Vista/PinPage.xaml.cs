using DeLaTourAndroid.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace DeLaTourAndroid.Vista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PinPage : ContentPage
	{
		public PinPage (pines pin)
		{
            NavigationPage.SetHasNavigationBar(this, false);
            Image image = new Image
            {
                Source = ImageSource.FromUri(new Uri(pin.imagen)),
                HeightRequest = 350,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            Label labelTitulo = new Label
            {
                Text = $"{pin.titulo}",
                FontSize = 20,
                FontFamily = Device.RuntimePlatform == Device.Android ? "WorkSans-Medium.ttf#WorkSans-Medium" : null,
                TextColor = Color.White,
            };
            Label labelDescripcion = new Label
            {
                Text = $"{pin.descripcion}",
                FontSize = 16,
                FontFamily = Device.RuntimePlatform == Device.Android ? "WorkSans-Regular.ttf#WorkSans-Regular" : null,
                TextColor = Color.White
            };
            Map map = new Map
            {
                MapType = MapType.Hybrid,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsShowingUser = true,
                HasScrollEnabled = false,
                HasZoomEnabled = false,
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(pin.coords.latitud,pin.coords.longitud), Distance.FromKilometers(.05)));
            StackLayout stackLayout = new StackLayout
            {
                Padding = new Thickness(8,8,8,8),
                BackgroundColor = Color.FromHex("142856"),
                Children = {
                    image,
                    labelTitulo,
                    labelDescripcion,
                    map
                }
            };
            this.Content = stackLayout;
		}
	}
}