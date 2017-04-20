using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using _2D_Peli_Harjoitustyö.Class;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _2D_Peli_Harjoitustyö
{
   
    public sealed partial class MainPage : Page
    {
        public static CanvasBitmap BG, StartScreen, Level1, ScoreScreen, Bullet;
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static float DesignWidth = 1600;
        public static float DesignHeight = 1200;
        public static float scaleWidth, scaleHeight, pointX, pointY, bulletX, bulletY;        

        public static int countdown = 6; // 60s roundtime
        public static bool RoundEnded = false;

        public static int GameState = 0; // startscreen

        public static DispatcherTimer RoundTimer = new DispatcherTimer();

        //Lists (Projectile)
        public static List<float> bulletXPOS = new List<float>();
        public static List<float> bulletYPOS = new List<float>();
        public static List<float> percent = new List<float>();

        /*private Player player;

               
        // game loop timer
        private DispatcherTimer timer;

        // canvas width and height (used to randomize a new flower)
        private double CanvasWidth;
        private double CanvasHeight;

        // which keys are pressed 
        private bool UpPressed;
        private bool LeftPressed;
        private bool RightPressed;*/


        /*// audio
        private MediaElement mediaElement;*/

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            Scaling.SetScale();
            bulletX = (float)bounds.Width / 2;
            bulletY = (float)bounds.Height;

            RoundTimer.Tick += Roundtimer_Tick;
            RoundTimer.Interval = new TimeSpan(0, 0, 1);
        
        

            // change the default startup mode
            /*ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            
            ApplicationView.PreferredLaunchViewSize = new Size(1600, 1200);*/

            // key listeners
            /*Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;*/

            // get canvas width and height
            /*CanvasWidth = MyCanvas.Width;
            CanvasHeight = MyCanvas.Height;

            
            player = new Player
            {
                LocationX = CanvasWidth / 2,
                LocationY = CanvasHeight / 2
            };
            MyCanvas.Children.Add(player);*/

            

            // init audio
           // InitAudio();

            // game loop 
            /*timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Start();*/
            
        }

        private void Roundtimer_Tick(object sender, object e)
        {
            countdown -= 1;

            if(countdown < 1)
            {
                RoundTimer.Stop();
                RoundEnded = true;
            }
        }




        /// <summary>
        /// Game loop.
        /// </summary>
        /*private void Timer_Tick(object sender, object e)
        {
            // move 
            if (UpPressed) player.Move();

            // rotate
            if (LeftPressed) player.Rotate(-1);
            if (RightPressed) player.Rotate(1);

            // update
            player.UpdateLocation();

            // collision
            //CheckCollision();
        }*/



        /// <summary>
        /// Check if some keys are pressed.
        /// </summary>
        /* private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
         {
             switch (args.VirtualKey)
             {
                 case VirtualKey.Up:
                     UpPressed = true;                    
                     break;
                 case VirtualKey.Left:
                     LeftPressed = true;
                     break;
                 case VirtualKey.Right:
                     RightPressed = true;
                     break;
                 default:
                     break;
             }
         }*/

        /// <summary>
        /// Check if some keys are released.
        /// </summary>
        /* private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
         {
             switch (args.VirtualKey)
             {
                 case VirtualKey.Up:
                     UpPressed = false;
                     break;
                 case VirtualKey.Left:
                     LeftPressed = false;
                     break;
                 case VirtualKey.Right:
                     RightPressed = false;
                     break;
                 default:
                     break;
             }
         }
         */
        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            Scaling.SetScale();

            bulletX = (float)bounds.Width / 2; // change position
            bulletY = (float)bounds.Height;
        }

        private void GameCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreateResourcesAsync(CanvasControl sender)
        {
            StartScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/startscreen.png"));
            Level1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/background.png"));
            ScoreScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/ScoreScreen.png"));
            Bullet = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/bullet.png"));
        }

        
        private void GameCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            GameManager.GAMEMANAGER();
            args.DrawingSession.DrawImage(Scaling.img(BG));
            args.DrawingSession.DrawText(countdown.ToString(), 100, 100, Colors.Yellow);

            //Display projectiles
            for (int i = 0; i < bulletXPOS.Count; i++) // every time new bullet is tapped to list -> shoot projectile
            {
                pointX = (bulletX + (bulletXPOS[i] - bulletX) * percent[i]);
                pointY = (bulletY + (bulletYPOS[i] - bulletY) * percent[i]);
                args.DrawingSession.DrawImage(Scaling.img(Bullet), pointX - (32 * scaleWidth), pointY -(32 * scaleHeight)); // delete half of the picture size to compensate the bullet and it will but the bullet right to the center of the tap

                percent[i] += (0.050f * scaleHeight);

                if(pointY < 0f) // delete bullets which are out of map
                {
                    bulletXPOS.RemoveAt(i);
                    bulletYPOS.RemoveAt(i);
                    percent.RemoveAt(i);
                }
            }

            GameCanvas.Invalidate();
        }

        private void GameCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(RoundEnded == true)
            {
                GameState = 0;
                RoundEnded = false;
                countdown = 6;

            }
            else
            {
                if(GameState == 0)
                {
                    GameState += 1;
                    RoundTimer.Start();
                    
                }
                else if (GameState > 0)
                {
                    bulletXPOS.Add((float)e.GetPosition(GameCanvas).X);
                    bulletYPOS.Add((float)e.GetPosition(GameCanvas).Y);
                    percent.Add(0f);
                }
            }
            
        }
    }
}
