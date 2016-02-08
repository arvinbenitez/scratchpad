using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.DynamicProxy;
using log4net;
using Nautilo.Core.Extensions;

namespace Nautilo.Core.Logging
{
    public class LoggingInterceptor : IInterceptor
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Intercept(IInvocation invocation)
        {
            try
            {
                LogStart(invocation);
                invocation.Proceed();
                LogComplete(invocation);
            }
            catch (Exception ex)
            {
                LogError(invocation, ex);
                throw;
            }
        }

        private void LogComplete(IInvocation invocation)
        {
            Log.Info(GetLogEntry(invocation, true));
        }

        private void LogError(IInvocation invocation, Exception exception)
        {
            var message = GetLogEntry(invocation);
            Log.Error(message, exception);
        }

        private void LogStart(IInvocation invocation)
        {
            Log.Info(GetLogEntry(invocation));
        }

        private string GetParameters(IInvocation invocation)
        {
            var dictionary = new Dictionary<string, object>();
            var parameters = invocation.Method.GetParameters();
            for (int index = 0; index < parameters.Length; ++index)
                dictionary.Add(parameters[index].Name, invocation.Arguments[index]);
            return dictionary.AsJson();
        }

        private string GetLogEntry(IInvocation invocation, bool result = false)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("Method", invocation.Method.Name);
            dictionary.Add("Class", invocation.Method.ReflectedType.FullName);
            dictionary.Add("Parameters", GetParameters(invocation));
            if (result)
            {
                dictionary.Add("Result", invocation.ReturnValue);
            }
            return dictionary.AsJson();
        }
    }
}