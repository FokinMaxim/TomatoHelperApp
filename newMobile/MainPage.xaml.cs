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
        public MainTimer HeadTimer;
        public Dictionary<string, ImageButton> Buttons;
        public MainPage()
        {
            BackgroundColor = Color.FromHex("#FF7373");
            
            if (UnifiedDataStorage.IsJustOpened)
            {
                UnifiedDataStorage.IsJustOpened = false;
                UnifiedDataStorage.PrepareStorage();
            }
            HeadTimer = new MainTimer(UnifiedDataStorage.TimerMinutes[0], UnifiedDataStorage.TimerSeconds[1]);
            MyLayout.Children.Add(HeadTimer.label, () => new Rectangle(this.Width / 4.7, this.Height / 4, 300, 500));

            if (FirstEnteryIndicator) 
            { 
                Navigation.PushModalAsync(new TomatoLabel()); 
                FirstEnteryIndicator = false;
            }

            Buttons = ButtonCreator.AlternativeWay(new string[] {
                "newMobile.images.timer.png", "newMobile.images.tasklist.png",
                "newMobile.images.calender.png", "newMobile.images.settings.png"  });
            
            Buttons["timer"].Clicked += StartTimer;
            Buttons["tasklist"].Clicked += MoveToTaskList;
            Buttons["calender"].Clicked += MoveToCalendar;
            Buttons["settings"].Clicked += MoveToSettings;

            var buttonsSize = new Size(40, 40);
            MyLayout.Children.Add(Buttons["calender"], () => new Rectangle(
                30, this.Height - buttonsSize.Height - 30,
                buttonsSize.Width, buttonsSize.Height));

            MyLayout.Children.Add(Buttons["timer"], () => new Rectangle(
                this.Width / 2 - buttonsSize.Width / 2, this.Height - buttonsSize.Height - 30,
                buttonsSize.Width, buttonsSize.Height));

            MyLayout.Children.Add(Buttons["tasklist"], () => new Rectangle(
                this.Width - 30 - buttonsSize.Width, this.Height - buttonsSize.Height - 30,
                buttonsSize.Width, buttonsSize.Height));

            MyLayout.Children.Add(Buttons["settings"], () => new Rectangle(
                this.Width / 2 - buttonsSize.Width / 2, this.Height - 2 * buttonsSize.Height - 80,
                buttonsSize.Width, buttonsSize.Height));
        }



        protected override void OnAppearing()
        {
            Content = MyLayout;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (HeadTimer.ChangeColor)
                {
                    HeadTimer.label.Text = ((HeadTimer.Minutes < 10) ? "0" + HeadTimer.Minutes.ToString() : HeadTimer.Minutes.ToString()) + ":" + ((HeadTimer.Second < 10) ? "0" + HeadTimer.Second.ToString() : HeadTimer.Second.ToString());
                    this.BackgroundColor = HeadTimer.BackgroundColors;
                    foreach (var button in Buttons)
                        button.Value.BackgroundColor = HeadTimer.BackgroundColors;
                    HeadTimer.ChangeColor = false;
                }


                return true;
            });
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
            if (HeadTimer.Run == false) HeadTimer.mainTimer();
        }
    }
}
