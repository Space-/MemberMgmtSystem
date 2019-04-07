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
        public void Age_is_integer_and_equal_to_zero_Invalid()
        {
            var customerModel = new Customers() { Age = 0 };
            ValidatePropertyResultShouldBe(false, customerModel);
        }

        [Test]
        public void Age_is_integer_and_greater_than_zero_Valid()
        {
            // Arrange
            var customerModel = new Customers() { Age = 16 };
            ValidatePropertyResultShouldBe(true, customerModel);
        }

        [Test]
        public void Age_is_not_integer_Invalid()
        {
            // Arrange
            var customerModel = new Customers() { Age = (int?)0.5 };
            ValidatePropertyResultShouldBe(false, customerModel);
        }

        [Test]
        public void Phone_format_is_Valid()
        {
            // Arrange
            var customerModel = new Customers() { Phone = "0928-123456" };
            ValidatePropertyResultShouldBe(true, customerModel);
        }

        [Test]
        public void Phone_number_total_length_too_long_Invalid()
        {
            // Arrange
            var customerModel = new Customers() { Phone = "0928-123456789123456" };
            ValidatePropertyResultShouldBe(false, customerModel);
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