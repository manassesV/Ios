using Realms;

namespace KraftSales.Realms
{
    public class RealmContext
    {
        private Realm _context;
        private RealmConfiguration _configuration;

        static RealmContext()
        {

        }

        public Realm GetContext() => _context ?? (_context = Realm.GetInstance(GetConfiguration("kraft.realm")));

        private RealmConfiguration GetConfiguration(string databasePath)
        {
            if (_configuration == null)
            {
                _configuration = new RealmConfiguration(databasePath)
                {
                    SchemaVersion = 3,
                    ShouldDeleteIfMigrationNeeded = false
                };
            }

            return _configuration;
        }
    }
}
