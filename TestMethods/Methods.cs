using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Задание.Models;

namespace TestMethods
{
    public class Methods
    {
        public IList<Car> Cars = new List<Car>();

        public IList<Car> GetAll()
        {
            return Cars;
        }

        public Car Add(Car car)
        {
            Cars.Add(car);
            return car;
        }

        public int CountStatistics()
        {
            return Cars.Where(x=>x.PowerReserve<7).Count();
        }


        public decimal SumPrice(decimal AvgFuelForHour,decimal Fuel , decimal PriseRent)
        {
            return Math.Round(Fuel / AvgFuelForHour * PriseRent * 60, 2);
        }

        public decimal SumPowerReserve(decimal AvgFuelForHour, decimal Fuel)
        {
            return Math.Round(Fuel / AvgFuelForHour , 2);
        }
    }
}
