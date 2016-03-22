using GryphonSecurity_v2_1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryphonSecurity_v2_1.DataSource
{
    public class LocalStorage
    {
        private static IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

        private long id = 0;

        private String KEY_FIRSTNAME = "FIRSTNAME";
        private String KEY_LASTNAME = "LASTNAME";
        private String KEY_ADDRESS = "ADDRESS";
        private String KEY_PHONENUMBER = "PHONENUMBER";
        private String KEY_USERNAME = "USERNAME";
        private String KEY_PASSWORD = "PASSWORD";

        private String KEY_ID_NFC = "ID_NFC";
        private String KEY_ID_ALARMREPORT = "ID_ALARMREPORT";
        private String KEY_ID_TEMP_ALARMREPORT = "ID_TEMP_ALARMREPORT";
        

        private String KEY_CURRENTNUMBEROFALARMREPORTS = "CURRENTNUMBEROFALARMREPORTS";
        private String KEY_CURRENTNUMBEROFNFCS = "CURRENTNUMBEROFNFCS";
        private String KEY_CURRENTNUMBEROFTEMPALARMREPORTS = "CURRENTNUMBEROFTEMPALARMREPORTS";

        private String KEY_REPORT_CUSTOMERNAME = "REPORT_CUSTOMERNAME";
        private String KEY_REPORT_CUSTOMERNUMBER = "REPORT_CUSTOMERNUMBER";
        private String KEY_REPORT_STREETANDHOUSENUMBER = "REPORT_STREETANDHOUSENUMBER";
        private String KEY_REPORT_ZIPCODE = "REPORT_ZIPCODE";
        private String KEY_REPORT_CITY = "REPORT_CITY";
        private String KEY_REPORT_PHONENUMBER = "REPORT_PHONENUMBER";
        private String KEY_REPORT_DATE = "REPORT_DATE";
        private String KEY_REPORT_TIME = "REPORT_TIME";
        private String KEY_REPORT_ZONE = "REPORT_ZONE";
        private String KEY_REPORT_BURGLARYVANDALISM = "REPORT_BURGLARYVANDALISM";
        private String KEY_REPORT_WINDOWDOORCLOSED = "REPORT_WINDOWDOORCLOSED";
        private String KEY_REPORT_APPREHENDEDPERSON = "REPORT_APPREHENDEDPERSON";
        private String KEY_REPORT_STAFFERROR = "REPORT_STAFFERROR";
        private String KEY_REPORT_NOTHINGTOREPORT = "REPORT_NOTHINGTOREPORT";
        private String KEY_REPORT_TECHNICALERROR = "REPORT_TECHNICALERROR";
        private String KEY_REPORT_UNKNOWNREASON = "REPORT_UNKNOWNREASON";
        private String KEY_REPORT_OTHER = "REPORT_OTHER";
        private String KEY_REPORT_CANCELDURINGEMERGENCY = "REPORT_CANCELDURINGEMERGENCY";
        private String KEY_REPORT_COVERMADE = "REPORT_COVERMADE";
        private String KEY_REPORT_REMARK = "REPORT_REMARK";
        private String KEY_REPORT_NAME = "REPORT_NAME";
        private String KEY_REPORT_INSTALLER = "REPORT_INSTALLER";
        private String KEY_REPORT_CONTROLCENTER = "REPORT_CONTROLCENTER";
        private String KEY_REPORT_GUARDRADIOEDDATE = "REPORT_GUARDRADIOEDDATE";
        private String KEY_REPORT_GUARDRADIOEDFROM = "REPORT_GUARDRADIOEDFROM";
        private String KEY_REPORT_GUARDRADIOEDTO = "REPORT_GUARDRADIOEDTO";
        private String KEY_REPORT_ARRIVEDAT = "REPORT_ARRIVEDAT";
        private String KEY_REPORT_DONE = "REPORT_DONE";

        private String KEY_TEMP_REPORT_CUSTOMERNAME = "TEMP_REPORT_CUSTOMERNAME";
        private String KEY_TEMP_REPORT_CUSTOMERNUMBER = "TEMP_REPORT_CUSTOMERNUMBER";
        private String KEY_TEMP_REPORT_STREETANDHOUSENUMBER = "TEMP_REPORT_STREETANDHOUSENUMBER";
        private String KEY_TEMP_REPORT_ZIPCODE = "TEMP_REPORT_ZIPCODE";
        private String KEY_TEMP_REPORT_CITY = "TEMP_REPORT_CITY";
        private String KEY_TEMP_REPORT_PHONENUMBER = "TEMP_REPORT_PHONENUMBER";
        private String KEY_TEMP_REPORT_DATE = "TEMP_REPORT_DATE";
        private String KEY_TEMP_REPORT_TIME = "TEMP_REPORT_TIME";
        private String KEY_TEMP_REPORT_ZONE = "TEMP_REPORT_ZONE";
        private String KEY_TEMP_REPORT_BURGLARYVANDALISM = "TEMP_REPORT_BURGLARYVANDALISM";
        private String KEY_TEMP_REPORT_WINDOWDOORCLOSED = "TEMP_REPORT_WINDOWDOORCLOSED";
        private String KEY_TEMP_REPORT_APPREHENDEDPERSON = "TEMP_REPORT_APPREHENDEDPERSON";
        private String KEY_TEMP_REPORT_STAFFERROR = "TEMP_REPORT_STAFFERROR";
        private String KEY_TEMP_REPORT_NOTHINGTOREPORT = "TEMP_REPORT_NOTHINGTOREPORT";
        private String KEY_TEMP_REPORT_TECHNICALERROR = "TEMP_REPORT_TECHNICALERROR";
        private String KEY_TEMP_REPORT_UNKNOWNREASON = "TEMP_REPORT_UNKNOWNREASON";
        private String KEY_TEMP_REPORT_OTHER = "TEMP_REPORT_OTHER";
        private String KEY_TEMP_REPORT_CANCELDURINGEMERGENCY = "TEMP_REPORT_CANCELDURINGEMERGENCY";
        private String KEY_TEMP_REPORT_COVERMADE = "TEMP_REPORT_COVERMADE";
        private String KEY_TEMP_REPORT_REMARK = "TEMP_REPORT_REMARK";
        private String KEY_TEMP_REPORT_NAME = "TEMP_REPORT_NAME";
        private String KEY_TEMP_REPORT_INSTALLER = "TEMP_REPORT_INSTALLER";
        private String KEY_TEMP_REPORT_CONTROLCENTER = "TEMP_REPORT_CONTROLCENTER";
        private String KEY_TEMP_REPORT_GUARDRADIOEDDATE = "TEMP_REPORT_GUARDRADIOEDDATE";
        private String KEY_TEMP_REPORT_GUARDRADIOEDFROM = "TEMP_REPORT_GUARDRADIOEDFROM";
        private String KEY_TEMP_REPORT_GUARDRADIOEDTO = "TEMP_REPORT_GUARDRADIOEDTO";
        private String KEY_TEMP_REPORT_ARRIVEDAT = "TEMP_REPORT_ARRIVEDAT";
        private String KEY_TEMP_REPORT_DONE = "TEMP_REPORT_DONE";

        private String KEY_NFC_RANGECHECK = "NFC_RANGECHECK";
        private String KEY_NFC_TAGADDRESS = "NFC_TAGADDRESS";
        private String KEY_NFC_PRESENTLATITUDE = "NFC_PRESENTLATITUDE";
        private String KEY_NFC_PRESENTLONGITUDE = "NFC_PRESENTLONGITUDE";

        public Boolean createUser(User user)
        {
            if (appSettings.Contains(KEY_FIRSTNAME))
            {
                appSettings.Remove(KEY_FIRSTNAME);
                appSettings.Remove(KEY_LASTNAME);
                appSettings.Remove(KEY_ADDRESS);
                appSettings.Remove(KEY_PHONENUMBER);
                appSettings.Remove(KEY_USERNAME);
                appSettings.Remove(KEY_PASSWORD);
            }

            try
            {
                Debug.WriteLine("" + user.toString());
                appSettings.Add(KEY_FIRSTNAME, user.Firstname);
                appSettings.Add(KEY_LASTNAME, user.Lastname);
                appSettings.Add(KEY_ADDRESS, user.Address);
                appSettings.Add(KEY_PHONENUMBER, user.Phonenumber);
                appSettings.Add(KEY_USERNAME, user.Username);
                appSettings.Add(KEY_PASSWORD, user.Password);
                appSettings.Save();
                return true;

            }
            catch (IsolatedStorageException)
            {
                return false;
            }
        }

        public User getUser()
        {
            if (appSettings.Contains(KEY_USERNAME))
            {
                String firstname = appSettings[KEY_FIRSTNAME] as String;
                String lastname = appSettings[KEY_LASTNAME] as String;
                String address = appSettings[KEY_ADDRESS] as String;
                long phonenumber = Convert.ToInt64(appSettings[KEY_PHONENUMBER] as String);
                String username = appSettings[KEY_USERNAME] as String;
                String password = appSettings[KEY_PASSWORD] as String;
                return new User(firstname, lastname, address, phonenumber, username, password);
            }
            else
            {
                return null;
            }

        }

        private long getCurrentAlarmReportId()
        {
            if (!appSettings.Contains(KEY_ID_ALARMREPORT))
            {
                appSettings.Add(KEY_ID_ALARMREPORT, "0");
                appSettings.Save();
            }
            return Convert.ToInt64(appSettings[KEY_ID_ALARMREPORT] as String);
        }

        private long getNextAlarmReportId()
        {
            long nextId = getCurrentAlarmReportId() + 1;
            appSettings.Remove(KEY_ID_ALARMREPORT);
            appSettings.Add(KEY_ID_ALARMREPORT, nextId + "");
            appSettings.Save();
            return nextId;
        }

        public int currentNumberOfAlarmReports()
        {
            if (!appSettings.Contains(KEY_CURRENTNUMBEROFALARMREPORTS))
            {
                appSettings.Add(KEY_CURRENTNUMBEROFALARMREPORTS, "0");
                appSettings.Save();
            }
            return Convert.ToInt32(appSettings[KEY_CURRENTNUMBEROFALARMREPORTS] as String);
        }

        public void addNumberOfAlarmReports()
        {
            int next = currentNumberOfAlarmReports() + 1;
            appSettings.Add(KEY_CURRENTNUMBEROFALARMREPORTS, next + "");
            appSettings.Save();
        }
        private long getCurrentTempAlarmReportId()
        {
            if (!appSettings.Contains(KEY_ID_TEMP_ALARMREPORT))
            {
                appSettings.Add(KEY_ID_TEMP_ALARMREPORT, "0");
                appSettings.Save();
            }
            return Convert.ToInt64(appSettings[KEY_ID_TEMP_ALARMREPORT] as String);
        }

        private long getNextTempAlarmReportId()
        {
            long nextId = getCurrentTempAlarmReportId() + 1;
            appSettings.Remove(KEY_ID_TEMP_ALARMREPORT);
            appSettings.Add(KEY_ID_TEMP_ALARMREPORT, nextId + "");
            appSettings.Save();
            return nextId;
        }

        public int currentNumberOfTempAlarmReports()
        {
            if (!appSettings.Contains(KEY_CURRENTNUMBEROFTEMPALARMREPORTS))
            {
                appSettings.Add(KEY_CURRENTNUMBEROFTEMPALARMREPORTS, "0");
                appSettings.Save();
            }
            return Convert.ToInt32(appSettings[KEY_CURRENTNUMBEROFTEMPALARMREPORTS] as String);
        }

        public void addNumberOfTempAlarmReports()
        {
            int next = currentNumberOfTempAlarmReports() + 1;
            appSettings.Remove(KEY_CURRENTNUMBEROFTEMPALARMREPORTS);
            appSettings.Add(KEY_CURRENTNUMBEROFTEMPALARMREPORTS, next + "");
            appSettings.Save();
        }

        private long getCurrentNfcId()
        {
            Debug.WriteLine(appSettings.Contains(KEY_ID_NFC));
            if (!appSettings.Contains(KEY_ID_NFC))
            {
                Debug.WriteLine("when are we here");
                appSettings.Add(KEY_ID_NFC, "0");
                appSettings.Save();
            }
            return Convert.ToInt64(appSettings[KEY_ID_NFC] as String);
        }

        private long getNextNfcId()
        {
            try
            {
                long test = getCurrentNfcId();
                Debug.WriteLine(test);
                long nextId = test + 1;
                Debug.WriteLine(nextId);
                appSettings.Remove(KEY_ID_NFC);
                appSettings.Add(KEY_ID_NFC, nextId+"");
                appSettings.Save();

                return nextId;

            }
            catch (IsolatedStorageException)
            {
                Debug.WriteLine("error");
                return 1111111111111111111;
            }
            
        }

        public int currentNumberOfNFCs()
        {
            if (!appSettings.Contains(KEY_CURRENTNUMBEROFNFCS))
            {
                appSettings.Add(KEY_CURRENTNUMBEROFNFCS, "0");
                appSettings.Save();
            }
            return Convert.ToInt32(appSettings[KEY_CURRENTNUMBEROFNFCS] as String);
        }

        public void addNumberOfNFCs()
        {
            try
            {
                int next = currentNumberOfNFCs() + 1;
                if (appSettings.Contains(KEY_CURRENTNUMBEROFNFCS))
                {
                    appSettings.Remove(KEY_CURRENTNUMBEROFNFCS);
                }
                appSettings.Add(KEY_CURRENTNUMBEROFNFCS, next + "");
                appSettings.Save();


            }
            catch (IsolatedStorageException)
            {
                return;
            }
        }

        public Boolean createAlarmReport(AlarmReport alarmReport)
        {
            long id = getNextAlarmReportId();           
            try
            {
                appSettings.Add(id + KEY_REPORT_CUSTOMERNAME, alarmReport.CustomerName);
                appSettings.Add(id + KEY_REPORT_CUSTOMERNUMBER, alarmReport.CustomerNumber);
                appSettings.Add(id + KEY_REPORT_STREETANDHOUSENUMBER, alarmReport.StreetAndHouseNumber);
                appSettings.Add(id + KEY_REPORT_ZIPCODE, alarmReport.ZipCode);
                appSettings.Add(id + KEY_REPORT_CITY, alarmReport.City);
                appSettings.Add(id + KEY_REPORT_PHONENUMBER, alarmReport.Phonenumber);
                appSettings.Add(id + KEY_REPORT_DATE, alarmReport.Date);
                appSettings.Add(id + KEY_REPORT_TIME, alarmReport.Time);
                appSettings.Add(id + KEY_REPORT_ZONE, alarmReport.Zone);
                appSettings.Add(id + KEY_REPORT_BURGLARYVANDALISM, alarmReport.BurglaryVandalism);
                appSettings.Add(id + KEY_REPORT_WINDOWDOORCLOSED, alarmReport.WindowDoorClosed);
                appSettings.Add(id + KEY_REPORT_APPREHENDEDPERSON, alarmReport.ApprehendedPerson);
                appSettings.Add(id + KEY_REPORT_STAFFERROR, alarmReport.StaffError);
                appSettings.Add(id + KEY_REPORT_NOTHINGTOREPORT, alarmReport.NothingToReport);
                appSettings.Add(id + KEY_REPORT_TECHNICALERROR, alarmReport.TechnicalError);
                appSettings.Add(id + KEY_REPORT_UNKNOWNREASON, alarmReport.UnknownReason);
                appSettings.Add(id + KEY_REPORT_OTHER, alarmReport.Other);
                appSettings.Add(id + KEY_REPORT_CANCELDURINGEMERGENCY, alarmReport.CancelDuringEmergency);
                appSettings.Add(id + KEY_REPORT_COVERMADE, alarmReport.CoverMade);
                appSettings.Add(id + KEY_REPORT_REMARK, alarmReport.CoverMade);
                appSettings.Add(id + KEY_REPORT_NAME, alarmReport.Name);
                appSettings.Add(id + KEY_REPORT_INSTALLER, alarmReport.Installer);
                appSettings.Add(id + KEY_REPORT_CONTROLCENTER, alarmReport.ControlCenter);
                appSettings.Add(id + KEY_REPORT_GUARDRADIOEDDATE, alarmReport.GuardRadioedDate);
                appSettings.Add(id + KEY_REPORT_GUARDRADIOEDFROM, alarmReport.GuardRadioedFrom);
                appSettings.Add(id + KEY_REPORT_GUARDRADIOEDTO, alarmReport.GuardRadioedTo);
                appSettings.Add(id + KEY_REPORT_ARRIVEDAT, alarmReport.ArrivedAt);
                appSettings.Add(id + KEY_REPORT_DONE, alarmReport.Done);
                appSettings.Save();
                addNumberOfAlarmReports();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<AlarmReport> getAlarmReports()
        {
            List<AlarmReport> alarmReports = new List<AlarmReport>();
            int length = currentNumberOfAlarmReports();

            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    id = i + 1;
                    String customerName = appSettings[id+KEY_REPORT_CUSTOMERNAME] as String;
                    long customerNumber = Convert.ToInt64(appSettings[id + KEY_REPORT_CUSTOMERNUMBER] as String);
                    String streetAndHouseNumber = appSettings[id + KEY_REPORT_STREETANDHOUSENUMBER] as String;
                    int zipCode = Convert.ToInt32(appSettings[id + KEY_REPORT_ZIPCODE] as String);
                    String city = appSettings[id + KEY_REPORT_CITY] as String;
                    long phonenumber = Convert.ToInt64(appSettings[id + KEY_REPORT_PHONENUMBER] as String);
                    DateTime date = DateTime.Parse(appSettings[id + KEY_REPORT_DATE] as String, CultureInfo.InvariantCulture);
                    DateTime time = DateTime.Parse(appSettings[id + KEY_REPORT_TIME] as String, CultureInfo.InvariantCulture);
                    String zone = appSettings[id + KEY_REPORT_ZONE] as String;
                    Boolean burglaryVandalism = Convert.ToBoolean(appSettings[id + KEY_REPORT_BURGLARYVANDALISM] as String);
                    Boolean windowDoorClosed = Convert.ToBoolean(appSettings[id + KEY_REPORT_WINDOWDOORCLOSED] as String);
                    Boolean apprehendedPerson = Convert.ToBoolean(appSettings[id + KEY_REPORT_APPREHENDEDPERSON] as String);
                    Boolean staffError = Convert.ToBoolean(appSettings[id + KEY_REPORT_STAFFERROR] as String);
                    Boolean nothingToReport = Convert.ToBoolean(appSettings[id + KEY_REPORT_NOTHINGTOREPORT] as String);
                    Boolean technicalError = Convert.ToBoolean(appSettings[id + KEY_REPORT_TECHNICALERROR] as String);
                    Boolean unknownReason = Convert.ToBoolean(appSettings[id + KEY_REPORT_UNKNOWNREASON] as String);
                    Boolean other = Convert.ToBoolean(appSettings[id + KEY_REPORT_OTHER] as String);
                    Boolean cancelDuringEmergency = Convert.ToBoolean(appSettings[id + KEY_REPORT_CANCELDURINGEMERGENCY] as String);
                    Boolean coverMade = Convert.ToBoolean(appSettings[id + KEY_REPORT_COVERMADE] as String);
                    String remark = appSettings[id + KEY_REPORT_REMARK] as String;
                    String name = appSettings[id + KEY_REPORT_NAME] as String;
                    String installer = appSettings[id + KEY_REPORT_INSTALLER] as String;
                    String controlCenter = appSettings[id + KEY_REPORT_CONTROLCENTER] as String;
                    DateTime guardRadioedDate = DateTime.Parse(appSettings[id + KEY_REPORT_GUARDRADIOEDDATE] as String, CultureInfo.InvariantCulture);
                    DateTime guardRadioedFrom = DateTime.Parse(appSettings[id + KEY_REPORT_GUARDRADIOEDFROM] as String, CultureInfo.InvariantCulture);
                    DateTime guardRadioedTo = DateTime.Parse(appSettings[id + KEY_REPORT_GUARDRADIOEDTO] as String, CultureInfo.InvariantCulture);
                    DateTime arrivedAt = DateTime.Parse(appSettings[id + KEY_REPORT_ARRIVEDAT] as String, CultureInfo.InvariantCulture);
                    DateTime done = DateTime.Parse(appSettings[id + KEY_REPORT_DONE] as String, CultureInfo.InvariantCulture);
                    alarmReports.Add(new AlarmReport(customerName, customerNumber, streetAndHouseNumber, zipCode, city, phonenumber, date, time, zone, burglaryVandalism,
                                            windowDoorClosed, apprehendedPerson, staffError, nothingToReport, technicalError, unknownReason, other, cancelDuringEmergency, coverMade,
                                            remark, name, installer, controlCenter, guardRadioedDate, guardRadioedFrom, guardRadioedTo, arrivedAt, done));
                }
            }

            return alarmReports;
        }


        public Boolean removeAlarmReports()
        {
            int length = currentNumberOfAlarmReports();
            Boolean itemsRemoved = false;

            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    id = i + 1;
                    appSettings.Remove(id + KEY_REPORT_CUSTOMERNAME);
                    appSettings.Remove(id + KEY_REPORT_CUSTOMERNUMBER);
                    appSettings.Remove(id + KEY_REPORT_STREETANDHOUSENUMBER);
                    appSettings.Remove(id + KEY_REPORT_ZIPCODE);
                    appSettings.Remove(id + KEY_REPORT_CITY);
                    appSettings.Remove(id + KEY_REPORT_PHONENUMBER);
                    appSettings.Remove(id + KEY_REPORT_DATE);
                    appSettings.Remove(id + KEY_REPORT_TIME);
                    appSettings.Remove(id + KEY_REPORT_ZONE);
                    appSettings.Remove(id + KEY_REPORT_BURGLARYVANDALISM);
                    appSettings.Remove(id + KEY_REPORT_WINDOWDOORCLOSED);
                    appSettings.Remove(id + KEY_REPORT_APPREHENDEDPERSON);
                    appSettings.Remove(id + KEY_REPORT_STAFFERROR);
                    appSettings.Remove(id + KEY_REPORT_NOTHINGTOREPORT);
                    appSettings.Remove(id + KEY_REPORT_TECHNICALERROR);
                    appSettings.Remove(id + KEY_REPORT_UNKNOWNREASON);
                    appSettings.Remove(id + KEY_REPORT_OTHER);
                    appSettings.Remove(id + KEY_REPORT_CANCELDURINGEMERGENCY);
                    appSettings.Remove(id + KEY_REPORT_COVERMADE);
                    appSettings.Remove(id + KEY_REPORT_REMARK);
                    appSettings.Remove(id + KEY_REPORT_NAME);
                    appSettings.Remove(id + KEY_REPORT_INSTALLER);
                    appSettings.Remove(id + KEY_REPORT_CONTROLCENTER);
                    appSettings.Remove(id + KEY_REPORT_GUARDRADIOEDDATE);
                    appSettings.Remove(id + KEY_REPORT_GUARDRADIOEDFROM);
                    appSettings.Remove(id + KEY_REPORT_GUARDRADIOEDTO);
                    appSettings.Remove(id + KEY_REPORT_ARRIVEDAT);
                    appSettings.Remove(id + KEY_REPORT_DONE);
                }
                itemsRemoved = true;
                appSettings.Remove(KEY_ID_ALARMREPORT);
                appSettings.Save();
            }
            return itemsRemoved;
        }

        public Boolean createNFC(double presentLatitude, double presentLongitude, String tagAddress)
        {
            long NFCId = getNextNfcId();
            Debug.WriteLine("this is create nfc local storage");
            try
            {

                //appSettings.Add(id + KEY_NFC_RANGECHECK, nfc.RangeCheck);
                Debug.WriteLine(NFCId);
                appSettings.Add(NFCId + KEY_NFC_PRESENTLATITUDE, presentLatitude + "");
                appSettings.Add(NFCId + KEY_NFC_PRESENTLONGITUDE, presentLongitude + "");
                appSettings.Add(NFCId + KEY_NFC_TAGADDRESS, tagAddress);

                appSettings.Save();
                addNumberOfNFCs();
                return true;
            }
            catch (IsolatedStorageException)
            {
                Debug.WriteLine("Addresses did not get saved in dummyDB");
                return false;
            }

        }

        public List<List<String>> getNFCs()
        {
            List<List<String>> nfcs = new List<List<String>>();
            int length = currentNumberOfNFCs();
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    List<String> items = new List<String>();
                    id = i + 1;
                    String presentLatitude = appSettings[id + KEY_NFC_PRESENTLATITUDE] as String;
                    String presentLongitude = appSettings[id + KEY_NFC_PRESENTLONGITUDE] as String;
                    String tagAddress = appSettings[id + KEY_NFC_TAGADDRESS] as String;
                    items.Add(presentLatitude);
                    items.Add(presentLongitude);
                    items.Add(tagAddress);
                    nfcs.Add(items);
                }
            }
            return nfcs;
        }

        public Boolean removeNFCs()
        {
            Debug.WriteLine("removeNFCs invoked");
            int length = currentNumberOfNFCs();
            Boolean itemsRemoved = false;
            Debug.WriteLine(length);
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    id = i + 1;
                    appSettings.Remove(id + KEY_NFC_PRESENTLATITUDE);
                    appSettings.Remove(id + KEY_NFC_PRESENTLONGITUDE);
                    appSettings.Remove(id + KEY_NFC_TAGADDRESS);
                }
                itemsRemoved = true;
                appSettings.Remove(KEY_ID_NFC);
                appSettings.Remove(KEY_CURRENTNUMBEROFNFCS);
                appSettings.Save();
            }
            return itemsRemoved;
        }
        public Boolean createTempAlarmReport(AlarmReport alarmReport)
        {
            long id = getNextTempAlarmReportId();
            try
            {
                appSettings.Add(id + KEY_TEMP_REPORT_CUSTOMERNAME, alarmReport.CustomerName + "");
                appSettings.Add(id + KEY_TEMP_REPORT_CUSTOMERNUMBER, alarmReport.CustomerNumber + "");
                appSettings.Add(id + KEY_TEMP_REPORT_STREETANDHOUSENUMBER, alarmReport.StreetAndHouseNumber + "");
                appSettings.Add(id + KEY_TEMP_REPORT_ZIPCODE, alarmReport.ZipCode + "");
                appSettings.Add(id + KEY_TEMP_REPORT_CITY, alarmReport.City + "");
                appSettings.Add(id + KEY_TEMP_REPORT_PHONENUMBER, alarmReport.Phonenumber + "");
                appSettings.Add(id + KEY_TEMP_REPORT_DATE, alarmReport.Date + "");
                appSettings.Add(id + KEY_TEMP_REPORT_TIME, alarmReport.Time + "");
                appSettings.Add(id + KEY_TEMP_REPORT_ZONE, alarmReport.Zone + "");
                appSettings.Add(id + KEY_TEMP_REPORT_BURGLARYVANDALISM, alarmReport.BurglaryVandalism + "");
                appSettings.Add(id + KEY_TEMP_REPORT_WINDOWDOORCLOSED, alarmReport.WindowDoorClosed + "");
                appSettings.Add(id + KEY_TEMP_REPORT_APPREHENDEDPERSON, alarmReport.ApprehendedPerson + "");
                appSettings.Add(id + KEY_TEMP_REPORT_STAFFERROR, alarmReport.StaffError + "");
                appSettings.Add(id + KEY_TEMP_REPORT_NOTHINGTOREPORT, alarmReport.NothingToReport + "");
                appSettings.Add(id + KEY_TEMP_REPORT_TECHNICALERROR, alarmReport.TechnicalError + "");
                appSettings.Add(id + KEY_TEMP_REPORT_UNKNOWNREASON, alarmReport.UnknownReason + "");
                appSettings.Add(id + KEY_TEMP_REPORT_OTHER, alarmReport.Other + "");
                appSettings.Add(id + KEY_TEMP_REPORT_CANCELDURINGEMERGENCY, alarmReport.CancelDuringEmergency + "");
                appSettings.Add(id + KEY_TEMP_REPORT_COVERMADE, alarmReport.CoverMade + "");
                appSettings.Add(id + KEY_TEMP_REPORT_REMARK, alarmReport.CoverMade + "");
                appSettings.Add(id + KEY_TEMP_REPORT_NAME, alarmReport.Name + "");
                appSettings.Add(id + KEY_TEMP_REPORT_INSTALLER, alarmReport.Installer + "");
                appSettings.Add(id + KEY_TEMP_REPORT_CONTROLCENTER, alarmReport.ControlCenter + "");
                appSettings.Add(id + KEY_TEMP_REPORT_GUARDRADIOEDDATE, alarmReport.GuardRadioedDate + "");
                appSettings.Add(id + KEY_TEMP_REPORT_GUARDRADIOEDFROM, alarmReport.GuardRadioedFrom + "");
                appSettings.Add(id + KEY_TEMP_REPORT_GUARDRADIOEDTO, alarmReport.GuardRadioedTo + "");
                appSettings.Add(id + KEY_TEMP_REPORT_ARRIVEDAT, alarmReport.ArrivedAt + "");
                appSettings.Add(id + KEY_TEMP_REPORT_DONE, alarmReport.Done + "");
                appSettings.Save();
                addNumberOfTempAlarmReports();
                return true;
            }
            catch (IsolatedStorageException i)
            {
                Debug.WriteLine("" + i.Message);

                return false;
            }
        }
        public List<AlarmReport> getTempAlarmReports()
        {
            List<AlarmReport> alarmReports = new List<AlarmReport>();
            int length = currentNumberOfTempAlarmReports();

            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    id = i + 1;
                    String customerName = appSettings[id + KEY_TEMP_REPORT_CUSTOMERNAME] as String;
                    long customerNumber = Convert.ToInt64(appSettings[id + KEY_TEMP_REPORT_CUSTOMERNUMBER] as String);
                    String streetAndHouseNumber = appSettings[id + KEY_TEMP_REPORT_STREETANDHOUSENUMBER] as String;
                    int zipCode = Convert.ToInt32(appSettings[id + KEY_TEMP_REPORT_ZIPCODE] as String);
                    String city = appSettings[id + KEY_TEMP_REPORT_CITY] as String;
                    long phonenumber = Convert.ToInt64(appSettings[id + KEY_TEMP_REPORT_PHONENUMBER] as String);
                    Debug.WriteLine("date from local storage " + appSettings[id + KEY_TEMP_REPORT_DATE] as String, CultureInfo.InvariantCulture);
                    DateTime date = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_DATE] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime time = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_TIME] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    String zone = appSettings[id + KEY_TEMP_REPORT_ZONE] as String;
                    Boolean burglaryVandalism = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_BURGLARYVANDALISM] as String);
                    Boolean windowDoorClosed = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_WINDOWDOORCLOSED] as String);
                    Boolean apprehendedPerson = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_APPREHENDEDPERSON] as String);
                    Boolean staffError = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_STAFFERROR] as String);
                    Boolean nothingToReport = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_NOTHINGTOREPORT] as String);
                    Boolean technicalError = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_TECHNICALERROR] as String);
                    Boolean unknownReason = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_UNKNOWNREASON] as String);
                    Boolean other = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_OTHER] as String);
                    Boolean cancelDuringEmergency = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_CANCELDURINGEMERGENCY] as String);
                    Boolean coverMade = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_COVERMADE] as String);
                    String remark = appSettings[id + KEY_TEMP_REPORT_REMARK] as String;
                    String name = appSettings[id + KEY_TEMP_REPORT_NAME] as String;
                    String installer = appSettings[id + KEY_TEMP_REPORT_INSTALLER] as String;
                    String controlCenter = appSettings[id + KEY_TEMP_REPORT_CONTROLCENTER] as String;
                    DateTime guardRadioedDate = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_GUARDRADIOEDDATE] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime guardRadioedFrom = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_GUARDRADIOEDFROM] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime guardRadioedTo = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_GUARDRADIOEDTO] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime arrivedAt = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_ARRIVEDAT] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime done = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_DONE] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    alarmReports.Add(new AlarmReport(customerName, customerNumber, streetAndHouseNumber, zipCode, city, phonenumber, date, time, zone, burglaryVandalism,
                                            windowDoorClosed, apprehendedPerson, staffError, nothingToReport, technicalError, unknownReason, other, cancelDuringEmergency, coverMade,
                                            remark, name, installer, controlCenter, guardRadioedDate, guardRadioedFrom, guardRadioedTo, arrivedAt, done));
                }
            }

            return alarmReports;
        }

        public AlarmReport getTempAlarmReport(int id)
        {
            
            int length = currentNumberOfTempAlarmReports();
            int currentId = 0;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    currentId = i + 1;
                    if(currentId == id)
                    { 
                        String customerName = appSettings[id + KEY_TEMP_REPORT_CUSTOMERNAME] as String;
                        long customerNumber = Convert.ToInt64(appSettings[id + KEY_TEMP_REPORT_CUSTOMERNUMBER] as String);
                        String streetAndHouseNumber = appSettings[id + KEY_TEMP_REPORT_STREETANDHOUSENUMBER] as String;
                        int zipCode = Convert.ToInt32(appSettings[id + KEY_TEMP_REPORT_ZIPCODE] as String);
                        String city = appSettings[id + KEY_TEMP_REPORT_CITY] as String;
                        long phonenumber = Convert.ToInt64(appSettings[id + KEY_TEMP_REPORT_PHONENUMBER] as String);
                        Debug.WriteLine("date from local storage " + appSettings[id + KEY_TEMP_REPORT_DATE] as String, CultureInfo.InvariantCulture);
                        DateTime date = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_DATE] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime time = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_TIME] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        String zone = appSettings[id + KEY_TEMP_REPORT_ZONE] as String;
                        Boolean burglaryVandalism = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_BURGLARYVANDALISM] as String);
                        Boolean windowDoorClosed = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_WINDOWDOORCLOSED] as String);
                        Boolean apprehendedPerson = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_APPREHENDEDPERSON] as String);
                        Boolean staffError = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_STAFFERROR] as String);
                        Boolean nothingToReport = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_NOTHINGTOREPORT] as String);
                        Boolean technicalError = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_TECHNICALERROR] as String);
                        Boolean unknownReason = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_UNKNOWNREASON] as String);
                        Boolean other = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_OTHER] as String);
                        Boolean cancelDuringEmergency = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_CANCELDURINGEMERGENCY] as String);
                        Boolean coverMade = Convert.ToBoolean(appSettings[id + KEY_TEMP_REPORT_COVERMADE] as String);
                        String remark = appSettings[id + KEY_TEMP_REPORT_REMARK] as String;
                        String name = appSettings[id + KEY_TEMP_REPORT_NAME] as String;
                        String installer = appSettings[id + KEY_TEMP_REPORT_INSTALLER] as String;
                        String controlCenter = appSettings[id + KEY_TEMP_REPORT_CONTROLCENTER] as String;
                        DateTime guardRadioedDate = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_GUARDRADIOEDDATE] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime guardRadioedFrom = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_GUARDRADIOEDFROM] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime guardRadioedTo = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_GUARDRADIOEDTO] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime arrivedAt = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_ARRIVEDAT] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime done = DateTime.ParseExact(appSettings[id + KEY_TEMP_REPORT_DONE] as String, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        AlarmReport alarmReport = new AlarmReport(customerName, customerNumber, streetAndHouseNumber, zipCode, city, phonenumber, date, time, zone, burglaryVandalism,
                                                windowDoorClosed, apprehendedPerson, staffError, nothingToReport, technicalError, unknownReason, other, cancelDuringEmergency, coverMade,
                                                remark, name, installer, controlCenter, guardRadioedDate, guardRadioedFrom, guardRadioedTo, arrivedAt, done);
                        return alarmReport;
                    }
                }
            }

            return null;
        }
    }
}
