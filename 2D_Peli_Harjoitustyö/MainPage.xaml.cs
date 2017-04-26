using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Core;
using _2D_Peli_Harjoitustyö.Class;
using Windows.UI;
using Windows.UI.Xaml.Media;
using System.Numerics;
using Windows.Storage;
using Windows.Media.Playback;
using Windows.Media.Core;


// Juho Askola H3465
// Sampa Lietonen H3957
// Niko Tamminen H8946
//
//
// Simppeli 2D-peli, jossa yksi kenttä. Pelaaja on paikallaan kentän keskellä alhaalla. 
// Vihollisia tulee satunnaisesti kentän yläkulmasta joita pitää ampua ja ne räjähtää.
// Kentän pääsee läpi tappamalla kaikki viholliset ennenkuin ne pääsevät karkuun.
//
// Ohjelmointi tehtiin käyttämällä Win2D.
// Se on ohjelmointirajapinta jolla voidaan luoda 2D grafiikkaa suoraan näytölle.
//
//
// Win2D Saadaan käyttöön kankaalle määrittelemällällä namespace: xmlns:canvas="using:Microsoft.Graphics.Canvas.UI.Xaml"
// Tällä saadaan sitten käyttöön kaikki Win2D kirjaston sisältämät namespacet
// 
// CanvasControl ja event handlerit joista on kerrottu alapuolella kuuluvat kaikki Win2D ohjelmointirajapintaan.
//
// MainPage.xaml tiedostossa määritellään kankaalle tarvittavat tiedot eli määritellään CanvasControl jolla voidaan sitten hallita kangasta. canvas:CanvasControl
//
// Alemmalla rivillä nähdään xaml koodia joka laitetaan samalle riville kuin canvas:CanvasControl ja suoraan sen perään.
// x:Name: meinaa sitä että siinä määritellään kankaan nimi jota sitten kutsutaan aina kun piirretään jotain kankaalle
//
// CreateResources, Draw , IsTapEnabled ja Tapped ovat kaikki event handlereita jotka hoitavat kaiken mitä kankaalla tapahtuu.
// CreateRecources = Hoitaa kaikkien resurssien lataamisen joita sitten Draw event handleri tarvitsee piirtääkseen kuvia yms. kankaalle.
// Draw =  Hoitaa piirtämisen kankaalle
// IsTapEnabled = Määrittelee rekisteröidäänkö painallukset kankaalle (True tai False)
// Tapped == Määritellään tänne mitä tehdään kun kangasta painetaan. (Hiirellä)
// x:Name="Canvas" CreateResources="Canvas_CreateResources" Draw="Canvas_Draw" IsTapEnabled="True" Tapped="Canvas_Tapped" 


namespace _2D_Peli_Harjoitustyö
{

    public sealed partial class MainPage : Page
    {
        // Muuttujien määrittely
        //
        // Static muuttuja meinaa sitä että sitä voidaan käyttää missä vain ohjelman sisällä.
        // CanvasBitmap = Määritellään kuvat(muuttujan nimet) jotka sitten piirretään kankaalle.
        // Kaikki kuvat täytyy ensiksi ladata ja määritellä CreateResources event handlerissä että niitä voidaan käyttää.
        public static CanvasBitmap BG, StartScreen, Map, ContinueScreen, Bullet, Enemy1, Enemy2, ENEMY, Player, Boom;        
        // Määritellään muuttuja joka saa arvoksi kankaan koko näkymän eli siis sen kuvan alueen joka piirretään näytölle. 1600x1200
        public static Rect visibleArea = ApplicationView.GetForCurrentView().VisibleBounds;
        // Muuttuja jolle asetetaan kentän leveys.
        public static float DesignWidth = 1600;
        // Muuttuja jolle asetetaan kentän korkeus.
        public static float DesignHeight = 1200;
        // Muuttujia joita tullaan tarvitsemaan.
        public static float scaleWidth, scaleHeight, pointX, pointY, bulletX, bulletY, boomX, boomY;
        // Muuttuja joka määrittelee framet
        public static int boomCount = 60;   
        // Muuttuja jolla katsotaan montako vihollista on lisätty
        public static int enemyAdded = 0;
        // Muuttuja jolla määritellään leveli
        public static int Level = 1;

