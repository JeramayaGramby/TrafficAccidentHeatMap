using System;
using System.Collections.Generic;

namespace Models
{
    public struct CrashDataSchema
    {
        public int CrashRecordNumber;
        public string? District;
        required public string CrashCounty;
        public string? Municipality;
        public DateTime CrashDate;
        public Byte CrashSceneLighting;
        public Byte Weather;
        public Byte RoadCondition;
        public Byte CollisionType;
        public UInt16 RelationToRoad;
        public Byte IntersectionType;
        public Byte TrafficControlDeviceType;
        public Byte UrbanRural;
        public Byte LocationType;
        public bool SchoolBusInvolved;
        public bool SchoolZone;
        public Byte PersonCount;
        public Byte VehicleCount;
        public Byte FatalCount;
        public Byte InjuryCount;
        public Byte PedestrianCount;
        public Byte PedestrianDeathCount;
        public double PoliceReportedLatitude;
        public double PoliceReportedLongitude;
        public double SecondaryResponderLatitude;
        public double SecondaryResponderLongitude;
        public bool PhantomVehicleInvolved;
        public Byte SpeedLimit;
        required public string StreetName;
        public bool ImpairedDriver;


        public CrashDataSchema()
        {

        }

    
    }

}