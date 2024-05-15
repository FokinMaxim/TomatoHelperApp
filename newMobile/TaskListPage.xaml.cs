using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace newMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskListPage : ContentPage
    {
        public Grid MyLayout = new Grid();
        
        public TaskListPage()
        {
            
            BackgroundColor = Color.FromHex("#FF7373");

            var TimerButton = ButtonCreator.CreateImageButton("newMobile.images.timer.png",
                LayoutOptions.End, LayoutOptions.Center, 10);
            TimerButton.Clicked += StartTimer;


            var TaskListButton = ButtonCreator.CreateImageButton("newMobile.images.tasklist.png",
                LayoutOptions.End, LayoutOptions.End, 15);


            var CalendarButton = ButtonCreator.CreateImageButton("newMobile.images.calender.png",
                            LayoutOptions.End, LayoutOptions.Start, 10);
            CalendarButton.Clicked += MoveToCalendar;
            
            MyLayout.Children.Add(TimerButton);
            MyLayout.Children.Add(TaskListButton);
            MyLayout.Children.Add(CalendarButton);
        }


        protected override void OnAppearing()
        {
            var TaskList = new TaskStack(MyLayout);
            TaskList.AddToLayout();
            Content = MyLayout;
        }

        private async void MoveToCalendar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());
        }

        private async void StartTimer(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }

    public class TaskStack
    {
        private static ImageButton AddButton;
        private static ImageButton[] Tasks;
        private Grid Layout;
        

        public TaskStack(Grid layout)
        {
            AddButton = new ImageButton
            {
                Source = ImageSource.FromResource("newMobile.images.AddTaskButton.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,

                Padding = new Thickness(10),
                Margin = new Thickness(10),
                Scale = 2
            };
            AddButton.Clicked += AddElement;
            Tasks = new ImageButton[] { };
            Layout = layout;
        }
        private void AddElement(object sender, EventArgs e)
        {
            var NewButton = AddButton = new ImageButton
            {
                Source = ImageSource.FromResource("newMobile.images.taskBackground.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,

                Padding = new Thickness(10),
                Margin = new Thickness(10),
                Scale = 2
            };
            AddButton.Clicked += ChangeElement;
            Tasks.Append(NewButton);
            AddToLayout();
        }

        private void ChangeElement(object sender, EventArgs e)
        {

        }

        public void AddToLayout()
        {
            if (Layout.Children.Contains(AddButton)) Layout.Children.Remove(AddButton);
            foreach(var task in Tasks)
            {
                if(!Layout.Children.Contains(task)) Layout.Children.Add(task);
                
            }
            Layout.Children.Add(AddButton);
        }
    }
}