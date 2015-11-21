using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Roggle.Core
{
    public static class RoggleHelper
    {
        public static void Retry(Action action, int retryInterval = 1000, int retryCount = 3)
        {
            Retry<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount);
        }

        public static TReturn Retry<TReturn>(Func<TReturn> action, int retryInterval = 1000, int retryCount = 3)
        {
            List<Exception> exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    if (retry > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }

                    return action();
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            throw new AggregateException(exceptions);
        }

        public static string GetDisplayValue<TEnum>(TEnum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(TEnum), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
    }
}
