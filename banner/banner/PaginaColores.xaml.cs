using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using banner;

namespace banner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaColores : ContentPage
    {
        List<Color> lista_colores = new List<Color>();

        string bkg = "0", txt = "0";

        
        public  PaginaColores()
        {

            InitializeComponent();
            pk_seleccion.SelectedIndex = 0;
            View CreateColorView(Color color, string name, int index)
            {
                Frame nuevo_frame = new Frame
                {

                    Padding = new Thickness(5),
                    TabIndex = index,
                    BackgroundColor = color,
                    HeightRequest = 80,

                };

                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += TapGestureRecognizer_Tapped;
                nuevo_frame.GestureRecognizers.Add(tap);


                return nuevo_frame;
            }


            int contador = 0;

            foreach (FieldInfo info in typeof(Color).GetRuntimeFields())
            {
                if (info.GetCustomAttribute<ObsoleteAttribute>() != null)
                    continue;
                if (info.IsPublic && info.IsStatic && info.FieldType == typeof(Color))
                {
                    
                        
                    lista_colores.Add((Color)info.GetValue(null));
                    stk.Children.Add(CreateColorView((Color)info.GetValue(null), info.Name, contador));

                    contador++;
                }

            }
            

        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame f = (Frame)sender;
            
            if (pk_seleccion.SelectedItem.ToString() == "Fondo")
            {
                bkg = f.TabIndex.ToString();
                fram.BackgroundColor = lista_colores.ElementAt(f.TabIndex);               
            }
            if (pk_seleccion.SelectedItem.ToString() == "Texto")
            {
                txt = f.TabIndex.ToString();
                pk_seleccion.TextColor = lista_colores.ElementAt(f.TabIndex);
            }

            

        }
        private void btn_aplicar_Clicked(object sender, EventArgs e)
        {
           
            Application.Current.Properties["bkg"] = bkg;
            Application.Current.Properties["txt"] = txt;

            Navigation.PopAsync();
        }

        private void btn_descartar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}