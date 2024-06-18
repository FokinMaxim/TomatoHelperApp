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
    public partial class Settings : ContentPage
    {
        public RelativeLayout MyLayout = new RelativeLayout() { };
        public Settings()
        {
            BackgroundColor = Color.FromHex("#FF7373");
            PreparePage(MyLayout);
           
            var buttons = ButtonCreator.AlternativeWay(new string[] {
                "newMobile.images.timer.png", "newMobile.images.tasklist.png",
                "newMobile.images.calender.png", "newMobile.images.settingsLightBackground.png"  });

            buttons["timer"].Clicked += StartTimer;
            buttons["tasklist"].Clicked += MoveToTaskList;
            buttons["calender"].Clicked += MoveToCalendar;

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

            MyLayout.Children.Add(buttons["settingsLightBackground"], () => new Rectangle(
                this.Width / 2 - buttonsSize.Width / 2, this.Height - 2 * buttonsSize.Height - 80,
                buttonsSize.Width, buttonsSize.Height));

        }
        protected override void OnAppearing()
        {
            Content = MyLayout;
        }

        private async void MoveToCalendar(object sender, EventArgs e)
        {
            UnifiedDataStorage.SaveTimerData();
            await Navigation.PushAsync(new CalendarPage());
        }

        private async void MoveToTaskList(object sender, EventArgs e)
        {
            UnifiedDataStorage.SaveTimerData();
            await Navigation.PushAsync(new TaskListPage());
        }

        private async void StartTimer(object sender, EventArgs e)
        {
            UnifiedDataStorage.SaveTimerData();
            await Navigation.PushAsync(new MainPage());
        }

        private void PreparePage(RelativeLayout layout)
        {
            var box = new BoxView()
            {
                Color = Color.FromHex("#FFAAAA"),
                CornerRadius = 10,
            };
            var boxSize = new Size(box.Width, box.Height);
            MyLayout.Children.Add(box, () => new Rectangle(0, 20, this.Width, this.Height - 120));


            
            MyLayout.Children.Add(new Label() { 
                Text = "WORK",
                FontSize = 50,
                FontFamily = "Grandstander",
            }
            , () => new Rectangle((this.Width - 120)/2, 20, 155, 64)); //Rectangle(137, 20, 155, 64));

            MyLayout.Children.Add(new Label()
            {
                Text = "BREAK",
                FontSize = 50,
                FontFamily = "Grandstander",
            }
            , () => new Rectangle((this.Width - 135) / 2, 201, 170, 64)); //Rectangle(129, 201, 170, 64))
            var lab = new Label() {Text = "LONG BREACK" };
            var labsize = new Size(lab.Width, lab.Height);
            MyLayout.Children.Add(new Label()
            {
                Text = "LONG BREAK",
                FontSize = 50,
                FontFamily = "Grandstander",
            }
            , () => new Rectangle(((int)this.Width - 270) / 2, 382, 346, 64)); //41


            var workEntry = new Entry()
            {
                Placeholder = 
                ((UnifiedDataStorage.TimerMinutes[0] < 10) ? "0" + UnifiedDataStorage.TimerMinutes[0].ToString() :UnifiedDataStorage.TimerMinutes[0].ToString()) + 
                ":" + ((UnifiedDataStorage.TimerSeconds[0] < 10) ? "0" + UnifiedDataStorage.TimerSeconds[0].ToString() : UnifiedDataStorage.TimerSeconds[0].ToString()),

                PlaceholderColor = Color.White,
                TextColor = Color.White,
                FontSize = 64,
                FontFamily = "Grandstander",
                Keyboard = Keyboard.Telephone,
            };
            workEntry.Completed += SetActivityTime;
            workEntry.TextChanged += timeChanged;
            MyLayout.Children.Add(workEntry, () => new Rectangle(((int)this.Width - 155) / 2, 84, 155, 94));

            var breakEntry = new Entry()
            {
                Placeholder =
                ((UnifiedDataStorage.TimerMinutes[1] < 10) ? "0" + UnifiedDataStorage.TimerMinutes[1].ToString() : UnifiedDataStorage.TimerMinutes[1].ToString()) +
                ":" + ((UnifiedDataStorage.TimerSeconds[1] < 10) ? "0" + UnifiedDataStorage.TimerSeconds[1].ToString() : UnifiedDataStorage.TimerSeconds[1].ToString()),

                PlaceholderColor = Color.White,
                TextColor = Color.White,
                FontSize = 64,
                FontFamily = "Grandstander",
                Keyboard = Keyboard.Telephone,
            };
            breakEntry.Completed += SetShortRestTime;    
            breakEntry.TextChanged += timeChanged;
            MyLayout.Children.Add(breakEntry, () => new Rectangle(((int)this.Width - 155) / 2, 265, 155, 96));

            var longBreakEntry = new Entry()
            {
                Placeholder =
                ((UnifiedDataStorage.TimerMinutes[5] < 10) ? "0" + UnifiedDataStorage.TimerMinutes[5].ToString() : UnifiedDataStorage.TimerMinutes[5].ToString()) +
                ":" + ((UnifiedDataStorage.TimerSeconds[5] < 10) ? "0" + UnifiedDataStorage.TimerSeconds[5].ToString() : UnifiedDataStorage.TimerSeconds[5].ToString()),
                
                PlaceholderColor = Color.White,
                TextColor = Color.White,
                FontSize = 64,
                FontFamily = "Grandstander",
                Keyboard = Keyboard.Telephone,
            };
            longBreakEntry.Completed += SetLongRestTime;
            longBreakEntry.TextChanged += timeChanged;
            MyLayout.Children.Add(longBreakEntry, () => new Rectangle(((int)this.Width - 155) / 2, 436, 155, 96));
        }

        private void timeChanged(object sender, EventArgs e)
        {
            if (!(sender is Entry)) return;
            var entry = (Entry)sender;
            if (entry.Text.Length == 3)
            {
                var newText = entry.Text.Remove(entry.Text.Length - 1) + ":" + entry.Text[entry.Text.Length - 1];
                entry.Text = newText;
            }
        }
        private void SetActivityTime(object sender, EventArgs e)
        {
            if (!(sender is Entry)) return;
            var entry = (Entry)sender;
            var timerData = entry.Text.Split(':');

            UnifiedDataStorage.UpdateActivityTime(int.Parse(timerData[0]), int.Parse(timerData[1]));
        }
        private void SetShortRestTime(object sender, EventArgs e)
        {
            if (!(sender is Entry)) return;
            var entry = (Entry)sender;
            var timerData = entry.Text.Split(':');

            UnifiedDataStorage.UpdateShortRestTime(int.Parse(timerData[0]), int.Parse(timerData[1]));
        }
        private void SetLongRestTime(object sender, EventArgs e)
        {
            if (!(sender is Entry)) return;
            var entry = (Entry)sender;
            var timerData = entry.Text.Split(':');

            UnifiedDataStorage.UpdateLongRestTime(int.Parse(timerData[0]), int.Parse(timerData[1]));
        }
    }
}