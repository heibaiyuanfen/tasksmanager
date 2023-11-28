using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace WpfApp1
{
    public class taskinfo
    {
        public string TaskNumber { get; set; }
        public int ProcessId { get; set; }
        public Icon icon { get; set; }
    }
    public class ProcessInfo
    {
        public string TaskID { get; set; }
        public int ProcessID { get; set; }
        public ImageSource Icon { get; set; }
    }
}
