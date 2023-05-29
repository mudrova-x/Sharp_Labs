using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab.Models
{
    internal interface ISuppliable
    {
        String Producer { get; set; }
        String Flavour { get; set; }
        int UnitPrice { get; set; }
       int BoxesEmount { get;  }

        void setBoxsInput(int boxEmount, int boxNum);
        int getBoxsInput(int boxNum);
        

        int getOrderPrice(); //за всю поставку со всеми в ней коробками

        //void output(OutputStream out) throws IOException;
        //void write(Writer out) throws IOException;

        void threadInputTest(int boxeEmount, int boxNum);
    }
}
