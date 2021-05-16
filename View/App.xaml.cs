using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.Services;
using GuestApp.ViewModel;
using Ninject;
using System;
using System.Windows;

namespace GuestApp.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User User { get; set; } = new User();
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(@"Something went wrong:   " + Environment.NewLine  + ex.Message
                    + Environment.NewLine + ex.InnerException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            this._container = new StandardKernel();
            _container.Bind<IEventRepository>().To<EventRepository>();
            _container.Bind<IGuestRepository>().To<GuestRepository>();
            _container.Bind<IUsersMessageService>().To<UsersMessageService>();
        }

        private void ComposeObjects()
        {
            var appVM = _container.Get<LoginWindowViewModel>();
            Current.MainWindow = new LogInWindow();
            Current.MainWindow.DataContext = appVM;
            //var appVM = new EventSelectorWindowViewModel(new EventRepository(new User() { Id = "SjlIykYCcxQ5aK0FoDvuw1Veyuh2" }));
            //var window = new EventSelectorWindow();
            //window.DataContext = appVM;
        }
    }
}