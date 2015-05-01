
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Summit_Stocks
{	
	static public class GlobalVariables
	{
		static string mainTicker { get; set; }

		static public string GetMainTicker() { return mainTicker; }

		static public void SetMainTicker(string ticker) { mainTicker = ticker; }
	}
}

