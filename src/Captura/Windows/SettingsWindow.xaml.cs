﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Captura
{
    public partial class SettingsWindow
    {
        SettingsWindow()
        {
            InitializeComponent();
        }

        static SettingsWindow _instance;

        public static void ShowInstance()
        {
            if (_instance == null)
            {
                _instance = new SettingsWindow();

                _instance.Closed += (S, E) => _instance = null;
            }

            _instance.ShowAndFocus();
        }

        void OnGoToPage(object Sender, ExecutedRoutedEventArgs E)
        {
            if (Sender is Frame frame)
            {
                switch (E.Parameter)
                {
                    case string s:
                        frame.Navigate(new Uri(s, UriKind.RelativeOrAbsolute));
                        break;

                    case { } o:
                        frame.Navigate(o);
                        break;
                }
            }
        }

        static void ShowPage(string PageName)
        {
            ShowInstance();

            _instance.NavFrame.Navigate(new Uri($"/Pages/{PageName}Page.xaml", UriKind.RelativeOrAbsolute));
        }

        public static void ShowFFmpegLogs() => ShowPage("FFmpegLogs");

        public static void ShowWebcamPage()
        {
            ShowInstance();

            _instance.NavFrame.Navigate(ServiceProvider.Get<WebcamPage>());
        }

        void OnGoBack(object Sender, RoutedEventArgs E)
        {
            NavFrame.GoBack();
        }

        void OnGoNext(object Sender, RoutedEventArgs E)
        {
            NavFrame.GoForward();
        }
    }
}