        // Muuttuja jolla määritellään kentän / kierroksen aika -> kestää nyt 10 sekuntia.
        public static int countdown = 10; 
        // Muuttuja jolla määritellään onko kierros loppunut vai ei.
        public static bool RoundEnded = false;

        // Muuttuja jonka avulla vaihdetaan taustakuvia
        public static int GameState = 0; // Aloitusruutu

        // Ajastimia joita tarvitaan
        public static DispatcherTimer RoundTimer = new DispatcherTimer();
        public static DispatcherTimer EnemyTimer = new DispatcherTimer();

        // Äänet
        private MediaPlayer mediaPlayer;
        private MediaPlayer mediaPlayer2;

        // Määritelty listat joita tullaan tarvitsemaan projectilen toimimista varten.
        public static List<float> bulletXPOS = new List<float>();
        public static List<float> bulletYPOS = new List<float>();
        public static List<float> bulletSPEED = new List<float>();

        // Määritelty listat joita tarvitaan vihollisten toimintaa varten.
        public static List<float> enemyXPOS = new List<float>();
        public static List<float> enemyYPOS = new List<float>();
        public static List<int> enemyTYPE = new List<int>();
        public static List<string> enemyDIR = new List<string>();

        // Satunnais generaattorit joita tarvitaan.
        public Random EnemyTypeRand = new Random(); // Vihollisen tyyppi eli käytetäänkö kuvaa 1 vai 2.       
        public Random EnemyGenRand = new Random(); // Millä tahdilla vihollisia luodaan kankaalle.
        public Random EnemyXrandomPOS = new Random(); // Satunnainen x akseli positio kankaalla viholliselle.
        

        public MainPage()
        {
            this.InitializeComponent();
            // Määritellään mitä tehdään kun kankaan koko muuttuu jota voidaan hallita sitten Current_SizeChanged metodilla
            Window.Current.SizeChanged += Current_SizeChanged;

            // Käytetään hyväksi Scaling luokan SetScale aliohjelmaa.
            // Jolla määritellään scaleHeight:lle ja scaleWidth:lle arvot.
            Scaling.SetScale();

            // Mistä kohtaa x akselilla ammus lähtee
            bulletX = (float)visibleArea.Width / 2 + -110;
            // Mistä kohtaa y akselilla ammus lähtee
            bulletY = (float)visibleArea.Height + -230;

            // Alustetaan kierroksen ajastin ja sille määritetään sekuntit
            RoundTimer.Tick += Roundtimer_Tick;
            RoundTimer.Interval = new TimeSpan(0, 0, 1);

            // Alustetaan vihollisten ajastin ja aikaväli jolla niiitä luodaan lisää, joka luo niitä satunnaisesti.
            EnemyTimer.Tick += EnemyTimer_Tick;
            EnemyTimer.Interval = new TimeSpan(0, 0, 0, 0, EnemyGenRand.Next(300, 3000));

            // Äänet
            InitAudio();            
                                                       
        }
        private void InitAudio()
        {
            // Määritellään äänet joita toistetaan ja ladataan ääni tiedostot joita sitten käytetään.
            // Se onnistuu määrittelemällä kaksi MediaPlayer objectia joille molemmille annetaan eri ääniraita arvoksi.
            mediaPlayer = new MediaPlayer();        
            // Ladataan ääni    
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/PEW2.MP3"));
            // Määritellään alkaako ääni soimaan heti kun ohjelma alkaa vai ei 
            mediaPlayer.AutoPlay = false;
            mediaPlayer2 = new MediaPlayer();
            // Ladataan ääni 
            mediaPlayer2.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Osuma.MP3"));
            // Määritellään alkaako ääni soimaan heti kun ohjelma alkaa vai ei 
            mediaPlayer2.AutoPlay = false;            
            // Nopeutetaan ääniraidan soittoa
            mediaPlayer2.PlaybackSession.PlaybackRate = 2.0;
        }



