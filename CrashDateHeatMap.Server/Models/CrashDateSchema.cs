using System;
using System.Collections.Generic;

namespace CrashDateHeatMap.Server.Models
{
    public struct CrashDateSchema
    {
        public int CrashRecordNumber;
        public string? District;
        public string? CrashCounty;
        public string? Municipality;
        public DateTime CrashDate;
        public byte CrashSceneLighting;
        public byte Weather;
        public byte RoadCondition;
        public byte CollisionType;
        public ushort RelationToRoad;
        public byte IntersectionType;
        public byte TrafficControlDeviceType;
        public byte UrbanRural;
        public byte LocationType;
        public bool SchoolBusInvolved;
        public bool SchoolZone;
        public byte PersonCount;
        public byte VehicleCount;
        public byte FatalCount;
        public byte InjuryCount;
        public byte PedestrianCount;
        public byte PedestrianDeathCount;
        public double PoliceReportedLatitude;
        public double PoliceReportedLongitude;
        public double SecondaryResponderLatitude;
        public double SecondaryResponderLongitude;
        public bool PhantomVehicleInvolved;
        public byte SpeedLimit;
        public string? StreetName;
        public bool ImpairedDriver;

        public CrashDateSchema()
        {

        }

    
    }

}