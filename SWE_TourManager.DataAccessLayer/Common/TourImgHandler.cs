using iText.Layout.Element;
using iText.Layout.Properties;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer
{
    public class TourImgHandler
    {

        private string imgFolderPath;
        private string imgUrl;

        public TourImgHandler()
        {
            this.imgFolderPath = ConfigurationManager.AppSettings["ImgFolderPath"].ToString();
            this.imgUrl = ConfigurationManager.AppSettings["ImgUrl"].ToString();
            System.IO.Directory.CreateDirectory(imgFolderPath);
        }

        public string GetImgPath(MapInfos mapInfo, string tourname)
        {
            string imgPath = imgFolderPath + "\\" + tourname + ".png";
            string finalUrl = imgUrl + "session=" + mapInfo.sessionId + "&boundingBox=" + mapInfo.boundingBox[0].ToString()+ "," + mapInfo.boundingBox[1].ToString() + "," + mapInfo.boundingBox[2].ToString() + "," + mapInfo.boundingBox[3].ToString();

            downLoadFile(finalUrl, imgPath);
            return imgPath;
        }

        private void downLoadFile(string finalUrl, string imgPath)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(finalUrl, imgPath);
            }
        }

        public Image GetTourImage(string tourname)
        {
            string imgPath = imgFolderPath + "\\" + tourname + ".png";
            Image img = new Image(iText.IO.Image.ImageDataFactory
                .Create(imgPath))
                .SetTextAlignment(TextAlignment.CENTER);
            return img;
        }
    }
}
