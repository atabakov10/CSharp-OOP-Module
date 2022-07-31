using System;
using System.Reflection.Metadata;
using NUnit.Framework;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void TestIfCtorWorksProperly()
        {
            int expectedCapacity = 20;
            Shop shop = new Shop(expectedCapacity);
            
           int actualCapacity = shop.Capacity;
            Assert.AreEqual(expectedCapacity, actualCapacity,
                "Constructor should initialize the Capacity of the Shop!");

        }
        [Test]
        public void TestIfCapacityGetterIsWorking()
        {
            int expectedCapacity = 1;
            Shop shop = new Shop(expectedCapacity);

            int actualCapacity = shop.Capacity;

            Assert.AreEqual(expectedCapacity,actualCapacity);

        }
        [TestCase(-10)]
        [TestCase(-2)]
        [TestCase(-1)]
        public void TestIfCapacitySetterIsValid(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Shop shop = new Shop(capacity);
            }, "Invalid capacity.");
        }
        [Test]
        public void TestIfAddMethodWorks()
        {
            Smartphone smartphone = new Smartphone("Nokia", 100);

            Shop shop = new Shop(10);

            shop.Add(smartphone);

        }

        [Test]
        public void TestIfPhoneCounterWorks()
        {
            Smartphone phone = new Smartphone("iPhone", 99);
            Shop shop = new Shop(20);
            shop.Add(phone);
            int actualCount = shop.Count;
            int expectedCount = actualCount;

            Assert.AreEqual(expectedCount,actualCount,
                "This count is invalid!");

        }

        [TestCase(null)]
        [TestCase("       ")]
        public void TestIfRemoveMethodWorksProperly(string modelName)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Shop shop = new Shop(10);
                Smartphone smartphone = new Smartphone(modelName , 100);
                shop.Remove(modelName);
            });
        }
        [TestCase("Samsung", 60)]
        public void TestIfMethodTestPhoneThereIsNullModelName(string modelName, int batteryUsage)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Smartphone smartphone = new Smartphone(modelName, 50);
                Shop shop = new Shop(10);
                shop.TestPhone(modelName, batteryUsage);
                smartphone.CurrentBateryCharge -= batteryUsage;
            }, $"The phone is low on batery.");
        }
        [Test]
        public void TestIfSmartphoneCtorWorks()
        {
            var expectedModel = "iPhone";
            var expectedMaxBatteryCharge = 100;
            Smartphone smartphone = new Smartphone(expectedModel, expectedMaxBatteryCharge);
            string actualModel = smartphone.ModelName;
            int actualMaxBatteryCharge = smartphone.MaximumBatteryCharge;

            Assert.AreEqual(expectedModel, actualModel,
                "The constructor does not work properly!");
        }

        [TestCase("iPhone")]
        public void ChargePhoneMethod(string modelName)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Shop shop = new Shop(20);
                shop.ChargePhone(modelName);
            });
        }

    }
}