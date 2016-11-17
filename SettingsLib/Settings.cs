using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SettingsLib
{
    public class Settings
    {
        public class Setts
        {
            [XmlElement("Categories")]
            public List<Sets> sets { get; set; }
            [XmlElement("Input")]
            public string inputfolder { get; set; }
            [XmlElement("Logging")]
            public bool log { get; set; }
            [XmlElement("LicenseKey")]
            public string lic { get; set; }
         /*   [XmlElement("TestVersion")]
            public string tst_ver { get; set; }*/
        }
        public class Sets
        {
            [XmlAttribute("Category")]
            public string category { get; set; }
            [XmlElement("Extentions")]
            public string extentions { get; set; }
            [XmlElement("Path")]
            public string path { get; set; }

        }

        public class category
        {
           public void serialize(Setts sets)
            {
                XmlSerializer ser = new XmlSerializer(typeof(Setts));
                using (TextWriter tw = new StreamWriter(Constants.savefile))
                {
                    ser.Serialize(tw, sets);
                }
            }
            public Setts deserialize()
            {
                XmlSerializer ser = new XmlSerializer(typeof(Setts));
                TextReader tr = new StreamReader(Constants.savefile);
                object obj = ser.Deserialize(tr);
                Setts sets = (Setts)obj;
                return sets;
            }
        }
    }
}
