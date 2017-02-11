﻿using System;
using System.Windows;
using PK_Finder.Classes;

namespace PK_Finder.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {

        private readonly MainWindow _mw;

        public SettingsWindow(MainWindow mw)
        {
            _mw = mw;

            InitializeComponent();
            LoadTheme();

            LoadSettings();
        }

        /// <summary>
        /// Change the visual style of the controls, depending on the settings.
        /// </summary>
        private void LoadTheme()
        {
            StyleManager.ChangeStyle(this);
        }

        /// <summary>
        /// Change the state of certain controls to represent the current settings
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                ChbAutoUpdate.IsChecked = Properties.Settings.Default.AutoUpdate;
                CboStyle.SelectedValue = Properties.Settings.Default.VisualStyle;
                CpMetroBrush.Color = Properties.Settings.Default.MetroColor;
                IntBorderThickness.Value = Properties.Settings.Default.BorderThickness;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Are you sure that you want to reset all settings?", "PK Finder", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();

                LoadSettings();

                _mw.LoadTheme();
                LoadTheme();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ChbAutoUpdate.IsChecked != null) Properties.Settings.Default.AutoUpdate = ChbAutoUpdate.IsChecked.Value;
                Properties.Settings.Default.VisualStyle = CboStyle.Text;

                Properties.Settings.Default.MetroColor = CpMetroBrush.Color;
                if (IntBorderThickness.Value != null) Properties.Settings.Default.BorderThickness = (int)IntBorderThickness.Value;

                Properties.Settings.Default.Save();

                _mw.LoadTheme();
                LoadTheme();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
