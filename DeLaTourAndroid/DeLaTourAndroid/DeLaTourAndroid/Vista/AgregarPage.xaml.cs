using DeLaTourAndroid.Controlador;
using DeLaTourAndroid.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace DeLaTourAndroid.Vista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarPage : ContentPage
	{
        Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map();
        Label label = new Label();
        Label lblTitulo = new Label();
        Label lblDescripcion = new Label();
        Button btnAgregar = new Button();
        Entry txtTitulo = new RoundedEntry();
        Entry txtDescripcion = new RoundedEntry();
        public AgregarPage ()
		{
            DisplayAlert("AVISO", "Al agregar una recomendación se utilizará tu ubicación, asesgúrese de estar en el punto a recomendar antes de continuar", "Ok");
            NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent ();
            label = new Label()
            {
                Text = "Recomendar sitio de interés",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontFamily = Device.RuntimePlatform == Device.Android ? "WorkSans-Medium.ttf#WorkSans-Medium" : null,
                TextColor = Color.White,
            };

            map = new Xamarin.Forms.Maps.Map()
            {
                MapType = MapType.Hybrid,
                HasZoomEnabled = true,
                HasScrollEnabled = false,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsShowingUser = true,
                HeightRequest = 150
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.152464, -101.711059), Distance.FromKilometers(.15)));

            lblTitulo = new Label()
            {
                Text = "Título",
                FontSize = 16,
                FontFamily = Device.RuntimePlatform == Device.Android ? "WorkSans-Medium.ttf#WorkSans-Medium" : null,
                TextColor = Color.White,
            };

            txtTitulo = new RoundedEntry()
            {
                FontFamily = Device.RuntimePlatform == Device.Android ? "WorkSans-Medium.ttf#WorkSans-Medium" : null,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Placeholder = "Título", 
                WidthRequest = 370,
                Margin = new Thickness(4,4,4,4)
            };

            lblDescripcion = new Label()
            {
                Text = "Descripción",
                FontSize = 16,
                FontFamily = Device.RuntimePlatform == Device.Android ? "WorkSans-Medium.ttf#WorkSans-Medium" : null,
                TextColor = Color.White,
            };

            txtDescripcion = new RoundedEntry()
            {
                FontFamily = Device.RuntimePlatform == Device.Android ? "WorkSans-Medium.ttf#WorkSans-Medium" : null,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Placeholder = "Descripción",
                WidthRequest = 370,
                Margin = new Thickness(4, 4, 4, 4)
            };

            btnAgregar = new Button()
            {
                BackgroundColor = Color.FromHex("F00A2A"),
                WidthRequest = 300,
                TextColor = Color.White,
                FontFamily = Device.RuntimePlatform == Device.Android ? "WorkSans-Medium.ttf#WorkSans-Medium" : null,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = "Enviar recomendación",
                BorderRadius = 30
            };

            btnAgregar.Clicked += BtnAgregar_Clicked;

            StackLayout stackLayout = new StackLayout()
            {
                BackgroundColor = Color.FromHex("142856"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    label,
                    map,
                    //lblTitulo,
                    txtTitulo,
                    //lblDescripcion,
                    txtDescripcion,
                    btnAgregar
                }
            };
            this.Content = stackLayout;
        }

        private async void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            PinController pinController = new PinController();
            pines pin = new pines();
            coords coords = new coords();
            try
            {
                if (txtTitulo.Text != "" && txtTitulo.Text != null && txtDescripcion.Text != "" && txtDescripcion.Text != null)
                {
                    Location location = await Geolocation.GetLocationAsync();
                    pin.titulo = txtTitulo.Text;
                    pin.descripcion = txtDescripcion.Text;
                    coords.longitud = float.Parse(location.Longitude.ToString());
                    coords.latitud = float.Parse(location.Latitude.ToString());
                    pin.coords = new coords()
                    { 
                        longitud = coords.longitud,
                        latitud = coords.latitud
                    };                  
                    pin.estatus = 0;
                    pin.imagen = "";
                    pines pin2 = new pines();
                    pin2 = await pinController.Registrar(pin);
                    if(pin2.titulo != "" && pin2.titulo != null)
                    {
                        DependencyService.Get<IToast>().LongAlert("La recomendación ha sido enviada exitosamente");
                    }
                    else
                        DependencyService.Get<IToast>().LongAlert("Ha fallado el envío de recomendación");
                }
                else
                    DependencyService.Get<IToast>().LongAlert("Faltan campos por llenar");
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().LongAlert($"Ha fallado el envío de recomendación {ex.Message}");
            }
        }
    }
}