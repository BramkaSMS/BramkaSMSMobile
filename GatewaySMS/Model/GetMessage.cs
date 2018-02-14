using System;

namespace GatewaySMS.Model
{
    public class GetMessage
    {
        public int CategoryID { get; set; }
        public string CategoryMessage { get; set; }
        public int PhoneNumberID { get; set; }
        public string PhoneNumber { get; set; }
        public int DeviceID { get; set; }
        public int Interval { get; set; }
    }
}