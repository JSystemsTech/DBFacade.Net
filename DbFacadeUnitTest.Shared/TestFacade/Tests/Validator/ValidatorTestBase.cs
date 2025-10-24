using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class ValidatorTestBase: UnitTestBase
    {
        protected void IsValid(IValidationResult result)
        {
            Assert.AreEqual(true, result.IsValid);
        }
        protected void IsInvalid(IValidationResult result)
        {
            Assert.AreEqual(false, result.IsValid);
        }
        protected void HasCorrectErrorCount(IValidationResult result, int expectedErrorCount)
        {
            Assert.AreEqual(expectedErrorCount, result.Errors.Count());
        }



        protected async Task IsValidAsync(IValidationResult result)
        {            
            Assert.AreEqual(true, result.IsValid);
            await Task.CompletedTask;
        }
        protected async Task IsInvalidAsync(IValidationResult result)
        {
            Assert.AreEqual(false, result.IsValid);
            await Task.CompletedTask;
        }
        protected async Task HasCorrectErrorCountAsync(IValidationResult result, int expectedErrorCount)
        {
            Assert.AreEqual(expectedErrorCount, result.Errors.Count());
            await Task.CompletedTask;
        }
        protected void IsValid(Task<IValidationResult> task)
        {
            task.Wait();
            Assert.AreEqual(true, task.Result.IsValid);
        }
        protected void IsInvalid(Task<IValidationResult> task)
        {
            task.Wait();
            Assert.AreEqual(false, task.Result.IsValid);
        }
        protected void HasCorrectErrorCount(Task<IValidationResult> task, int expectedErrorCount)
        {
            task.Wait();
            Assert.AreEqual(expectedErrorCount, task.Result.Errors.Count());
        }

        internal Validator<T> MakeValidator<T>(Action<IValidator<T>> initializer)
        => ValidatorFactory.Create(initializer);
    }
}
