using GryphonSecurity_v2_1.DataSource;
using GryphonSecurity_v2_1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryphonSecurity_v2_1
{
    class Controller
    {
        private DBFacade dBFacade;
        Boolean startup = true;


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
        public void login(String userName, String password)
        {
            dBFacade.login(userName, password);
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
    }
}
