using System;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.Interfaces;
using GuestApp.Services;
using GuestApp.View;
using GuestApp.ViewModel;
using Ninject;

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

                MessageBox.Show(@"Something went wrong:   " + ex.Message
                    + ex.InnerException, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            //Current.MainWindow = new EventSelectorWindow();
            //Current.MainWindow.DataContext = new EventSelectorWindowViewModel(new EventRepository(new User { Id = "SjlIykYCcxQ5aK0FoDvuw1Veyuh2" }));
        }

       
    }
}
