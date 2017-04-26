using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace _2D_Peli_Harjoitustyö.Class
{
    class Levels
    {       

        public static void LevelManager()
        {
            // Aliohjelma joka hoitaa leveleiden muuttamisen ja mitä tehdään kun kaikki viholliset kuolee.
            // Esimerkki toiminnasta selitetty Level1 kohdalla muut toistavat samaa rakennetta.

            if (MainPage.Level == 1)
            {   
                // Määritetään kuinka monta vihollista luodaan kankaalle.           
                if (MainPage.enemyAdded < 5)
                {
                    return;
                }
                else
                {
                    // Kun kankaalle on luotu määritelty määrä vihollisia niin pysäytetään vihollisten luonti.
                    MainPage.EnemyTimer.Stop();
                    // Jos vihollisten määrä on 0 eli kaikki on tuhottu niin päästään seuraavalle levelille.
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        // Kierros loppu.
                        MainPage.RoundEnded = true;
                        // Päästään seuraavalle levelille.
                        MainPage.Level++;
                        // Alustetaan vihollisten määrä.
                        MainPage.enemyAdded = 0;
                    }
                    // Jos aika loppuu ja kaikkia vihollisia ei ole tapettu, alustetaan viholisten määrä ja kenttä alkaa alusta.
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }                                     
                }          
            }
            if (MainPage.Level == 2)
            {
                if (MainPage.enemyAdded < 6)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
            if (MainPage.Level == 3)
            {
                if (MainPage.enemyAdded < 7)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
            if (MainPage.Level == 4)
            {
                if (MainPage.enemyAdded < 8)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
            if (MainPage.Level == 5)
            {
                if (MainPage.enemyAdded < 9)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
            if (MainPage.Level == 6)
            {
                if (MainPage.enemyAdded < 10)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
            if (MainPage.Level == 7)
            {
                if (MainPage.enemyAdded < 11)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
            if (MainPage.Level == 8)
            {
                if (MainPage.enemyAdded < 12)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
            if (MainPage.Level == 9)
            {
                if (MainPage.enemyAdded < 13)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
            if (MainPage.Level == 10)
            {
                if (MainPage.enemyAdded < 14)
                {
                    return;
                }
                else
                {
                    MainPage.EnemyTimer.Stop();
                    if (MainPage.enemyXPOS.Count == 0)
                    {
                        MainPage.RoundEnded = true;
                        MainPage.Level++;
                        MainPage.enemyAdded = 0;
                    }
                    if (MainPage.countdown == 0)
                    {
                        MainPage.enemyAdded = 0;
                    }
                }
            }
        }
    }
}
