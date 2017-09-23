using Microsoft.AspNetCore.Mvc;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;
using System.Threading;

namespace aspnet.webapi.rpi.gpio.Controllers
{
    [Route("api/[controller]")]
    public class BlinkyController : Controller
    {
        [HttpPost]
        public void Post([FromBody]bool isOn)
        {
            //Control GPIO pin 5
            var pin = Pi.Gpio.Pin05;
            pin.PinMode = GpioPinDriveMode.Output;
            pin.Write(!isOn);

            pin = Pi.Gpio.Pin06;
            pin.PinMode = GpioPinDriveMode.Output;
            for (int i = 1; i < 5; i++)
            {
                pin.Write(!isOn);
                Thread.Sleep(300);
                pin.Write(isOn);
                Thread.Sleep(200);
            }

            pin = Pi.Gpio.Pin13;
            pin.PinMode = GpioPinDriveMode.Output;
            for (int i = 1; i < 50; i++)
            {
                pin.Write(!isOn);
                Thread.Sleep(300);
                pin.Write(isOn);
                Thread.Sleep(200);
            }
        }
    }
}
