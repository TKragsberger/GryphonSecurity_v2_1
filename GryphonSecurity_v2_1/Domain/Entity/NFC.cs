using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryphonSecurity_v2_1.Domain.Entity
{
    class NFC
    {
        private Boolean rangeCheck;
        private String tagAddress;
        private User user;

        public NFC(Boolean rangeCheck, String tagAddress, User user)
        {
            this.rangeCheck = rangeCheck;
            this.tagAddress = tagAddress;
            this.user = user;
        }

        public Boolean RangeCheck
        {
            get
            {
                return rangeCheck;
            }

            set
            {
                rangeCheck = value;
            }
        }

        public string TagAddress
        {
            get
            {
                return tagAddress;
            }

            set
            {
                tagAddress = value;
            }
        }

        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }
    }
}
