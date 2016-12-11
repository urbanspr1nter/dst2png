using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace dst2png
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            dst2png.Main form = new dst2png.Main();
            Application.Run(form);
        }
    }
}
