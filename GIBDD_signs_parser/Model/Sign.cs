using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace GIBDD_signs_parser.Model
{
    public class Sign
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public Uri ImageUrl { get; set; }

        //public void SaveAsync(string BasePath)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append(BasePath);
        //        sb.Append("\\");
        //        //sb.Append(Group);
        //        //sb.Append("\\");
        //        sb.Append(Name);
        //        sb.Append(".gif");

        //        string pathToSave = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + sb.ToString();

        //        using (WebClient client = new WebClient())
        //        {
        //            client.DownloadFileCompleted += client_DownloadFileCompleted;
        //            client.DownloadFileAsync(ImageUrl, pathToSave);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(string.Format("Загрузка файла {0} - {1} не удалась, ошибка: {2}", Name, ImageUrl.ToString()), e.Message);
        //    }
        //}

        //void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    Console.WriteLine(string.Format("Загружен файл {0} - {1}", Name, ImageUrl.ToString()));
        //}

        internal void Save(string basePath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                sb.Append(basePath);
                sb.Append("\\");
                sb.Append(Group);
                sb.Append("\\");

                if (!Directory.Exists(sb.ToString()))
                    Directory.CreateDirectory(sb.ToString());

                sb.Append(Name);
                sb.Append(".gif");


                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(ImageUrl, sb.ToString());
                    Console.WriteLine(string.Format("Загружен файл {0} - {1}", Name, ImageUrl.ToString()));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Загрузка файла {0} - {1} не удалась, ошибка: {2}", Name, ImageUrl.ToString()), e.Message);
            }
        }
    }
}
