using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AbdrakhmanovPCConfigurator.WPF
{
    public static class GetItemObject
    {
        public static TextBlock MainTitle { get; set; }
        public static Person AuthrizationPerson { get; set; }
        public static Frame FrameMenu { get; set; }
        public static Frame FrameMain { get; set; }
        public static AbdrakhmanovAPCConfiguratorEntities DB { get; } = new AbdrakhmanovAPCConfiguratorEntities();
    }
}
