using FluentAssertions;
using System;
using Xunit;
using �������.Models;

namespace UnitTests
{
    public class UnitTest1
    {
        public TestMethods.Methods Cars = new TestMethods.Methods();

        /// <summary>
        /// �������� ������ ��������� �����
        /// </summary>
        [Fact]
        public void AddElement()
        {
            var item = new Car                                      //������� ������� ������
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

            var result = Cars.Add(item);                           //�������� ������� � ������
            var allitems = Cars.GetAll();                          //������� ������

            result.Should().Be(item);                              //�������� ������ ���� �����
            allitems.Should().HaveCount(1).And.NotBeEmpty();       //������ �������� 1 �������� � �� ��������
        }

        /// <summary>
        /// �������� �������� �������� �� �����
        /// </summary>
        [Fact]
        public void DeleteButton()
        {
            var item = new Car                                     //������� ������� ������
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

            Cars.Add(item);                                        //�������� ������� � ������
            var result = Cars.GetAll();                            //������� ������ 

            result.Should().HaveCount(1).And.NotBeEmpty();         //������ ������ ���� �� ������
            result.Remove(item);                                   //������� ������� �� ������
            result.Should().HaveCount(0).And.BeEmpty();            //������ ������ �������� ������
        }

        /// <summary>
        /// �������� ��������������
        /// </summary>         
        [Fact]
        public void ChangeButton()
        {
            var itemfirst = new Car                                //������� ������� ������
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

            var itemsecond = new Car                              //������� ����� ����� �� ������� ��� ������                
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

            Cars.Add(itemfirst);                                  //��������� �� � ������
            Cars.Add(itemsecond);  

            var result = Cars.GetAll();                           //������� ������ 

            result.Should().HaveCount(2).And.NotBeEmpty();        //�������� ������ �� ������� ���������

            itemfirst.Probeg = 1;                                 //������� �������� ������� � 0 �������� ������
            result[0].Probeg.Should().NotBe(result[1].Probeg);    //��������� �������� ������� � 0 � ������ �������� ������
            
        }


        /// <summary>
        /// �������� ����� �������� ����� � ����������� ������� ����(< 7)
        /// </summary>
        [Fact]
        public void Statictics()
        {
            var itemfirst = new Car                               //������� ������ ������� ������(����������� �����)
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

            var itemsecond = new Car                              //������� ������ ������� ������(������������� �����)
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

            Cars.Add(itemfirst);                                 //��������� �������� � ������
            Cars.Add(itemsecond);

            var result = Cars.GetAll();                          //������� ������
            result.Should().HaveCount(2).And.NotBeEmpty();       //�������� ��� � ������ ��� �������� � �� �� ������
            
            var count = Cars.CountStatistics();                  //������ �� ������ ������� ����� � ����. ������� ����
            count.Should().Be(1);                                //��������� ���������� �������� � ����������(1)
        }



        /// <summary>
        /// �������� ����� �������� ����� ������
        /// </summary>
        /// <param name="AvgFuelForHour">������ ������� � ���</param>
        /// <param name="Fuel">����� �������</param>
        /// <param name="PriseRent">��������� ������ (���.)</param>
        /// <param name="result">����� ������</param>
        [Theory]
        [InlineData(2, 6, 1, 180)] 
        [InlineData(2, 2, 4, 240)] 
        //[InlineData(5, 5, 5, 45 )] false
        public void SumElements(decimal AvgFuelForHour, decimal Fuel, decimal PriseRent,decimal result)
        {
            var Sum = Cars.SumPrice(AvgFuelForHour, Fuel, PriseRent); //������� ����� �� ������
            Sum.Should().Be(result);                                    //��������� � ���������� ���������
        }



        /// <summary>
        /// �������� ����� �������� ������ ���� ����������
        /// </summary>
        /// <param name="AvgFuelForHour">������ ������� � ���</param>
        /// <param name="Fuel">����� �������</param>
        /// <param name="result">����� ����</param>
        [Theory]
        [InlineData(2, 6, 3)]
        [InlineData(2, 2, 1)]
        //[InlineData(5, 5,4 )] false
        public void SumPowerReserve(decimal AvgFuelForHour, decimal Fuel,decimal result)
        {
            var Sum = Cars.SumPowerReserve(AvgFuelForHour, Fuel); //������� ����� ����
            Sum.Should().Be(result);                              //��������� � ���������� ���������
        }

    }
}