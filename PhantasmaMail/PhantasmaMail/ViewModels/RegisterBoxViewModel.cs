﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PhantasmaMail.ViewModels.Base;
using Xamarin.Forms;

namespace PhantasmaMail.ViewModels
{
    public class RegisterBoxViewModel : ViewModelBase
    {
        private string _boxName;

        public string BoxName
        {
            get => _boxName;
            set
            {
                if (_boxName == value) return;
                _boxName = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateBoxCommand => new Command(async () => await CreateBoxExecute());

        private async Task CreateBoxExecute()
        {
            DialogService.ShowLoading();
            try
            {
                if (!string.IsNullOrEmpty(BoxName))
                {
                    var tx = await PhantasmaService.RegisterMailbox(BoxName);
                    if (string.IsNullOrEmpty(tx))
                    {
                        await DialogService.ShowAlertAsync("Something went wrong", "Error");
                    }
                    else
                    {
                        await DialogService.ShowAlertAsync(
                            "Email created, you need to wait 20/40 seconds minutes before sending any e-mails",
                            "Success");
                        await NavigationService.NavigateToAsync<MainViewModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                await DialogService.ShowAlertAsync(ex.Message, "Error");
            }
            DialogService.HideLoading();
        }
    }
}
