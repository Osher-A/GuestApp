using GuestApp.Extentions;
using GuestApp.Services;
using GuestApp.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GuestApp.ViewModel
{
    public class LabelWindowViewModel 
    {
        public static ObservableCollection<DTO.Guest> LabelGuests { get; set; }
        public static int NumberOfGroups {get; set;}
        public static ObservableCollection<ObservableCollection<DTO.Guest>> GroupsOfGuests { get; set; }
        public ICommand PrintCommand { get; set; }
        public static event EventHandler CloseWindow;

        public LabelWindowViewModel()
        {
                PrintCommand = new CustomCommand(printLabels, canPrintLabels);
        }
        public LabelWindowViewModel(List<DTO.Guest> labelGuests)
            :this()
        {
            
            LabelGuests = labelGuests.ToObservableCollection();
            GroupsOfGuests = new ObservableCollection<ObservableCollection<DTO.Guest>>();
            groupArangement();
        }
        private bool canPrintLabels(object obj)
        {
            if (LabelGuests != null)
                return true;
            else return false;
        }
        private void printLabels(object obj)
        {
            PrintLabelsService.PrintLabels(obj);
            CloseLabelsWindow(this, new EventArgs());
        }

        private  void CloseLabelsWindow(object sender, EventArgs e)
        {
            CloseWindow(this, new EventArgs());
        }

        private void groupArangement()
        {
            LabelsDataService labelsDataService = new LabelsDataService(LabelGuests.ToList());
           var groups = labelsDataService.GroupingGuests();
            NumberOfGroups = groups.Count;

            for (int i = 0; i < groups.Count; i++)
            GroupsOfGuests.Add(groups[i].ToObservableCollection());
        }
    }
}
