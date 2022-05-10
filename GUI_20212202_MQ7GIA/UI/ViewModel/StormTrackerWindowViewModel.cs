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
        ImageBrush SC1 { get; set; }
        ImageBrush SC2 { get; set; }
        ImageBrush SC3 { get; set; }
        ImageBrush SC4 { get; set; }
        ImageBrush SC5 { get; set; }
        ImageBrush SC6 { get; set; }
        public ICommand StormCard1 { get; set; }
        public ICommand StormCard2 { get; set; }
        public ICommand StormCard3 { get; set; }
        public ICommand StormCard4 { get; set; }
        public ICommand StormCard5 { get; set; }
        public ICommand StormCard6 { get; set; }
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
                    throw new Exception("There is no unblocked tile around you.");

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
            StormCard1 = new RelayCommand(
                () => PutStormCard1(),
                () => SC1 != null
                );
            StormCard2 = new RelayCommand(
                () => PutStormCard2(),
                () => SC2 != null
                );
            StormCard3 = new RelayCommand(
                () => PutStormCard3(),
                () => SC3 != null
                );
            StormCard4 = new RelayCommand(
                () => PutStormCard4(),
                () => SC4 != null
                );
            StormCard5 = new RelayCommand(
                () => PutStormCard5(),
                () => SC5 != null
                );
            StormCard6 = new RelayCommand(
                () => PutStormCard6(),
                () => SC6 != null
                );
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
                if (AvailableStormCards[i].IsDiscarded != true)
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
                        default:
                            break;
                    }
                }
            }
        }

        private ImageBrush FindProperImage(StormCard stormCard)
        {
            ImageBrush brush = null;
            int X = stormCard.XMove;
            int Y = stormCard.YMove;
            if (X == -1 && Y == 0)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 1Left.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == -2 && Y == 0)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 2Left.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == -3 && Y == 0)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 3Left.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 1 && Y == 0)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 1Right.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 2 && Y == 0)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 2Right.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 3 && Y == 0)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 3Right.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 0 && Y == 1)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 1Up.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 0 && Y == 2)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 2Up.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 0 && Y == 3)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 3Up.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 0 && Y == -1)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 1Down.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 0 && Y == -2)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 2Down.png", UriKind.RelativeOrAbsolute)));
            }
            else if (X == 0 && Y == -3)
            {
                brush = new ImageBrush(new BitmapImage(new Uri("/ImageAssets/Storm Cards/Storm 3Down.png", UriKind.RelativeOrAbsolute)));
            }
            return brush;
        }
    }
}
