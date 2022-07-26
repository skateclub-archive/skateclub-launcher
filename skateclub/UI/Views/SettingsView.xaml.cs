using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using skateclub.Properties;
using skateclub.UI;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl, IView
    {
        public string ViewName => "settings";

        public SettingsView()
        {
            InitializeComponent();
        }

        public ViewManager manager { get; set; }

        public bool Hide()
        {
            UI_SaveSettings();

            return true;
        }

        public void Show()
        {
            UI_LoadSettings();
        }

        /// <summary>
        /// Shows the settings menu
        /// </summary>
        void UI_LoadSettings()
        {
            PlayerNameSettingsText.Text = Settings.Default.Playername;
            ShadowFix.IsChecked = Settings.Default.DX11;
            //ShowFPS.IsChecked = Settings.Default.ShowFPS;
            HideWatermark.IsChecked = Settings.Default.HideWatermark;
            EnableCosmetic.IsChecked = Settings.Default.EnableCosmetic;
            DebugRender.IsChecked = Settings.Default.DebugRender;
            RemoveClothes.IsChecked = Settings.Default.RemoveClothes;
            ExperimentalMode.IsChecked = Settings.Default.ExperimentalMode;
            TruckTightness.Value = Settings.Default.TruckTightness;
            Fullscreen.IsChecked = Settings.Default.Fullscreen;
            SkipCutscene.IsChecked = Settings.Default.SkipCutscene;
            AACombo.SelectedIndex = Settings.Default.AAIndex;
            AOCombo.SelectedIndex = Settings.Default.AOIndex;
            ShaderQualityCombo.SelectedIndex = Settings.Default.ShaderQualityIndex;

            int i = levelPicker.Items.IndexOf(Settings.Default.level);

            levelPicker.SelectedIndex = i == -1 ? 0 : i;
        }

        /// <summary>
        /// Saves the settings and hides settings menu
        /// </summary>
        void UI_SaveSettings()
        {
            //try
            //{
            //    Settings.Default.Playername = PlayerNameSettingsText.Text;
            //    Settings.Default.DX11 = ShadowFix.IsChecked.Value;
            //    //   Settings.Default.ShowFPS = ShowFPS.IsChecked.Value;
            //    Settings.Default.HideWatermark = HideWatermark.IsChecked.Value;
            //    Settings.Default.EnableCosmetic = EnableCosmetic.IsChecked.Value;
            //    Settings.Default.DebugRender = DebugRender.IsChecked.Value;
            //    Settings.Default.RemoveClothes = RemoveClothes.IsChecked.Value;
            //    Settings.Default.ExperimentalMode = ExperimentalMode.IsChecked.Value;
            //    Settings.Default.TruckTightness = TruckTightness.Value;
            //    Settings.Default.Fullscreen = Fullscreen.IsChecked.Value;
            //    Settings.Default.SkipCutscene = SkipCutscene.IsChecked.Value;
            //    Settings.Default.ShaderQualityIndex = ShaderQualityCombo.SelectedIndex;
            //    Settings.Default.ShaderQuality = ShaderQualityCombo.Text;
            //    Settings.Default.AAIndex = AACombo.SelectedIndex;
            //    Settings.Default.AA = AACombo.Text;
            //    Settings.Default.AOIndex = AOCombo.SelectedIndex;
            //    Settings.Default.AO = AOCombo.Text;

            //    Settings.Default.level = levelPicker.SelectedValue as string;

            //    Settings.Default.Save();
            //} catch { }

           // manager.PreviousView();
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e) => UI_SaveSettings();

        private void TruckTightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TruckTightness.Value > 1)
            {
                if (TruckTightness.Value < 2.5)
                {
                    TruckTightnessValue.Content = "Tight";
                }
                else if (TruckTightness.Value > 2.5)
                {
                    TruckTightnessValue.Content = "Super Tight";
                }
            }
            else if (TruckTightness.Value < -1)
            {
                if (TruckTightness.Value > -2.5)
                {
                    TruckTightnessValue.Content = "Loose";
                }
                else if (TruckTightness.Value < -2.5)
                {
                    TruckTightnessValue.Content = "Super Loose";
                }
            }
            else
            {
                TruckTightnessValue.Content = "Normal";
            }

            TruckTightnessValue.Content += " (" + Math.Round((decimal)TruckTightness.Value, 2) + ")";
        }
    }
}
