using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace fruitwin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Fruit Win";
            btn_start_game.Clicked += Btn_start_game_Clicked;

            lb_my_point.Text = "Total Score: " + Preferences.Get("score", 0).ToString();
        }

        private void Btn_start_game_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new start_game());
            lb_my_point.Text = "Total Score: " + Preferences.Get("score", 0).ToString();
        }
    }
}
