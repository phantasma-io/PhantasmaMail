﻿using PhantasmaMail.Models;
using PhantasmaMail.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhantasmaMail.ViewModels
{
    public class InboxViewModel : ViewModelBase
    {
        private ObservableCollection<InboxMessage> _inboxList;

        public ObservableCollection<InboxMessage> InboxList
        {
            get => _inboxList;
            set
            {
                _inboxList = value;
                OnPropertyChanged();
            }
        }

        private InboxMessage _messageSelected;

        public InboxMessage MessageSelected
        {
            get => _messageSelected;
            set
            {
                if (_messageSelected != value)
                {
                    _messageSelected = value;
                    OnPropertyChanged();
                    MessageSelectedCommand.Execute(_messageSelected);
                }
            }
        }

        public ICommand NewMessageCommand => new Command(async () => await NewMessageExecute());

        public ICommand MessageSelectedCommand =>
            new Command<InboxMessage>(async message => await MessageSelectedExecute(message));

        public InboxViewModel()
        {
        }

        public override async Task InitializeAsync(object navigationData)
        {
            InitTestList();
            await Task.Delay(1);
        }


        private void InitTestList()
        {
            InboxList = new ObservableCollection<InboxMessage>
            {
                new InboxMessage
                {
                    Content = "It is a long established fact that a reader will be distracted by the readable content if you want any desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for any desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for any desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for.",
                    FromEmail = "test@phantasma.io",
                    FromName = "John Test",
                    ReceiveDate = "string date",
                    Subject = "This is a test"
                },
                new InboxMessage
                {
                    Content = "This is a small content",
                    FromEmail = "test@phantasma.io",
                    FromName = "John Test",
                    ReceiveDate = "string date",
                    Subject = "This is a test"
                },
                new InboxMessage
                {
                    Content = "This is a long content dsadasdadsadsadadadsadadas dasdas asdas sa",
                    FromEmail = "test@phantasma.io",
                    FromName = "John Test",
                    ReceiveDate = "string date",
                    Subject = "This is a test"
                },
                new InboxMessage
                {
                    Content = "This is a long content dsadasdadsadsadadadsadadas dasdas asdas sa",
                    FromEmail = "test@phantasma.io",
                    FromName = "John Test",
                    ReceiveDate = "string date",
                    Subject = "This is a test"
                }
            };
        }

        private async Task NewMessageExecute()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                await NavigationService.NavigateToAsync<DraftViewModel>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task MessageSelectedExecute(InboxMessage item)
        {
            if (item != null)
            {
                await NavigationService.NavigateToAsync<MessageDetailViewModel>(item);
                MessageSelected = null;
            }
        }
    }
}