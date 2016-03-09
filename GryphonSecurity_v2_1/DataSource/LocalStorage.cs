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

        private String KEY_ID_NFC = "NFCID";
        private String KEY_ID_ALARMREPORT = "ALARMREPORTID";

        private String KEY_CURRENTNUMBEROFALARMREPORTS = "CURRENTNUMBEROFALARMREPORTS";
        private String KEY_CURRENTNUMBEROFNFCS = "CURRENTNUMBEROFNFCS";

        private String KEY_REPORT_CUSTOMERNAME = "CUSTOMERNAME";
        private String KEY_REPORT_CUSTOMERNUMBER = "CUSTOMERNUMBER";
        private String KEY_REPORT_STREETANDHOUSENUMBER = "STREETANDHOUSENUMBER";
        private String KEY_REPORT_ZIPCODE = "ZIPCODE";
        private String KEY_REPORT_CITY = "CITY";
        private String KEY_REPORT_PHONENUMBER = "PHONENUMBER";
        private String KEY_REPORT_DATE = "DATE";
        private String KEY_REPORT_TIME = "TIME";
        private String KEY_REPORT_ZONE = "ZONE";
        private String KEY_REPORT_BURGLARYVANDALISM = "BURGLARYVANDALISM";
        private String KEY_REPORT_WINDOWDOORCLOSED = "WINDOWDOORCLOSED";
        private String KEY_REPORT_APPREHENDEDPERSON = "APPREHENDEDPERSON";
        private String KEY_REPORT_STAFFERROR = "STAFFERROR";
        private String KEY_REPORT_NOTHINGTOREPORT = "NOTHINGTOREPORT";
        private String KEY_REPORT_TECHNICALERROR = "TECHNICALERROR";
        private String KEY_REPORT_UNKNOWNREASON = "UNKNOWNREASON";
        private String KEY_REPORT_OTHER = "OTHER";
        private String KEY_REPORT_CANCELDURINGEMERGENCY = "CANCELDURINGEMERGENCY";
        private String KEY_REPORT_COVERMADE = "COVERMADE";
        private String KEY_REPORT_REMARK = "REMARK";
        private String KEY_REPORT_NAME = "NAME";
        private String KEY_REPORT_INSTALLER = "INSTALLER";
        private String KEY_REPORT_CONTROLCENTER = "CONTROLCENTER";
        private String KEY_REPORT_GUARDRADIOEDDATE = "GUARDRADIOEDDATE";
        private String KEY_REPORT_GUARDRADIOEDFROM = "GUARDRADIOEDFROM";
        private String KEY_REPORT_GUARDRADIOEDTO = "GUARDRADIOEDTO";
        private String KEY_REPORT_ARRIVEDAT = "ARRIVEDAT";
        private String KEY_REPORT_DONE = "DONE";

        private String KEY_NFC_RANGECHECK = "RANGECHECK";
        private String KEY_NFC_TAGADDRESS = "TAGADDRESS";


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
                appSettings.Add(KEY_ID_ALARMREPORT, id);
                appSettings.Save();
            }
            return Convert.ToInt64(appSettings[KEY_ID_ALARMREPORT] as String);
        }

        private long getNextAlarmReportId()
        {
            long nextId = getCurrentAlarmReportId() + 1;
            appSettings.Remove(KEY_ID_ALARMREPORT);
            appSettings.Add(KEY_ID_ALARMREPORT, nextId);
            appSettings.Save();
            return nextId;
        }

        public int currentNumberOfAlarmReports()
        {
            if (!appSettings.Contains(KEY_CURRENTNUMBEROFALARMREPORTS))
            {
                appSettings.Add(KEY_CURRENTNUMBEROFALARMREPORTS, 0);
                appSettings.Save();
            }
            return Convert.ToInt32(appSettings[KEY_CURRENTNUMBEROFALARMREPORTS] as String);
        }

        public void addNumberOfAlarmReports()
        {
            appSettings.Add(KEY_CURRENTNUMBEROFALARMREPORTS, currentNumberOfAlarmReports() + 1);
            appSettings.Save();
        }

        private long getCurrentNfcId()
        {
            if (!appSettings.Contains(KEY_ID_NFC))
            {
                appSettings.Add(KEY_ID_NFC, id);
                appSettings.Save();
            }
            return Convert.ToInt64(appSettings[KEY_ID_NFC] as String);
        }

        private long getNextNfcId()
        {
            long nextId = getCurrentNfcId() + 1;
            appSettings.Remove(KEY_ID_NFC);
            appSettings.Add(KEY_ID_NFC, nextId);
            appSettings.Save();
            return nextId;
        }

        public int currentNumberOfNFCs()
        {
            if (!appSettings.Contains(KEY_CURRENTNUMBEROFNFCS))
            {
                appSettings.Add(KEY_CURRENTNUMBEROFNFCS, 0);
                appSettings.Save();
            }
            return Convert.ToInt32(appSettings[KEY_CURRENTNUMBEROFNFCS] as String);
        }

        public void addNumberOfNFCs()
        {
            appSettings.Add(KEY_CURRENTNUMBEROFNFCS, currentNumberOfNFCs() + 1);
            appSettings.Save();
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
            }
            return itemsRemoved;
        }

        public Boolean createNFC(NFC nfc)
        {
            long id = getNextNfcId();
            try
            {

                appSettings.Add(id + KEY_NFC_RANGECHECK, nfc.RangeCheck);
                appSettings.Add(id + KEY_NFC_TAGADDRESS, nfc.TagAddress);

                appSettings.Save();
                return true;
            }
            catch (IsolatedStorageException)
            {
                Debug.WriteLine("Addresses did not get saved in dummyDB");
                return false;
            }

        }

        public List<NFC> getNFC()
        {
            List<NFC> nfcs = new List<NFC>();
            int length = currentNumberOfAlarmReports();
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    id = i + 1;
                    Boolean rangeCheck = Convert.ToBoolean(appSettings[id + KEY_NFC_RANGECHECK] as String);
                    String tagAddress = appSettings[id + KEY_NFC_TAGADDRESS] as String;
                    User user = getUser();
                    nfcs.Add(new NFC(rangeCheck, tagAddress, user));
                }
            }
            return nfcs;
        }

        public Boolean removeNFCs()
        {
            int length = currentNumberOfAlarmReports();
            Boolean itemsRemoved = false;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    id = i + 1;
                    appSettings.Remove(id + KEY_NFC_RANGECHECK);
                    appSettings.Remove(id + KEY_NFC_TAGADDRESS);
                }
                itemsRemoved = true;
            }
            return itemsRemoved;
        }
    }
}
