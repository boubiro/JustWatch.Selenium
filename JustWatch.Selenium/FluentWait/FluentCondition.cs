using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace JustWatch.Selenium.FluentWait
{
    public class FluentCondition
    {
        private readonly IWaitCondition currentCondition;

        private FluentCondition(IWaitCondition condition)
        {
            this.currentCondition = condition;
        }

        #region Static methods

        public static FluentCondition If(Func<IWebDriver, bool> condition)
        {
            return new FluentCondition(new CustomCondition(condition));
        }

        public static FluentCondition Not(Func<IWebDriver, bool> condition)
        {
            return new FluentCondition(new OppositeCondition(new CustomCondition(condition)));
        }

        public static FluentCondition IsNull<T>(Func<IWebDriver, T> condition) where T : class
        {
            return new FluentCondition(new IsNullCondition<T>(condition));
        }

        public static FluentCondition Throws<TException>(Delegate function) where TException : Exception
        {
            return new FluentCondition(new ExceptionCondition<TException>(function));
        }

        public static FluentCondition IsEmpty<TElement>(Func<IWebDriver, IEnumerable<TElement>> function)
        {
            return new FluentCondition(new IsEmptyCondition<TElement>(function));
        }

        #endregion

        #region Instance methods

        public FluentCondition And(Func<IWebDriver, bool> condition)
        {
            return new FluentCondition(new ConjuctionCondition(
                currentCondition, 
                new CustomCondition(condition)));
        }

        public FluentCondition Or(Func<IWebDriver, bool> condition)
        {
            return new FluentCondition(new DisjunctionCondition(
                currentCondition,
                new CustomCondition(condition)));
        }

        public FluentCondition Exlusive(Func<IWebDriver, bool> condition)
        {
            return new FluentCondition(new ExclusiveCondition(
                currentCondition,
                new CustomCondition(condition)));
        }

        #endregion

        #region Properties

        public Func<IWebDriver, bool> Condition
        {
            get
            {
                return this.currentCondition.Condition;
            }
        }

        #endregion
    }
}
