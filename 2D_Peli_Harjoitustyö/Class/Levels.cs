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
            if (MainPage.Level == 1)
            {               
                if (MainPage.enemyAdded < 5)
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
