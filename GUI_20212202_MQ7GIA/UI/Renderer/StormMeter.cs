using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_20212202_MQ7GIA.UI.Renderer
{
    public class StormMeter : FrameworkElement
    {
        GameLogic logic;
        public void SetupModel(GameLogic logic)
        {
            this.logic = logic;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            double Height = (842/10)*(400/216.5); //calculated from 842, it's 116.39 pixels
            double Width = 400; //original is 2165
            double firstBarLocation = 580 * (400 / 2165);
            double indicatorPosition = 0;
            double indicatorHeight = 235* (400 / 2165);
            double indicatorWidth = 141 * (400/2165);
            double gapsBetweenBars =102* (400/2165);
            double middleOfIndicator = indicatorWidth / 2;
            //There are 15 bars for 3 players, 14 bars for 2 players (and the first one is the 3 player's second)
            // The gaps between two bars are 102 px originally, but because i decreased its size, I multiply it with the ratio. (400/2165)
            //We need to push the indicator a bit left because that's how its center is positioned
            ImageBrush meter = null;
            meter = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Storm Meter", "Storm Meter.png"), UriKind.RelativeOrAbsolute)));
            drawingContext.DrawRectangle(meter, new Pen(Brushes.Black, 1), new Rect(0, 0, Width, Height));
            ImageBrush indicator = null;
            if (logic.NumberOfPlayers == 2)
            {
                indicator = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Storm Meter", "indicator_2_player.png"), UriKind.RelativeOrAbsolute)));
            }
            else
            {
                indicator = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Storm Meter", "indicator_3_player.png"), UriKind.RelativeOrAbsolute)));
                indicatorPosition = Height - indicatorHeight;
            }
            drawingContext.DrawRectangle(indicator, null, new Rect(firstBarLocation - middleOfIndicator + (logic.StormProgress * 15 * gapsBetweenBars), indicatorPosition, indicatorWidth,indicatorHeight));
            //we need 15 at the storm progress
        }
    }
}
