using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace newMobile
{
    
    public  class Tomato
    {
        public Image TomatoImage = new Image
        {
            Source = ImageSource.FromResource("newMobile.tomato.png"),
            Scale = 2.5,
        };
        public double posi = 0;
        public Point Step = new Point(0, 7);
        public Point Position = new Point(0.310, -0.3);
    }

    
}