        private void EnemyTimer_Tick(object sender, object e)
        {
            // Hallitaan vihollisten luontia ja mihin kohti ne piirretään kankaalla.
            // Määritellään vihollisen tyyppi. (Enemy1 tai Enemy2) satunnaisesti.
            int Enemy = EnemyTypeRand.Next(1,3);   
            // Satunnainen positio x akselilla. Tässä käytetään hyväksy visibleArea muuttujaa jolle on asetettu arvoksi se alue joka on näkyvissä.
            // Estetään siis vihollisen luominen kohtaan mitä ei nähdä    
            int Start = EnemyXrandomPOS.Next(0, (int)visibleArea.Width);    
            
            // If, Else lauseke jolla määritellään että kuuluuko vihollinen vasemmalle puolelle ruutua vai oikealle
            // Jos start positio arvo on pienempi kuin näkymän arvo kun se on jaettu kahdella -> niin silloin vihollinen on vasemmalla puolella   
            if (Start > visibleArea.Width / 2)
            {
                enemyDIR.Add("left");
            }
            // Jos vihollinen näkymän oikealla puolella
            else
            {
                enemyDIR.Add("right");
            }

            // Lisätään vihollisen x positioksi aloitus piste joka määriteltiin yläpuolella.
            // Se lisätään sitten listaan.          
            enemyXPOS.Add(Start);
            // Lisätään Vihollisen y positio listalle, joka saadaan -50 * scaleHeight. Missä -50 meinaa sitä että vihollinen piirretään kankaalle näkyvän osan ulkopuolella
            // eikä suoraan ruudulle joka näyttäisi hieman tönköltä. scaleHeight määrittelee ruudun korkeuden joka on määritelty Scaling luokassa.
            enemyYPOS.Add(-50 * scaleHeight);
            // Lisätään listalle vihollisen tyyppi. (Enemy1 vai Enemy2)
            enemyTYPE.Add(Enemy);
            // Aikaväli jolla vihollisia luodaan näytölle.
            EnemyTimer.Interval = new TimeSpan(0, 0, 0, 0, EnemyGenRand.Next(500, 2000));
            // Otetaan ylös että yksi vihollinen lisätty.
            enemyAdded++;

        }

        private void Roundtimer_Tick(object sender, object e)
        {
            // Miinustetaaan countdown muuttujasta yksi. Periaatteessa toimii kuin sekuntikello
            countdown -= 1;

            // jos countdown on alle 1 niin pysäytetään RoundTimer ajastin ja määritetään että kierros on loppunut
            // jolloin taustakuva vaihtuu
            if(countdown < 1)
            {
                RoundTimer.Stop();
                RoundEnded = true;
            }
        }



         
        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            // Määritetään näkyväalue
            visibleArea = ApplicationView.GetForCurrentView().VisibleBounds;
            // Käytetään hyväksi Scaling luokan SetScale aliohjelmaa.
            // Jolla määritellään scaleHeight:lle ja scaleWidth:lle arvot.
            Scaling.SetScale();

            // Ammuksen positio määritelty joka vaihtuu kun kankaan kokoa muuttaa
            bulletX = (float)visibleArea.Width / 2 + -110;  
            bulletY = (float)visibleArea.Height + -230;
        }

        private void Canvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            // Hoitaa kaikkien resurssien lataamisen.
            // Ladataan bitmapit asynkronoidusti.
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreateResourcesAsync(CanvasControl sender)
        {
            // Ladataan bitmapit määritellyistä poluista.
            // Eli piirrettävät kuvat.
            StartScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/StartScreen.png"));
            Map = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/background.png"));            
            Bullet = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/bullet.png"));
            Player = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Player.png"));
            Enemy1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Enemy1.png"));
            Enemy2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Enemy2.png"));
            Boom = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Boom.png"));
            ContinueScreen = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/ContinueScreen.png"));

        }

        
        private void Canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            // Canvas_Draw hoitaa kaiken piirtämisen kankaalle.
            //
            // Kutsutaan GAMEMANAGER aliohjelmaa jolla muutetaan pelin tilannetta. (Aloitusruutu, kenttä vai loppuiko aika)
            GameManager.GAMEMANAGER();

