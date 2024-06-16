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
            await Navigation.PushAsync(new CalendarPage());
        }

        private async void MoveToTaskList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskListPage());
        }

        private async void StartTimer(object sender, EventArgs e)
        {
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
            , () => new Rectangle(137, 20, 155, 64));

            MyLayout.Children.Add(new Label()
            {
                Text = "BREAK",
                FontSize = 50,
                FontFamily = "Grandstander",
            }
            , () => new Rectangle(129, 201, 170, 64));

            MyLayout.Children.Add(new Label()
            {
                Text = "LONG BREAK",
                FontSize = 50,
                FontFamily = "Grandstander",
            }
            , () => new Rectangle(41, 382, 346, 64));


            var workEntry = new Entry()
            {
                Placeholder = "25:00",
                PlaceholderColor = Color.White,
                TextColor = Color.White,
                FontSize = 64,
                FontFamily = "Grandstander",
                Keyboard = Keyboard.Telephone,
            };
            workEntry.TextChanged += timeChanged;
            MyLayout.Children.Add(workEntry, () => new Rectangle(98, 84, 234, 94));

            var breakEntry = new Entry()
            {
                Placeholder = "05:00",
                PlaceholderColor = Color.White,
                TextColor = Color.White,
                FontSize = 64,
                FontFamily = "Grandstander",
                Keyboard = Keyboard.Telephone,
            };
            breakEntry.TextChanged += timeChanged;
            MyLayout.Children.Add(breakEntry, () => new Rectangle(121, 265, 187, 96));

            var longBreakEntry = new Entry()
            {
                Placeholder = "10:00",
                PlaceholderColor = Color.White,
                TextColor = Color.White,
                FontSize = 64,
                FontFamily = "Grandstander",
                Keyboard = Keyboard.Telephone,
            };
            longBreakEntry.TextChanged += timeChanged;
            MyLayout.Children.Add(longBreakEntry, () => new Rectangle(109, 436, 223, 96));
        }

        private void timeChanged(object sender, EventArgs e)
        {
            var i = 0;
            if (!(sender is Entry)) return;
            var entry = (Entry)sender;

            /*if (!int.TryParse(entry.Text[entry.Text.Length-1].ToString(), out i) || entry.Text.Length > 5)
            {
                var newText = entry.Text.Remove(entry.Text.Length-1);
                entry.Text = newText;
                return;
            }*/

            if (entry.Text.Length == 3)
            {
                var newText = entry.Text.Remove(entry.Text.Length - 1) + ":" + entry.Text[entry.Text.Length - 1];
                entry.Text = newText;
            }
        }
    }
}