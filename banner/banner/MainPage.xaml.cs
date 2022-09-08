using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace banner
{
    public partial class MainPage : ContentPage
    {
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
            }
            else
            {
                stk_base.BackgroundColor = Color.White;
                btn_color.Source = "oscuro.png";
                clockLabel.TextColor = Color.Gray;
            }
            
        
            
        
        }       
    }
}
