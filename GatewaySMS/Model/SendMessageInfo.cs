using System;

namespace GatewaySMS.Model
{
    public class SendMessageInfo
    {
        public int PhoneNumberID { get; set; }
        public string PhoneNumberStatus { get; set; }
        public int CategoryID { get; set; }
        public string CategoryStatus { get; set; }
        public int DeviceID { get; set; }
        public int DeviceStatus { get; set; }
    }
}