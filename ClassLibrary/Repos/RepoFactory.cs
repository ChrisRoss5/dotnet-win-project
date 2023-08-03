namespace ClassLibrary.Repo
{
    public static class RepoFactory
    {
        public static IRepo GetRepo()
        {
            return new FileRepo();
        }
    }
}
