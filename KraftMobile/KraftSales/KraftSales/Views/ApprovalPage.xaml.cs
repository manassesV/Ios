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
	public partial class ApprovalPage : ContentPage
	{
		public ApprovalPage ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}