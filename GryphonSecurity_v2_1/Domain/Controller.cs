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
    class Controller
    {
        private Windows.Networking.Proximity.ProximityDevice device;
        private DBFacade dBFacade;
        Boolean startup = true;
        GeoCoordinate presentCoordinate;
        GeoCoordinate targetCoordinates;
        private double checker;


        private static Controller instance;
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
            Debug.WriteLine("Der sker noget");

            //var buffer = message.Data.ToArray();

            var buffer = DataReader.FromBuffer(message.Data);
            Debug.WriteLine("1: " + buffer.ReadByte());
            Debug.WriteLine("2: " + buffer.ReadByte());
            int payloadLength = buffer.ReadByte();
            Debug.WriteLine("5: " + buffer.ReadByte());
            Debug.WriteLine("jaja length: " + payloadLength);
            var payload = new byte[payloadLength];
            Debug.WriteLine("3: " + payload);

            buffer.ReadBytes(payload);

            var langLen = (byte)(payload[0] & 0x3f);
            Debug.WriteLine("LanLeng: " + langLen);
            var textLeng = payload.Length - 1 - langLen;
            var textBuf = new byte[textLeng];
            System.Buffer.BlockCopy(payload, 1 + langLen, textBuf, 0, textLeng);
            var scanned_message = Encoding.UTF8.GetString(textBuf, 0, textBuf.Length);
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

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );


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
            var latitude = 0d;
            var longitude = 0d;
            var locator = new Geolocator();
            var geocodequery = new GeocodeQuery();
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
                                var result = args.Result.FirstOrDefault();
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
                }

                else
                {
                    MessageBox.Show("Service Geolocation not enabled!", AppResources.ApplicationTitle, MessageBoxButton.OK);

                    return;
                }
            } catch(OperationCanceledException e)
            {
                Debug.WriteLine("cancellation token");
            }
        }

        public void getDistance(GeoCoordinate targetCoordinates, String tagAddress)
        {
            if (!presentCoordinate.Equals(targetCoordinates))
            {
                double distance = targetCoordinates.GetDistanceTo(presentCoordinate);
                Boolean rangeCheck = false;
                Debug.WriteLine("Distance: " + distance);
                if (checker > 500)
                {
                    rangeCheck = false;
                }
                else
                {
                    rangeCheck = true;
                }
                dBFacade.createNFC(new NFC(rangeCheck, tagAddress, dBFacade.getUser()));
            } else
            {
                //TODO gem i tempstorage
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
            var ni = NetworkInterface.NetworkInterfaceType;

            bool IsConnected = false;
            if ((ni == NetworkInterfaceType.Wireless80211) || (ni == NetworkInterfaceType.MobileBroadbandCdma) || (ni == NetworkInterfaceType.MobileBroadbandGsm))
                IsConnected = true;
            else if (ni == NetworkInterfaceType.None)
                IsConnected = false;
            return IsConnected;
        }

    }
}

