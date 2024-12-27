using PalMathy.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PalMathy.Sortings
{
    public class RandomExpert
    {
        private int _minBound;
        private int _maxBound;
        private int _lengthOfArray;

        public void CheckLength()
        {
            if(LengthOfArray > _maxBound - _minBound)
            {
                LengthOfArray = _maxBound - _minBound;
            }
        }

        public int MinBound
        {
            get
            {
                return _minBound;
            }
            set
            {
                if (value < _maxBound)
                {
                    _minBound = value;
                    CheckLength();
                }                
            }
        }        
        public int MaxBound { 
            get 
            { 
                return _maxBound;
            }
            set 
            {
                if(value > _minBound)
                {
                    _maxBound = value;
                    CheckLength();
                }
            } 
        }
        public int LengthOfArray
        {
            get
            {
                return _lengthOfArray;
            }
            set
            {
                if (value <= _maxBound - _minBound)
                {
                    _lengthOfArray = value;
                }
            }
        }

        public RandomExpert()
        {
            MinBound = 0; MaxBound = 100; LengthOfArray = 100;
        }

        public RandomExpert(int min, int max)
        {
            MinBound = min; MaxBound = max; 
            LengthOfArray = 100;
        }

        public int GetRandomNumber()
        {
            return Random.Shared.Next(MinBound, MaxBound);
        }

        public ObservableCollection<int> GetRandomArray()
        {
            int step = (MaxBound - MinBound) / LengthOfArray;

            ObservableCollection<int> array = new ObservableCollection<int>();
            for (int i = MinBound; i < MaxBound; i += step)
            {
                array.Add(i);
                if(array.Count == LengthOfArray)
                {
                    break;
                }
            }
            array.Shuffle();

            return array;
        }
    }
}
