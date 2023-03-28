using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Project.Models
{
    [Serializable]
    internal class FontSettings
    {
        public int fontSize { get; set; } 
        public int razmerWidth { get; set; }
        public int shriftWidth { get; set; }
        public int sortWidth { get; set; }
        public FontFamily fontFamily { get; set; }
        public FontSettings()
        {
            fontSize = 16;
            razmerWidth = 119;
            shriftWidth = 67;
            sortWidth = 131;
            fontFamily = new FontFamily("Times New Roman"); 
        }


    }
}
