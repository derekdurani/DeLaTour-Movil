using DeLaTourAndroid.Controlador;
using DeLaTourAndroid.Modelo;
using DeLaTourAndroid.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeLaTourAndroid
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);          
        }
       
        private async void OnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            UsuarioController usuarioController = new UsuarioController();
            usuarios usuario = new usuarios();
            try
            {
                if (txtCorreo.Text != "" && txtContrasenia.Text != "" && txtCorreo.Text != null && txtContrasenia.Text != null)
                {
                    if (IsValidEmail(txtCorreo.Text))
                    {
                        usuario.correo = txtCorreo.Text;
                        usuario.contrasenia = txtContrasenia.Text;
                        pgrBar.Progress = 0;
                        pgrBar.IsVisible = true;
                        await pgrBar.ProgressTo(1, 1000, Easing.Linear);
                        usuario = await usuarioController.Login(usuario);
                        pgrBar.IsVisible = false;
                        if (usuario.nombre != "" || usuario.nombre != null)
                        {
                            if (usuario.estatus == 0)
                            {
                                DependencyService.Get<IToast>().LongAlert($"Bienvenido {usuario.nombre}");
                                Navigation.InsertPageBefore(new PrincipalPage(), this);
                                await Navigation.PopAsync().ConfigureAwait(false);
                            }
                            else
                                DependencyService.Get<IToast>().LongAlert("Tu usuario no tiene acceso a este sistema");
                        }
                        else
                            DependencyService.Get<IToast>().LongAlert("Código / Contraseña incorrectos");
                    }
                    else
                        DependencyService.Get<IToast>().LongAlert("Correo electrónico no válido");
                }
                else
                    DependencyService.Get<IToast>().LongAlert("Faltan campos por llenar");
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().LongAlert($"Código / Contraseña incorrectos");
            }
        }

        private void OnRegitrarse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegistroPage());
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
