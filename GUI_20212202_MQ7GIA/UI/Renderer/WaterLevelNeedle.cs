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
    public class WaterLevelNeedle : FrameworkElement
    {
        Player player;
        ImageBrush indicator = new ImageBrush(new BitmapImage(new Uri(Path.Combine("ImageAssets/Charachter Cards", "indicator.png"), UriKind.RelativeOrAbsolute)));
        double needlePosition = 0;
        double lowestBarPosition = 0;
        double highestBarPosition = 0;
        double indicatorWidth = 235 / 2.15;
        double indicatorHeight = 141 / 2.15;
        double middleOfIndicator = 69.5 / 2.15;
        public void SetupPlayer(Player player)
        {
            this.player = player;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            

            switch (player.PlayerRoleName)
            {
                case RoleName.Archeologist:
                    highestBarPosition = 196.27; // calculated, the ratio is 2.15 because I made the render of each card 2.15x smaller
                    lowestBarPosition = 447.44;
                    break;
                case RoleName.Climber:
                    highestBarPosition = 200; // calculated, the ratio is 2.15 because I made the render of each card 2.15x smaller
                    lowestBarPosition = 450.69;
                    break;
                case RoleName.Explorer:
                    highestBarPosition = 136.27; // calculated, the ratio is 2.15 because I made the render of each card 2.15x smaller
                    lowestBarPosition = 449.76;
                    break;
                case RoleName.Meteorologist:
                    highestBarPosition = 137.20; // calculated, the ratio is 2.15 because I made the render of each card 2.15x smaller
                    lowestBarPosition = 450.69;
                    break;
                case RoleName.Navigator:
                    highestBarPosition = 133.95; // calculated, the ratio is 2.15 because I made the render of each card 2.15x smaller
                    lowestBarPosition = 446.97;
                    break;
                case RoleName.WaterCarrier:
                    highestBarPosition = 129.30; // calculated, the ratio is 2.15 because I made the render of each card 2.15x smaller
                    lowestBarPosition = 448.83;
                    break;
                default: break;
            }
            //Lowest position is the lowestBarposition if it's not subtracted by anything (so the subtractor is 0).
            //Highest position is when the lowestBarPosition is subtracted to a level that it shows the maximum point. (like 447- 251)
            //we need to divide this by +1 more than the waterlevel, because we also have the skull symbol which would be a -1 regarding the game
            //Basically by that we get the gaps, and also we have to multiply the result by player's waterlevel + 1 so to have correct position
            double distanceBtwnLowHigh = lowestBarPosition - highestBarPosition;
            needlePosition = lowestBarPosition - (distanceBtwnLowHigh / (player.MaxWaterLevel + 1)) * (player.WaterLevel + 1) - middleOfIndicator;
            drawingContext.DrawRectangle(indicator, null, new Rect(0, needlePosition, indicatorWidth, indicatorHeight));
        }
    }
}
