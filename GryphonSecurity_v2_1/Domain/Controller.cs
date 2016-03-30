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
        private GeoCoordinate targetCoordinate;
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
            if (checkNetworkConnection())
            {
                Debug.WriteLine("DB");
                return dBFacade.createAlarmReport(alarmReport);
            } else
            {
                Debug.WriteLine("TEMP");
                return dBFacade.createLocalStorageAlarmReport(alarmReport);
            }

        }
        public Boolean createTempAlarmReport(AlarmReport alarmReport)
        {
            Debug.WriteLine("Thomas her er vi");
            return dBFacade.createTempLocalStorageAlarmReport(alarmReport);
        }
        public List<AlarmReport> getLocalStorageTempAlarmReports()
        {
            return dBFacade.getLocalStorageTempAlarmReports();
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
            byte[] payload = new byte[payloadLength];
            buffer.ReadBytes(payload);
            byte langLen = (byte)(payload[0] & 0x3f);
            int textLeng = payload.Length - 1 - langLen;
            byte[] textBuf = new byte[textLeng];
            System.Buffer.BlockCopy(payload, 1 + langLen, textBuf, 0, textLeng);
            return Encoding.UTF8.GetString(textBuf, 0, textBuf.Length);
        }
        
        public async Task<Boolean> onLocationScan(String tagAddress, Boolean isConnected)
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            Boolean check = false;
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
                check = calcPosition(tagAddress, presentCoordinate, isConnected);

            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    Debug.WriteLine("location  is disabled in phone settings.");
                }
            }

            return check;
        }

        public Boolean calcPosition(String tagAddress, GeoCoordinate presentCoordinate, Boolean isConnected)
        {
            double latitude = 0d;
            double longitude = 0d;
            String address;
            Boolean check = false;
            //Geolocator locator = new Geolocator();
            //GeocodeQuery geocodequery = new GeocodeQuery();
            Debug.WriteLine("calcPosition 1");
            CancellationTokenSource cts = new CancellationTokenSource();

            try
            {
                if (!isConnected)
                {
                    dBFacade.createLocalStorageNFCs(presentCoordinate.Latitude, presentCoordinate.Longitude, tagAddress);
                    return check;
                }
                cts.CancelAfter(10000);
                List<String> tag = dBFacade.getAdress(tagAddress);
                Debug.WriteLine("calcPosition 2");
                //geocodequery.GeoCoordinate = new GeoCoordinate(0, 0);
                //geocodequery.SearchTerm = tagAddress + "Denmark";
                //geocodequery.QueryAsync();
                if (!cts.IsCancellationRequested)
                {
                    Debug.WriteLine("calcPosition 3");
                    address = tag[0]; 
                    longitude = Convert.ToDouble(tag[1]);
                    latitude = Convert.ToDouble(tag[2]);
                    Debug.WriteLine("calcPosition 5 " + longitude + " " + latitude);
                    targetCoordinate = new GeoCoordinate(latitude, longitude);
                    check = getDistance(presentCoordinate, targetCoordinate, address);                   
                } else
                {
                    Debug.WriteLine("calcPosition 4");
                    getDistance(presentCoordinate, presentCoordinate, tagAddress);
                }
                
            } catch(OperationCanceledException)
            {
                Debug.WriteLine("cancellation token");
            }
            return check;
        }

        public Boolean getDistance(GeoCoordinate presentCoordinate, GeoCoordinate targetCoordinates, String tagAddress)
        {
            Boolean check = false;
            if (!presentCoordinate.Equals(targetCoordinates) && !object.ReferenceEquals(targetCoordinates, null))
            {
                Debug.WriteLine("getDistance 1");
                double distance = targetCoordinates.GetDistanceTo(presentCoordinate);
                Boolean rangeCheck = false;
                Debug.WriteLine("Distance: " + distance);
                if (distance > 500)
                {
                    Debug.WriteLine("getDistance 2");
                    rangeCheck = false;
                } else
                {
                    Debug.WriteLine("getDistance 3");
                    rangeCheck = true;
                }
                check = dBFacade.createNFC(new NFC(rangeCheck, tagAddress, DateTime.Now, dBFacade.getUser()));
            } else
            {
                Debug.WriteLine("getDistance 4");
                //TODO gem i tempstorage
                dBFacade.createLocalStorageNFCs(presentCoordinate.Latitude, presentCoordinate.Longitude, tagAddress);
            }
            return check;


        }

        public Boolean createAddresses()
        {
            return dBFacade.createAddresses();
        }

        public NFC getNFC(long id)
        {
            return dBFacade.getNFC(id);
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
        public AlarmReport getLocalTempAlarmReport(long id)
        {
            return dBFacade.getLocalTempAlarmReport(id);
        }

        public Boolean removeLocalStorageTempSelectedAlarmReport(long id)
        {
            return dBFacade.removeLocalStorageTempSelectedAlarmReport(id);
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
            List<List<String>> tags = dBFacade.getLocalStorageNFCs();
            check = false;
            foreach (List<String> tag in tags)
            {
                double presentLatitude = Convert.ToDouble(tag[0]);
                double presentLongitude = Convert.ToDouble(tag[1]);
                Debug.WriteLine("id for tag " + tag[2]);
                List<String> nfcs = dBFacade.getAdress(tag[2]);
                String tagAddress = nfcs[0];
                double targetLongtitude = Convert.ToDouble(nfcs[1]);
                double targetLatitude = Convert.ToDouble(nfcs[2]);
                presentCoordinate = new GeoCoordinate(presentLatitude, presentLongitude);
                targetCoordinate = new GeoCoordinate(targetLatitude, targetLongtitude);
                Debug.WriteLine("tagAddress " + tagAddress);
                check = getDistance(presentCoordinate, targetCoordinate, tagAddress);
            }
            dBFacade.removeLocalStorageNFCs();
            return check;
        }

        public Boolean sendPendingAlarmReports()
        {
            Boolean alarmReportCheck = false;
            List<AlarmReport> alarmReports = dBFacade.getLocalStorageAlarmReports();
            alarmReportCheck = dBFacade.createAlarmReports(alarmReports);

            if (alarmReportCheck)
            {
                return dBFacade.removeLocalStorageAlarmReports();
            }

            return alarmReportCheck;
        }
        
    }
}

