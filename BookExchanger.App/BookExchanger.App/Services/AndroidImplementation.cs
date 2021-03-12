using Android.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookExchanger.App.Services
{
    public class AndroidImplementation : IStatusBar
    {
        private WindowManagerFlags _originalFlags;

        public void HideStatusBar()
        {
            throw new NotImplementedException();
        }

        public void ShowStatusBar()
        {
            throw new NotImplementedException();
        }
    }

    public interface IStatusBar
    {
        void HideStatusBar();

        void ShowStatusBar();
    }
}