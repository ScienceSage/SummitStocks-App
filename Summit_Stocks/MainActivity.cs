using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Summit_Stocks
{
	[Activity (Label = "Summit Stocks", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : TabActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			CreateTab(typeof(Ticker), "tickers", "Tickers");
			CreateTab(typeof(TestResults), "testResults", "Test Results");
			//CreateTab (typeof(Simulator), "simulator", "Sim");
			CreateTab(typeof(Extra), "extra", "Extra");
		}

		private void CreateTab(Type activityType, string tag, string label )
		{
			var intent = new Intent(this, activityType);
			intent.AddFlags(ActivityFlags.NewTask);

			var spec = TabHost.NewTabSpec(tag);
			//var drawableIcon = Resources.GetDrawable(drawableId);
			spec.SetIndicator(label);
			spec.SetContent(intent);

			TabHost.AddTab(spec);
		}
	}

	[Activity]
	public class Simulator : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Simulator);
		}
	}

	[Activity]
	public class Extra : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Extra);
		}
	}

	[Activity]
	public class TestResults : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.TestResults);

			// Best Weeks to Buy Text
			EditText bestToBuytext = FindViewById<EditText>(Resource.Id.pvBestWeeksToBuyText);
			// Best Weeks to Sell Text
			EditText bestToSelltext = FindViewById<EditText>(Resource.Id.pvBestWeeksToSellText);

			// Refresh Button
			Button refresh = FindViewById<Button>(Resource.Id.refreshButton);
			refresh.Click += (o, e) => {
				string ticker = GlobalVariables.GetMainTicker ();

				switch (ticker)
				{
				case "AAPL":
					bestToBuytext.Text = "[26][5][9][39][3][15][12][19]";
					bestToSelltext.Text = "[2][11][19][8][21][47][24][27]";
					break;
				case "MSFT":
					bestToBuytext.Text = "[22][35][10][18][32][15][26][27]";
					bestToSelltext.Text = "[27][43][28][36][2][13][32][4]";
					break;
				case "GOOG":
					bestToBuytext.Text = "ERROR";
					bestToSelltext.Text = "ERROR";
					break;
				case "TSLA":
					bestToBuytext.Text = "[2][8][12][15][21][3][10][18]";
					bestToSelltext.Text = "[26][45][47][2][7][9][10][12]";
					break;
				case "YHOO":
					bestToBuytext.Text = "[30][34][3][10][15][22][23][25]";
					bestToSelltext.Text = "[13][2][20][45][6][10][15][25]";
					break;
				case "AMZN":
					bestToBuytext.Text = "[11][30][5][7][22][14][18][25]";
					bestToSelltext.Text = "[46][29][41][3][37][6][10][13]";
					break;
				}


			};
		}

		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			string toast = string.Format ("Displaying {0} weeks", spinner.GetItemAtPosition (e.Position));
			Toast.MakeText (this, toast, ToastLength.Short).Show ();
		}
	}

	[Activity]
	public class Ticker : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Ticker);

			// Add Text Box
			EditText edittext = FindViewById<EditText>(Resource.Id.addTickerText);

			// Add Button
			Button button = FindViewById<Button>(Resource.Id.addATickerButton);

			// Spinner
			Spinner spinner = FindViewById<Spinner> (Resource.Id.tickerSpinner);

			spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.ticker_array, Android.Resource.Layout.SimpleSpinnerItem);

			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;

			// Ticker Data Boxes
			EditText bidPriceText = FindViewById<EditText>(Resource.Id.bidPriceText);
			EditText askPriceText = FindViewById<EditText>(Resource.Id.askPriceText);
			EditText changeText = FindViewById<EditText>(Resource.Id.changeText);
			EditText percentChangeText = FindViewById<EditText>(Resource.Id.percentChangeText);

			button.Click += (o, e) => {
				string toast = string.Format ("The ticker {0} has been added", edittext.Text);
				Toast.MakeText (this, toast, ToastLength.Short).Show ();

				edittext.Text = "";
			};
		}

		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			string ticker = string.Format ("{0}", spinner.GetItemAtPosition (e.Position));

			GlobalVariables.SetMainTicker(ticker);
			string toast = string.Format ("The ticker is now {0}", spinner.GetItemAtPosition (e.Position));
			Toast.MakeText (this, toast, ToastLength.Short).Show ();

			EditText bidPriceText = FindViewById<EditText>(Resource.Id.bidPriceText);
			EditText askPriceText = FindViewById<EditText>(Resource.Id.askPriceText);
			EditText changeText = FindViewById<EditText>(Resource.Id.changeText);
			EditText percentChangeText = FindViewById<EditText>(Resource.Id.percentChangeText);

			switch (ticker) {
			case "AAPL":
				bidPriceText.Text = "130.55";
				askPriceText.Text = "130.56";
				changeText.Text = "+0.61";
				percentChangeText.Text = "+0.47%";
				break;
			case "MSFT":
				bidPriceText.Text = "47.65";
				askPriceText.Text = "47.69";
				changeText.Text = "+4.53";
				percentChangeText.Text = "+10.45%";
				break;
			case "GOOG":
				bidPriceText.Text = "ERROR";
				askPriceText.Text = "ERROR";
				changeText.Text = "ERROR";
				percentChangeText.Text = "ERROR";
				break;
			case "TSLA":
				bidPriceText.Text = "217.20";
				askPriceText.Text = "218.79";
				changeText.Text = "-0.17";
				percentChangeText.Text = "0.080%";
				break;
			case "YHOO":
				bidPriceText.Text = "44.45";
				askPriceText.Text = "44.54";
				changeText.Text = "+0.83";
				percentChangeText.Text = "+1.89%";
				break;
			case "AMZN":
				bidPriceText.Text = "443.00";
				askPriceText.Text = "445.77";
				changeText.Text = "+55.11";
				percentChangeText.Text = "+14.13%";
				break;
			}
		}
	}
}


