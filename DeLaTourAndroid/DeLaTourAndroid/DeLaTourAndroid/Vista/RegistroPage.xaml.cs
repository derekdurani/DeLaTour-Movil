using DeLaTourAndroid.Controlador;
using DeLaTourAndroid.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeLaTourAndroid.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
        }

        private async void OnRegitrarse_Clicked(object sender, EventArgs e)
        {
            UsuarioController usuarioController = new UsuarioController();
            var usu = new usuarios();
            try
            {
                if(txtCorreo.Text != "" && txtContrasenia.Text != "" && txtCorreo.Text != null && txtContrasenia.Text != null && txtContrasenia2.Text != null && txtContrasenia2.Text != null && txtNombre.Text != "" && txtNombre.Text != null)
                {
                    if (IsValidEmail(txtCorreo.Text))
                    {
                        if(txtContrasenia.Text == txtContrasenia2.Text)
                        {
                            usu.correo = txtCorreo.Text;
                            usu.contrasenia = txtContrasenia.Text;
                            usu.nombre = txtNombre.Text;
                            usu.estatus = 0;
                            pgrBar.Progress = 0;
                            pgrBar.IsVisible = true;
                            await pgrBar.ProgressTo(1, 1000, Easing.Linear);
                            usu = await usuarioController.Registrar(usu);
                            pgrBar.IsVisible = false;
                            if(usu.nombre != "" || usu.nombre != null)
                            {
                                DependencyService.Get<IToast>().LongAlert($"Registro exitoso {usu.nombre}");
                            }
                            else
                                DependencyService.Get<IToast>().LongAlert($"Registro fallido");
                        }
                        else
                            DependencyService.Get<IToast>().LongAlert($"Las contraseñas no coinciden");
                    }
                    else
                        DependencyService.Get<IToast>().LongAlert($"El correo electrónico no es válido");
                }
                else
                    DependencyService.Get<IToast>().LongAlert($"Faltan campos por llenar");
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().LongAlert($"Registro fallido {ex.Message}");
            }
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