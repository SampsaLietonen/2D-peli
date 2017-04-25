using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Peli_Harjoitustyö.Class
{
    class Levels
    {       

        public static void LevelManager(int Level)
        {
            if (Level == 1)
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
                        MainPage.countdown = 0;
                    }                   
                }          
            }
        }
    }
}
