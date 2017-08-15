using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;

namespace JustWatch.Selenium.FluentWait
{
    public interface IWaitCondition
    {
        bool Condition(IWebDriver driver);
    }

    #region Unary conditions

    public class OppositeCondition : IWaitCondition
    {
        private readonly IWaitCondition innerCondition;

        public OppositeCondition(IWaitCondition condition)
        {
            this.innerCondition = condition;
        }

        public bool Condition(IWebDriver driver)
        {
            return !innerCondition.Condition(driver);
        }
    }

    #endregion

    #region Binary conditions

    public abstract class BinaryCondition : IWaitCondition
    {
        protected readonly IWaitCondition leftCondition;
        protected readonly IWaitCondition rightCondition;

        protected BinaryCondition(IWaitCondition leftCondition, IWaitCondition rightCondition)
        {
            this.leftCondition = leftCondition;
            this.rightCondition = rightCondition;
        }

        public abstract bool Condition(IWebDriver driver);
    }

    public class ConjuctionCondition : BinaryCondition
    {
        public ConjuctionCondition(IWaitCondition leftCondition, IWaitCondition rightCondition)
            : base(leftCondition, rightCondition)
        {
        }

        public override bool Condition(IWebDriver driver)
        {
            return leftCondition.Condition(driver) &&
                   rightCondition.Condition(driver);
        }
    }

    public class DisjunctionCondition : BinaryCondition
    {
        public DisjunctionCondition(IWaitCondition leftCondition, IWaitCondition rightCondition)
            : base(leftCondition, rightCondition)
        {
        }

        public override bool Condition(IWebDriver driver)
        {
            return leftCondition.Condition(driver) ||
                   rightCondition.Condition(driver);
        }
    }

    public class ExclusiveCondition : BinaryCondition
    {
        public ExclusiveCondition(IWaitCondition leftCondition, IWaitCondition rightCondition)
            : base(leftCondition, rightCondition)
        {
        }

        public override bool Condition(IWebDriver driver)
        {
            return leftCondition.Condition(driver) ^
                   rightCondition.Condition(driver);
        }
    }

    #endregion

    #region Custom conditions

    public class IsNullCondition<T> : IWaitCondition where T : class
    {
        private readonly Func<IWebDriver, T> function;

        public IsNullCondition(Func<IWebDriver, T> function)
        {
            this.function = function;
        }


        public bool Condition(IWebDriver driver)
        {
            return function(driver) == null;
        }
    }

    public class ExceptionCondition<TException> : IWaitCondition where TException : Exception
    {
        private readonly Delegate function;

        public ExceptionCondition(Delegate function)
        {
            this.function = function;
        }

        public bool Condition(IWebDriver driver)
        {
            try
            {
                function.DynamicInvoke(driver);
                return false;
            }
            catch (TargetInvocationException ex)
            {
                return ex.InnerException?.GetType() == typeof(TException);
            }
        }
    }

    public class CustomCondition : IWaitCondition
    {
        private readonly Func<IWebDriver, bool> function;

        public CustomCondition(Func<IWebDriver, bool> function)
        {
            this.function = function;
        }

        public bool Condition(IWebDriver driver)
        {
            return function(driver);
        }
    }

    public class IsEmptyCondition<TElement>: IWaitCondition
    {
        private readonly Func<IWebDriver, IEnumerable<TElement>> function;

        public IsEmptyCondition(Func<IWebDriver, IEnumerable<TElement>> function)
        {
            this.function = function;
        }

        public bool Condition(IWebDriver driver)
        {
            return !function(driver).Any();
        }
    }

    #endregion
}
