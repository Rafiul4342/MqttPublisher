using MqttClientSouthboundSimulatedvalues;

using Newtonsoft.Json;
using System;
using System.Security.Cryptography.X509Certificates;
using uPLibrary.Networking.M2Mqtt;

class Program
{

    // Main Method
   static MqttClient client ;
    static public void Main(String[] args)
    {
        // create client instance and connect to publicly hosted broker
       client = new uPLibrary.Networking.M2Mqtt.MqttClient("test.mosquitto.org");
       string clientId = Guid.NewGuid().ToString();
      client.Connect(clientId);
       String topic = "MacnineData/ID-0000";

        RandomNumberGernarator rn = new RandomNumberGernarator();
        
        while (true)
        {
            double Tval = rn.Getrandomvalues(10,35);

            string Temp = String.Format("{0:0.00}", Tval);

            double Hval = rn.Getrandomvalueshumidity(22, 50);
            String Humidity = String.Format("{0:0.00}", Hval);

            double Spindlespeed = rn.GetSpindlespeed(0, 50);
            String speed = String.Format("{0:0.00}", Spindlespeed);

            var jsonObject = new
            {
                Temperature = Temp,
                Humidity = Humidity,
                Speed = speed,
                Timestamp = DateTime.Now.ToString()
            };

            // convert JSON object to string
            string data = JsonConvert.SerializeObject(jsonObject);

            // print JSON string

            Console.WriteLine("Temperature : " + Temp);
            Console.WriteLine("Humidity : " + Humidity);
            Console.WriteLine("Spindle Speed : " + speed);
            Console.WriteLine("Timestamp : " + jsonObject.Timestamp);           
            client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(data));

           Console.WriteLine("Data published to topic: " + topic + " with payload: " + data);
           // Console.ReadLine();
            System.Threading.Thread.Sleep(1000);

            

        }
    }
}