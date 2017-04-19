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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _2D_Peli_Harjoitustyö
{
   
    public sealed partial class MainPage : Page
    {
        CanvasBitmap StartScreen;
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static float DesignWidth = 1600;
        public static float DesignHeight = 1200;
        public static float scaleWidth, scaleHeight;

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
        }

        private void GameCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreateResourcesAsync(CanvasControl sender)
        {
            StartScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/start-screen.png"));
        }

        
        private void GameCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(Scaling.img(StartScreen));

            GameCanvas.Invalidate();
        }

        private void GameCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
