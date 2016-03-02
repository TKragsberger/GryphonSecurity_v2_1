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
    class DummyDB
    {
        private static IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
        private Boolean dummyDBStatus = false;

        private String KEY_FIRSTNAME = "FIRSTNAME";
        private String KEY_LASTNAME = "LASTNAME";
        private String KEY_ADDRESS = "ADDRESS";
        private String KEY_PHONENUMBER = "PHONENUMBER";
        private String KEY_USERNAME = "USERNAME";
        private String KEY_PASSWORD = "PASSWORD";

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
                dummyDBStatus = true;

            }
            catch (IsolatedStorageException)
            {
                dummyDBStatus = false;
            }
            return dummyDBStatus;
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

        public Boolean createAlarmReport(AlarmReport alarmReport)
        {
            if (appSettings.Contains(KEY_REPORT_CUSTOMERNAME))
            {
                appSettings.Remove(KEY_REPORT_CUSTOMERNAME);
                appSettings.Remove(KEY_REPORT_CUSTOMERNUMBER);
                appSettings.Remove(KEY_REPORT_STREETANDHOUSENUMBER);
                appSettings.Remove(KEY_REPORT_ZIPCODE);
                appSettings.Remove(KEY_REPORT_CITY);
                appSettings.Remove(KEY_REPORT_PHONENUMBER);
                appSettings.Remove(KEY_REPORT_DATE);
                appSettings.Remove(KEY_REPORT_TIME);
                appSettings.Remove(KEY_REPORT_ZONE);
                appSettings.Remove(KEY_REPORT_BURGLARYVANDALISM);
                appSettings.Remove(KEY_REPORT_WINDOWDOORCLOSED);
                appSettings.Remove(KEY_REPORT_APPREHENDEDPERSON);
                appSettings.Remove(KEY_REPORT_STAFFERROR);
                appSettings.Remove(KEY_REPORT_NOTHINGTOREPORT);
                appSettings.Remove(KEY_REPORT_TECHNICALERROR);
                appSettings.Remove(KEY_REPORT_UNKNOWNREASON);
                appSettings.Remove(KEY_REPORT_OTHER);
                appSettings.Remove(KEY_REPORT_CANCELDURINGEMERGENCY);
                appSettings.Remove(KEY_REPORT_COVERMADE);
                appSettings.Remove(KEY_REPORT_REMARK);
                appSettings.Remove(KEY_REPORT_NAME);
                appSettings.Remove(KEY_REPORT_INSTALLER);
                appSettings.Remove(KEY_REPORT_CONTROLCENTER);
                appSettings.Remove(KEY_REPORT_GUARDRADIOEDDATE);
                appSettings.Remove(KEY_REPORT_GUARDRADIOEDFROM);
                appSettings.Remove(KEY_REPORT_GUARDRADIOEDTO);
                appSettings.Remove(KEY_REPORT_ARRIVEDAT);
                appSettings.Remove(KEY_REPORT_DONE);
            }
            try
            {
                appSettings.Add(KEY_REPORT_CUSTOMERNAME, alarmReport.CustomerName);
                appSettings.Add(KEY_REPORT_CUSTOMERNUMBER, alarmReport.CustomerNumber);
                appSettings.Add(KEY_REPORT_STREETANDHOUSENUMBER, alarmReport.StreetAndHouseNumber);
                appSettings.Add(KEY_REPORT_ZIPCODE, alarmReport.ZipCode);
                appSettings.Add(KEY_REPORT_CITY, alarmReport.City);
                appSettings.Add(KEY_REPORT_PHONENUMBER, alarmReport.Phonenumber);
                appSettings.Add(KEY_REPORT_DATE, alarmReport.Date);
                appSettings.Add(KEY_REPORT_TIME, alarmReport.Time);
                appSettings.Add(KEY_REPORT_ZONE, alarmReport.Zone);
                appSettings.Add(KEY_REPORT_BURGLARYVANDALISM, alarmReport.BurglaryVandalism);
                appSettings.Add(KEY_REPORT_WINDOWDOORCLOSED, alarmReport.WindowDoorClosed);
                appSettings.Add(KEY_REPORT_APPREHENDEDPERSON, alarmReport.ApprehendedPerson);
                appSettings.Add(KEY_REPORT_STAFFERROR, alarmReport.StaffError);
                appSettings.Add(KEY_REPORT_NOTHINGTOREPORT, alarmReport.NothingToReport);
                appSettings.Add(KEY_REPORT_TECHNICALERROR, alarmReport.TechnicalError);
                appSettings.Add(KEY_REPORT_UNKNOWNREASON, alarmReport.UnknownReason);
                appSettings.Add(KEY_REPORT_OTHER, alarmReport.Other);
                appSettings.Add(KEY_REPORT_CANCELDURINGEMERGENCY, alarmReport.CancelDuringEmergency);
                appSettings.Add(KEY_REPORT_COVERMADE, alarmReport.CoverMade);
                appSettings.Add(KEY_REPORT_REMARK, alarmReport.CoverMade);
                appSettings.Add(KEY_REPORT_NAME, alarmReport.Name);
                appSettings.Add(KEY_REPORT_INSTALLER, alarmReport.Installer);
                appSettings.Add(KEY_REPORT_CONTROLCENTER, alarmReport.ControlCenter);
                appSettings.Add(KEY_REPORT_GUARDRADIOEDDATE, alarmReport.GuardRadioedDate);
                appSettings.Add(KEY_REPORT_GUARDRADIOEDFROM, alarmReport.GuardRadioedFrom);
                appSettings.Add(KEY_REPORT_GUARDRADIOEDTO, alarmReport.GuardRadioedTo);
                appSettings.Add(KEY_REPORT_ARRIVEDAT, alarmReport.ArrivedAt);
                appSettings.Add(KEY_REPORT_DONE, alarmReport.Done);
                appSettings.Save();
                dummyDBStatus = true;
            }catch
            {
                dummyDBStatus = false;
            }
            return dummyDBStatus;
        }
        
        public AlarmReport getAlarmReport()
        {
            if (appSettings.Contains(KEY_USERNAME))
            {
                String customerName = appSettings[KEY_REPORT_CUSTOMERNAME] as String;
                long customerNumber = Convert.ToInt64(appSettings[KEY_REPORT_CUSTOMERNUMBER] as String);
                String streetAndHouseNumber = appSettings[KEY_REPORT_STREETANDHOUSENUMBER] as String;
                int zipCode = Convert.ToInt32(appSettings[KEY_REPORT_ZIPCODE] as String);
                String city = appSettings[KEY_REPORT_CITY] as String;
                long phonenumber = Convert.ToInt64(appSettings[KEY_REPORT_PHONENUMBER] as String);
                DateTime date = DateTime.Parse(appSettings[KEY_REPORT_DATE] as String, CultureInfo.InvariantCulture);
                DateTime time = DateTime.Parse(appSettings[KEY_REPORT_TIME] as String, CultureInfo.InvariantCulture);
                String zone = appSettings[KEY_REPORT_ZONE] as String;
                Boolean burglaryVandalism = Convert.ToBoolean(appSettings[KEY_REPORT_BURGLARYVANDALISM] as String);
                Boolean windowDoorClosed = Convert.ToBoolean(appSettings[KEY_REPORT_WINDOWDOORCLOSED] as String);
                Boolean apprehendedPerson = Convert.ToBoolean(appSettings[KEY_REPORT_APPREHENDEDPERSON] as String);
                Boolean staffError = Convert.ToBoolean(appSettings[KEY_REPORT_STAFFERROR] as String);
                Boolean nothingToReport = Convert.ToBoolean(appSettings[KEY_REPORT_NOTHINGTOREPORT] as String);
                Boolean technicalError = Convert.ToBoolean(appSettings[KEY_REPORT_TECHNICALERROR] as String);
                Boolean unknownReason = Convert.ToBoolean(appSettings[KEY_REPORT_UNKNOWNREASON] as String);
                Boolean other = Convert.ToBoolean(appSettings[KEY_REPORT_OTHER] as String);
                Boolean cancelDuringEmergency = Convert.ToBoolean(appSettings[KEY_REPORT_CANCELDURINGEMERGENCY] as String);
                Boolean coverMade = Convert.ToBoolean(appSettings[KEY_REPORT_COVERMADE] as String);
                String remark = appSettings[KEY_REPORT_REMARK] as String;
                String name = appSettings[KEY_REPORT_NAME] as String;
                String installer = appSettings[KEY_REPORT_INSTALLER] as String;
                String controlCenter = appSettings[KEY_REPORT_CONTROLCENTER] as String;
                DateTime guardRadioedDate = DateTime.Parse(appSettings[KEY_REPORT_GUARDRADIOEDDATE] as String, CultureInfo.InvariantCulture);
                DateTime guardRadioedFrom = DateTime.Parse(appSettings[KEY_REPORT_GUARDRADIOEDFROM] as String, CultureInfo.InvariantCulture);
                DateTime guardRadioedTo = DateTime.Parse(appSettings[KEY_REPORT_GUARDRADIOEDTO] as String, CultureInfo.InvariantCulture);
                DateTime arrivedAt = DateTime.Parse(appSettings[KEY_REPORT_ARRIVEDAT] as String, CultureInfo.InvariantCulture);
                DateTime done = DateTime.Parse(appSettings[KEY_REPORT_DONE] as String, CultureInfo.InvariantCulture);
                return new AlarmReport(customerName, customerNumber, streetAndHouseNumber, zipCode, city, phonenumber, date, time, zone, burglaryVandalism,
                                        windowDoorClosed, apprehendedPerson, staffError, nothingToReport, technicalError, unknownReason, other, cancelDuringEmergency, coverMade,
                                        remark, name, installer, controlCenter, guardRadioedDate, guardRadioedFrom, guardRadioedTo, arrivedAt, done);
            }
            else
            {
                return null;
            }
        }
    }
}
