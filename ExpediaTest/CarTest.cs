using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod()]
        public void TestThatCarDoesGetLocationFromTheDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();

            String location = "Terre Haute, IN";
            String otherLocation = "Shelbyville, IN";

            Expect.Call(mockDB.getCarLocation(12)).Return(location);
            Expect.Call(mockDB.getCarLocation(20)).Return(otherLocation);

            mocks.ReplayAll();

            Car target = new Car(10);
            target.Database = mockDB;

            String result = target.getCarLocation(12);
            Assert.AreEqual(location, result);

            result = target.getCarLocation(20);
            Assert.AreEqual(otherLocation, result);

            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TestThatCarDoesGetMileageFromTheDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();

            Expect.Call(mockDB.Miles).PropertyBehavior();

            mocks.ReplayAll();

            Int32 miles = 250;
            mockDB.Miles = miles;

            var target = new Car(10);
            target.Database = mockDB;

            int mileage = target.Mileage;

            Assert.AreEqual(mileage, miles);

            mocks.VerifyAll();
        }


        [TestMethod]
        public void TestObjectMotherHasCorrectBasePriceForTenDays()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual(80, target.getBasePrice());
        }
         
	}
}
