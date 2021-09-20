﻿

using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using q5id.guardian.Models;
using q5id.guardian.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class HomeViewModel : BaseViewModel<User>
    {
        public HomeViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            OpenSettingCommand = new MvxAsyncCommand(OnSettingClicked);
            HomeVm = new HomeContentViewModel(navigationService, logProvider);
            LovedOnesVm = new LovedOnesViewModel(navigationService, logProvider);
            AlertsVm = new AlertsViewModel(navigationService, logProvider);
            Task.Run(async () =>
            {
               await HomeVm.Initialize();
            });
        }

        public HomeContentViewModel HomeVm { get; private set; }
        public LovedOnesViewModel LovedOnesVm { get; private set; }
        public AlertsViewModel AlertsVm { get; private set; }

        private User mUser;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
                RaisePropertyChanged(nameof(User));
                HomeVm.User = mUser;
                LovedOnesVm.User = mUser;
                AlertsVm.User = mUser;
            }
        }

        public Command OpenAlertCommand { get; }

        public MvxAsyncCommand OpenSettingCommand { get; }

        public Command ShowProfileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var profileViewModel = new ProfileViewModel(this.NavigationService, this.LoggerFactory)
                    {
                        User = User
                    };
                    await NavigationService.Navigate(profileViewModel);
                });
            }
        }

        public override void Prepare(User parameter)
        {
            User = parameter;
        }

        public override async Task Initialize()
        {
            GetAccountEntity();
            await HomeVm.Initialize();
            await LovedOnesVm.Initialize();
            await AlertsVm.Initialize();
        }

        private void GetAccountEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                ContactEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.CONTACT_ENTITY_SETTING_KEY;
                });
            }
        }

        private async void GetProfile()
        {
            if(ContactEntity != null && User != null)
            {
                var currentUserResponse = await AppService.Instances.GetUserProfile(User.Id);
                if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject != null)
                {
                    var entityUser = currentUserResponse.ResponseObject;
                    var user = entityUser.Data;
                    user.Id = entityUser.Id;
                    User = user;
                }
            }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
        }

        private async Task OnSettingClicked()
        {
            await NavigationService.Navigate<SettingViewModel>();
        }

        

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            GetProfile();
        }

        public Command OpenHomeTapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    HomeVm.GetAlerts();
                });
            }
        }

        public Command OpenLovedOnesTapCommand
        {
            get
            {
                return new Command(() =>
                {
                    LovedOnesVm.GetLoves();
                });
            }
        }

        public Command OpenAlertTapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await AlertsVm.GetLoves();
                    AlertsVm.GetAlerts();
                });
            }
        }

        public StructureEntity ContactEntity { get; private set; }
    }
}