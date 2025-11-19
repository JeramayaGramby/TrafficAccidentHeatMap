using System;
using System.Collections.Generic;

namespace CrashDateHeatMap.Server.Models
{
    public struct CrashDateSchema
    {
        public List<int> CrashRecordNumber;
        public List<string?> District;
        public List<string?> CrashCounty;
        public List<string?> Municipality;
        public List<DateTime> CrashDate;
        public List<byte> CrashSceneLighting;
        public List<byte> Weather;
        public List<byte> RoadCondition;
        public List<byte> CollisionType;
        public List<ushort> RelationToRoad;
        public List<byte> IntersectionType;
        public List<byte> TrafficControlDeviceType;
        public List<byte> UrbanRural;
        public List<byte> LocationType;
        public List<bool> SchoolBusInvolved;
        public List<bool> SchoolZone;
        public List<byte> PersonCount;
        public List<byte> VehicleCount;
        public List<byte> FatalCount;
        public List<byte> InjuryCount;
        public List<byte> PedestrianCount;
        public List<byte> PedestrianDeathCount;
        public List<double> PoliceReportedLatitude;
        public List<double> PoliceReportedLongitude;
        public List<double> SecondaryResponderLatitude;
        public List<double> SecondaryResponderLongitude;
        public List<bool> PhantomVehicleInvolved;
        public List<byte> SpeedLimit;
        public List<string?> StreetName;
        public List<bool> ImpairedDriver;

        public CrashDateSchema()
        {
            CrashRecordNumber = new List<int>();
            District = new List<string?>();
            CrashCounty = new List<string?>();
            Municipality = new List<string?>();
            CrashDate = new List<DateTime>();
            CrashSceneLighting = new List<byte>();
            Weather = new List<byte>();
            RoadCondition = new List<byte>();
            CollisionType = new List<byte>();
            RelationToRoad = new List<ushort>();
            IntersectionType = new List<byte>();
            TrafficControlDeviceType = new List<byte>();
            UrbanRural = new List<byte>();
            LocationType = new List<byte>();
            SchoolBusInvolved = new List<bool>();
            SchoolZone = new List<bool>();
            PersonCount = new List<byte>();
            VehicleCount = new List<byte>();
            FatalCount = new List<byte>();
            InjuryCount = new List<byte>();
            PedestrianCount = new List<byte>();
            PedestrianDeathCount = new List<byte>();
            PoliceReportedLatitude = new List<double>();
            PoliceReportedLongitude = new List<double>();
            SecondaryResponderLatitude = new List<double>();
            SecondaryResponderLongitude = new List<double>();
            PhantomVehicleInvolved = new List<bool>();
            SpeedLimit = new List<byte>();
            StreetName = new List<string?>();
            ImpairedDriver = new List<bool>();
        }
        // You may have to make the IEnumerable type generic
        // In the future, remember to use this as a generator to yield return CrashDateRecord instances
        // An empty interface is fine so it can compile.
        public IEnumerable<CrashDateRecord> RecordProducer()
        {
            yield break;
        }
    }
}