using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace newMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        public Grid MyLayout = new Grid();
        public CalendarPage()
        {
            BackgroundColor = Color.FromHex("#FF7373");

            var Lab = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            Lab.Text = "Calendar";
            MyLayout.Children.Add(Lab);

            var TimerButton = ButtonCreator.CreateImageButton("newMobile.images.timer.png",
                LayoutOptions.End, LayoutOptions.Center, 10);
            TimerButton.Clicked += StartTimer;


            var TaskListButton = ButtonCreator.CreateImageButton("newMobile.images.tasklist.png",
                LayoutOptions.End, LayoutOptions.End, 15);
            TaskListButton.Clicked += MoveToTaskList;


            var CalendarButton = ButtonCreator.CreateImageButton("newMobile.images.calender.png",
                            LayoutOptions.End, LayoutOptions.Start, 10);

            MyLayout.Children.Add(TimerButton);
            MyLayout.Children.Add(TaskListButton);
            MyLayout.Children.Add(CalendarButton);

        }
        protected override void OnAppearing()
        {
            Content = MyLayout;
        }


        private async void MoveToTaskList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskListPage());
        }

        private async void StartTimer(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}