using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace AirportLibrary
{
    public class AirportLib
    {
        public string getData(string from, string to)
        {

                string uri = "http://www.flyontime.us/routes/" + from.ToUpper() + "/" + to.ToUpper() + ".xml";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse(); //This does not exit cleanly if bad request.
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                string responseString = reader.ReadToEnd();

                reader.Close();
                responseStream.Close();
                response.Close();

                return responseString;          
        }

        public bool checkIfValid(string response)
        {
            string output;
            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                reader.ReadToFollowing("on_time");
                output = reader.ReadElementContentAsString();
            }

            if (output == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string[] getValues(string response)
        {
            string[] values = new string[4];

            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                reader.ReadToFollowing("on_time");
                values[0] = "On Time: " + reader.ReadElementContentAsString() + "%";
            }

            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                reader.ReadToFollowing("short_delay");
                values[1] = "5-20 Min delay: " + reader.ReadElementContentAsString() + "%";
            }

            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                reader.ReadToFollowing("long_delay");
                values[2] = "Over 20 Min delay: " + reader.ReadElementContentAsString() + "%";
            }

            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                reader.ReadToFollowing("cancelled");
                values[3] = "Cancelled: " + reader.ReadElementContentAsString() + "%";
            }


            return values;
        }
    }
    //Powered by FlyOnTime.us
    //find airport codes at http://www.flyontime.us/airports
    //use this link for xml http://www.flyontime.us/routes/BTV/ABI.xml
}
