using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Entity
{
    public static class ModelExtension
    {
        public static string GetHomeTeam(this Smarkets.Entity.Match src) => src.Key.Split('_')[0];

        public static string GetAwayTeam(this Smarkets.Entity.Match src) => src.Key.Split('_')[1];
    }
}
