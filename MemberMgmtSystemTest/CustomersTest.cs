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
            ValidatePropertyResultShouldBe(expected, customerModel, "ContactName", customerModel.ContactName);
        }

        [Test]
        public void ContactName_length_fourteen_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = new string('a', 14) };
            ValidatePropertyResultShouldBe(true, customerModel, "ContactName", customerModel.ContactName);
        }

        [Test]
        public void ContactName_length_fifteen_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = new string('a', 15) };
            ValidatePropertyResultShouldBe(true, customerModel, "ContactName", customerModel.ContactName);
        }

        [Test]
        public void ContactName_length_sixteen_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = new string('a', 16) };
            ValidatePropertyResultShouldBe(false, customerModel, "ContactName", customerModel.ContactName);
        }

        [Test]
        public void Age_is_integer_and_equal_to_zero_Invalid()
        {
            var customerModel = new Customers() { Age = 0 };
            ValidatePropertyResultShouldBe(false, customerModel, "Age", customerModel.Age);
        }

        [Test]
        public void Age_is_integer_and_greater_than_zero_Valid()
        {
            // Arrange
            var customerModel = new Customers() { Age = 16 };
            ValidatePropertyResultShouldBe(true, customerModel, "Age", customerModel.Age);
        }

        //        [Test]
        //        public void Age_is_not_integer_Invalid()
        //        {
        //            // Arrange
        //            var customerModel = new Customers() { Age = (int?)0.5 };
        //            ValidatePropertyResultShouldBe(false, customerModel, "Age", customerModel.Age);
        //        }

        [TestCase(true, "0928-123456")]
        [TestCase(false, "0928-123456789123456")]
        [TestCase(false, "0928123-456")]
        public void Phone_format_validate(bool expected, string phone)
        {
            // Arrange
            var customerModel = new Customers() { Phone = phone };
            ValidatePropertyResultShouldBe(expected, customerModel, "Phone", customerModel.Phone);
        }

        private static void ValidatePropertyResultShouldBe(bool expected, Customers customerModel, string propertyName,
            object toValidatedPropertyValue)
        {
            var validationResults = new List<ValidationResult>();
            var attributes = typeof(Customers)
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .OfType<ValidationAttribute>()
                .ToArray();

            var actual = Validator.TryValidateValue(toValidatedPropertyValue, new ValidationContext(customerModel), validationResults, attributes);
            var errorMessage = actual ? string.Empty : validationResults.FirstOrDefault<ValidationResult>()?.ErrorMessage;

            // Assert
            Assert.AreEqual(expected, actual, errorMessage);
        }
    }
}