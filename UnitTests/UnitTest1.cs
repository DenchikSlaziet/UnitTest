using FluentAssertions;
using System;
using Xunit;
using Задание.Models;

namespace UnitTests
{
    public class UnitTest1
    {
        public TestMethods.Methods Cars = new TestMethods.Methods();

        /// <summary>
        /// Проверка метода получения листа
        /// </summary>
        [Fact]
        public void AddElement()
        {
            var item = new Car                                      //создаем элемент списка
            {
                Id = Guid.NewGuid(),
                Probeg = 4000,
                Fuel = 10,
                AvgFuelForHour = 2,
                GosNumber = "aa000a000",
                Mark = MarkCars.LadaVesta,
                PriseRent = 3,
                PowerReserve =5,
                RentalAmount = 900
            };

            var result = Cars.Add(item);                           //добавляю элемент в список
            var allitems = Cars.GetAll();                          //получаю список

            result.Should().Be(item);                              //значения должны быть равны
            allitems.Should().HaveCount(1).And.NotBeEmpty();       //список содержит 1 элеменнт и он непустой
        }

        /// <summary>
        /// Проверка удаления элемента из листа
        /// </summary>
        [Fact]
        public void DeleteButton()
        {
            var item = new Car                                     //создаем элемент списка
            {
                Id = Guid.NewGuid(),
                Probeg = 4000,
                Fuel = 10,
                AvgFuelForHour = 2,
                GosNumber = "aa000a000",
                Mark = MarkCars.LadaVesta,
                PriseRent = 3,
                PowerReserve = 5,
                RentalAmount = 900
            };

            Cars.Add(item);                                        //добавляю элемент в список
            var result = Cars.GetAll();                            //получаю список 

            result.Should().HaveCount(1).And.NotBeEmpty();         //Список должен быть не пустой
            result.Remove(item);                                   //Удаляем элемент из списка
            result.Should().HaveCount(0).And.BeEmpty();            //Список должен остаться пустым
        }

        /// <summary>
        /// Проверка редактирования
        /// </summary>         
        [Fact]
        public void ChangeButton()
        {
            var itemfirst = new Car                                //создаем элемент списка
            {
                Id = Guid.NewGuid(),
                Probeg = 4000,
                Fuel = 10,
                AvgFuelForHour = 2,
                GosNumber = "aa000a000",
                Mark = MarkCars.LadaVesta,
                PriseRent = 3,
                PowerReserve = 5,
                RentalAmount = 900
            };

            var itemsecond = new Car                              //создаем точно такой же элемент как первый                
            {
                Id = Guid.NewGuid(),
                Probeg = 4000,
                Fuel = 10,
                AvgFuelForHour = 2,
                GosNumber = "aa000a000",
                Mark = MarkCars.LadaVesta,
                PriseRent = 3,
                PowerReserve = 5,
                RentalAmount = 900
            };

            Cars.Add(itemfirst);                                  //добавляем их в список
            Cars.Add(itemsecond);  

            var result = Cars.GetAll();                           //получаю список 

            result.Should().HaveCount(2).And.NotBeEmpty();        //проверяю список на наличие элементов

            itemfirst.Probeg = 1;                                 //изменяю значение пробега в 0 элементе списка
            result[0].Probeg.Should().NotBe(result[1].Probeg);    //Сравниваю значения пробега в 0 и первом элементе списка
            
        }


        /// <summary>
        /// Проверяю класс подсчета машин с критическим запасом хода(< 7)
        /// </summary>
        [Fact]
        public void Statictics()
        {
            var itemfirst = new Car                               //создаем первый элемент списка(критический запас)
            {
                Id = Guid.NewGuid(),
                Probeg = 4000,
                Fuel = 10,
                AvgFuelForHour = 2,
                GosNumber = "aa000a000",
                Mark = MarkCars.LadaVesta,
                PriseRent = 3,
                PowerReserve = 5,
                RentalAmount = 900
            };

            var itemsecond = new Car                              //создаем второй элемент списка(некритический запас)
            {
                Id = Guid.NewGuid(),
                Probeg = 4000,
                Fuel = 20,
                AvgFuelForHour = 2,
                GosNumber = "bb111b111",
                Mark = MarkCars.LadaVesta,
                PriseRent = 3,
                PowerReserve = 10,
                RentalAmount = 900
            };

            Cars.Add(itemfirst);                                 //добавляем элементы в список
            Cars.Add(itemsecond);

            var result = Cars.GetAll();                          //получаю список
            result.Should().HaveCount(2).And.NotBeEmpty();       //проверяю что в списке два элемента и он не пустой
            
            var count = Cars.CountStatistics();                  //считаю по методу сколько машин с крит. запасом хода
            count.Should().Be(1);                                //сравниваю полученное значение с правильным(1)
        }



        /// <summary>
        /// Проверяю метод подсчета суммы аренды
        /// </summary>
        /// <param name="AvgFuelForHour">Расход топлива в час</param>
        /// <param name="Fuel">Объем топлива</param>
        /// <param name="PriseRent">Стоимость аренды (мин.)</param>
        /// <param name="result">Сумма Аренды</param>
        [Theory]
        [InlineData(2, 6, 1, 180)] 
        [InlineData(2, 2, 4, 240)] 
        //[InlineData(5, 5, 5, 45 )] false
        public void SumElements(decimal AvgFuelForHour, decimal Fuel, decimal PriseRent,decimal result)
        {
            var Sum = Cars.SumPrice(AvgFuelForHour, Fuel, PriseRent); //Получаю сумму из метода
            Sum.Should().Be(result);                                    //Сравниваю с правильным значением
        }



        /// <summary>
        /// Проверяю метод подсчета запаса хода автомобиля
        /// </summary>
        /// <param name="AvgFuelForHour">Расход топлива в час</param>
        /// <param name="Fuel">Объем топлива</param>
        /// <param name="result">Запас хода</param>
        [Theory]
        [InlineData(2, 6, 3)]
        [InlineData(2, 2, 1)]
        //[InlineData(5, 5,4 )] false
        public void SumPowerReserve(decimal AvgFuelForHour, decimal Fuel,decimal result)
        {
            var Sum = Cars.SumPowerReserve(AvgFuelForHour, Fuel); //Получаю запас хода
            Sum.Should().Be(result);                              //Сравниваю с правильным значением
        }

    }
}