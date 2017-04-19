using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace _2D_Peli_Harjoitustyö
{
    public sealed partial class Player : UserControl
    {
        
        private DispatcherTimer timer;

        // offset to show
        private int currentFrame = 0;
        private int direction = 1;
        private int frameHeigth = 220;

        // location
        public double LocationX { get; set; }
        public double LocationY { get; set; }

        // speed
        private readonly double MaxSpeed = 10.0;
        private readonly double Accelerate = 0.5;
        private double speed;

        // angle
        private double Angle = 0;
        private double AngleStep = 5;

       
        public Player()
        {
            this.InitializeComponent();
            // animate 
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 125);
            timer.Start();            
        }
        

        
        private void Timer_Tick(object sender, object e)
        {
            // frame
            if (direction == 1) currentFrame++;
            else currentFrame--;
            if (currentFrame == 0 || currentFrame == 19) direction = -1 * direction;
            // set offset
            SpriteSheetOffset.Y = currentFrame * -frameHeigth;
        }

     
        public void Move()
        {
            
            speed += Accelerate;
            if (speed > MaxSpeed) speed = MaxSpeed;
            SetValue(Canvas.TopProperty, LocationY);

            
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle + 180))) * speed;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle + 180))) * speed;
        }

        
        public void Rotate(int angle)
        {
            Angle += angle * AngleStep;
            PlayerRotateAngle.Angle = Angle;
        }

        
        public void UpdateLocation()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }
    }
}
