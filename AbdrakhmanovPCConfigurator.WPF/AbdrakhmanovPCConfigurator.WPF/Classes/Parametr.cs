using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AbdrakhmanovPCConfigurator.WPF.Classes
{
    public class Parametr
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public byte[] imageInByte { get; set; }

        public BitmapSource GetBitmapSource()
        {
            return (BitmapSource)new ImageSourceConverter().ConvertFrom(imageInByte);
        }
    }
}
