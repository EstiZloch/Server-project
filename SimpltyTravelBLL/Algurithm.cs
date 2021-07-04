using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
using Newtonsoft.Json;
using Models;
namespace SimpltyTravelBLL
{
    public class Algurithm
    {
        #region data
        //
        DBConnection dbCon;
        Random random = new Random();
        //lists cf data
        List<Sites> listOfSites;
        #endregion
        public Algurithm()
        {
            dbCon = new DBConnection();
        }
        //dictionary of the time spend
        Dictionary<bool, int> misHours = new Dictionary<bool, int>()
        {
            { true, 7 },
            { false, 14 }
        };
        //arr of num of the site each type in a trip 
        double[] numOfSitesOfType;
        int numForRand;
        #region function
        //יצירת הרכב טיולים ללקוח
        //car_bus?true-car:false:bus
        public Dictionary<int, List<SiteModel>> CreateTravels(int MinAge, int MaxAge, bool Car_bus, 
            bool halfDay_allDay, int codeSubRegion,string myAddress)
        {

            if (codeSubRegion >= 0)
            {
                numForRand = 4;
                listOfSites = dbCon.GetDbSet<Sites>().Where(i => i.minAge <= MinAge && i.maxAge >= MaxAge
                && i.codeSub_Region == codeSubRegion && i.statusSite == "true").ToList();
                if (Car_bus == false)
                    listOfSites = listOfSites.Where(i => i.car_bus == false).ToList();
            }
            else
            {
                numForRand = 10;
                listOfSites = dbCon.GetDbSet<Sites>();
            }
            //dictionary of 5 options to trips
            Dictionary<int, List<SiteModel>> dictOfTrips = new Dictionary<int, List<SiteModel>>();
            //list of coffee shops 
            List<SiteModel> coffeeShops =SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel( listOfSites.Where(i => i.codeSiteKind == 2).ToList());
            //list of restaurants
            List<SiteModel> restaurants = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(i => i.codeSiteKind == 3).ToList());
            //list of tombs-קברים
            List<SiteModel> tombs = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(i => i.codeSiteKind == 1).ToList());
            //list of dry trails 
            List<SiteModel> dryTrails = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(i => i.codeSiteKind == 7 ).ToList());
            //list of wet trails 
            List<SiteModel> wetTrails = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(i => i.codeSiteKind == 6 ).ToList());
            //list of dry attractions 
            List<SiteModel> dryAttractions= SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(i => i.codeSiteKind == 5 ).ToList());
            //list of wet attractions 
            List<SiteModel> wetAttractions = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(i => i.codeSiteKind == 4).ToList());
            //list of nature reserves
            List<SiteModel> natureReserves = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(i => i.codeSiteKind == 8).ToList());
            //list of museuns
            List<SiteModel> museuns = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(i => i.codeSiteKind == 9).ToList());
            //mat to calculation number of sites from all of kind
            double[,] matPercents = new double[5, 9] { { 0,  1, 22, 22, 0,15, 0, 40, 0 }, 
                { 10,  10, 15, 15, 0,10, 0, 20, 20 }, { 0, 17, 0, 0, 0,10, 0, 28, 45 },
                { 0,  0, 17.5, 17.5, 25,15, 25, 0, 0 }, { 5,  18, 9, 9, 9,5, 9, 18, 18 } };
            //arr of the names trips kind     
            string[] arrNamesKinds = new string[5] { "טבע", "רגוע", "הסטוריה", "אתגרי", "קלאסי" };
            //arr of the list trips kind     
            List<SiteModel>[] arrListKinds = new List<SiteModel>[9] {coffeeShops,tombs,dryTrails,
                wetTrails,dryAttractions,restaurants,wetAttractions,natureReserves,museuns };
            //number of sites in a trip
            //get the average time to a site
            SiteBL s = new SiteBL();
            int avg = s.GetAvgTime();
            //local variable
            double misLeft;
            //mis sites in trip
            int numOfSiteInTrip = misHours[halfDay_allDay] / avg;
            for (int i = 0; i < 5; i++)
            {
                //number of sites per type 
                numOfSitesOfType = new double[9];
                for (int j = 0; j < 9; j++)
                {
                    numOfSitesOfType[j] =Math.Ceiling (numOfSiteInTrip * (matPercents[i, j] * 0.01) );
                }
                if(i==4)
                {
                    numOfSitesOfType[1]--;
                    numOfSitesOfType[2]--;
                    numOfSitesOfType[4]--;
                    numOfSitesOfType[8]--;
                }
                //Refresh
                  if (i > 0)
                {
                    coffeeShops = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 1).ToList());
                    restaurants = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 2).ToList());
                    tombs = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 3).ToList());
                    natureReserves = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 8).ToList());
                    museuns = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 9).ToList());
                    if (i == 1)
                    {
                        dryTrails = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 4
                        && c.extraLevel==1).ToList());
                        wetTrails = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 5 
                        && c.extraLevel == 1).ToList());
                        dryAttractions = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 6 
                        && c.extraLevel == 1).ToList());
                        wetAttractions = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 7 
                        && c.extraLevel == 1).ToList());
                    }
                    if (i == 3)
                    {
                        dryTrails = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 4 
                        && c.extraLevel == 3).ToList());
                        wetTrails = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 5 
                        && c.extraLevel == 3).ToList());
                        dryAttractions = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 6 
                        && c.extraLevel == 3).ToList());
                        wetAttractions = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 7 
                        && c.extraLevel == 3).ToList());
                    }
                    else
                    {
                        dryTrails = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 4).ToList());
                        wetTrails = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 5).ToList());
                        dryAttractions = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 6).ToList());
                        wetAttractions = SimplyTravelDAL.Converts.SiteConvert.ConvertSiteListToModel(listOfSites.Where(c => c.codeSiteKind == 7).ToList());
                    }
                    arrListKinds = new List<SiteModel>[9] {coffeeShops,tombs,dryTrails,
                wetTrails,dryAttractions,restaurants,wetAttractions,natureReserves,museuns };
                }
                misLeft = misHours[halfDay_allDay];
                dictOfTrips[i] = new List<SiteModel>();
                dictOfTrips[i] = SitesRandom(misLeft, numOfSitesOfType, arrListKinds,myAddress);
            }
            return dictOfTrips;

        }
        public List<SiteModel> SitesRandom(double misHours,double[]arrNumOfSitesToType,
            List<SiteModel>[] arrListOfSitesToType,string myAddress)
        {
            List<SiteModel> listToRandom = new List<SiteModel>();
            List<SiteModel> result = new List<SiteModel>();
            SiteModel sToRemove,siteToAdd;
            result.Add(new SiteModel() { Adress = myAddress });
         // while(misHours>0)
           // {

                for (int i = 0; i < arrNumOfSitesToType.Length; i++)
                {

                    if (arrNumOfSitesToType[i] > 0)
                        {
                       for(int k=0;k<numForRand;k++)
                        {
                            if (arrListOfSitesToType[i].Count > 0)
                            {
                                sToRemove = arrListOfSitesToType[i][random.Next(arrListOfSitesToType[i].Count)];
                                listToRandom.Add(sToRemove);
                                arrListOfSitesToType[i].Remove(sToRemove);
                            }
                            else
                                break;
                        }
                    if (listToRandom.Count > 0)
                    {
                        siteToAdd = ChooseTheShortDistance(listToRandom, result[result.Count - 1].Adress);
                        result.Add(siteToAdd);
                        misHours -= result[result.Count - 1].TimeSpend.GetValueOrDefault();
                        arrNumOfSitesToType[i]--;
                        if (arrNumOfSitesToType[i] == 0)
                            listToRandom = new List<SiteModel>();
                        else
                            listToRandom.Remove(siteToAdd);
                    }
                    else
                        arrNumOfSitesToType[i]--;

                }
                }
           // }
            return result;
        }

        private SiteModel ChooseTheShortDistance(List<SiteModel> sites,string address)
        {
            Task<SiteModel> s= GetDistance(address, sites);
            return s.Result;
        }
        public async Task<SiteModel> GetDistance(string origin, List<SiteModel> sites)
        {
            int j = 1;
            SiteModel s = sites[0];
            string[] locationUrls=new string[(sites.Count)+1];
            locationUrls[0]=BuildUrlForLocationId(origin.Split());
               string[] idLocations = new string[numForRand];
            foreach (SiteModel site in sites)
            {
                locationUrls[j++] = BuildUrlForLocationId(site.Adress.Split());
            }
            HttpClient http = new HttpClient();

            for (int i = 0; i < sites.Count; i++)
            {
                var responseId = Task.Run(()=> http.GetAsync(locationUrls[i]));

                if (responseId.Result!=null)
                {
                    var result = await responseId.Result.Content.ReadAsStringAsync();

                    RootLocationBase root = JsonConvert.DeserializeObject<RootLocationBase>(result);
                    if(root.results.Length!=0)
                    idLocations[i] = root.results[0].place_id;
                }
            }
            string url = BuildUrlForDistance(idLocations[0], idLocations[1]);
            for (int i = 2; i < sites.Count; i++)
            {
                if(idLocations[i]!=null)
                url += "|place_id:" + idLocations[i];
            }
            var responseDistance = Task.Run(() => http.GetAsync(url));

            if (responseDistance.Result!=null)
            {
                var result = responseDistance.Result.Content.ReadAsStringAsync();
                RootDistanceBase root = JsonConvert.DeserializeObject<RootDistanceBase>(result.Result);
                string directionMin = root.rows[0].elements[0].distance.text;
                directionMin = directionMin.Replace("mi", "");
                double minDistance = double.Parse(directionMin),min;
                string direction;
                
                for (int i = 1; i < sites.Count; i++)
                {

                    if (root.rows[0].elements.Count() ==i)
                        break;
                    direction = root.rows[0].elements[i].distance.text.Replace("mi","");
                    min = double.Parse(direction);
                    if (min <= minDistance)
                    {
                        minDistance = min;
                        s = sites[i];
                    }
                }
            }
            return s;
        }
        static string BuildUrlForLocationId(string[] address)
        {
            string location = "";
            string[] locationAsArray;
            locationAsArray =address;

            for (int i = 0; i < locationAsArray.Length; i++)
            {
                if (i < locationAsArray.Length - 1)
                    location += locationAsArray[i] + "+";
                else
                    location += locationAsArray[i];
            }
            return "https://maps.googleapis.com/maps/api/place/textsearch/json?key=AIzaSyCKs4oBHYDXVTUCm-mHhbu7CERTQgbEM2Y&query=" + location + "&mode=driving&units=imperial&sensor=true";
        }

        static string BuildUrlForDistance(string place1, string place2)
        {
            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?key=AIzaSyCKs4oBHYDXVTUCm-mHhbu7CERTQgbEM2Y&units=imperial&origins=";
            return url + "place_id:" + place1 + "&destinations=place_id:" + place2;
        }
        #endregion

    }
}

