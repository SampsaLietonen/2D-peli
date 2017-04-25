using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Peli_Harjoitustyö.Class
{
    class GameManager
    {
        public static void GAMEMANAGER()
        {
            if (MainPage.RoundEnded == true)
            {
                MainPage.BG = MainPage.ContinueScreen;
            }
            else
            {
                if (MainPage.GameState == 0)
                {
                    MainPage.BG = MainPage.StartScreen;
                }

                else if(MainPage.GameState == 1)
                {
                    MainPage.BG = MainPage.Level1;
                }
            }
            

        }
        
    }
}
