﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhantasmaMail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : MasterDetailPage
	{
		public MainView()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}
	}
}