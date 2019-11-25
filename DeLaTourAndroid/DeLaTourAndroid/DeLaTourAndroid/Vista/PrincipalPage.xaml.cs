using DeLaTourAndroid.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

using DeLaTourAndroid.Modelo;
using Xamarin.Essentials;
using Plugin.Geolocator;

namespace DeLaTourAndroid.Vista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrincipalPage : ContentPage
	{
        List<pines> pinesGlobal = new List<pines>();
        Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map();
        Pin pin = new Pin();
        StackLayout stackLayout = new StackLayout();

        public PrincipalPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            UpdateMapAsync();
            StartListening();
		}

        private async void UpdateMapAsync()
        {
            var pinController = new PinController();
            List<pines> pines = new List<pines>();
            pines = await pinController.GetPinesAgregados();
            pinesGlobal = pines;

            map = new Xamarin.Forms.Maps.Map()
            {
                MapType = MapType.Hybrid,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsShowingUser  = true,
                HasScrollEnabled = false
            };
            
            stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("142856"),
                Children =
                {
                    map
                }
            };
            List<Pin> pins = new List<Pin>();
            for (int i = 0; i < pinesGlobal.Count; i++)
            {
                pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(pinesGlobal[i].coords.latitud, pinesGlobal[i].coords.longitud),
                    Label = pinesGlobal[i].titulo,
                };
                pins.Add(pin);
                pin.MarkerClicked += Pin_MarkerClicked; ; ;
            }
            for (int i = 0; i < pins.Count; i++)
            {
                map.Pins.Add(pins[i]);
            }
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.152464, -101.711059), Distance.FromKilometers(.15)));
            this.Content = stackLayout;
        }

        private async void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            Pin pin = (Pin)sender;
            var pines = new pines();
            pines.titulo = pin.Label;
            var pinController = new PinController();
            pines = await pinController.GetPinNombre(pines);
            await Navigation.PushModalAsync(new PinPage(pines));

            var location = await Geolocation.GetLastKnownLocationAsync();

            var coord1 = new Location(location.Latitude, location.Longitude);
            var coord2 = new Location(pin.Position.Latitude, pin.Position.Longitude);

            var distance = Location.CalculateDistance(coord1,coord2,DistanceUnits.Kilometers);
            DependencyService.Get<IToast>().LongAlert($"Distancia: {distance.ToString()}");
        }

        async Task StartListening()
        {
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true, new Plugin.Geolocator.Abstractions.ListenerSettings
            {
                ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(1),
                ListenForSignificantChanges = true,
                PauseLocationUpdatesAutomatically = false
            });

            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var test = e.Position;
                Location gpsLocation = new Location(test.Latitude, test.Longitude); 
                for (int i = 0; i < pinesGlobal.Count; i++)
                {
                    Location pinLocation = new Location(pinesGlobal[i].coords.latitud, pinesGlobal[i].coords.longitud);
                    var distance = Location.CalculateDistance(gpsLocation, pinLocation, DistanceUnits.Kilometers);
                    if(distance <= 0.070)
                        DependencyService.Get<IToast>().LongAlert($"Estas cerca del: {pinesGlobal[i].titulo.ToString()}");
                }
            });
        }
    }
}