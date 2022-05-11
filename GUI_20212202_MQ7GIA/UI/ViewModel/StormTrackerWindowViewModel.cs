using GUI_20212202_MQ7GIA.Logic;
using GUI_20212202_MQ7GIA.Models;
using GUI_20212202_MQ7GIA.UI.Renderer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_20212202_MQ7GIA.UI.ViewModel
{
    public class StormTrackerWindowViewModel : ObservableRecipient
    {
        public GameLogic Logic { get; set; }
        public ObservableCollection<StormCard> AvailableStormCards { get; set; } //its size depends on where is the storm meter.
        public BoardWindow boardWindow { get; set; }
        StormTrackerWindow window;
        public void SetupLogic(GameLogic logic, BoardWindow boardWindow)
        {
            this.Logic = logic;
            this.boardWindow = boardWindow;
        }
        public BitmapImage SC1 { get; set; }
        public BitmapImage SC2 { get; set; }
        public BitmapImage SC3 { get; set; }
        public BitmapImage SC4 { get; set; }
        public BitmapImage SC5 { get; set; }
        public BitmapImage SC6 { get; set; }
        public void PutStormCard1()
        {
            try
            {
                Logic.PutStormCardToBack(AvailableStormCards[0]);
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public void PutStormCard2()
        {
            try
            {
                Logic.PutStormCardToBack(AvailableStormCards[1]);
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public void PutStormCard3()
        {
            try
            {
                Logic.PutStormCardToBack(AvailableStormCards[2]);
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public void PutStormCard4()
        {
            try
            {
                Logic.PutStormCardToBack(AvailableStormCards[3]);
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public void PutStormCard5()
        {
            try
            {
                Logic.PutStormCardToBack(AvailableStormCards[4]);
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public void PutStormCard6()
        {
            try
            {
                Logic.PutStormCardToBack(AvailableStormCards[5]);
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            finally
            {
                window.Close(); //this should always run since even if an error this window should go away.
            }

        }
        public bool ShowWindow()
        {
            try
            {
                if (AvailableStormCards.Count > 0)
                {
                    window = new StormTrackerWindow(this);
                    window.ShowDialog();
                    return true;
                }
                else
                {
                    //this has to be implemented here, otherwise the window would open unnecessarily
                    throw new Exception("There is no available storm cards.");

                }
            }
            catch (Exception ex)
            {
                boardWindow.CatchException(ex);
            }
            return false;
        }

        public StormTrackerWindowViewModel()
        {
        }

        public void ConvertListToObservable(List<StormCard> cards)
        {
            AvailableStormCards = new ObservableCollection<StormCard>(cards);
            SC1 = null;
            SC2 = null;
            SC3 = null;
            SC4 = null;
            SC5 = null;
            SC6 = null;
            for (int i = 0; i < AvailableStormCards.Count; i++) //Assign images to pictures (will be set in StormTrackerWindow)
            {
                switch (i)
                {
                    case 0:
                        SC1 = FindProperImage(AvailableStormCards[i]);
                        break;
                    case 1:
                        SC2 = FindProperImage(AvailableStormCards[i]);
                        break;
                    case 2:
                        SC3 = FindProperImage(AvailableStormCards[i]);
                        break;
                    case 3:
                        SC4 = FindProperImage(AvailableStormCards[i]);
                        break;
                    case 4:
                        SC5 = FindProperImage(AvailableStormCards[i]);
                        break;
                    case 5:
                        SC6 = FindProperImage(AvailableStormCards[i]);
                        break;
                }
            }
        }

        private BitmapImage FindProperImage(StormCard stormCard)
        {
            BitmapImage brush = null;
            if (stormCard.Name == "oneLeft")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/oneLeft.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "twoLeft")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/twoLeft.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "threeLeft")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/threeLeft.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "oneRight")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/oneRight.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "twoRight")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/twoRight.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "threeRight")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/threeRight.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "oneUp")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/oneUp.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "twoUp")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/twoUp.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "threeUp")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/threeUp.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "oneDown")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/oneDown.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "twoDown")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/twoDown.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "threeDown")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/threeDown.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "Sun Beats Down")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/Sun Beats Down.png", UriKind.RelativeOrAbsolute));
            }
            else if (stormCard.Name == "Storm Picks Up")
            {
                brush = new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm Picks Up.png", UriKind.RelativeOrAbsolute));
            }
            return brush;
        }
    }
}
