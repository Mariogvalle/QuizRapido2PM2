namespace QuizRapido2PM2
{
    public partial class App : Application
    {
        static Controllers.AutoresControllers dbautors;

        public static Controllers.AutoresControllers DataBase
        {
            get
            {
                if (dbautors == null)
                {
                    dbautors = new Controllers.AutoresControllers();
                }
                return dbautors;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.PageListAutor());
        }
    }
}
