using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleApp.Pages;

namespace BibleApp.Services
{
    public static class BibleVersions
    {
        public static List<BibleVersion> All = new()
        {

           new BibleVersion
           {
              Name = "Kig James (KJV)",
              Id = "de4e12af7f28f599-01"

           },

           new BibleVersion
           {
             Name = "World English Bible (WEB)",
             Id = "9879dbb7cfe39e4d-01"

           },
           new BibleVersion
           {
             Name = "American Standard Version (ASV)",
             Id = "06125adad2d5898a-01"


           }



        };



    }
}
