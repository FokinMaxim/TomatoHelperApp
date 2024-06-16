using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace newMobile
{
    
    public partial class TomatoLabel : ContentPage
    {
        public static Label label = new Label
        {

            FontSize = 50,
            Text = "TomatoHelper",
            FontFamily = "Grandstander",
            TextColor = Color.White
        };
        public static Tomato MyTomato = new Tomato();
        public RelativeLayout MyLayout = new RelativeLayout();
        public TapGestureRecognizer trr = new TapGestureRecognizer();
        public TomatoLabel()
        {
            MyTomato.TomatoImage.IsVisible = true;
            
            BackgroundColor = Color.FromHex("#FF7373");
            Content = MyLayout;
            trr.Tapped += (sender, e) =>
            {
                Jump();
            };
            MyLayout.GestureRecognizers.Add(trr);
           
        }

        public void TomatoLabelAnimation()
        {

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                MyTomato.TomatoImage.TranslateTo(0, 500, 1250);
                label.TranslateTo(0, -480, 1250);
                return false;
            });
        }
        
        public void Jump()
        {
            Navigation.PopModalAsync(false);
        }
        protected override void OnAppearing()
        {

            MyLayout.Children.Add(
                MyTomato.TomatoImage,
                Constraint.RelativeToParent((parent) => parent.Width * MyTomato.Position.X),
                Constraint.RelativeToParent((parent) => parent.Height * MyTomato.Position.Y)
                );
            MyLayout.Children.Add(
                label,
                Constraint.RelativeToParent((parent) => parent.Width * 0.08),
                Constraint.RelativeToParent((parent) => parent.Height * 1.2)
                );
            
            TomatoLabelAnimation();
            
        }
    }
}