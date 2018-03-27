using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialize.Linq.Factories;
using Serialize.Linq.Serializers;

namespace Serilaize.Linq.Tests
{
    [TestClass]
    public class ExpressionSerializationTests
    {
        [TestMethod]
        public void Func_int_string_Test()
        {
            Expression<Func<int, string>> exp = x => new string('*', x);
            var setting = new FactorySettings { UseRelaxedTypeNames = true };
            var internalSerializer = new JsonSerializer();
            var serializer = new ExpressionSerializer(internalSerializer, setting);
            var json = serializer.SerializeText(exp, setting);
            Trace.WriteLine(json);
            Expression<Func<int, string>> exp1 = (Expression<Func<int, string>>)serializer.DeserializeText(json);
            Func<int, string> f1 = exp1.Compile();
            string result = f1(4);
            Trace.WriteLine(result);
        }
    }
}
