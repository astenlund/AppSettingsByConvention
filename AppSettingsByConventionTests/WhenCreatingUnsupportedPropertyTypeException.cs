using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AppSettingsByConvention;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests
{
    [TestFixture]
    public class WhenCreatingUnsupportedPropertyTypeException
    {
        [Test]
        public void AllExpectedConstructorsShouldWork()
        {
            var constructorCalls = new List<Action>
            {
                // ReSharper disable ObjectCreationAsStatement
                () => new UnsupportedPropertyTypeException(),
                () => new UnsupportedPropertyTypeException(typeof(object)),
                () => new UnsupportedPropertyTypeException("Message"),
                () => new UnsupportedPropertyTypeException("Message", new Exception("Inner exception"))
                // ReSharper restore ObjectCreationAsStatement
            };
            foreach (var constructorCall in constructorCalls)
            {
                constructorCall.ShouldNotThrow();
            }
        }

        [Test]
        public void ShouldSerializeAndDeserializeCorrectly()
        {
            var originalException = new UnsupportedPropertyTypeException("Message");

            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File));
                formatter.Serialize(memoryStream, originalException);
                memoryStream.Position = 0;
                var deserializedException = (UnsupportedPropertyTypeException)formatter.Deserialize(memoryStream);
                originalException.ShouldBeEquivalentTo(deserializedException);
            }
        }
    }
}