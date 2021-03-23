using GuestApp.Extentions;
using GuestApp.Services;
using GuestApp.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GuestApp.DTO;
using System.Windows.Input;
using GuestApp.DAL;
using GuestApp.View;
using System.Windows;
using System.Text;

namespace GuestApp.ViewModel
{
    public partial class GuestListWindowViewModel
    {
        private GuestDataService _guestDataService ;
        private SearchGuestService _searchGuestService;
        private IGuestRepository _guestRepository;

        public ICommand AddGuestCommand { get; set; }
        public ICommand EditGuestCommand { get; set; }
        public ICommand DeleteGuestCommand { get; set; }
        public ICommand SearchGuestCommand { get; set; }
        public ICommand LabelCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }
        public ICommand CustomizedListCommand { get; set; }                                                                                                     
        public ICommand ClearListCommand { get; set; }
        public ICommand RemoveGuestCommand { get; set;} 
        public ICommand AddToEventListCommand { get; set; }
        public ICommand ChangeEventCommand { get; set; }

        public static event EventHandler CloseListWindow;

        public static Action<List<Guest>> LabelsWindowHandler;

        public GuestListWindowViewModel(IGuestRepository guestRepository, IUsersMessageService usersMessageService, Event currentEvent = null)
        {
            CurrentEvent = currentEvent;
            _guestRepository = guestRepository;
           _guestDataService = new GuestDataService(guestRepository, usersMessageService);
            _searchGuestService = new SearchGuestService(guestRepository);
            CustomizedList = new ObservableCollection<Guest>();

            AddGuestCommand = new CustomCommand(AddGuest, CanAddGuest);
            EditGuestCommand = new CustomCommand(EditGuest, CanEditGuest);
            DeleteGuestCommand = new CustomCommand(DeleteGuest, CanDeleteGuest);
            SearchGuestCommand = new CustomCommand(GuestSearcher, CanSearchGuest);
            LabelCommand = new CustomCommand(LabelGuest, CanLabelGuest);
            PrintCommand = new CustomCommand(PrintGuestList, CanPrintGuestList);
            SaveAsCommand = new CustomCommand(SaveListAs, CanSaveListAs);
            CustomizedListCommand = new CustomCommand(CreateCustomizedList, CanCreateCustomizedList);
            ClearListCommand = new CustomCommand(ClearList, CanClearList);
            RemoveGuestCommand = new CustomCommand(RemoveGuest, CanRemoveGuest);
            AddToEventListCommand = new CustomCommand(AddList, CanAddList);
            ChangeEventCommand = new CustomCommand(ChangeEvent, CanChangeEvent);


           LoadData();
        }


        private bool CanAddGuest(object obj)
        {
             return true;
        }

        private void AddGuest(object obj)
        {
            _guestDataService.AddGuest(this.NewGuest);
            this.NewGuest = new DTO.Guest();
            LoadData();
        }

        private bool CanEditGuest(object obj)
        {
            return this.SelectedGuest != null;
        }

        private void EditGuest(object obj)
        {
            try
            {
                _guestDataService.EditGuest(SelectedGuest);
                LoadData();
            }
            catch (Exception)
            {
            }
        }
        private bool CanDeleteGuest(object obj)
        {
            return this.SelectedGuest != null;
        }

        private void DeleteGuest(object obj)
        {
            if (SelectedTabItem == 0)
                _guestDataService.DeleteGuestFromAllEvents(SelectedGuest);
            else
            _guestDataService.RemoveGuestFromCurrentEvent(SelectedGuest);
            LoadData();
        }

        private bool CanSearchGuest(object obj)
        {
            return this.AllGuests != null;
        }

        private async void GuestSearcher(object obj)
        {
            var search =  await _searchGuestService.SearchListAsync(this.SearchGuest);
            if (search.GuestExists == true)
                this.SearchGuests = search.SearchList.ToObservableCollection();
            this.SearchGuest = new DTO.Guest();
        }

        private bool CanLabelGuest(object obj)
        {
            return this.AllGuests != null;
        }

        private void LabelGuest(object obj)
        {
            if (SelectedTabItem == 1)
                LabelGuests = this.EventGuests;
            else if (SelectedTabItem == 2)
                LabelGuests = this.CustomizedList;
            else if (SelectedTabItem == 3)
                LabelGuests = this.SearchGuests;
            else
                LabelGuests = this.AllGuests;

            if (LabelsWindowHandler != null)
                LabelsWindowHandler(LabelGuests.ToList());
        }
        private bool CanSaveListAs(object obj)
        {
            return this.AllGuests != null;
        }

        private void SaveListAs(object obj)
        {
            List<DTO.Guest> guestList = new List<DTO.Guest>();
            if (SelectedTabItem == 1)
                guestList = this.EventGuests.ToList();
            else if (SelectedTabItem == 2)
                guestList = this.SearchGuests.ToList();
            else
                guestList = this.AllGuests.ToList();
            SaveAsService.SaveAs(guestList);
        }

        private void PrintGuestList(object obj)
        {
            PrintService.PrintVisual(obj);
        }

        private bool CanPrintGuestList(object obj)
        {
            return this.AllGuests != null;
        }

        private bool CanCreateCustomizedList(object obj)
        {
            return this.SelectedGuest != null;
        }

        private void CreateCustomizedList(object obj)
        {
            CustomizedList = CustomizedListService.CustomizedList(obj);
        }

        private bool CanClearList(object obj)
        {
            return this.CustomizedList.Count > 0;
        }

        private void ClearList(object obj)
        {
            this.CustomizedList.Clear();
        }

        private bool CanRemoveGuest(object obj)
        {
            return this.CustomizedList.Count > 0;   
        }

        private void RemoveGuest(object obj)
        {
            this.CustomizedList.Remove(SelectedGuest);
        }

        private bool CanAddList(object obj)
        {
            return this.CustomizedList.Count > 0;
        }

        private void AddList(object obj)
        {
            var eventListService = new AddListToEventService(_guestRepository);
            eventListService.AddCustomList(CustomizedList.ToList());
            LoadData();
        }

        private bool CanChangeEvent(object obj)
        {
            return true;
        }

        private void ChangeEvent(object obj)
        {
            //App.Current.Windows.OfType<GuestListsWindow>().ElementAt(0).Close();
            if (CloseListWindow != null)
                CloseListWindow(this, EventArgs.Empty);
            App.Current.MainWindow.Show();

        }
        
        private  void LoadData()
        {
             this.AllGuests = (_searchGuestService.GetAllGuests()).ToObservableCollection();
            if(CurrentEvent.Id != 0)
            this.EventGuests = (_searchGuestService.GetEventGuests(CurrentEvent.Id)).ToObservableCollection();
            this.CustomizedList.Clear();
        }
    }
}
