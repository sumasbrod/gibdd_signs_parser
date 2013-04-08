using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GIBDD_signs_parser.Model
{
    public class GaiPageParser
    {
        //public  IEnumerable<Uri> SubPageList
        //{
        //    get 
        //    {
        //        return _subPageList;
        //    }
        //}

        //private List<Uri> _subPageList;

        public IEnumerable<Sign> SignsList
        {
            get
            {
                return _signsList;
            }
        }

        private List<Sign> _signsList;

        private string _directoryToSave { get; set; }

        private string _baseUrl { get; set; }

        public GaiPageParser(string directoryToSave, string baseUrl)
        {
            _signsList = new List<Sign>();
            _directoryToSave = directoryToSave;
            _baseUrl = baseUrl;
        }

        public void Parse(Uri baseUrl)
        {

            try
            {
                IEnumerable<Uri> subPageList = ParseBasePage(baseUrl);

                foreach (Uri url in subPageList)
                {
                    ParseSubPage(url);
                }

                if (_signsList.Count > 0)
                {
                    foreach (var sign in _signsList)
                    {
                        sign.Save(_directoryToSave);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<Uri> ParseBasePage(Uri basePageUrl)
        {
            List<Uri> subPageList = new List<Uri>();
            string pageFilePath = GetPageFile(basePageUrl);

            HtmlDocument doc = new HtmlDocument();
            doc.Load(pageFilePath);

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//div[@class='simple-format']/p/ul/li/a[@href]"))
            {
                subPageList.Add(new Uri(_baseUrl + link.Attributes["href"].Value));
            }

            return subPageList;
        }

        private static string GetPageFile(Uri basePageUrl)
        {
            string tempBasePath = Path.GetTempFileName();
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(basePageUrl, tempBasePath);
            }
            return tempBasePath;
        }

        private void ParseSubPage(Uri pageUrl)
        {
            string subPageFilePath = GetPageFile(pageUrl);
            
            HtmlDocument doc = new HtmlDocument();
            doc.Load(subPageFilePath, Encoding.UTF8);

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//div[@class='ri-item']/a/img"))
            {
                string[] imageAltStr = link.Attributes["alt"].Value.Split('-');

                _signsList.Add(new Sign
                {
                    Name = imageAltStr[0].Trim(' '),
                    Group = imageAltStr[1].Trim(' '),
                    ImageUrl = new Uri(_baseUrl + link.Attributes["src"].Value)
                });
            }
        }

    }


}
