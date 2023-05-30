using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab.Models
{
    internal static class StreamHelper
    {
        //записи в двоичном формате
        public static void outputISuppliable(ISuppliable obj, BinaryWriter bw)
        {
            obj.output(bw);
        }

        //чтение в двоичном формате
        public static ISuppliable inputISuppliable(BinaryReader br) {
            
            ISuppliable shipment;
            String name = br.ReadString();
            String flavour = br.ReadString();
            int unitPrice = br.ReadInt32();
            int emount = br.ReadInt32();
            int[] boxesInput = new int[emount];
            for (int i = 0; i < emount; i++)
                boxesInput[i] = br.ReadInt32(); ;

            if (name != "" && flavour != "")
                shipment = new ChocolateBoxes(name, flavour, boxesInput, unitPrice);
            else throw new Exception("считаны недопустимые значения полей");

            return shipment;

        }

        //запись в файл
        public static void writeISuppliable(ISuppliable obj, StreamWriter sw) { 
            obj.write(sw);
        }

        //чтение из файла объектов - поставок шоколада
        public static ISuppliable readISuppliable(StreamReader sr) {
            
            ISuppliable shipment;
            
            string[] line=(sr.ReadLine()).Split(' ');
            
            String name = line[0];
            String flavour = (line[1].Contains("-")) ? line[1].Replace("-", " ") : line[1];
            int unitPrice = Convert.ToInt32(line[2]);
            int emount = Convert.ToInt32(line[3]);
            int[] boxesInput = new int[emount];
            for (int i = 0; i < emount; i++)
            {
                boxesInput[i] = Convert.ToInt32(line[3+i]);
            }

            if (name != "" && flavour != "")
                shipment = new ChocolateBoxes(name, flavour, boxesInput, unitPrice);
            else throw new Exception("считаны недопустимые значения полей");

            return shipment;
        }

    }
}
