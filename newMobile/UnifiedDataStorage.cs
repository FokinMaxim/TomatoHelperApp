﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

using Plugin.LocalNotification;


namespace newMobile
{
    public static class UnifiedDataStorage
    {
        public static List<TaskListElement> TaskListData = new List<TaskListElement> { };
        public static bool IsJustOpened = true;
        public static List<int> TimerMinutes = new List<int>() {25, 5, 25, 5, 25, 10 };
        public static List<int> TimerSeconds = new List<int>() { 0, 0, 0, 0, 0, 0 };

        public async static void RefreshTaskList(List<TaskListElement> taskList)
        {
            TaskListData = taskList;
        }

        public static void PrepareStorage()
        {
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            
            TaskListData = new List<TaskListElement> { };
            if (File.Exists(Path.Combine(docs, "taskListData.txt")))
            {
                var TLdata = File.ReadAllText(Path.Combine(docs, "taskListData.txt"));
                foreach(var elem in TLdata.Split())
                {
                    var elemdata = elem.Split('/');
                    if (elemdata.Length != 3) continue;
                    TaskListData.Add(new TaskListElement(elemdata[0], new DateTime(1, 1, 1, 1, int.Parse(elemdata[1]), int.Parse(elemdata[2]))));
                }
            }

            if (File.Exists(Path.Combine(docs, "timerData.txt")))
            {
                var TimerData = File.ReadAllText(Path.Combine(docs, "timerData.txt")).Split();
                
                TimerMinutes = TimerData[0].Split('/').Select(x => int.Parse(x)).ToList();
                TimerSeconds = TimerData[1].Split('/').Select(x => int.Parse(x)).ToList();
            }
        }

        public static void UpdateActivityTime(int minutes, int seconds)
        {
            TimerMinutes[0] = minutes;
            TimerMinutes[2] = minutes;
            TimerMinutes[4] = minutes;

            TimerSeconds[0] = seconds;
            TimerSeconds[2] = seconds;
            TimerSeconds[4] = seconds;
        }

        public static void UpdateShortRestTime(int minutes, int seconds)
        {
            TimerMinutes[1] = minutes;
            TimerMinutes[3] = minutes;

            TimerSeconds[1] = seconds;
            TimerSeconds[3] = seconds;
        }

        public static void UpdateLongRestTime(int minutes, int seconds)
        {
            TimerMinutes[5] = minutes;

            TimerSeconds[5] = seconds;
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
        }

        public async static void SaveTimerData()
        {
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filename = Path.Combine(docs, "timerData.txt");

            StreamWriter sw = new StreamWriter(filename);
            
            sw.WriteLine(string.Join("/", TimerMinutes));
            sw.WriteLine(string.Join("/", TimerSeconds));
            sw.Close();
        }

        public static void DeleteElementFromTaskList(TaskListElement elemToDelete)
        {
            var layout = (StackLayout)(elemToDelete.DisplayElement.Parent);
            layout.Children.Remove(elemToDelete.DisplayElement);
            TaskListData.Remove(elemToDelete);
        }
    }

    public class TaskListElement
    {
        public string Name;
        public DateTime DateTime;
        public Frame DisplayElement;
        public ImageButton DeleteButton;
        public Button SaveButton;

        public TaskListElement(string name, DateTime dateTime)
        {
            Name = name;
            DateTime = dateTime;


            var image = new Image() { Source = ImageSource.FromResource("newMobile.images.taskBackground.png") };

            var nameLable = new Entry()
            {
                Placeholder = name,
                FontFamily = "Grandstander",
                WidthRequest = 100,
            };
            nameLable.Completed += NameChanged;

            var dateLabel = new Entry()
            {
                Placeholder = ((dateTime.Minute < 10) ? "0" + dateTime.Minute.ToString() : dateTime.Minute.ToString()) +
                ":" + ((dateTime.Second < 10) ? "0" + dateTime.Second.ToString() : dateTime.Second.ToString()),
                FontFamily = "Grandstander",
                Keyboard = Keyboard.Telephone,
                WidthRequest = 60,
            };
            dateLabel.TextChanged += TimeChanged;
            dateLabel.Completed += NewDateSet;

            SaveButton = new Button()
            {
                Text = "Save",
                WidthRequest = 55,
                FontSize = 12,
                BackgroundColor = Color.FromHex("#FFAAAA"),
                Margin = 0,
            };
            SaveButton.Clicked += CreateNotification;

            DeleteButton = new ImageButton()
            {
                Source = ImageSource.FromResource("newMobile.images.trash-svgrepo-com.jpg"),
                WidthRequest = 50,
                BackgroundColor = Color.FromHex("#FFAAAA"),
                Margin = 0,
            };
            DeleteButton.Clicked += DeleteElement;


            var Layout = new StackLayout()
            {
                Children = { nameLable, dateLabel,  SaveButton, DeleteButton},
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
            SaveButton.TextColor = Color.Black;
        }

        private void TimeChanged(object sender, EventArgs e)
        {
            SaveButton.TextColor = Color.Black;
            var i = 0;
            if (!(sender is Entry)) return;
            var entry = (Entry)sender;

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

        private void CreateNotification(object sender, EventArgs e)
        {
            SaveButton.TextColor = Color.Green;
            var now = DateTime.Now;
            var notification = new NotificationRequest()
            {
                BadgeNumber = 1,
                NotificationId = 69,
                Title = Name,
                Description = "Dedline is approach",
                NotifyTime = new DateTime(now.Year, now.Month, now.Day, DateTime.Minute, DateTime.Second, 0),
            };

            NotificationCenter.Current.Show(notification);
        }
    }
}
