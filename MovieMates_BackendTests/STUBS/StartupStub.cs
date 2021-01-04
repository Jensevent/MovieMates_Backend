namespace MovieMates_BackendTests.STUBS
{
    public static class StartupStub
    {
        static bool Testing = false;

        public static void ChangeTesting(bool newValue)
        {
            Testing = newValue;
        }
    }
}
