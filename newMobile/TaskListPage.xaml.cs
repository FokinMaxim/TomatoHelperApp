using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;


namespace newMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskListPage : ContentPage
    {
        public RelativeLayout MyLayout = new RelativeLayout(){};
        
        public TaskListPage()
        {
            BackgroundColor = Xamarin.Forms.Color.FromHex("#FF7373");

            var buttons = ButtonCreator.AlternativeWay(new string[] {
                "newMobile.images.timer.png", "newMobile.images.tasklist.png","newMobile.images.calender.png"  });
            buttons["timer"].Clicked += StartTimer;
            buttons["calender"].Clicked += MoveToCalendar;

            var buttonsSize = new Xamarin.Forms.Size(40, 40);
            MyLayout.Children.Add(buttons["calender"], () => new Xamarin.Forms.Rectangle(
                30, this.Height- buttonsSize.Height - 30, 
                buttonsSize.Width, buttonsSize.Height));

            MyLayout.Children.Add(buttons["timer"], () => new Xamarin.Forms.Rectangle(
                this.Width/2 - buttonsSize.Width/2, this.Height - buttonsSize.Height - 30,
                buttonsSize.Width, buttonsSize.Height));

            MyLayout.Children.Add(buttons["tasklist"], () => new Xamarin.Forms.Rectangle(
                this.Width - 30 - buttonsSize.Width, this.Height - buttonsSize.Height - 30,
                buttonsSize.Width, buttonsSize.Height));
        }


        protected override void OnAppearing()
        {
            var TaskList = new TaskStack(MyLayout);
            Content = MyLayout;
        }

        private async void MoveToCalendar(object sender, EventArgs e)
        {
            UnifiedDataStorage.SaveTaskList();
            await Navigation.PushAsync(new CalendarPage());
        }

        private async void StartTimer(object sender, EventArgs e)
        {
            UnifiedDataStorage.SaveTaskList();
            await Navigation.PushAsync(new MainPage());
        }
    }

    public class TaskStack
    {
        private static ImageButton AddButton;
        private static List<TaskListElement> Tasks;
        private RelativeLayout MainLayout;
        private ScrollView ScrollLayout;
        private StackLayout StackLayout;


        public TaskStack(RelativeLayout layout)
        {
            StackLayout = new StackLayout() { BackgroundColor = Xamarin.Forms.Color.FromHex("#FF7373") };
            ScrollLayout = new ScrollView() { BackgroundColor = Xamarin.Forms.Color.FromHex("#FF7373"), Content = StackLayout };


            AddButton = new ImageButton
            {
                Source = ImageSource.FromResource("newMobile.images.AddTaskButton.png"),
                Scale = 2,
                BackgroundColor = Xamarin.Forms.Color.FromHex("#FF7373"),
                Margin = new Thickness(20)
            };
            AddButton.Clicked += AddElement;

            Tasks = UnifiedDataStorage.TaskListData;
            foreach (var task in Tasks) { StackLayout.Children.Add(task.DisplayElement); }
            StackLayout.Children.Add(AddButton);

            MainLayout = layout;
            MainLayout.Children.Add(ScrollLayout, () => new Xamarin.Forms.Rectangle(20, 50, MainLayout.Width - 120, MainLayout.Height - 200));
        }
        private void AddElement(object sender, EventArgs e)
        {
            var newTask = new TaskListElement("task", new DateTime(1, 1, 1, 0, 0, 0));
            Tasks = Tasks.Append(newTask).ToList();
            StackLayout.Children.Add(newTask.DisplayElement);

            if (StackLayout.Children.Contains(AddButton)) StackLayout.Children.Remove(AddButton);
            StackLayout.Children.Add(AddButton);
            UnifiedDataStorage.RefreshTaskList(Tasks);
        }
    }
}