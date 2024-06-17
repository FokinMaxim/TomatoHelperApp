using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace newMobile
{
    public static class ButtonCreator
    {
        
        public static ImageButton CreateImageButton(string sourse, LayoutOptions vertical, LayoutOptions horisontal, int margin)
        {
            return new ImageButton
            {
                Source = ImageSource.FromResource(sourse),
                HorizontalOptions = horisontal,
                VerticalOptions = vertical,

                Padding = new Thickness(5),
                Margin = new Thickness(margin),

                Scale = 2,
            };
        }
        public static Dictionary<string, ImageButton> AlternativeWay(string[] sourses)
        {
            var DictButton = new Dictionary<string, ImageButton>();

            foreach(var sourse in sourses)
            {
                var nameSourse = sourse.Split('.');
                DictButton[nameSourse[nameSourse.Length - 2]] = new ImageButton
                {
                    Source = ImageSource.FromResource(sourse),
                    Scale = 2,
                    BackgroundColor = Color.FromHex("#FF7373"),
                    Margin = new Thickness(0),
                };
            }

            return DictButton;
        }
    }
}
