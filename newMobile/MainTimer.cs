using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace newMobile
{
    public class MainTimer
    {
        public int[] TimerTipeMinuteArray = UnifiedDataStorage.TimerMinutes.ToArray();
        public int[] TimerTipeSecondsArray = UnifiedDataStorage.TimerSeconds.ToArray();
        public Color[] ColorsArray = new Color[]
        {
            Color.FromHex("#FF7373"),
            Color.FromHex("#67E667"),
            Color.FromHex("#AD66D5")
        };
        public int Minutes;
        public int Second;
        public Label label;
        public bool Run;
        public bool ChangeColor = false;
        public Color BackgroundColors;
        public MainTimer(int minutes, int second)
        {
            Minutes = minutes;
            Second = second;
            Run = false;
            BackgroundColors = ColorsArray[0];
            label = new Label
            {
                Text = ((Minutes < 10) ? "0" + Minutes.ToString() : Minutes.ToString()) + ":" + ((Second < 10) ? "0" + Second.ToString() : Second.ToString()),
                FontSize = 100,
                FontFamily = "Grandstander",
                TextColor = Color.White
            };
        }
        public void changeTeme(int indexTimerArray)
        {
            Second = TimerTipeSecondsArray[indexTimerArray];
            Minutes = TimerTipeMinuteArray[indexTimerArray];           
            if (indexTimerArray == TimerTipeMinuteArray.Length - 1)
                BackgroundColors = ColorsArray[2];
            else
                BackgroundColors = ColorsArray[indexTimerArray % 2];
            ChangeColor = true;
        }
        public void mainTimer()
        {
            
            Run = true;
            var i = 0;
            changeTeme(0);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (Second == 0 && Minutes == 0)
                {
                    i = (i + 1) % (TimerTipeMinuteArray.Length);
                    changeTeme(i);
                    return true;
                }
                        
                else if (Second == 0)
                {
                    Second = 59;
                    Minutes--;
                }
                else
                    Second--;
                label.Text = ((Minutes < 10) ? "0" + Minutes.ToString() : Minutes.ToString()) + ":" + ((Second < 10) ? "0" + Second.ToString() : Second.ToString());
                return true;
            });

            
        }
    }
}
