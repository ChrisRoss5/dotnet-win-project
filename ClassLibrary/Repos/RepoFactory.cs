using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
