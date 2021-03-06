﻿using System;
using System.Threading.Tasks;
using PhantasmaMail.Models;
using PhantasmaMail.Utils;
using PhantasmaMail.ViewModels;
using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhantasmaMail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SentView : ContentPage
	{
	    private SentViewModel Vm => BindingContext as SentViewModel;

        public SentView ()
		{
			InitializeComponent ();
		    sentList.DataSource.GroupDescriptors.Add(new GroupDescriptor()
		    {
		        PropertyName = "Date",
		        KeySelector = (obj1) =>
		        {
		            var item = (obj1 as Message);
		            return item.GroupDate;
		        },
		        Comparer = new MessageDateGroupComparer()
		    });
		    MessagingCenter.Subscribe<SentViewModel>(this, "resetToolbar", (sender) =>
		    {
		        SetupToolbar();
		    });
        }

	    protected override void OnAppearing()
	    {
	        pullToRefreshList.ForceLayout();
            AddNewMessageToolbar();
	    }

        private async void PullToRefresh_Refreshing(object sender, EventArgs args)
	    {
	        pullToRefreshList.IsRefreshing = true;
	        await Task.Delay(2000);
	        await Vm?.RefreshExecute();
            pullToRefreshList.IsRefreshing = false;
	    }

	    private void AddNewMessageToolbar()
	    {
	        if (ToolbarItems.Count == 1) return;
            var item = new ToolbarItem
	        {
	            Command = Vm?.NewMessageCommand,
	            Icon = Device.RuntimePlatform == Device.UWP ? "Assets/WriteEmail.png" : "WriteEmail.png",
            };
	        ToolbarItems.Add(item);
	    }

	    private void AddDeleteMessageToolbar()
	    {
	        if (ToolbarItems.Count == 1) return;
            var item = new ToolbarItem
	        {
	            Command = Vm?.DeleteSelectedMessages,
	            Icon = Device.RuntimePlatform == Device.UWP ? "Assets/trash_bar.png" : "trash_bar.png",
            };
	        ToolbarItems.Add(item);
	    }

        private void SentList_OnItemHolding(object sender, ItemHoldingEventArgs e)
	    {
	        Vm?.ActivateMultipleSelectionCommand.Execute(null);
	        SetupToolbar();
        }

	    private void SetupToolbar()
	    {
	        ToolbarItems.Clear();
	        if (Vm != null && Vm.IsMultipleSelectionActive)
	        {
	            AddDeleteMessageToolbar();
	        }
	        else
	        {
	            AddNewMessageToolbar();
	        }
	    }
    }
}