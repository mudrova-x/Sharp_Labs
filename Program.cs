using OOP_Lab.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace OOP_Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~ Добавление информации о поставках ~");
            
            Console.WriteLine("Внести данные о поставках вручную или заполнить автомтически? (0/1)");
            int variant = Convert.ToUInt16(Console.ReadLine());

            int shipmentsEmount = variant == 0 ? 1 : 6;
            if (variant == 0 )
            {
                Console.WriteLine("Введите количество поставок:");
                shipmentsEmount = Convert.ToUInt16(Console.ReadLine());
                if (shipmentsEmount == 0 ) { throw new Exception("некорректные данные"); }
            }

            ISuppliable[] shipments = new ISuppliable[shipmentsEmount];

            Console.WriteLine("\nДобавить информацию о поставках: \n Шоколада - 1 /Печенья - 2");
            int type = Convert.ToUInt16(Console.ReadLine());
            if (variant == 1)
            {
                if (type == 1)
                {

                    shipments[0] = new ChocolateBoxes("C1", "Мята", new int[] { 2, 3, 4 }, 80);
                    shipments[1] = new ChocolateBoxes("C2", "Апельсин", new int[] { 5, 3 }, 40);
                    shipments[2] = new ChocolateBoxes("C3", "Финики", new int[] { 9, 8, 9, 8, 5 }, 50);
                    shipments[3] = new ChocolateBoxes("C4", "Лесные орехи", new int[] { 3, 3 }, 325);
                    shipments[4] = new ChocolateBoxes("C5", "Морская соль", new int[] { 4, 7, 8, 6 }, 98);
                    shipments[5] = new ChocolateBoxes("C5", "Морская соль", new int[] { 4, 7, 8, 6 }, 98);
                }
                else
                {
                    shipments[0] = new BiscuitBoxes("B1", "Карамель", new int[] { 300, 420 }, 100);
                    shipments[1] = new BiscuitBoxes("B2", "Апельсин", new int[] { 100, 100, 120 }, 100);
                    shipments[2] = new BiscuitBoxes("B3", "Имбирь", new int[] { 200, 150, 300 }, 300);
                    shipments[3] = new BiscuitBoxes("B4", "Кокос", new int[] { 90, 90, 90, 120, 80 }, 100);
                    shipments[4] = new BiscuitBoxes("B5", "Изюм", new int[] { 100, 100 }, 300);
                    shipments[5] = new BiscuitBoxes("B5", "Изюм", new int[] { 100, 100 }, 300);
                }
            }
            else
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    Console.WriteLine("\n\nВведите поставщика:");
                    String producerName = Console.ReadLine();
                    Console.WriteLine("Введите вкус:");
                    String flavour = Console.ReadLine();
                    Console.WriteLine("Введите количество коробок в поставке:");
                    int boxesNum = Convert.ToUInt16(Console.ReadLine());

                    if (type == 1)
                    {
                        int[] boxesWeight = new int[boxesNum];
                        for (int j = 0; j < boxesWeight.Length; j++)
                        {
                            Console.WriteLine(string.Format("Введите вес упаковки в граммах № {0}", j + 1));
                            boxesWeight[j] = Convert.ToUInt16(Console.ReadLine());
                        }
                        Console.WriteLine("Введите цену за 100 грамм (кратную 100):");
                        int gramPrice = Convert.ToUInt16(Console.ReadLine());
                        try { 
                            shipments[i] = new BiscuitBoxes(producerName, flavour, boxesWeight, gramPrice); 
                        }catch(PriceException e)
                        {
                            Console.WriteLine("PriceException: " + e.Message);
                            Console.ReadLine();
                            return;
                        }catch(Exception e) { 
                            Console.WriteLine("Error: " + e.Message);
                            Console.ReadLine();
                            return;
                        }
                    }
                    if (type == 2)
                    {
                        int[] barsEmount = new int[boxesNum];
                        for (int j = 0; j < barsEmount.Length; j++)
                        {
                            Console.WriteLine(string.Format("Введите количество плиток в коробке № %d", j + 1));
                            barsEmount[j] = Convert.ToUInt16(Console.ReadLine());
                        }
                        Console.WriteLine("Введите цену (за плитку):");
                        int barPrice = Convert.ToUInt16(Console.ReadLine());
                        shipments[i] = new ChocolateBoxes(producerName, flavour, barsEmount, barPrice);
                    }
                    Console.Write(shipments[i]);
                }
            }

            bool key = true;
            while (key)
            {
                Console.WriteLine("\n_________________________________________________________________");
                Console.WriteLine("\nВыберите пункт меню:");
                Console.WriteLine("1 - Просмотр списка поставок");
                Console.WriteLine("2 - Поиск дубликатов в списке.");
                Console.WriteLine("3 - Запись и чтение из файла (StreamWriter/StreamReader).");
                Console.WriteLine("4 - Запись и чтение из бинарного файла (BinaryWriter/BinaryReader).");
                //
                Console.WriteLine("9 - Выход.");
                variant = Convert.ToUInt16(Console.ReadLine());

                switch (variant)
                {
                    case (1):
                        {
                            Console.WriteLine("Список поставок:");
                            foreach (ISuppliable sh in shipments)
                                Console.WriteLine(sh.ToString());
                            break;
                        }

                    case (2):
                        {
                            Console.WriteLine("Введите порядковый номер элемента для поиска дубликатов:");
                            int num  = Convert.ToUInt16(Console.ReadLine());
                            if (num > 0)
                            {
                                var sameList = shipments.Where(sh => sh.Equals(shipments[num - 1]));
                                if (sameList.ToList().Count() == 1)
                                    Console.WriteLine("Дубликатов нет");
                                else
                                { 
                                    Console.WriteLine("Дубликатов: {0}", sameList.ToList().Count() - 1); 
                                    foreach (var sh in sameList)
                                        Console.WriteLine("HashCode: " + sh.GetHashCode());
                                }

                            }
                            else Console.WriteLine("Ошибка ввода номера");
                            break;
                        }

                    case (3):
                        {
                            using (var sw = new StreamWriter("shipments.txt", false, Encoding.UTF8))
                            {
                                foreach (var sh in shipments)
                                    StreamHelper.writeISuppliable(sh, sw);
                            }

                            using (var sr = new StreamReader("shipments.txt", Encoding.UTF8))
                            {
                                Console.WriteLine("Информация из файла shipments.txt:\n" + new string('-', 24));
                                sr.ReadLine();
                                for (int i=0; i<shipments.Length; i++)
                                {
                                    ISuppliable s = StreamHelper.readISuppliable(sr);
                                    Console.WriteLine(s.ToString());
                                }
                            }
                            break;

                        }

                    case (4):
                        {
                            using (var bw = new BinaryWriter(File.Open("shipments.dat", FileMode.OpenOrCreate)))
                            {
                                foreach (var sh in shipments)
                                    StreamHelper.outputISuppliable(sh, bw);
                            }

                            using (var br = new BinaryReader(File.Open("shipments.dat", FileMode.Open)))
                            {
                                Console.WriteLine("Информация из файла shipments.dat:\n" + new string('-', 24));
                                for (int i = 0; i < shipments.Length; i++)
                                {
                                    ISuppliable s = StreamHelper.inputISuppliable(br);
                                    Console.WriteLine(s.ToString());
                                }
                            }
                            break;

                        }

                    case (9):
                        {
                            key = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Ошибка. Повторите ввод.");
                            break;
                        }
                }
        
        }   }
    }
}
