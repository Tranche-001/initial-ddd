using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FluentResults;

namespace studyRats.Service.Platform.Facade
{
    public static class Result
    {
        // Now your team uses this instead of FluentResults.Result.Fail()
        public static FluentResults.Result Fail(string message, string code)
        {
            return FluentResults.Result.Fail(new Error(message, code));
        }

        public static FluentResults.Result<T> Fail<T>(string message, string code)
        {
            return FluentResults.Result.Fail<T>(new Error(message, code));
        }

        public static FluentResults.Result Ok() => FluentResults.Result.Ok();
        public static FluentResults.Result<T> Ok<T>(T value) => FluentResults.Result.Ok(value);
    }
}
