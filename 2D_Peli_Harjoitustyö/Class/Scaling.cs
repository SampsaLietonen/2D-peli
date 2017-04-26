using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace _2D_Peli_Harjoitustyö.Class
{
    class Scaling
    {
        public static void SetScale()
        {

            // Määritellään koko
            
            MainPage.scaleWidth = (float)MainPage.visibleArea.Width / MainPage.DesignWidth;
            MainPage.scaleHeight = (float)MainPage.visibleArea.Height / MainPage.DesignHeight;

        }
        public static Transform2DEffect img(CanvasBitmap source)
        {
            // Tämän avulla voidaan määritellä skaalautuvuus kuville kun näkymän koko muutellaan.
            Transform2DEffect image;
            // Source on siis kuva jota skaalataan.
            image = new Transform2DEffect() { Source = source };
            image.TransformMatrix = Matrix3x2.CreateScale(MainPage.scaleWidth, MainPage.scaleHeight);
            return image;
        }

    }
}
