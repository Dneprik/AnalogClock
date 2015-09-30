using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AnalogClock.Annotations;

namespace AnalogClock.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private readonly Action _hourChanged;
        private int _hoursArrowRotation;
        private readonly Action _minuteChanged;
        private int _minutesArrowRotation;
        private readonly Action _secondChanged;
        private int _seconds;
        private int _secondsArrowRotation;

        private int hours;
        private int minutes;

        public HomePageViewModel(Action SecondChanged, Action MinuteChanged, Action HourChanged)
        {
            _secondChanged = SecondChanged;
            _minuteChanged = MinuteChanged;
            _hourChanged = HourChanged;

            SetTime(DateTime.Now);
            StartClock();
        }

        public int SecondsArrowRotation
        {
            get { return _secondsArrowRotation - 90; }
            set
            {
                if (value == _secondsArrowRotation) return;
                _secondsArrowRotation = value;
                OnPropertyChanged();
            }
        }

        public int MinutesArrowRotation
        {
            get { return _minutesArrowRotation - 90; }
            set
            {
                if (value == _minutesArrowRotation) return;
                _minutesArrowRotation = value;
                OnPropertyChanged();
            }
        }

        public int HoursArrowRotation
        {
            get { return _hoursArrowRotation - 90; }
            set
            {
                if (value == _hoursArrowRotation) return;
                _hoursArrowRotation = value;
                OnPropertyChanged();
            }
        }

        public int Hours
        {
            get { return hours; }
            set
            {
                if (value == hours) return;
                hours = value;
                OnPropertyChanged();
            }
        }

        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value == minutes) return;
                minutes = value;
                OnPropertyChanged();
            }
        }

        public int Seconds
        {
            get { return _seconds; }
            set
            {
                if (value == _seconds) return;
                _seconds = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetTime(DateTime time)
        {
            hours = (time.Hour%12)*5;
            minutes = time.Minute;
            Seconds = time.Second;
            HoursArrowRotation = hours*6;
            MinutesArrowRotation = minutes*6;
            SecondsArrowRotation = Seconds*6;
        }

        public async void StartClock()
        {
            while (true)
            {
                var i = Seconds;
                for (i = Seconds; i <= 60; i++)
                {
                    await Task.Delay(1000);
                    _secondChanged();
                }
                if (Minutes == 59)
                {
                    Minutes = 0;
                    _minuteChanged();
                    if (Hours == 11) Hours = 0;
                    else
                    {
                        Hours++;
                        _hourChanged();
                    }
                }
                else
                {
                    Minutes++;
                    _minuteChanged();
                }
                Seconds = 1;

            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}