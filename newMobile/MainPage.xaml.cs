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
        public RelativeLayout MyLayout = new RelativeLayout() { };
        public MainPage()
        {
            if (UnifiedDataStorage.IsJustOpened)
            {
                UnifiedDataStorage.IsJustOpened = false;
                UnifiedDataStorage.PrepareStorage();
            }
            BackgroundColor = Color.FromHex("#FF7373");
            UnifiedDataStorage.GetSavedValues();

            var Lab = new Label() { Text = "Timer" };
            MyLayout.Children.Add(Lab, () => new Rectangle(20, 20, 100, 100));

            if (FirstEnteryIndicator) 
            { 
                Navigation.PushModalAsync(new TomatoLabel()); 
                FirstEnteryIndicator = false;
            }

            var buttons = ButtonCreator.AlternativeWay(new string[] {
                "newMobile.images.timer.png", "newMobile.images.tasklist.png", 
                "newMobile.images.calender.png", "newMobile.images.settings.png"  });

            buttons["timer"].Clicked += StartTimer;
            buttons["tasklist"].Clicked += MoveToTaskList;
            buttons["calender"].Clicked += MoveToCalendar;
            buttons["settings"].Clicked += MoveToSettings;

            var buttonsSize = new Size(40, 40);
            MyLayout.Children.Add(buttons["calender"], () => new Rectangle(
                30, this.Height - buttonsSize.Height - 30,
                buttonsSize.Width, buttonsSize.Height));

            MyLayout.Children.Add(buttons["timer"], () => new Rectangle(
                this.Width / 2 - buttonsSize.Width / 2, this.Height - buttonsSize.Height - 30,
                buttonsSize.Width, buttonsSize.Height));

            MyLayout.Children.Add(buttons["tasklist"], () => new Rectangle(
                this.Width - 30 - buttonsSize.Width, this.Height - buttonsSize.Height - 30,
                buttonsSize.Width, buttonsSize.Height));

            MyLayout.Children.Add(buttons["settings"], () => new Rectangle(
                this.Width / 2 - buttonsSize.Width / 2, this.Height - 2 * buttonsSize.Height - 80,
                buttonsSize.Width, buttonsSize.Height));
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
