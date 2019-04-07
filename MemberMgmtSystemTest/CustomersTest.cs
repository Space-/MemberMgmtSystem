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

        [Test]
        public void ContactName_length_zero_Invalid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = "" };
            ValidatePropertyResultShouldBe(false, customerModel);
        }

        [Test]
        public void ContactName_length_three_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = "abc" };
            ValidatePropertyResultShouldBe(true, customerModel);
        }

        [Test]
        public void ContactName_length_fifteen_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = new string('a', 15) };
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