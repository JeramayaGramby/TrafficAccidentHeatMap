using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using CrashDateHeatMap.Server.Models;

namespace CrashDateHeatMap.Server.Controllers {

    class CrashDateController
    {
        public static CrashDateSchema LoadCrashData(string zipFilePath, string csvFileName)
        {
            var crashData = new CrashDateSchema();

            using (var zip = ZipFile.OpenRead(zipFilePath))
            {
                var entry = zip.GetEntry(csvFileName);

                if (entry == null)
                {
                    throw new FileNotFoundException($"The file {csvFileName} was not found in the zip archive.");
                }

                using (var stream = entry.Open())
                using (var reader = new StreamReader(stream))
                using (var parser = new TextFieldParser(reader))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    // Skip header line
                    if (!parser.EndOfData)
                    {
                        parser.ReadLine();
                    }

                    while (!parser.EndOfData)
                    {
                        var fields = parser.ReadFields();

                        if (fields == null || fields.Length <30) {
                            continue;
                        }

                        crashData.CrashRecordNumber.Add(int.Parse(fields[0]));
                        crashData.District.Add(fields[1]);
                        crashData.CrashCounty.Add(fields[2]);
                        crashData.Municipality.Add(fields[3]);
                        crashData.CrashDate.Add(DateTime.Parse(fields[4]));
                        crashData.CrashSceneLighting.Add(byte.Parse(fields[5]));
                        crashData.Weather.Add(byte.Parse(fields[6]));
                        crashData.RoadCondition.Add(byte.Parse(fields[7]));
                        crashData.CollisionType.Add(byte.Parse(fields[8]));
                        crashData.RelationToRoad.Add(ushort.Parse(fields[9]));
                        crashData.IntersectionType.Add(byte.Parse(fields[10]));
                        crashData.TrafficControlDeviceType.Add(byte.Parse(fields[11]));
                        crashData.UrbanRural.Add(byte.Parse(fields[12]));
                        crashData.LocationType.Add(byte.Parse(fields[13]));
                        crashData.SchoolBusInvolved.Add(bool.Parse(fields[14]));
                        crashData.SchoolZone.Add(bool.Parse(fields[15]));
                        crashData.PersonCount.Add(byte.Parse(fields[16]));
                        crashData.VehicleCount.Add(byte.Parse(fields[17]));
                        crashData.FatalCount.Add(byte.Parse(fields[18]));
                        crashData.InjuryCount.Add(byte.Parse(fields[19]));
                        crashData.PedestrianCount.Add(byte.Parse(fields[20]));
                        crashData.PedestrianDeathCount.Add(byte.Parse(fields[21]));
                        crashData.PoliceReportedLatitude.Add(double.Parse(fields[22]));
                        crashData.PoliceReportedLongitude.Add(double.Parse(fields[23]));
                        crashData.SecondaryResponderLatitude.Add(double.Parse(fields[24]));
                        crashData.SecondaryResponderLongitude.Add(double.Parse(fields[25]));
                        crashData.PhantomVehicleInvolved.Add(bool.Parse(fields[26]));
                        crashData.SpeedLimit.Add(byte.Parse(fields[27]));
                        crashData.StreetName.Add(fields[28]);
                        crashData.ImpairedDriver.Add(bool.Parse(fields[29]));

                    }

                    // return the populated schema
                    return crashData;
                }
            }
        }

        // You may have to make the List type generic
        public List<CrashDateRecord> RecordProducer()
        {   
            var crashData = new CrashDateSchema();
            // If there's an error with the line below, you might have to change the logic
            // The CSV is technically is inside a compressed .gz file so the second path doesn't exist as a standalone file

            crashData = LoadCrashData("CrashDateHeatMap.Server\\Assets\\compressed_data.csv.gz", "CrashDateHeatMap.Server\\Assets\\compressed_data.csv");
            var records = crashData.CrashRecordNumber;

            var result = new List<CrashDateRecord>();
            for (int i =0; i < records.Count; i++)
            {
                result.Add(new CrashDateRecord
                {
                    CrashRecordNumber = records[i],
                    District = crashData.District[i],
                    CrashCounty = crashData.CrashCounty[i],
                    Municipality = crashData.Municipality[i],
                    CrashDate = crashData.CrashDate[i],
                    CrashSceneLighting = crashData.CrashSceneLighting[i],
                    Weather = crashData.Weather[i],
                    RoadCondition = crashData.RoadCondition[i],
                    CollisionType = crashData.CollisionType[i],
                    RelationToRoad = crashData.RelationToRoad[i],
                    IntersectionType = crashData.IntersectionType[i],
                    TrafficControlDeviceType = crashData.TrafficControlDeviceType[i],
                    UrbanRural = crashData.UrbanRural[i],
                    LocationType = crashData.LocationType[i],
                    SchoolBusInvolved = crashData.SchoolBusInvolved[i],
                    SchoolZone = crashData.SchoolZone[i],
                    PersonCount = crashData.PersonCount[i],
                    VehicleCount = crashData.VehicleCount[i],
                    FatalCount = crashData.FatalCount[i],
                    InjuryCount = crashData.InjuryCount[i],
                    PedestrianCount = crashData.PedestrianCount[i],
                    PedestrianDeathCount = crashData.PedestrianDeathCount[i],
                    PoliceReportedLatitude = crashData.PoliceReportedLatitude[i],
                    PoliceReportedLongitude = crashData.PoliceReportedLongitude[i],
                    SecondaryResponderLatitude = crashData.SecondaryResponderLatitude[i],
                    SecondaryResponderLongitude = crashData.SecondaryResponderLongitude[i],
                    PhantomVehicleInvolved = crashData.PhantomVehicleInvolved[i],
                    SpeedLimit = crashData.SpeedLimit[i],
                    StreetName = crashData.StreetName[i],
                    ImpairedDriver = crashData.ImpairedDriver[i]

                });
            }

            return result;
        }

    }
}