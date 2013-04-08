using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIBDD_signs_parser.Model;

namespace GIBDD_signs_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryToSave = "\\images";
            string baseUrl = "http://www.gai.ru";

            try
            {
                GaiPageParser parser = new GaiPageParser(directoryToSave, baseUrl);
                parser.Parse(new Uri("http://www.gai.ru/voditelskoe-udostoverenie/dorojnyie-znaki/"));
            }
            catch
            {
                throw;
            }

        }
    }
}
