using OpenQA.Selenium.Support.UI;
using System;

namespace JustWatch.Selenium.Extensions
{
    public static class WaitExtensions
    {
        public static bool TryToWaitUntil<T, TResult>(this IWait<T> wait, Func<T, TResult> condition)
        {
            try
            {
                wait.Until(condition);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Until<T, TResult>(this IWait<T> wait, Func<T, TResult> condition, string message)
        {
            try
            {
                wait.Until(condition);
            }
            catch (Exception ex)
            {
                throw new Exception(message, ex);
            }
        }
    }
}
