using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace banner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaColores : ContentPage
    {
        List<Color> lista_colores = new List<Color>();
        List<Color> list_col = new List<Color>();
        
        public PaginaColores()
        {
            list_col.Add(Color.White);
            list_col.Add(Color.Black);

            InitializeComponent();
            pk_seleccion.SelectedIndex = 0;
            View CreateColorView(Color color, string name, int index)
            {
                Frame nuevo_frame = new Frame
                {
                    BorderColor = Color.Blue,
                    Padding = new Thickness(5),
                    TabIndex = index,
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 15,
                        Children =
                        {

                                new BoxView
                                {
                                    Color = color
                                },
                                new Label
                                {
                                    Text = name,
                                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                    FontAttributes = FontAttributes.Bold,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.StartAndExpand
                                },
                                new StackLayout
                                {
                                    Children =
                                    {
                                        new Label
                                        {
                                            Text = String.Format("{0:X2}-{1:X2}-{2:X2}",
                                            (int)(255 * color.R),
                                            (int)(255 * color.G),
                                            (int)(255 * color.B)),
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                            IsVisible = color != Color.Default
                                        },
                                        new Label
                                        {
                                            Text = String.Format("{0:F2}, {1:F2}, {2:F2}",
                                            color.Hue,
                                            color.Saturation,
                                            color.Luminosity),
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                            IsVisible = color != Color.Default
                                        }
                                    },
                                    HorizontalOptions = LayoutOptions.End
                                }
                        }

                    }

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
                fram.BackgroundColor = lista_colores.ElementAt(f.TabIndex);
                list_col[0] = lista_colores.ElementAt(f.TabIndex);
            }
            if (pk_seleccion.SelectedItem.ToString() == "Texto")
            {
                pk_seleccion.TextColor = lista_colores.ElementAt(f.TabIndex);
                list_col[1] = lista_colores.ElementAt(f.TabIndex);
            }

            

        }
    }
}