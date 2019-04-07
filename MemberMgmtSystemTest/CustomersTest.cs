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
        public void ContactName_length_three_Valid()
        {
            // Arrange
            var customerModel = new Customers() { ContactName = "abc" };

            // Act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(customerModel, new ValidationContext(customerModel),
                validationResults, validateAllProperties: true);

            // Assert
            Assert.True(actual);
        }
    }
}