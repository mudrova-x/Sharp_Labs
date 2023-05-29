using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab.Models
{
    public class BiscuitBoxes : ISuppliable
    {
        private String producerName;
        private String biscuitFlavour;
        private int gramsPrice; 
        private int[] boxesWeight;

        public BiscuitBoxes()
        {
            this.producerName = "Default";
            this.biscuitFlavour = "Default";
            this.boxesWeight = new int[] { 100 };
            this.gramsPrice = 100;
        }

        public BiscuitBoxes(String name, String biscuitFlavour, int[] boxesWeight, int gramsPrice)
        {
            this.producerName = name;
            this.biscuitFlavour = biscuitFlavour;
            if (boxesWeight.Length == 0)
                throw new Exception("Длина списка упаковок должна быть строго положительной");
            this.boxesWeight = boxesWeight;
            if (gramsPrice <= 0 || gramsPrice % 100 != 0)
                throw new Exception("Цена должна быть быть строго положительной и кратной 100");
            this.gramsPrice = gramsPrice;
        }

        public string Producer { 
            get => producerName; 
            set => producerName=value; 
        }
        public string Flavour { 
            get => biscuitFlavour; 
            set => biscuitFlavour=value; 
        }
        public int UnitPrice { 
            get => gramsPrice; 
            set => gramsPrice=value; 
        }

        public int BoxesEmount
        {
           get => boxesWeight.Length;
        }

        public int getBoxsInput(int boxNum)
        {
            return boxesWeight[boxNum];
        }

        public int getOrderPrice()
        {
            int orderPrice = 0;

            foreach (int element in this.boxesWeight)
                orderPrice += element * gramsPrice / 100;

            if (orderPrice <= gramsPrice)
                throw new PriceException("Ошибка: Цена всей поставки должна превышать цену 100 грамм печенья.");
            
            return orderPrice;
        }

        public void setBoxsInput(int boxWeight, int boxNum)
        {
            if (boxWeight < 0) 
                throw new Exception("Ошибка: вес не может быть задан отрицательным числом.");
            
            if (boxNum < 0 || boxNum >= boxesWeight.Length) 
                throw new Exception("Ошибка: некорректный индекс.");

            this.boxesWeight[boxNum] = boxWeight;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(BiscuitBoxes))
            {
                return false;
            }

            if (this == obj)
                return true;

            BiscuitBoxes box = (BiscuitBoxes)obj;

            for (int i = 0; i < this.boxesWeight.Length; i++)
                if (this.boxesWeight[i] != box.boxesWeight[i])
                    return false;

            bool result = this.gramsPrice == box.gramsPrice
                && this.biscuitFlavour == box.biscuitFlavour
                && this.producerName == box.producerName;
            return result;
        }

        public override int GetHashCode()
        {
            int result = producerName == null ? 0 : producerName.GetHashCode();
            result += 29 * result + (biscuitFlavour == null ? 0 : biscuitFlavour.GetHashCode());
            if (boxesWeight == null)
                result = 29 * result;
            else
                foreach (int element in boxesWeight)
                    result += element;
            result = 29 * result + gramsPrice;
            return result;
        }

        public override string ToString()
        {
            string result = string.Format("Производитель - \"{0}\";" +
                        " \n Вкус - \"{1}\";" +
                        " \n Количество упаковок: {2};"
                        + " \n Цена за 100 грамм: {3}.\n"
                        + "В каждой коробке:",
                producerName, biscuitFlavour, boxesWeight.Length, gramsPrice);
            for (int i = 0; i < boxesWeight.Length; i++)
                result += string.Format("\n В коробке № {0} - {1} грамм печенья", (i + 1), boxesWeight[i]);
            return result;
        }

        public void threadInputTest(int boxeEmount, int boxNum)
        {
            throw new NotImplementedException();
        }
    }
}
