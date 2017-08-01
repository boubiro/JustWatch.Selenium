using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JustWatch.Selenium.Extensions
{
    public static class PageObjectExtensions
    {
        public static By GetElementLocator<TPage>(this TPage page, Expression<Func<TPage, IWebElement>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;

            var memberInfo = memberExpression.Member;

            var locators = CustomPageObjectMemberDecorator.CreateLocatorListPublic(memberInfo);

            return locators.FirstOrDefault();
        }

        private class CustomPageObjectMemberDecorator : DefaultPageObjectMemberDecorator
        {
            public static IEnumerable<By> CreateLocatorListPublic(MemberInfo memberInfo)
            {
                return CreateLocatorList(memberInfo);
            }
        }
    }
}
