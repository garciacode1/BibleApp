using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleApp.Services
{
    public class FavoriteStore
    {
        public static List<FavoriteChapter> Favorites { get; } = new();
    }
}
