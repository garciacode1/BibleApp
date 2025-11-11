using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleApp.Services.Responses
{
    public class ChapterResponse
    {
        public string Id { get; set; }
        public int Chapter { get; set; }
        public string Reference { get; set; }

        public override string ToString()
        {
            // This controls what appears in the CollectionView list
            return Reference ?? Id;
        }
    }
}
