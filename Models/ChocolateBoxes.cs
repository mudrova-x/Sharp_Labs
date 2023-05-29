using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab.Models
{
    public class ChocolateBoxes : ISuppliable
    {
        private String producerName;
        private String chokolateFlavour;
        private int barPrice;
        private int[] barsEmount;

        public string Producer { 
            get => producerName; 
            set => producerName=value; }
        public string Flavour { 
            get => chokolateFlavour; 
            set => chokolateFlavour=value; }
        public int UnitPrice { 
            get => barPrice; 
            set => barPrice=value; }

        public int BoxesEmount => barsEmount.Length;


        public ChocolateBoxes() {
            producerName = "Default";
            chokolateFlavour = "Default";
            barsEmount = new int[] { 10 };
            barPrice = 10;
        }

        public ChocolateBoxes(String producerName, String chokolateFlavour, int[] barsEmount, int barPrice)
        {
            this.producerName = producerName;
            this.chokolateFlavour = chokolateFlavour;
            if (barsEmount.Length == 0) 
                throw new Exception("Длина списка упаковок должна быть строго положительной");
            this.barsEmount = barsEmount;
            if (barPrice <= 0) 
                throw new Exception("Цена должна быть быть строго положительной");
            this.barPrice = barPrice;
        }

        public void setBoxsInput(int boxEmount, int boxNum)
        {
            if (boxEmount < 0) 
                throw new Exception("Ошибка: количество не может быть задано отрицательным числом.");
            if (boxNum < 0 || boxNum >= barsEmount.Length) 
                throw new Exception("Ошибка: некорректный индекс.");
            
            barsEmount[boxNum] = boxEmount;
        }

        public int getBoxsInput(int boxNum)
        {
            return barsEmount[boxNum];
        }

        public int getOrderPrice()
        {
            int orderPrice = 0;
            foreach (int element in barsEmount)
                orderPrice += element * barPrice;
            if (orderPrice <= barPrice) 
                throw new PriceException("Ошибка: Цена всей поставки должна превышать цену одной плитки шоколада.");
            return orderPrice;
        }

        public override bool Equals(object obj)
        {
            
            if (this == obj) // ссылки
                return true;

            if (obj == null || obj.GetType() != typeof(ChocolateBoxes))
            {
                return false;
            }

            ChocolateBoxes box = (ChocolateBoxes)obj;

            for (int i = 0; i < this.barsEmount.Length; i++)
                if (this.barsEmount[i] != box.barsEmount[i])
                    return false;

            bool result = this.barPrice == box.barPrice
                && this.chokolateFlavour == box.chokolateFlavour
                && this.producerName == box.producerName;
            return result;
        }

        public override int GetHashCode()
        {
            int result = producerName==null ? 0 : producerName.GetHashCode();
            result += 29 * result + (chokolateFlavour == null ? 0 : chokolateFlavour.GetHashCode());
            if (barsEmount == null)
                result = 29 * result;
            else
                foreach (int element in barsEmount)
                    result += element;
            result = 29 * result + barPrice;
            return result;
        }

        public override string ToString()
        {
            string result = string.Format("Производитель - \"{0}\"; \n " +
                "Вкус - \"{1}\"; \n " +
                "Количество упаковок: {2}; \n " +
                " Цена за плитку: {3};\n" +
                "В каждой коробке:", 
                producerName, chokolateFlavour, barsEmount.Length, barPrice);
            for (int i = 0; i < barsEmount.Length; i++)
                result += string.Format("\n В коробке № {0} - {1} плиток шоколада", (i + 1), barsEmount[i]);
            return result;
        }

        public void threadInputTest(int boxeEmount, int boxNum)
        {
            throw new NotImplementedException();
        }


    }
}
