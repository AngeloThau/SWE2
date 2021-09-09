using Newtonsoft.Json.Linq;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.BusinessLayer
{
    public class HTTPResponseHandler
    {
        private JObject jsonData;

        public void setJson(string json)
        {
            this.jsonData = JObject.Parse(json);
        }

        public MapInfos GetImgData()
        {
            MapInfos mapInfo = new MapInfos();
            mapInfo.sessionId = jsonData["route"]["sessionId"].ToString();
            mapInfo.boundingBox = new List<string>();
            mapInfo.boundingBox.Add(jsonData["route"]["boundingBox"]["ul"]["lat"].ToString());
            mapInfo.boundingBox.Add(jsonData["route"]["boundingBox"]["ul"]["lng"].ToString());
            mapInfo.boundingBox.Add(jsonData["route"]["boundingBox"]["lr"]["lat"].ToString());
            mapInfo.boundingBox.Add(jsonData["route"]["boundingBox"]["lr"]["lng"].ToString());

            for (int i = 0; i < mapInfo.boundingBox.Count; i++)
            {
                mapInfo.boundingBox[i] = mapInfo.boundingBox[i].Replace(",", ".");

            }

            mapInfo.distance = jsonData["route"]["distance"].ToString();

            return mapInfo;
        }

    }
}
