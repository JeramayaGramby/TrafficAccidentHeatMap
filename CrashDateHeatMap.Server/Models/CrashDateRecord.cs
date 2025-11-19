using System;
using System.Collections.Generic;

namespace CrashDateHeatMap.Server.Models
{
    public class CrashDateRecord
    {
        public int CrashRecordNumber { get; set; }
        public string? District { get; set; }
        public string? CrashCounty { get; set; }
        public string? Municipality { get; set; }
        public DateTime CrashDate { get; set; }
        public byte CrashSceneLighting { get; set; }
        public byte Weather { get; set; }
        public byte RoadCondition { get; set; }
        public byte CollisionType { get; set; }
        public ushort RelationToRoad { get; set; }
        public byte IntersectionType { get; set; }
        public byte TrafficControlDeviceType { get; set; }
        public byte UrbanRural { get; set; }
        public byte LocationType { get; set; }
        public bool SchoolBusInvolved { get; set; }
        public bool SchoolZone { get; set; }
        public byte PersonCount { get; set; }
        public byte VehicleCount { get; set; }
        public byte FatalCount { get; set; }
        public byte InjuryCount { get; set; }
        public byte PedestrianCount { get; set; }
        public byte PedestrianDeathCount { get; set; }
        public double PoliceReportedLatitude { get; set; }
        public double PoliceReportedLongitude { get; set; }
        public double SecondaryResponderLatitude { get; set; }
        public double SecondaryResponderLongitude { get; set; }
        public bool PhantomVehicleInvolved { get; set; }
        public byte SpeedLimit { get; set; }
        public string? StreetName { get; set; }
        public bool ImpairedDriver { get; set; }
    }
    
}
