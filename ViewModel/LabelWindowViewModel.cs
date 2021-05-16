using GuestApp.Extentions;
using GuestApp.Services;
using GuestApp.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace GuestApp.ViewModel
{
    public class LabelWindowViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<DTO.Guest> LabelGuests { get; set; }
        public static int NumberOfGroups { get; set; }
        public static ObservableCollection<ObservableCollection<DTO.Guest>> GroupsOfGuests { get; set; }

        public ICommand PrintCommand { get; set; }
        public static event EventHandler CloseWindow;
        public event PropertyChangedEventHandler PropertyChanged;

        public LabelWindowViewModel()
        {
            PrintCommand = new CustomCommand(PrintLabels, CanPrintLabels);
        }
        public LabelWindowViewModel(List<DTO.Guest> labelGuests)
            : this()
        {
            LabelGuests = labelGuests.ToObservableCollection();
            GroupsOfGuests = new ObservableCollection<ObservableCollection<DTO.Guest>>();
            GroupArangement();
        }
        private bool CanPrintLabels(object obj)
        {
            if (LabelGuests != null)
                return true;
            else return false;
        }
        private void PrintLabels(object obj)
        {
            PrintLabelsService.PrintLabels(obj);
            CloseLabelsWindow(this, new EventArgs()); // This event is handled by the LabelsWindow View
        }

        private void CloseLabelsWindow(object sender, EventArgs e)
        {
            CloseWindow(this, new EventArgs());
        }

        private void GroupArangement()
        {
            LabelsDataService labelsDataService = new LabelsDataService(LabelGuests.ToList());
            var groups = labelsDataService.GroupingGuests();
            NumberOfGroups = groups.Count;

            for (int i = 0; i < groups.Count; i++)
                GroupsOfGuests.Add(groups[i].ToObservableCollection());
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
