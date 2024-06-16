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
        public RelativeLayout MyLayout = new RelativeLayout() { };
        public CalendarPage()
        {
            BackgroundColor = Color.FromHex("#FF7373");

            var Lab = new Label()
            {Text = "Calendar"};

            MyLayout.Children.Add(Lab, () => new Rectangle(20, 20, 100, 100));

            var buttons = ButtonCreator.AlternativeWay(new string[] {
                "newMobile.images.timer.png", "newMobile.images.tasklist.png","newMobile.images.calender.png"  });
            buttons["timer"].Clicked += StartTimer;
            buttons["tasklist"].Clicked += MoveToTaskList;

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