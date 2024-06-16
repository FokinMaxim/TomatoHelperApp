using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;


namespace newMobile
{
    public static class UnifiedDataStorage
    {
        public static List<TaskListElement> TaskListData = new List<TaskListElement> { };
        public static List<int> aboba = new List<int>();
        public static bool IsJustOpened = true;

        public async static void RefreshTaskList(List<TaskListElement> taskList)
        {
            TaskListData = taskList;
        }

        public static void PrepareStorage()
        {
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            TaskListData = new List<TaskListElement> { };
           
            var TLdata = File.ReadAllText(Path.Combine(docs, "taskListData.txt"));
            foreach(var elem in TLdata.Split())
            {
                var elemdata = elem.Split('/');
                if (elemdata.Length != 3) continue;
                TaskListData.Add(new TaskListElement(elemdata[0], new DateTime(1, 1, 1, 1, int.Parse(elemdata[1]), int.Parse(elemdata[2]))));
            }
        }

        public async static void SaveTaskList()
        {
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filename = Path.Combine(docs, "taskListData.txt");

            StreamWriter sw = new StreamWriter(filename);
            foreach( var elem in TaskListData)

            {
                var stringBuilder = new StringBuilder(elem.Name);
                stringBuilder.Append("/" + elem.DateTime.Minute.ToString() + "/" + elem.DateTime.Second.ToString());
                sw.WriteLine(stringBuilder);
            }
            sw.Close();

            Console.WriteLine(File.ReadAllText(filename));
        }

        public static void GetSavedValues()
        {
            var taskList = new Object();
            if (Application.Current.Properties.TryGetValue("taskList", out taskList))
            {
                TaskListData = (List<TaskListElement>)taskList;
            }
        }

        public static void DeleteElementFromTaskList(TaskListElement elemToDelete)
        {
            var layout = (StackLayout)(elemToDelete.DisplayElement.Parent);
            layout.Children.Remove(elemToDelete.DisplayElement);
            TaskListData.Remove(elemToDelete);
        }
    }

    public class TimerData
    {

    }

    public class TaskListElement
    {
        public string Name;
        public DateTime DateTime;
        public Frame DisplayElement;
        public Button DeleteButton;

        public TaskListElement(string name, DateTime dateTime)
        {
            Name = name;
            DateTime = dateTime;


            var image = new Image() { Source = ImageSource.FromResource("newMobile.images.taskBackground.png") };

            var nameLable = new Entry()
            {
                Placeholder = "Task",
                FontFamily = "Grandstander",
            };
            nameLable.Completed += NameChanged;

            var dateLabel = new Entry()
            {
                Placeholder = "00:00",
                FontFamily = "Grandstander",
                Keyboard = Keyboard.Telephone,
            };
            dateLabel.TextChanged += TimeChanged;
            dateLabel.Completed += NewDateSet;

            DeleteButton = new Button()
            {
                Text = "Delete",
                TextColor = Color.Red,
                BackgroundColor = Color.FromHex("#FFAAAA"),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
            };
            DeleteButton.Clicked += DeleteElement;



            var Layout = new StackLayout()
            {
                Children = { nameLable, dateLabel, DeleteButton },
                BackgroundColor = Color.FromHex("FFAAAA"),
                Orientation = StackOrientation.Horizontal,
            };

            var frame = new Frame
            {
                Content = Layout,
                BorderColor = Color.FromHex("#FFAAAA"),
                BackgroundColor = Color.FromHex("#FFAAAA"),
                WidthRequest = 10,
                HeightRequest = 45,
                CornerRadius = 30,
            };

            DisplayElement = frame;
        }

        private void NewDateSet(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            var minutesAndSeconds = entry.Text.Split(':');
            DateTime = new DateTime(1, 1, 1, 1, int.Parse(minutesAndSeconds[0]), int.Parse(minutesAndSeconds[1]));
        }

        private void NameChanged(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            Name = entry.Text;
        }

        private void TimeChanged(object sender, EventArgs e)
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

        private void DeleteElement(object sender, EventArgs e)
        {
            UnifiedDataStorage.DeleteElementFromTaskList(this);
        }
    }
}
