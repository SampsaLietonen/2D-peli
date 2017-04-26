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
            // Aliohjelma jolla halittaan pelintiloja
            // Hallitaan kuvia joita piirretään kankaalle
            if (MainPage.RoundEnded == true)
            {
                // Jos kierros loppunut -> piirretään tietty kuva
                MainPage.BG = MainPage.ContinueScreen;
            }
            else
            {
                if (MainPage.GameState == 0)
                {
                    // Piirretään aloitusruutu kun ehto täyttyy eli kun aloitetaan peli.
                    MainPage.BG = MainPage.StartScreen;
                }

                else if(MainPage.GameState == 1)
                {
                    // Piirretään kenttä kankaalle jolla pelataan.
                    MainPage.BG = MainPage.Map;
                }
            }
            

        }
        
    }
}
