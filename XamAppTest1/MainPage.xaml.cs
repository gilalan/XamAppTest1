using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using XamAppTest1.Models;
using XamAppTest1.Services;
using XamAppTest1.ViewModels;
using XamAppTest1.Views;
using Xamarin.Forms;

namespace XamAppTest1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        SoccerApiService _service = new SoccerApiService();
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
        }

        private async void BtnLogin_Clicked(object sender, System.EventArgs e)
        {
            //var response = await _service.GetJobsAsync();
            //if (string.IsNullOrEmpty(response))
            //{

            //}
            List<Job> jobsResponse = await _service.GetJobsAsync();
            if (jobsResponse.Count > 0)
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
                //await Navigation.PushAsync(new HomePage());
            } 
            else
            {
                await DisplayAlert("Alert", "Não tinha Jobs", "OK");
            }
        }
    }
}
