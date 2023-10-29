using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarlightPOS.Managers;
using StarlightPOS.Model;
using System;

namespace StarlightPOS.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _cols = 5;

        [ObservableProperty]
        private int _rows = 5;

        [ObservableProperty]
        private string _name = string.Empty;

        public SettingsViewModel()
        {
            Cols = DataManager.Setting.TableCols;
            Rows = DataManager.Setting.TableRows;
            Name = DataManager.Setting.Name;
        }

        [RelayCommand]
        private void OnSaveTable(string parameter)
        {
            DataManager.Tables.Clear();

            for (int i = 0; i < Cols * Rows; i++)
            {
                DataManager.Tables.Add(new Table
                {
                    Id = Guid.NewGuid(),
                    Column = i % Cols,
                    Row = i / Cols,
                    Name = $"테이블 {i + 1}"
                });
            }

            DataManager.Setting.TableCols = Cols;
            DataManager.Setting.TableRows = Rows;

            DataManager.SaveData("settings\\setting.json", DataManager.Setting);
            DataManager.SaveData("settings\\tables.json", DataManager.Tables);
        }

        [RelayCommand]
        private void OnSaveName(string parameter)
        {
            DataManager.Setting.Name = Name;

            DataManager.SaveData("settings\\setting.json", DataManager.Setting);
        }
    }
}
