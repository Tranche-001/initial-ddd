using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace studyRats.Service.Platform.Domain.ValueObjects
{
    public static class ErrorExtensions
    {
        //When you write this IError error in the method signature,
        //you are telling the C# compiler:
        //"Hey, whenever you see a variable of type IError,
        //I want you to pretend this method belongs to it."
        public static string Serialize(this IError error)
        {
            // If it's our custom Error, use its logic
            if (error is Error domainError)
                return domainError.Serialize();

            // Otherwise, throw new error;
            throw new Exception("Serialization exception");
        }
    }
    public static class ResultExtensions
    {
        /// <summary>
        /// Returns the first error as our custom Domain Error class.
        /// Returns null if there are no errors or if the first error is not our type.
        /// </summary>
        public static Error Error(this ResultBase result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot access Error on a successful result.");

            // 1. Grab the first IError from the collection
            var firstError = result.Errors.FirstOrDefault();

            // 2. Safely cast it to your custom Error class
            // This 'as' will return null if the cast fails instead of crashing
            var newError = firstError as Error;
            if (newError is null) throw new NullReferenceException("Problem converting Errors. ErrorExtension.");

            return newError;
        }
    }
}
