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

        public Boolean createUser(User user)
        {
            return connection.createUser(user);
        }

        public User getUser()
        {
            return connection.getUser();
        }

        public Boolean createAlarmReport(AlarmReport alarmReport)
        {
            return connection.createAlarmReport(alarmReport);
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
    }
}
