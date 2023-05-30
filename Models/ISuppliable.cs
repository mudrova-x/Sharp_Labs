using System;
using System.Collections.Generic;
using System.IO;
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

        void output(BinaryWriter bw);
        void write(StreamWriter sw);



    }
}
