using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace banner
{
    public partial class MainPage : ContentPage
    {
        List<Color> lista_colores = new List<Color>();
        public MainPage()
        {
            
            // Handle the SizeChanged event for the page.
            SizeChanged += (object sender, EventArgs args) =>
            {
                // Scale the font size to the page width
                // (based on 11 characters in the displayed string).
                if (this.Width > 0)
                    clockLabel.FontSize = this.Width / 6.5;
            };
            // Start the timer going.
            Device.StartTimer(TimeSpan.FromSeconds(0.9), () =>
            {
                // Set the Text property of the Label.
                clockLabel.Text = DateTime.Now.ToString("h:mm:ss tt");

                return true;
            });
            InitializeComponent();
            



        }

        private void btn_color_Clicked(object sender, EventArgs e)
        {
            
            if (stk_base.BackgroundColor == Color.White)
            {
                clockLabel.TextColor = Color.White;
                stk_base.BackgroundColor = Color.Black;
                btn_color.Source = "claro.png";

                Application.Current.Properties["bkg"] = "7";
                Application.Current.Properties["txt"] = "137";
              
                
            }
            else
            {
                clockLabel.TextColor = Color.Black;
                stk_base.BackgroundColor = Color.White;
                btn_color.Source = "oscuro.png";
                
                Application.Current.Properties["bkg"] = "137";
                Application.Current.Properties["txt"] = "7";
            }
        }

        private async void btn_paleta_color_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaColores());
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();


            foreach (FieldInfo info in typeof(Color).GetRuntimeFields())
            {
                if (info.GetCustomAttribute<ObsoleteAttribute>() != null)
                    continue;
                if (info.IsPublic && info.IsStatic && info.FieldType == typeof(Color))
                {

                    lista_colores.Add((Color)info.GetValue(null));

                }

            }

            int background_color = 0;
            int text_color = 1;
            if (Application.Current.Properties.ContainsKey("bkg"))
            {
                if (Application.Current.Properties["bkg"] as string != null && Application.Current.Properties["bkg"] as string != "")
                {
                    background_color = int.Parse(Application.Current.Properties["bkg"] as string);

                }
            }
            if (Application.Current.Properties.ContainsKey("txt"))
            {
                if (Application.Current.Properties["txt"] as string != null && Application.Current.Properties["txt"] as string != "")
                {
                    text_color = int.Parse(Application.Current.Properties["txt"] as string);
                }
            }
            stk_base.BackgroundColor = lista_colores[background_color];
            clockLabel.TextColor = lista_colores[text_color];

        }
    }
}
