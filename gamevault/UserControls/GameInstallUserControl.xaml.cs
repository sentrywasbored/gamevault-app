﻿using gamevault.Helper;
using gamevault.Models;
using gamevault.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace gamevault.UserControls
{
    /// <summary>
    /// Interaction logic for GameInstallUserControl.xaml
    /// </summary>
    public partial class GameInstallUserControl : UserControl
    {
        private GameInstallViewModel ViewModel { get; set; }

        private string m_Directory { get; set; }
        private string m_SavedExecutable { get; set; }
        private string[]? m_IgnoreList { get; set; }
        public GameInstallUserControl(Game game, string directory, string[]? ignoreList)
        {
            InitializeComponent();
            ViewModel = new GameInstallViewModel();
            this.DataContext = ViewModel;
            ViewModel.Game = game;
            m_Directory = directory;
            m_Directory = m_Directory.Replace(@"\\", @"\");
            m_IgnoreList = ignoreList;
            FindGameExecutables(m_Directory, true);
        }        
        public string GetDirectory()
        {
            return m_Directory;
        }
        public int GetGameId()
        {
            return ViewModel.Game.ID;
        }
        public int GetBoxImageID()
        {
            return ViewModel.Game.BoxImage.ID;
        }
        private void FindGameExecutables(string directory, bool checkForSavedExecutable)
        {
            ViewModel.Executables.Clear();
            if (true == checkForSavedExecutable)
            {
                m_SavedExecutable = Preferences.Get(AppConfigKey.Executable, $"{m_Directory}\\gamevault-exec");
            }

            string[] allExecutables = Directory.GetFiles(directory, "*.EXE", SearchOption.AllDirectories);
            for (int count = 0; count < allExecutables.Length; count++)
            {
                if (ContainsValueFromIgnoreList(allExecutables[count]))
                    continue;
                var currentItem = new KeyValuePair<string, string>(allExecutables[count], allExecutables[count].Replace(m_Directory, string.Empty));
                ViewModel.Executables.Add(currentItem);
                if (true == checkForSavedExecutable && allExecutables[count] == m_SavedExecutable)
                {
                    uiCbExecutables.SelectedItem = currentItem;
                }
                else if (true == checkForSavedExecutable && m_SavedExecutable == string.Empty)
                {
                    checkForSavedExecutable = false;
                    uiCbExecutables.SelectedItem = currentItem;
                }
            }
        }
        private void ExecutablesCombobox_Click(object sender, MouseButtonEventArgs e)
        {
            FindGameExecutables(m_Directory, false);
        }
        private void Executable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                m_SavedExecutable = ((KeyValuePair<string, string>)e.AddedItems[0]).Key;
                if (Directory.Exists(m_Directory))
                {
                    Preferences.Set(AppConfigKey.Executable, m_SavedExecutable, $"{m_Directory}\\gamevault-exec");
                }
            }
        }

        private void Play_Clicked(object sender, RoutedEventArgs e)
        {

            if (File.Exists(m_SavedExecutable))
            {
                try
                {
                    ProcessHelper.StartApp(m_SavedExecutable);
                }
                catch
                {

                    try
                    {
                        ProcessHelper.StartApp(m_SavedExecutable, true);
                    }
                    catch
                    {
                        MainWindowViewModel.Instance.AppBarText = $"Can not execute '{m_SavedExecutable}'";
                    }
                }
            }
            else
            {
                MainWindowViewModel.Instance.AppBarText = $"Could not find executable '{m_SavedExecutable}'";
            }
        }

        private void GameImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindowViewModel.Instance.SetActiveControl(new GameViewUserControl(ViewModel.Game, LoginManager.Instance.IsLoggedIn()));
        }

        private void OpenDirectory_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Directory.Exists(m_Directory))
                Process.Start("explorer.exe", m_Directory);
        }
        private bool ContainsValueFromIgnoreList(string value)
        {
            return (m_IgnoreList != null && m_IgnoreList.Any(s => Path.GetFileNameWithoutExtension(value).Contains(s, StringComparison.OrdinalIgnoreCase)));
        }      
    }
}
