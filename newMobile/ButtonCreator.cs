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
    }
}
