using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using MemberMgmtSystem.Models;
using NUnit.Framework;

namespace MemberMgmtSystemTest
{
    public class MemberMgmtSystemTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(false, "")]
        [TestCase(false, "ab")]
        [TestCase(true, "abc")]
        [TestCase(true, "abcd")]
        public void ContactName_length_validate(bool expected, string contactName)
        {
            // Arrange
            var customerModel = new Customers() { ContactName = contactName };
            ValidatePropertyResultShouldBe(expected, customerModel);
        }

        [Test]
        public void ContactName_length_fourteen_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = new string('a', 14) };
            ValidatePropertyResultShouldBe(true, customerModel);
        }

        [Test]
        public void ContactName_length_fifteen_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = new string('a', 15) };
            ValidatePropertyResultShouldBe(true, customerModel);
        }

        [Test]
        public void ContactName_length_sixteen_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = new string('a', 16) };
            ValidatePropertyResultShouldBe(false, customerModel);
        }

        [Test]
        public void Age_is_integer_and_greater_than_zero_Valid()
        {
            // Arrange
            var customerModel = new Customers() { Age = 16 };
            ValidatePropertyResultShouldBe(true, customerModel);
        }

        private static void ValidatePropertyResultShouldBe(bool expected, Customers customerModel)
        {
            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customerModel, new ValidationContext(customerModel),
                validationResults, validateAllProperties: true);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}