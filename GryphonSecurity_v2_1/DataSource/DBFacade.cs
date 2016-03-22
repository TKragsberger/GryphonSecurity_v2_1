using GryphonSecurity_v2_1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryphonSecurity_v2_1.DataSource
{
    class DBFacade
    {
        DummyDB connection = new DummyDB();
        LocalStorage localStorage = new LocalStorage();

        public Boolean createUser(User user)
        {
            return localStorage.createUser(user);
        }

        public User getUser()
        {
            return localStorage.getUser();
        }

        public Boolean createAlarmReport(AlarmReport alarmReport)
        {
            return connection.createAlarmReport(alarmReport);
        }
        public Boolean createTempLocalStorageAlarmReport(AlarmReport alarmReport)
        {
            return localStorage.createTempAlarmReport(alarmReport);
        }
        public String getAdress(String id)
        {
            return connection.getAddress(id);
        }

        public Boolean createAddresses()
        {
            return connection.createAddresses();
        }

        public Boolean createNFC(NFC nfc)
        {
            return connection.createNFC(nfc);
        }

        public NFC getNFC()
        {
            return connection.getNFC();
        }

        public Boolean createLocalStorageNFCs(double presentLatitude, double presentLongitude, String tagAddress)
        {
            return localStorage.createNFC(presentLatitude, presentLongitude, tagAddress);
        }

        public Boolean createLocalStorageAlarmReports(AlarmReport alarmReport)
        {
            return localStorage.createAlarmReport(alarmReport);
        }

        public List<List<String>> getLocalStorageNFCs()
        {
            return localStorage.getNFCs();
        }

        public List<AlarmReport> getLocalStorageAlarmReports()
        {
            return localStorage.getAlarmReports();
        }
        public List<AlarmReport> getLocalStorageTempAlarmReports()
        {
            return localStorage.getTempAlarmReports();
        }

        public Boolean removeLocalStorageNFCs()
        {
            return localStorage.removeNFCs();
        }

        public Boolean removeLocalStorageAlarmReports()
        {
            return localStorage.removeAlarmReports();
        }

        public int getLocalStorageNumberOfNFCs()
        {
            return localStorage.currentNumberOfNFCs();
        }

        public int getLocalStorageNumberOfAlarmReports()
        {
            return localStorage.currentNumberOfAlarmReports();
        }

    }
}
