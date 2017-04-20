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
using System.Numerics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _2D_Peli_Harjoitustyö
{
   
    public sealed partial class MainPage : Page
    {
        public static CanvasBitmap BG, StartScreen, Level1, ScoreScreen, Bullet, Enemy1, Enemy2, ENEMY_IMG, Player, Boom;
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static float DesignWidth = 1600;
        public static float DesignHeight = 1200;
        public static float scaleWidth, scaleHeight, pointX, pointY, bulletX, bulletY, MyScore, boomX, boomY;
        public static int boomCount = 60;   // frames

        public static int countdown = 60; // 60s roundtime
        public static bool RoundEnded = false;

        public static int GameState = 0; // startscreen

        public static DispatcherTimer RoundTimer = new DispatcherTimer();
        public static DispatcherTimer EnemyTimer = new DispatcherTimer();

        //Lists (Projectile)
        public static List<float> bulletXPOS = new List<float>();
        public static List<float> bulletYPOS = new List<float>();
        public static List<float> percent = new List<float>();

        //Lists (Enemies)
        public static List<float> enemyXPOS = new List<float>();
        public static List<float> enemyYPOS = new List<float>();
        public static List<int> enemyTYPE = new List<int>();
        public static List<string> enemyDIR = new List<string>();

        //Random Generators
        public Random EnemyTypeRand = new Random(); // enemy type 1 or 2
        public Random EnemyGenRand = new Random(); // generation interval

        public Random EnemyXstart = new Random(); // random start position to all enemies


        //private Player player;

               
        // game loop timer
        private DispatcherTimer timer;

        // canvas width and height (used to randomize a new flower)
        //private double CanvasWidth;
        //private double CanvasHeight;

        // which keys are pressed 
        private bool UpPressed;
        private bool LeftPressed;
        private bool RightPressed;


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

            EnemyTimer.Tick += EnemyTimer_Tick;
            EnemyTimer.Interval = new TimeSpan(0, 0, 0, 0, EnemyGenRand.Next(300, 3000));
        
        

            // change the default startup mode
            /*ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            
            ApplicationView.PreferredLaunchViewSize = new Size(1600, 1200);*/

            // key listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            // get canvas width and height
            /*CanvasWidth = MyCanvas.Width;
            CanvasHeight = MyCanvas.Height;*/

            
            /*player = new Player
            {
                LocationX = CanvasWidth / 2,
                LocationY = CanvasHeight / 2
            };*/
            

            

            // init audio
           // InitAudio();

            // game loop 
            /*timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            timer.Start();*/
            
        }

        private void EnemyTimer_Tick(object sender, object e)
        {
            int ES = EnemyTypeRand.Next(1,3);
            int SP = EnemyXstart.Next(0, (int)bounds.Width); // starting position X
            if (SP > bounds.Width / 2)
            {
                enemyDIR.Add("left");
            }
            else
            {
                enemyDIR.Add("right");
            }
                        
            enemyXPOS.Add(SP);
            enemyYPOS.Add(-50 * scaleHeight);
            enemyTYPE.Add(ES);

            EnemyTimer.Interval = new TimeSpan(0, 0, 0, 0, EnemyGenRand.Next(500, 2000));
        }

        private void Roundtimer_Tick(object sender, object e)
        {
            countdown -= 1;
            /*// move 
            if (UpPressed) player.Move();

            // rotate
            if (LeftPressed) player.Rotate(-1);
            if (RightPressed) player.Rotate(1);

            // update
            player.UpdateLocation();

            // collision
            //CheckCollision();*/

            if (countdown < 1)
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
         private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
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
         }

        /// <summary>
        /// Check if some keys are released.
        /// </summary>
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
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
            Player = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/hahmo.png"));
            Enemy1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Enemy1.png"));
            Enemy2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Enemy2.png"));
            Boom = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Boom.png"));
        }

        
        private void GameCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            GameManager.GAMEMANAGER();
            args.DrawingSession.DrawImage(Scaling.img(BG));
            args.DrawingSession.DrawText(countdown.ToString(), 100, 100, Colors.Yellow);

            if (GameState > 0)
            {
                
                args.DrawingSession.DrawText("Score: " + MyScore.ToString(), (float)bounds.Width / 2, 10, Colors.White);

                if (boomX > 0 && boomY > 0 && boomCount > 0)
                {
                    args.DrawingSession.DrawImage(Scaling.img(Boom), boomX, boomY);
                    boomCount -= 1;
                }
                else
                {
                    boomCount = 60;
                    boomX = 0;
                    boomY = 0;
                }

                // Enemies
                for(int j = 0; j < enemyXPOS.Count; j++)
                {

                    if (enemyTYPE[j] == 1) { ENEMY_IMG = Enemy1; }
                    if (enemyTYPE[j] == 2) { ENEMY_IMG = Enemy2; }


                    if (enemyDIR[j] == "left")
                    {
                        enemyXPOS[j] -= 3;
                    }
                    else
                    {
                        enemyXPOS[j] += 3;
                    }

                    enemyYPOS[j] += 3;
                    args.DrawingSession.DrawImage(Scaling.img(ENEMY_IMG), enemyXPOS[j], enemyYPOS[j]);
                }
            

                //Display projectiles
                for (int i = 0; i < bulletXPOS.Count; i++) // every time new bullet is tapped to list -> shoot projectile
                {
                    pointX = (bulletX + (bulletXPOS[i] - bulletX) * percent[i]); //linear interpolation
                    pointY = (bulletY + (bulletYPOS[i] - bulletY) * percent[i]);
                    args.DrawingSession.DrawImage(Scaling.img(Bullet), pointX - (32 * scaleWidth), pointY -(32 * scaleHeight)); // delete half of the picture size to compensate the bullet and it will but the bullet right to the center of the tap

                    percent[i] += (0.050f * scaleHeight);


                    for (int h = 0; h < enemyXPOS.Count; h++)
                    {
                        if (pointX >= enemyXPOS[h] && pointX <= enemyXPOS[h] + (256 * scaleWidth) && pointY >= enemyYPOS[h] && pointY <= enemyYPOS[h] + (256 * scaleHeight))
                        {

                            boomX = pointX -(128 * scaleWidth);
                            boomY = pointY -(128 * scaleHeight);

                            enemyXPOS.RemoveAt(h);
                            enemyYPOS.RemoveAt(h);
                            enemyTYPE.RemoveAt(h);
                            enemyDIR.RemoveAt(h);

                            bulletXPOS.RemoveAt(i);
                            bulletYPOS.RemoveAt(i);
                            percent.RemoveAt(i);

                            MyScore = MyScore + 100;
                            break;
                        }
                    }
                   

                    if (pointY < 0f) // delete bullets which are out of map
                    {
                        bulletXPOS.RemoveAt(i);
                        bulletYPOS.RemoveAt(i);
                        percent.RemoveAt(i);
                    }
                }

                // Draw Player
                args.DrawingSession.DrawImage(Scaling.img(Player), (float)bounds.Width / 2 - (139 * scaleWidth), (float)bounds.Height - (260 * scaleHeight));
                
            }


            GameCanvas.Invalidate();
        }

        private void GameCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(RoundEnded == true)
            {
                GameState = 0;
                RoundEnded = false;
                countdown = 60;

                //Stop Enemy Timer
                EnemyTimer.Stop();
                enemyXPOS.Clear();
                enemyYPOS.Clear();
                enemyTYPE.Clear();
                enemyDIR.Clear();

            }
            else
            {
                if(GameState == 0)
                {
                    GameState += 1;
                    RoundTimer.Start();
                    EnemyTimer.Start();
                    
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
