using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SettingsLib
{
    public static class BinaryTools
    {
        public static void ToBinary(List<string> categor)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Constants.categotries, FileMode.Create);
            bf.Serialize(fs, categor);
            fs.Close();
        }

        public static List<string> FromBinary()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Constants.categotries, FileMode.Open);
            List<string> outp = (List<string>)bf.Deserialize(fs);
            return outp;
        }
    }
}
