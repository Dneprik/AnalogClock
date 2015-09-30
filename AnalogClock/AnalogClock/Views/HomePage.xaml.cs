using System;
using AnalogClock.ViewModels;
using Xamarin.Forms;

namespace AnalogClock.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            _secondChanged += HomePage__secondChanged;
            _minuteChanged += HomePage__minuteChanged;
            _hourChanged += HomePage__hourChanged;
            BindingContext = new HomePageViewModel(_secondChanged, _minuteChanged, _hourChanged);
        }

        private event Action _secondChanged;
        private event Action _minuteChanged;
        private event Action _hourChanged;

        private async void HomePage__hourChanged()
        {
            await HoursArrow.RelRotateTo(30, 2000, Easing.CubicInOut);
        }

        private async void HomePage__minuteChanged()
        {
            await MinutesArrow.RelRotateTo(6, 2000, Easing.CubicInOut);
        }

        private async void HomePage__secondChanged()
        {
            await SecondsArrow.RelRotateTo(7, 400, new Easing(d => d*d));
            await SecondsArrow.RelRotateTo(-2, 100, Easing.CubicIn);
            await SecondsArrow.RelRotateTo(1, 100, Easing.CubicInOut);
        }
    }
}