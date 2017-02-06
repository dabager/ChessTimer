using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace ChessTimer
{
    public class Model : INotifyPropertyChanged
    {
        #region NotifyProp

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        DispatcherTimer timer;
        bool initialized = false;

        public event EventHandler bitti;

        public Model()
        {
            init();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(Sirabirdemi)
            {
                if(DtmPlayer1.TotalSeconds > 0)
                {
                    DtmPlayer1 = DtmPlayer1.Subtract(TimeSpan.FromSeconds(1));
                }
                else
                {
                    bitti.Invoke("Oyuncu 2 kazandı!", null);
                }
            }
            else
            {
                if (DtmPlayer2.TotalSeconds > 0)
                {
                    DtmPlayer2 = DtmPlayer2.Subtract(TimeSpan.FromSeconds(1));
                }
                else
                {
                    bitti.Invoke("Oyuncu 1 kazandı!", null);
                }
            }
        }

        public void init()
        {
            initialized = false;
            timer = null;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            DtmPlayer1 = TimeSpan.FromMinutes(30);
            DtmPlayer2 = TimeSpan.FromMinutes(30);
            Sirabirdemi = true;
            initialized = true;
        }

        public void basla()
        {
            timer.IsEnabled = true;
            timer.Start();
        }

        public void dur()
        {
            timer.Stop();
            timer.IsEnabled = false;
        }

        private bool _sirabirdemi;
        public bool Sirabirdemi
        {
            get
            {
                return _sirabirdemi;
            }
            set
            {
                if (_sirabirdemi != value)
                {
                    _sirabirdemi = value;
                    if(initialized)
                    {
                        dur();
                        basla();
                    }
                }
            }
        }

        private TimeSpan _dtmPlayer1;
        public TimeSpan DtmPlayer1
        {
            get
            {
                return _dtmPlayer1;
            }
            set
            {
                if (_dtmPlayer1 != value)
                {
                    _dtmPlayer1 = value;
                    NotifyPropertyChanged("DtmPlayer1");
                }
            }
        }

        private TimeSpan _dtmPlayer2;
        public TimeSpan DtmPlayer2
        {
            get
            {
                return _dtmPlayer2;
            }
            set
            {
                if (_dtmPlayer2 != value)
                {
                    _dtmPlayer2 = value;
                    NotifyPropertyChanged("DtmPlayer2");
                }
            }
        }
    }
}
