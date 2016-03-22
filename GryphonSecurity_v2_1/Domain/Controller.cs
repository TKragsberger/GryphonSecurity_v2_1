using GryphonSecurity_v2_1.DataSource;
using GryphonSecurity_v2_1.Domain.Entity;
using GryphonSecurity_v2_1.Resources;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Windows.Devices.Geolocation;
using Windows.Networking.Proximity;
using Windows.Storage.Streams;

namespace GryphonSecurity_v2_1
{
    public class Controller
    {
        private Windows.Networking.Proximity.ProximityDevice device;
        private DBFacade dBFacade;
        private Boolean startup = true;
        private GeoCoordinate presentCoordinate;
        private GeoCoordinate targetCoordinates;
        private static Controller instance;
        private Boolean check = false;



        private Controller()
        {
            dBFacade = new DBFacade();
        }
        public static Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance;
            }
        }

        public void startUp()
        {
            startup = false;
        }

        public Boolean getStartup()
        {
            return startup;
        }

        public Boolean createUser(User user)
        {
            return dBFacade.createUser(user);
        }

        public User getUser()
        {
            return dBFacade.getUser();
        }

        public Boolean createAlarmReport(AlarmReport alarmReport)
        {
            return dBFacade.createAlarmReport(alarmReport);
        }

        private Boolean initializeProximitySample()
        {
            Boolean deviceProxomity = true;
            device = Windows.Networking.Proximity.ProximityDevice.GetDefault();
            if (device == null)
            {
                Debug.WriteLine("Failed to initialized proximity device.\n" +
                                 "Your device may not have proximity hardware.");
                deviceProxomity = false;
            }
            return deviceProxomity;

        }

        public String readDataFromNFCTag(ProximityMessage message, Boolean isConnected)
        {
            DataReader buffer = DataReader.FromBuffer(message.Data);
            Debug.WriteLine("1: " + buffer.ReadByte());
            Debug.WriteLine("2: " + buffer.ReadByte());
            int payloadLength = buffer.ReadByte();
            Debug.WriteLine("5: " + buffer.ReadByte());
            Debug.WriteLine("jaja length: " + payloadLength);
            byte[] payload = new byte[payloadLength];
            Debug.WriteLine("3: " + payload);
            buffer.ReadBytes(payload);
            byte langLen = (byte)(payload[0] & 0x3f);
            Debug.WriteLine("LanLeng: " + langLen);
            int textLeng = payload.Length - 1 - langLen;
            byte[] textBuf = new byte[textLeng];
            System.Buffer.BlockCopy(payload, 1 + langLen, textBuf, 0, textLeng);
            String scanned_message = Encoding.UTF8.GetString(textBuf, 0, textBuf.Length);
            if (isConnected)
            {
                String tagAddress = dBFacade.getAdress(scanned_message);
                return tagAddress;          
            } else
            {
                return scanned_message;
            }
        }
        
        public async void onLocationScan()
        {
            //if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
            //{
            //    // The user has opted out of Location.

            //}

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            Debug.WriteLine("test 1");
            try
            {
                Debug.WriteLine("test 2");
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );
                Debug.WriteLine("test 3");
                double latitude = geoposition.Coordinate.Point.Position.Latitude;
                double longitude = geoposition.Coordinate.Point.Position.Longitude;
                presentCoordinate = new GeoCoordinate(latitude, longitude);
                Debug.WriteLine("PresentCoordinate: " + presentCoordinate);

            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    Debug.WriteLine("location  is disabled in phone settings.");
                }
            }


        }

        public void calcPosition(String tagAddress)
        {
            double latitude = 0d;
            double longitude = 0d;
            Geolocator locator = new Geolocator();
            GeocodeQuery geocodequery = new GeocodeQuery();
            CancellationTokenSource cts = new CancellationTokenSource();

            try
            {
                cts.CancelAfter(10000);

                if (!locator.LocationStatus.Equals(PositionStatus.Disabled))
                {
                    geocodequery.GeoCoordinate = new GeoCoordinate(0, 0);
                    geocodequery.SearchTerm = tagAddress + "Denmark";
                    geocodequery.QueryAsync();
                    if (!cts.IsCancellationRequested)
                    {
                        geocodequery.QueryCompleted += (sender, args) =>
                        {
                            if (!args.Result.Equals(null))
                            {
                                Debug.WriteLine("new test");
                                MapLocation result = args.Result.FirstOrDefault();
                                latitude = result.GeoCoordinate.Latitude;
                                Debug.WriteLine("Latitude: " + latitude);
                                longitude = result.GeoCoordinate.Longitude;
                                Debug.WriteLine("Longitude: " + longitude);
                                targetCoordinates = new GeoCoordinate(latitude, longitude);
                                getDistance(targetCoordinates, tagAddress);
                            }
                        };
                    } else
                    {
                        getDistance(presentCoordinate, tagAddress);
                    }
                } else
                {
                    MessageBox.Show("Service Geolocation not enabled!", AppResources.ApplicationTitle, MessageBoxButton.OK);
                    return;
                }
            } catch(OperationCanceledException)
            {
                Debug.WriteLine("cancellation token");
            }
        }

        public void getDistance(GeoCoordinate targetCoordinates, String tagAddress)
        {
            if (!presentCoordinate.Equals(targetCoordinates) && !object.ReferenceEquals(targetCoordinates, null))
            {
                double distance = targetCoordinates.GetDistanceTo(presentCoordinate);
                Boolean rangeCheck = false;
                Debug.WriteLine("Distance: " + distance);
                if (distance > 500)
                {
                    rangeCheck = false;
                } else
                {
                    rangeCheck = true;
                }
                check = dBFacade.createNFC(new NFC(rangeCheck, tagAddress, dBFacade.getUser()));
            } else
            {
                //TODO gem i tempstorage
                dBFacade.createLocalStorageNFCs(presentCoordinate.Latitude, presentCoordinate.Longitude, tagAddress);
            }


        }

        public Boolean createAddresses()
        {
            return dBFacade.createAddresses();
        }

        public NFC getNFC()
        {
            return dBFacade.getNFC();
        }

        public bool checkNetworkConnection()
        {
            NetworkInterfaceType ni = NetworkInterface.NetworkInterfaceType;
            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }

        public int getLocalStorageNFCs()
        {
            return dBFacade.getLocalStorageNumberOfNFCs();
        }

        public int getLocalStorageAlarmReports()
        {
            return dBFacade.getLocalStorageNumberOfAlarmReports();
        }

        public Boolean sendPendingNFCs()
        {
            List<List<String>> items = dBFacade.getLocalStorageNFCs();
            Debug.WriteLine(items.Count);
            foreach (List<String> item in items)
            {
                double presentLatitude = Convert.ToDouble(item[0]);
                Debug.WriteLine("Latitude " + presentLatitude);
                double presentLongitude = Convert.ToDouble(item[1]);
                Debug.WriteLine("Longitude " + presentLongitude);
                presentCoordinate = new GeoCoordinate(presentLatitude, presentLongitude);
                String tagAddress = item[2];
                Debug.WriteLine("tagAddress " + tagAddress);
                //calcPosition(tagAddress);
                //if (!check)
                //{
                //    return check;
                //}
            }
            dBFacade.removeLocalStorageNFCs();
            return true;
        }

        public Boolean sendPendingAlarmReports()
        {
            Boolean alarmReportCheck = true;

            //TODO når der er lavet backend så lav metoder der kan modtage en hel list i items i stedet for kun 1 item afgangen
            //alarmReportCheck = dBFacade.createAlarmReports(alarmReports);

            if (alarmReportCheck)
            {
                return dBFacade.removeLocalStorageAlarmReports();
            }

            return false;
        }
    }
}

