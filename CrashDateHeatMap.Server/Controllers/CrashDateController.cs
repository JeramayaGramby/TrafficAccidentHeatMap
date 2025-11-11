// After the basic using statements, don't forget to include the using statement for the Models namespace. 

using System;
using System.Collections.Generic;
using System.Linq;
using CrashDateHeatMap.Server.Models;

namespace CrashDateHeatMap.Server.Controllers {

    class CrashDateController {
        void CrashDateLists() {
            List<int> CrashRecordNumberList = new List<int>();
            List<string> DistrictList = new List<string>();
            List<string> CrashCountyList = new List<string>();
            List<string> MunicipalityList = new List<string>();
            List<DateTime> CrashDateList = new List<DateTime>();
            List<byte> CrashSceneLightingList = new List<byte>();
            List<byte> WeatherList = new List<byte>();
            List<byte> RoadConditionList = new List<byte>();
            List<byte> CollisionTypeList = new List<byte>();
            List<ushort> RelationToRoadList = new List<ushort>();
            List<byte> IntersectionTypeList = new List<byte>();
            List<byte> TrafficControlDeviceTypeList = new List<byte>();
            List<byte> UrbanRuralList = new List<byte>();
            List<byte> LocationTypeList = new List<byte>();
            List<bool> SchoolBusInvolvedList = new List<bool>();
            List<bool> SchoolZoneList = new List<bool>();
            List<byte> PersonCountList = new List<byte>();
            List<byte> VehicleCountList = new List<byte>();
            List<byte> FatalCountList = new List<byte>();
            List<byte> InjuryCountList = new List<byte>();
            List<byte> PedestrianCountList = new List<byte>();
            List<byte> PedestrianDeathCountList = new List<byte>();
            List<double> PoliceReportedLatitudeList = new List<double>();
            List<double> PoliceReportedLongitudeList = new List<double>();
            List<double> SecondaryResponderLatitudeList = new List<double>();
            List<double> SecondaryResponderLongitudeList = new List<double>();
            List<bool> PhantomVehicleInvolvedList = new List<bool>();
            List<byte> SpeedLimitList = new List<byte>();
            List<string> StreetNameList = new List<string>();
            List<bool> ImpairedDriverList = new List<bool>();

        }

        void ExtractCrashDateData()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader("CrashData.csv");

            var crashDateLists = new CrashDateController();
            crashDateLists.CrashDateLists();

        }
            CrashDateSchema crashDateSchema = new CrashDateSchema() {
                
            };
        }
    }