            // Kutsutaan LevelManager aliohjelmaa jolla säädellään pelin leveleitä.
            Levels.LevelManager();
            // Piirretään kankaalle tausta joka määritellään GameManager.GAMEMANAGER aliohjelmalla. Riippuu gamestatesta.
            args.DrawingSession.DrawImage(Scaling.img(BG));
            // Piirretään kankaalle ajastin josta näkyy kauan on aikaa jälellä.
            args.DrawingSession.DrawText(countdown.ToString(), 100, 100, Colors.Yellow);

            // Mitä tehdään jos GameState on isompi kuin 0.
            if (GameState > 0)
            {

                // Piirretään kankaalle Teksti jossa lukee millä levelillä ollaan.               
                args.DrawingSession.DrawText("Level: " + Level.ToString(), (float)visibleArea.Width / 2, 30, Colors.White);

                // Määritellään if lauseke joka määrittää onko ammus osunut viholliseen vai ei.
                // Tilanne joka tapahtuu vain jos osuu viholliseen.
                // Mitä sen jälkeen tapahtuu
                if (boomX > 0 && boomY > 0 && boomCount > 0)
                {
                    // Soitetaan ääniraita
                    mediaPlayer2.Play();  
                    // Piiretään näytölle kuva joka kuvastaa räjähdystä.             
                    args.DrawingSession.DrawImage(Scaling.img(Boom), boomX, boomY);
                    // Muutetaan arvoa jotta ohjelma alustaa sen takaisin.
                    boomCount -= 1;                    
                }
                else // Mitä tapahtuu jos ammus ei osu.
                {               
                    boomCount = 60;
                    boomX = 0;
                    boomY = 0;
                }

                // Määritellään viholliset for loopissa.
                for(int j = 0; j < enemyXPOS.Count; j++)
                {
                    // Määritellään vihollisen tyyppi tarkastamalla listasta tietystä kohtaa arvo indeksillä j.
                    // Annetaan ENEMY:lle arvoksi jompi kumpi vihollisen kuvista
                    if (enemyTYPE[j] == 1)
                    {
                        ENEMY = Enemy1;
                    }

                    if (enemyTYPE[j] == 2)
                    {
                        ENEMY = Enemy2;
                    }

                    // Jos vihollisen suunta = left niin otetaan vihollisen x akselin arvo ja miinustetaan siitä vielä 3 jotta se ei ole ihan keskellä ruutua.
                    if (enemyDIR[j] == "left")
                    {
                        enemyXPOS[j] -= 3;
                    }
                    else // Muussa tapauksessa vihollinen on oikealla puolella.
                    {
                        enemyXPOS[j] += 3;
                    }

                    // Määritellään kulma jossa viholliset liikkuvat näytöllä.
                    enemyYPOS[j] += 4;
                    // Piirretään kankaalle vihollinen x position sekä y position määrittelemälle paikalle. Ja skaalataan se vielä Scaling luokassa määritetyllä efectillä.
                    // Jonka avulla saadaan skaalattua vihollisen koko suhteessa näytön kokoon.
                    args.DrawingSession.DrawImage(Scaling.img(ENEMY), enemyXPOS[j], enemyYPOS[j]);
                }
            

                // Piiretään ammukset näytölle.
                for (int i = 0; i < bulletXPOS.Count; i++) // Joka kerta kuin painetaan näyttöä -> Lisätään ammus listaan.
                {
                    
                    // bulletSPEED määrittelee kuinka kovaa ammus lähtee. Jos painetaaan pelaajan vieressä kangasta niin ammus lähtee hitaammin kuin silloin kun painetaan kauempana pelaajasta.
                    // Ammuksen toimintaan on käytetty lineaarista interpolaatiota jolla on saatu laskettua tunnettujen pisteiden välistä puuttuvia pisteitä.
                    // Eli tässä tapauksessa suora reitti pelaajan aseen kohdalta siihen kohtaan jossa hiirellä painetaan kangasta.
                    // Tämä suora reitti saadaan sitten laskettua lineaarisella interpolaatiolla.
                    // Tämä pystytään laskemaan siis kun tiedetään X0 ja Y0 jotka siis määritelty pelaajan aseen kohdalle ylempänä koodissa.
                    // Sekä X1 ja Y1 jotka ovat se kohta missä hiireä painetaan kankaalla. (bulletXPOS ja bulletYPOS)                    
                    
                    pointX = (bulletX + (bulletXPOS[i] - bulletX) * bulletSPEED[i]); 
                    pointY = (bulletY + (bulletYPOS[i] - bulletY) * bulletSPEED[i]);
                    // Piirretään kankaalle ammuksen kuva ja ammutaan se siihen suuntaan mitkä pointX ja pointY määrittelevät. 
                    // Sekä poistetaan puolet kuvan koosta jotta ammus menee suoraan siihen missä kohti on painettu hiirellä.
                    args.DrawingSession.DrawImage(Scaling.img(Bullet), pointX - (32 * scaleWidth), pointY -(32 * scaleHeight)); 

                    // Ammuksen nopeus määritelty.
                    bulletSPEED[i] += (0.050f * scaleHeight);

                    // Toistetaan ääniraita kun ammutaan.
                    mediaPlayer.Play();

                    // Looppi jossa lisätään h:n arvoa yhdellä joka vihollisen lisäyksellä.
                    for (int h = 0; h < enemyXPOS.Count; h++)
                    {
                        // Ehdot jotka määrittää onko viholliseen osunut vai ei.
                        if (pointX >= enemyXPOS[h] && pointX <= enemyXPOS[h] + (256 * scaleWidth) && pointY >= enemyYPOS[h] && pointY <= enemyYPOS[h] + (256 * scaleHeight))
                        {
                            // Määrittää missä kohtaa räjähdys kuva näyteään kankaalla.
                            // Miinustetaan puolet räjähdys kuvan koosta jotta se tulee juuri siihen mihin halutaan sen tulevan.
                            boomX = pointX -(128 * scaleWidth);
                            boomY = pointY -(128 * scaleHeight);

                            // Poistetaan vihollinen listalta tietyltä paikalta listalta.
                            // Eli juuri se johon on osunut
                            enemyXPOS.RemoveAt(h);
                            enemyYPOS.RemoveAt(h);
                            enemyTYPE.RemoveAt(h);
                            enemyDIR.RemoveAt(h);

                            // Poistetaan myös ammus.
                            bulletXPOS.RemoveAt(i);
                            bulletYPOS.RemoveAt(i);
                            bulletSPEED.RemoveAt(i);
                           
                            break;
                        }
                    }
                   
                    // Poistetaan ammukset jotka lentävät yli kentän.
                    if (pointY < 0f) 
                    {
                        bulletXPOS.RemoveAt(i);
                        bulletYPOS.RemoveAt(i);
                        bulletSPEED.RemoveAt(i);
                    }
                }

                // Piirretään pelaaja kankaalle. Pysyvä kohta jota ei muuteta.              
                args.DrawingSession.DrawImage(Scaling.img(Player), (float)visibleArea.Width / 2 - (258 * scaleWidth), (float)visibleArea.Height - (260 * scaleHeight));
                
            }

