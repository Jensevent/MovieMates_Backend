namespace MovieMates_Backend.DAL.Db
{
    public class ConnHelper
    {
        readonly bool testing = Startup.Testing;

        public string GetConnName()
        {
            if (testing)
            {
                return "TestConnStr";
            }
            return "DBConnStr";
        }


    }
}
