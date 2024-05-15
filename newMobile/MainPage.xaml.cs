using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace newMobile
{
    
    public partial class MainPage : ContentPage
    {
        public static bool FirstEnteryIndicator = true;
        public Tomato MyTomato = new Tomato();
        public Grid MyLayout = new Grid();
        public MainPage()
        {
            BackgroundColor = Color.FromHex("#FF7373");

            var Lab = new Label() {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            Lab.Text = "Timer";
            MyLayout.Children.Add(Lab);

            if (FirstEnteryIndicator) 
            { 
                Navigation.PushModalAsync(new TomatoLabel()); 
                FirstEnteryIndicator = false;
            }

            var TimerButton = new ImageButton
            {
                Source = ImageSource.FromResource("newMobile.images.timer.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,

                Padding = new Thickness(5),
                Margin = new Thickness(10),

                Scale = 2,
            };
            TimerButton.Clicked += StartTimer;

            var TaskListButton = new ImageButton
            {
                Source = ImageSource.FromResource("newMobile.images.tasklist.png"),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,

                Padding = new Thickness(5),
                Margin = new Thickness(15),

                Scale =2,

            };
            TaskListButton.Clicked += MoveToTaskList;

            var SettingsButton = new ImageButton
            {
                Source = ImageSource.FromResource("newMobile.images.settings.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                
                Padding = new Thickness(5),
                Margin = new Thickness(100),

                Scale = 2,
            };
            SettingsButton.Clicked += MoveToSettings;

            var CalendarButton = new ImageButton
            {
                Source = ImageSource.FromResource("newMobile.images.calender.png"),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,

                Padding = new Thickness(5),
                Margin = new Thickness(10),

                Scale = 2,
            };
            CalendarButton.Clicked += MoveToCalendar;

            
            MyLayout.Children.Add(SettingsButton);
            MyLayout.Children.Add(TimerButton);
            MyLayout.Children.Add(TaskListButton);
            MyLayout.Children.Add(CalendarButton);

        }



        protected override void OnAppearing()
        {
            Content = MyLayout;
        }

        private async void MoveToCalendar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());
        }

        private async void MoveToSettings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }
        private async void MoveToTaskList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskListPage());
        }

        private async void StartTimer(object sender, EventArgs e)
        {
        }
    }
}
