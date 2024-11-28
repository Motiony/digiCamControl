using CameraControl.Devices.Classes;
using CameraControl.Devices.Others;
using System.Net;


namespace CameraControl.Devices.Wifi
{

    public class PtzOpticsProvider : IWifiDeviceProvider
    {
        public string Name { get; set; }
        public string DefaultIp { get; set; }
        public DeviceDescriptor Connect(string address)
        {

            var data = GetData("http://" + address + "/cgi-bin/param.cgi?get_device_conf");
            if (!data.ContainsKey("serial_num"))
                throw new Exception("Invalid connection data");
            var camera = new PtzOpticsCamera(address)
            {
                SerialNumber = data["serial_num"],
                Manufacturer = "PTZOptics",
                DeviceName = data["devname"]
            };
            DeviceDescriptor descriptor = new DeviceDescriptor
            {
                WpdId = "PTZOptics",
                CameraDevice = camera
            };
            //cameraDevice.SerialNumber = StaticHelper.GetSerial(portableDevice.DeviceId);
            return descriptor;
        }

        public PtzOpticsProvider()
        {
            Name = "PTZOptics";
            DefaultIp = "192.168.1.1";
        }


        private Dictionary<string, string> GetData(string url)
        {
            var res = new Dictionary<string, string>();
            WebClient Client = new WebClient();
            var data = Client.DownloadString(url);
            foreach (var lines in data.Split('\n'))
            {
                if (lines?.Contains("=") == true)
                {
                    res.Add(lines.Split('=')[0].Trim(), lines.Split('=')[1].Replace("\"", ""));
                }
            }
            return res;
        }
    }
}
