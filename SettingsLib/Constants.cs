using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLib
{
    public static class Constants
    {
        public static string categotries
        {
            get
            {
                return Environment.CurrentDirectory + "\\categories.dat";
            }
        }
        public static string cyrilic
        {
            get
            {
                return "^[А-Яа-яёЁЇїІіЄєҐґ]+$";
            }
        }
        public static string savefile
        {
            get
            {
                return Environment.CurrentDirectory + "\\settings.xml";
            }
        }
    }
}