            // Voidaan piirtää asioita uudelleen.
            Canvas.Invalidate();
        }

        private void Canvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Määritellään kaikki mitä tapahtuu kun kankaalle painetaan hiirellä.

            // Jos kierros on loppunut -> mitä tehdään.
            if(RoundEnded == true)
            {
                GameState = 0;
                RoundEnded = false;
                countdown = 10;

                // Pysäytetään vihollisten luonti ja tyhjennetään listat.
                EnemyTimer.Stop();
                enemyXPOS.Clear();
                enemyYPOS.Clear();
                enemyTYPE.Clear();
                enemyDIR.Clear();

            }
            else
            {
                // Aloitusruutu
                if(GameState == 0)
                {
                    // Siirrytään kenttään kun painetaan kangasta
                    GameState += 1;
                    RoundTimer.Start();
                    EnemyTimer.Start();
                    
                }
                // Jos ollaan pelissä ja painetaan kangasta eli GameState = 1
                // Täältä saadaan arvot ammukselle siitä kohtaa jossa hiirtä painetaan kankaalla.
                else if (GameState > 0)
                {
                    bulletXPOS.Add((float)e.GetPosition(Canvas).X);
                    bulletYPOS.Add((float)e.GetPosition(Canvas).Y);
                    bulletSPEED.Add(0f);
                }
            }
            
        }
        
    }
}
