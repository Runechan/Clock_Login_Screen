using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace Time
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                if (Properties.Settings.Default.seconds) time.Text = DateTime.Now.ToString("h:mm:ss tt");
                else time.Text = DateTime.Now.ToString("h:mm tt");
            }
            catch (Exception)
            { }

            Loaded += MainWindow_Loaded;

            StartTimer();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                seconds.IsChecked = Properties.Settings.Default.seconds;
                if (Properties.Settings.Default.seconds)
                {
                    seconds_Checked_1(sender, e);
                }

                if (Properties.Settings.Default.isLight)
                {
                    setLight(sender, e);
                    light.IsChecked = true;
                    return;
                }
                else 
                { 
                    setDark(sender, e);
                    dark.IsChecked = true;
                    return;
                }
            }
            catch (Exception){Dispatcher.Invoke(new Action(() => { time.Text = "Cannot load settings."; }));}
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }

        private void windowTerminate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                this.Close();
            }
        }

        Timer tmr = null;

        private void StartTimer()
        {
            tmr = new Timer() { Interval = 1000, Enabled = true };
            tmr.Start();
            tmr.Elapsed += tmr_Tick;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() => { time.Text = DateTime.Now.ToString("h:mm tt"); }));
        }

        void tmr_Tick2(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() => { time.Text = DateTime.Now.ToString("h:mm:ss tt"); }));
        }

        private void drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        bool showSeconds;

        private void seconds_Checked_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.seconds = true;
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                settingsPane.Visibility = Visibility.Collapsed;
                Dispatcher.Invoke(new Action(() => { time.Text = "Cannot save settings."; }));
            }

            tmr.Elapsed -= tmr_Tick;
            tmr.Elapsed += tmr_Tick2;
        }

        private void seconds_Unchecked_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.seconds = false;
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                settingsPane.Visibility = Visibility.Collapsed;
                Dispatcher.Invoke(new Action(() => { time.Text = "Cannot save settings."; }));
            }

            tmr.Elapsed -= tmr_Tick2;
            tmr.Elapsed += tmr_Tick;
        }

        private void mouseoverButtonHighlight(object sender, MouseEventArgs e)
        {
            (sender as Border).Opacity = 1;
        }

        private void mouseleaveButtonHighlight(object sender, MouseEventArgs e)
        {
            (sender as Border).Opacity = 0;
        }

        private void showSettings(object sender, MouseButtonEventArgs e)
        {
            settingsPane.Visibility = Visibility.Visible;
        }

        private void setLight(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.isLight = true;
                time.Foreground = Brushes.White;
            }
            catch (Exception)
            {
                settingsPane.Visibility = Visibility.Collapsed;
                Dispatcher.Invoke(new Action(() => { time.Text = "Cannot set settings."; }));
            }
        }

        private void setDark(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.isLight = false;
                time.Foreground = Brushes.Gray;
            }
            catch (Exception)
            {
                settingsPane.Visibility = Visibility.Collapsed;
                Dispatcher.Invoke(new Action(() => { time.Text = "Cannot set settings."; }));
            }
        }

        private void hideSettings(object sender, MouseButtonEventArgs e)
        {
            settingsPane.Visibility = Visibility.Collapsed;

            try{Properties.Settings.Default.Save();}
            catch (Exception)
            {
                Dispatcher.Invoke(new Action(() => { time.Text = "Cannot save settings."; }));
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
