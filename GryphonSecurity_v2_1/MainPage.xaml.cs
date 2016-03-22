using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GryphonSecurity_v2_1.Resources;
using System.Globalization;
using GryphonSecurity_v2_1.Domain.Entity;
using Windows.Networking.Proximity;
using System.Diagnostics;
using System.Text;
using Windows.Storage.Streams;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace GryphonSecurity_v2_1
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Controller controller = Controller.Instance;
        private Windows.Networking.Proximity.ProximityDevice device;
        private long deviceId;
        private Boolean isConnected = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            controller.createAddresses();
            initializeProximitySample();
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;            
            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
        }

        private void sendReport_Click(object sender, RoutedEventArgs e)
        {
            String customerNameTB = textBoxCustomerName.Text;
            long customerNumberTB = Convert.ToInt64(textBoxCustomerNumber.Text);
            String streetAndHouseNumberTB = textBoxStreetAndHouseNumber.Text;
            int zipCodeTB = Convert.ToInt32(textBoxZipCode.Text);
            String cityTB = textBoxCity.Text;
            long phonenumberTB = Convert.ToInt64(textBoxPhonenumber.Text);
            DateTime dateTB = (DateTime)textBoxDate.Value;
            DateTime timeTB = (DateTime)textBoxTime.Value;
            String zoneTB = textBoxZone.Text;
            Boolean burglaryVandalismCB = (Boolean)checkBoxBurglaryVandalism.IsChecked;
            Boolean windowDoorClosedCB = (Boolean)checkBoxWindowDoorClosed.IsChecked;
            Boolean apprehendedPersonCB = (Boolean)checkBoxApprehendedPerson.IsChecked;
            Boolean staffErrorCB = (Boolean)checkBoxStaffError.IsChecked;
            Boolean nothingToReportCB = (Boolean)checkBoxNothingToReport.IsChecked;
            Boolean technicalErrorCB = (Boolean)checkBoxTechnicalError.IsChecked;
            Boolean unknownReasonCB = (Boolean)checkBoxUnknownReason.IsChecked;
            Boolean otherCB = (Boolean)checkBoxOther.IsChecked;
            Boolean cancelDuringEmergencyCB = (Boolean)checkBoxCancelsDuringEmergency.IsChecked;
            Boolean coverMadeCB = (Boolean)checkBoxCoverMade.IsChecked;
            String remarkTB = textBoxRemark.Text;
            String nameTB = textBoxName.Text;
            String installerTB = textBoxInstaller.Text;
            String controlCenterTB = textBoxControlCenter.Text;
            DateTime guardRadioedDateTB = (DateTime)textBoxGuardRadioedDate.Value;
            DateTime guardRadioedFromTB = (DateTime)textBoxGuardRadioedFrom.Value;
            DateTime guardRadioedToTB = (DateTime)textBoxGuardRadioedTo.Value;
            DateTime arrivedAtTB = (DateTime)textBoxArrivedAt.Value;
            DateTime doneTB = (DateTime)textBoxDone.Value;
            if (controller.createAlarmReport(new AlarmReport(customerNameTB, customerNumberTB, streetAndHouseNumberTB, zipCodeTB, cityTB, phonenumberTB, dateTB, timeTB, zoneTB, burglaryVandalismCB,
                                        windowDoorClosedCB, apprehendedPersonCB, staffErrorCB, nothingToReportCB, technicalErrorCB, unknownReasonCB, otherCB, cancelDuringEmergencyCB, coverMadeCB,
                                        remarkTB, nameTB, installerTB, controlCenterTB, guardRadioedDateTB, guardRadioedFromTB, guardRadioedToTB, arrivedAtTB, doneTB)))
            {
                MessageBox.Show(AppResources.ReportAlarmReportSuccess);
            }
            else
            {
                MessageBox.Show(AppResources.ReportAlarmReportFailed);
            }
        }

        private void scanButton_Click(object sender, RoutedEventArgs e)
        {
            NFC nfc = controller.getNFC();
            textBlockTest.Text = " Range Check: "+nfc.RangeCheck+"\r\n Tag Address: "+nfc.TagAddress+ "\r\n User Firstname: " + nfc.User.Firstname;
        }        

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            if (object.ReferenceEquals(controller.getUser(), null))
            {
                NavigationService.Navigate(new Uri("/RegisterLayout.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton registerBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/pencil.png", UriKind.Relative));
            registerBarButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(registerBarButton);
            registerBarButton.Click += RegisterBarButton_Click;

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem aboutMe = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            ApplicationBar.MenuItems.Add(aboutMe);
            aboutMe.Click += AboutMe_Click;
        }

        private void messageReceived(ProximityDevice sender, ProximityMessage message)
        {
            isConnected = controller.checkNetworkConnection();
            controller.onLocationScan();
            if (isConnected)
            {
                String tagAddress = controller.readDataFromNFCTag(message, isConnected);
                for(int i = 0; i < 10; i++)
                {
                    Debug.WriteLine("hvad starter i på "+i);
                }
                Dispatcher.BeginInvoke(() =>
                {
                    Debug.WriteLine("Tekst: " + tagAddress);
                    controller.calcPosition(tagAddress);
                    System.Threading.Tasks.Task.Delay(10000).Wait();
                    Debug.WriteLine("this should take be shown after 5 sek");
                    textBlockTest.Text = tagAddress;
                });
            } else
            {
                String tagId = controller.readDataFromNFCTag(message, isConnected);
                Dispatcher.BeginInvoke(() =>
                {
                    System.Threading.Tasks.Task.Delay(5000).Wait();
                    textBlockTest.Text = "NFC chip " + tagId + " Scannet \r\nInformation gemt på telefon indtil der er adgang til nettet";
                    controller.getDistance(null, tagId);
                });
            }
        }

        private void initializeProximitySample()
        {
            device = Windows.Networking.Proximity.ProximityDevice.GetDefault();
            if (device == null)
                Debug.WriteLine("Failed to initialized proximity device.\n" +
                                 "Your device may not have proximity hardware.");
        }

        private void AboutMe_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RegisterBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/RegisterLayout.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pivot.SelectedIndex == 0)
            { 
                deviceId = device.SubscribeForMessage("NDEF", messageReceived);
                Debug.WriteLine("this is scan");
            } else if(pivot.SelectedIndex == 1)
            {
                User user = controller.getUser();
                if (user != null)
                {
                    textBoxName.Text = user.Firstname + " " + user.Lastname;
                }
                device.StopSubscribingForMessage(deviceId);
                Debug.WriteLine("this is alarm report");
            } else if(pivot.SelectedIndex == 2)
            {
                int nfcs = controller.getLocalStorageNFCs();
                int alarmReports = controller.getLocalStorageAlarmReports();
                textBlockPendingNFCScans.Text = "Pending NFCs: " + nfcs;
                textBlockPendingAlarmReports.Text = "Pending Alarm Reports: " + alarmReports;
                Debug.WriteLine("this is pending");
            }
            
        }

        private void sendPendingButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Pending button pushed");
            isConnected = controller.checkNetworkConnection();
            if (isConnected)
            {
                Debug.WriteLine("is connected");
                if (controller.sendPendingNFCs())
                {
                    textBlockPendingNFCScans.Text = "Pending NFCs: " + 0;
                }

                if (controller.sendPendingAlarmReports())
                {
                    textBlockPendingAlarmReports.Text = "Pending Alarm Reports: " + 0;
                }
            }
        }
    }
}