using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.Model
{
    public class TransportBean
    {
        private int one;
        private int two;
        private int three;
        private int four;
        private int five;
        private int six;
        private int seven;
        private int eight;
        public TransportBean()
        {
            
        }
        public TransportBean(int one, int two, int three, int four, int five,
                int six, int seven, int eight)
        {
            
            this.one = one;
            this.two = two;
            this.three = three;
            this.four = four;
            this.five = five;
            this.six = six;
            this.seven = seven;
            this.eight = eight;
        }
        public int getOne()
        {
            return one;
        }
        public void setOne(int one)
        {
            this.one = one;
        }
        public int getTwo()
        {
            return two;
        }
        public void setTwo(int two)
        {
            this.two = two;
        }
        public int getThree()
        {
            return three;
        }
        public void setThree(int three)
        {
            this.three = three;
        }
        public int getFour()
        {
            return four;
        }
        public void setFour(int four)
        {
            this.four = four;
        }
        public int getFive()
        {
            return five;
        }
        public void setFive(int five)
        {
            this.five = five;
        }
        public int getSix()
        {
            return six;
        }
        public void setSix(int six)
        {
            this.six = six;
        }
        public int getSeven()
        {
            return seven;
        }
        public void setSeven(int seven)
        {
            this.seven = seven;
        }
        public int getEight()
        {
            return eight;
        }
        public void setEight(int eight)
        {
            this.eight = eight;
        }



    }

}
