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

namespace GryphonSecurity_v2_1
{
    public partial class MainPage : PhoneApplicationPage
    {
        Controller controller = Controller.Instance;
        private Windows.Networking.Proximity.ProximityDevice device;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
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

            device.SubscribeForMessage("NDEF", messageReceived);
            //  Debug.WriteLine("Published Message. ID is {0}", Id);
        }

        private void messageReceived(ProximityDevice sender, ProximityMessage message)
        {
            Debug.WriteLine("Der sker noget");
            //var buffer = message.Data.ToArray();
            var buffer = DataReader.FromBuffer(message.Data);
            Debug.WriteLine("1: " + buffer.ReadByte());
            Debug.WriteLine("2: " + buffer.ReadByte());
            int payloadLength = buffer.ReadByte();
            Debug.WriteLine("5: " + buffer.ReadByte());
            Debug.WriteLine("payload length: " + payloadLength);
            var payload = new byte[payloadLength];
            Debug.WriteLine("3: " + payload);

            buffer.ReadBytes(payload);

            var langLen = (byte)(payload[0] & 0x3f);
            Debug.WriteLine("LangLen: " + langLen);
            var textLeng = payload.Length - 1 - langLen;
            var textBuf = new byte[textLeng];
            System.Buffer.BlockCopy(payload, 1 + langLen, textBuf, 0, textLeng);
            //var messageType = Encoding.UTF8.GetString(buffer, 0, mimesize);
            //Debug.WriteLine("Buffer: " + buffer + "buffer length: " + buffer.Length);
            var scanned_message = Encoding.UTF8.GetString(textBuf, 0, textBuf.Length);

            Dispatcher.BeginInvoke(() =>
            {
                Debug.WriteLine("Tekst: " + scanned_message);
                textBlockTest.Text = scanned_message;
            });
        }

        private void initializeProximitySample()
        {
            device = Windows.Networking.Proximity.ProximityDevice.GetDefault();
            if (device == null)
                Debug.WriteLine("Failed to initialized proximity device.\n" +
                                 "Your device may not have proximity hardware.");
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            User user = controller.getUser();
            if (user != null)
            {
                textBoxName.Text = user.Firstname + " " + user.Lastname;
            }
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
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
                Debug.WriteLine("this is alarm report");
            } else if(pivot.SelectedIndex == 1)
            {
                Debug.WriteLine("this is scan");
            }
            
        }
    }
}