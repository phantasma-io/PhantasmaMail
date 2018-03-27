﻿using PhantasmaMail.Models;
using PhantasmaMail.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PhantasmaMail.ViewModels
{
	public class InboxViewModel : ViewModelBase
	{
		private ObservableCollection<InboxEmail> _inboxList;

		public ObservableCollection<InboxEmail> InboxList
		{
			get
			{
				return _inboxList;
			}
			set
			{
				_inboxList = value;
				OnPropertyChanged();
			}
		}

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
			InboxList = new ObservableCollection<InboxEmail>
			{
				new InboxEmail
				{
					Content = "This is a long content dsadasdadsadsadadadsadadas dasdas asdas sa",
					FromEmail = "test@phantasma.io",
					FromName = "John Test",
					ReceiveDate = "string date",
					Subject = "This is a test"
				},
				new InboxEmail
				{
					Content = "This is a long content dsadasdadsadsadadadsadadas dasdas asdas sa",
					FromEmail = "test@phantasma.io",
					FromName = "John Test",
					ReceiveDate = "string date",
					Subject = "This is a test"
				},
				new InboxEmail
				{
					Content = "This is a long content dsadasdadsadsadadadsadadas dasdas asdas sa",
					FromEmail = "test@phantasma.io",
					FromName = "John Test",
					ReceiveDate = "string date",
					Subject = "This is a test"
				},
				new InboxEmail
				{
					Content = "This is a long content dsadasdadsadsadadadsadadas dasdas asdas sa",
					FromEmail = "test@phantasma.io",
					FromName = "John Test",
					ReceiveDate = "string date",
					Subject = "This is a test"
				}
			};
		}
	}
}
