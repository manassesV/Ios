﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KraftSales.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddClientPage : ContentPage
	{
		public AddClientPage ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
		}
	}
}