using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.SqliteScoreApp
{
    //
    using Shaman.Runtime;

    class ShamanRepo
    {
        const string ss = "ScoreStore";
        static ShamanRepo()
        {
            System.IO.Directory.CreateDirectory(ss);
        }

        public string GetJson(string id)
        {
            if (BlobStore.Exists(System.IO.Path.Combine(ss, id)))
                return BlobStore.ReadAllText(System.IO.Path.Combine(ss, id));
            else
                return null;

        }
    }




}
