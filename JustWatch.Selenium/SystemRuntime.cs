namespace JustWatch.Selenium
{
    public static class SystemRuntime
    {
        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
