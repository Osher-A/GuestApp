using GuestApp.DAL;
using GuestApp.Services;
using GuestApp.ViewModel;

namespace GuestApp.View
{
    public class ViewModelLocator
    {
        private static GuestListWindowViewModel _guestData = new GuestListWindowViewModel(new GuestRepository(), new UsersMessageService());
        public static GuestListWindowViewModel GuestData => _guestData;

        private static LabelWindowViewModel _labelData = new LabelWindowViewModel();
        public static LabelWindowViewModel LabelData => _labelData;

        public static EventSelectorWindowViewModel EventViewModel { get; set; } = new EventSelectorWindowViewModel(new EventRepository());
    }
}

