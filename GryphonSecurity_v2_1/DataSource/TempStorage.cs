using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryphonSecurity_v2_1.DataSource
{
    public class TempStorage
    {
        private static IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
        private long id = 0;

        private String KEY_ID = "ID";

        private String KEY_CURRENTNUMBEROFALARMREPORTS = "CURRENTNUMBEROFALARMREPORTS";
        private String KEY_CURRENTNUMBEROFNFCS = "CURRENTNUMBEROFNFCS";

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

        private String KEY_NFC_RANGECHECK = "RANGECHECK";
        private String KEY_NFC_TAGADDRESS = "TAGADDRESS";

        private long getCurrentID()
        {
            if (!appSettings.Contains(KEY_ID))
            {
                appSettings.Add(KEY_ID, id);
                appSettings.Save();
            }
            return Convert.ToInt64(appSettings[KEY_ID] as String);
        }

        private long getNextId()
        {
            long nextId = getCurrentID() + 1;
            appSettings.Remove(KEY_ID);
            appSettings.Add(KEY_ID, nextId);
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
    }
}
