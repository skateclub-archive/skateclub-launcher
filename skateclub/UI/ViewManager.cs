using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Timers;

namespace skateclub.UI
{
    public interface IView
    {
        public ViewManager manager { get; set; }

        public string ViewName { get; }

        public void Show();

        public bool Hide();
    }
    public class ViewManager
    {
        public IView[] views { get; private set; }

        public IView currentView { get; private set; }
        public IView previousView { get; private set; }

        public ViewManager(Grid parent, params IView[] views)
        {
            this.views = views;

            foreach (var view in views)
            {
                view.manager = this;

                var control = view as Control;

                control.Visibility = Visibility.Hidden;

                parent.Children.Add(control);
            }

            //     m_showView(null);
        }

        public void ShowView(string name)
        {
            m_showView(views.First(view => view.ViewName == name));
        }

        public void ShowView(int index) => m_showView(views[index]);

        public void PreviousView()
        {
            if(previousView != null)
            {
                m_showView(previousView);
            }
        }

        private void m_showView(IView view)
        {
            if (view != currentView && (currentView == null || currentView.Hide()))
            {
                previousView = currentView;

                currentView = view;

                currentView.Show();

                (currentView as Control).Visibility = Visibility.Visible;

                FadeEffect(0.1f);
            }
        }

        void FadeEffect(float fadeLength)
        {
            (currentView as Control).Opacity = 0;

            if (previousView != null)
            {
                (previousView as Control).Opacity = 1;
            }

            var fade = new DispatcherTimer();
            fade.Interval = TimeSpan.FromMilliseconds(20);
            fade.Tick += (sender, args) =>
            {
                double val = (fade.Interval.Milliseconds / 1000f) / fadeLength;
                (currentView as Control).Opacity += val;

                if (previousView != null)
                    (previousView as Control).Opacity -= val;

                if ((currentView as Control).Opacity >= 1)
                {
                    if (previousView != null)
                        (previousView as Control).Visibility = Visibility.Hidden;

                    fade.Stop();
                }
            };
            fade.Start();
        }
    }
}
