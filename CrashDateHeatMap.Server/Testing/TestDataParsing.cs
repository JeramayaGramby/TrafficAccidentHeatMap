using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Xunit;
using CrashDateHeatMap.Server.Models;
using CrashDateHeatMap.Server.Controllers;

namespace CrashDateHeatMap.Server.Testing
{
    public class TestDataParsing
    {
        #region Test Data Generation

        /// <summary>
        /// Creates a sample CSV content with correct data types for all 30 fields
        /// </summary>
        private string CreateValidCsvContent()
        {
        var csv = new StringBuilder();
    csv.AppendLine("CrashRecordNumber,District,CrashCounty,Municipality,CrashDate,CrashSceneLighting,Weather,RoadCondition,CollisionType,RelationToRoad,IntersectionType,TrafficControlDeviceType,UrbanRural,LocationType,SchoolBusInvolved,SchoolZone,PersonCount,VehicleCount,FatalCount,InjuryCount,PedestrianCount,PedestrianDeathCount,PoliceReportedLatitude,PoliceReportedLongitude,SecondaryResponderLatitude,SecondaryResponderLongitude,PhantomVehicleInvolved,SpeedLimit,StreetName,ImpairedDriver");
   csv.AppendLine("1001,District1,Allegheny,West Mifflin,2023-01-15 14:30:00,1,2,3,4,100,5,6,7,8,true,false,2,2,0,1,0,0,40.3623,-79.8664,40.3625,-79.8665,false,35,Lebanon Church Rd,false");
  csv.AppendLine("1002,District2,Allegheny,Pittsburgh,2023-02-20 09:15:00,2,1,1,2,200,3,4,5,6,false,true,3,1,1,2,1,1,40.4406,-79.9959,40.4408,-79.9960,true,25,Forbes Ave,true");
            csv.AppendLine("1003,District3,Allegheny,McKeesport,2023-03-10 18:45:00,3,3,2,1,150,2,3,4,5,false,false,1,2,0,0,0,0,40.3483,-79.8642,40.3485,-79.8643,false,45,Curry Hollow Rd,false");
   return csv.ToString();
        }

 /// <summary>
        /// Creates a CSV with invalid data types to test error handling
    /// </summary>
private string CreateInvalidCsvContent()
   {
    var csv = new StringBuilder();
            csv.AppendLine("CrashRecordNumber,District,CrashCounty,Municipality,CrashDate,CrashSceneLighting,Weather,RoadCondition,CollisionType,RelationToRoad,IntersectionType,TrafficControlDeviceType,UrbanRural,LocationType,SchoolBusInvolved,SchoolZone,PersonCount,VehicleCount,FatalCount,InjuryCount,PedestrianCount,PedestrianDeathCount,PoliceReportedLatitude,PoliceReportedLongitude,SecondaryResponderLatitude,SecondaryResponderLongitude,PhantomVehicleInvolved,SpeedLimit,StreetName,ImpairedDriver");
  csv.AppendLine("INVALID,District1,Allegheny,West Mifflin,NOT_A_DATE,999,999,999,999,99999,999,999,999,999,NOT_BOOL,NOT_BOOL,999,999,999,999,999,999,INVALID_LAT,INVALID_LON,INVALID_LAT,INVALID_LON,NOT_BOOL,999,Test St,NOT_BOOL");
            return csv.ToString();
        }

   /// <summary>
        /// Creates a temporary zip file with CSV content for testing
        /// </summary>
        private string CreateTestZipFile(string csvContent, string csvFileName = "test_data.csv")
 {
       var tempZipPath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.zip");
      
            using (var archive = ZipFile.Open(tempZipPath, ZipArchiveMode.Create))
            {
  var entry = archive.CreateEntry(csvFileName);
    using (var writer = new StreamWriter(entry.Open()))
             {
        writer.Write(csvContent);
      }
 }

            return tempZipPath;
   }

     #endregion

        #region Data Type Validation Tests

        [Fact]
        public void LoadCrashData_ValidCsv_ParsesIntFieldsCorrectly()
        {
            // Arrange
   var csvContent = CreateValidCsvContent();
      var zipPath = CreateTestZipFile(csvContent);

        try
      {
// Act
             var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

         // Assert
                Assert.NotNull(result.CrashRecordNumber);
       Assert.Equal(3, result.CrashRecordNumber.Count);
   Assert.Equal(1001, result.CrashRecordNumber[0]);
    Assert.Equal(1002, result.CrashRecordNumber[1]);
     Assert.Equal(1003, result.CrashRecordNumber[2]);
      Assert.All(result.CrashRecordNumber, id => Assert.IsType<int>(id));
            }
    finally
            {
                File.Delete(zipPath);
         }
        }

        [Fact]
        public void LoadCrashData_ValidCsv_ParsesStringFieldsCorrectly()
        {
     // Arrange
       var csvContent = CreateValidCsvContent();
 var zipPath = CreateTestZipFile(csvContent);

            try
        {
    // Act
var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

      // Assert
      Assert.NotNull(result.District);
              Assert.Equal(3, result.District.Count);
     Assert.Equal("District1", result.District[0]);
     Assert.Equal("Allegheny", result.CrashCounty[0]);
                Assert.Equal("West Mifflin", result.Municipality[0]);
  Assert.Equal("Lebanon Church Rd", result.StreetName[0]);
            }
       finally
    {
  File.Delete(zipPath);
            }
        }

        [Fact]
 public void LoadCrashData_ValidCsv_ParsesDateTimeFieldsCorrectly()
        {
       // Arrange
      var csvContent = CreateValidCsvContent();
            var zipPath = CreateTestZipFile(csvContent);

    try
        {
   // Act
       var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

     // Assert
       Assert.NotNull(result.CrashDate);
                Assert.Equal(3, result.CrashDate.Count);
  Assert.All(result.CrashDate, date => Assert.IsType<DateTime>(date));
             Assert.Equal(new DateTime(2023, 1, 15, 14, 30, 0), result.CrashDate[0]);
          Assert.Equal(new DateTime(2023, 2, 20, 9, 15, 0), result.CrashDate[1]);
        }
            finally
       {
   File.Delete(zipPath);
            }
        }

        [Fact]
        public void LoadCrashData_ValidCsv_ParsesByteFieldsCorrectly()
  {
     // Arrange
    var csvContent = CreateValidCsvContent();
   var zipPath = CreateTestZipFile(csvContent);

       try
            {
    // Act
       var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

     // Assert
  Assert.NotNull(result.CrashSceneLighting);
     Assert.Equal(3, result.CrashSceneLighting.Count);
      Assert.All(result.CrashSceneLighting, val => Assert.IsType<byte>(val));
      Assert.Equal((byte)1, result.CrashSceneLighting[0]);
        Assert.Equal((byte)2, result.Weather[0]);
Assert.Equal((byte)35, result.SpeedLimit[0]);
            }
            finally
            {
    File.Delete(zipPath);
      }
        }

        [Fact]
        public void LoadCrashData_ValidCsv_ParsesUshortFieldsCorrectly()
        {
     // Arrange
            var csvContent = CreateValidCsvContent();
     var zipPath = CreateTestZipFile(csvContent);

     try
    {
       // Act
       var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

   // Assert
           Assert.NotNull(result.RelationToRoad);
           Assert.Equal(3, result.RelationToRoad.Count);
         Assert.All(result.RelationToRoad, val => Assert.IsType<ushort>(val));
      Assert.Equal((ushort)100, result.RelationToRoad[0]);
     Assert.Equal((ushort)200, result.RelationToRoad[1]);
            }
      finally
   {
             File.Delete(zipPath);
   }
}

        [Fact]
        public void LoadCrashData_ValidCsv_ParsesBooleanFieldsCorrectly()
        {
  // Arrange
  var csvContent = CreateValidCsvContent();
     var zipPath = CreateTestZipFile(csvContent);

            try
         {
            // Act
       var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

  // Assert
          Assert.NotNull(result.SchoolBusInvolved);
    Assert.Equal(3, result.SchoolBusInvolved.Count);
                Assert.All(result.SchoolBusInvolved, val => Assert.IsType<bool>(val));
    Assert.True(result.SchoolBusInvolved[0]);
            Assert.False(result.SchoolZone[0]);
      Assert.False(result.PhantomVehicleInvolved[0]);
           Assert.True(result.ImpairedDriver[1]);
         }
        finally
       {
        File.Delete(zipPath);
            }
        }

        [Fact]
        public void LoadCrashData_ValidCsv_ParsesDoubleFieldsCorrectly()
        {
            // Arrange
            var csvContent = CreateValidCsvContent();
          var zipPath = CreateTestZipFile(csvContent);

            try
    {
      // Act
       var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

// Assert
      Assert.NotNull(result.PoliceReportedLatitude);
  Assert.Equal(3, result.PoliceReportedLatitude.Count);
       Assert.All(result.PoliceReportedLatitude, val => Assert.IsType<double>(val));
          Assert.Equal(40.3623, result.PoliceReportedLatitude[0], precision: 4);
    Assert.Equal(-79.8664, result.PoliceReportedLongitude[0], precision: 4);
     }
  finally
  {
          File.Delete(zipPath);
            }
        }

        #endregion

        #region Schema Validation Tests

  [Fact]
    public void CrashDateSchema_Initialization_CreatesAllListsWithZeroCount()
  {
      // Act
            var schema = new CrashDateSchema();

          // Assert
    Assert.NotNull(schema.CrashRecordNumber);
            Assert.NotNull(schema.District);
            Assert.NotNull(schema.CrashCounty);
        Assert.NotNull(schema.Municipality);
   Assert.NotNull(schema.CrashDate);
            Assert.NotNull(schema.CrashSceneLighting);
   Assert.NotNull(schema.Weather);
  Assert.NotNull(schema.RoadCondition);
            Assert.NotNull(schema.CollisionType);
            Assert.NotNull(schema.RelationToRoad);
            Assert.NotNull(schema.IntersectionType);
          Assert.NotNull(schema.TrafficControlDeviceType);
            Assert.NotNull(schema.UrbanRural);
            Assert.NotNull(schema.LocationType);
            Assert.NotNull(schema.SchoolBusInvolved);
   Assert.NotNull(schema.SchoolZone);
            Assert.NotNull(schema.PersonCount);
         Assert.NotNull(schema.VehicleCount);
Assert.NotNull(schema.FatalCount);
  Assert.NotNull(schema.InjuryCount);
  Assert.NotNull(schema.PedestrianCount);
   Assert.NotNull(schema.PedestrianDeathCount);
  Assert.NotNull(schema.PoliceReportedLatitude);
       Assert.NotNull(schema.PoliceReportedLongitude);
    Assert.NotNull(schema.SecondaryResponderLatitude);
       Assert.NotNull(schema.SecondaryResponderLongitude);
            Assert.NotNull(schema.PhantomVehicleInvolved);
      Assert.NotNull(schema.SpeedLimit);
      Assert.NotNull(schema.StreetName);
    Assert.NotNull(schema.ImpairedDriver);

       Assert.Equal(0, schema.CrashRecordNumber.Count);
        }

    [Fact]
        public void LoadCrashData_ValidCsv_AllListsHaveEqualCounts()
        {
// Arrange
            var csvContent = CreateValidCsvContent();
    var zipPath = CreateTestZipFile(csvContent);

            try
 {
                // Act
    var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

     // Assert - all lists should have 3 records
      Assert.Equal(3, result.CrashRecordNumber.Count);
        Assert.Equal(3, result.District.Count);
   Assert.Equal(3, result.CrashCounty.Count);
                Assert.Equal(3, result.Municipality.Count);
     Assert.Equal(3, result.CrashDate.Count);
            Assert.Equal(3, result.CrashSceneLighting.Count);
        Assert.Equal(3, result.Weather.Count);
          Assert.Equal(3, result.RoadCondition.Count);
         Assert.Equal(3, result.CollisionType.Count);
         Assert.Equal(3, result.RelationToRoad.Count);
        Assert.Equal(3, result.IntersectionType.Count);
    Assert.Equal(3, result.TrafficControlDeviceType.Count);
        Assert.Equal(3, result.UrbanRural.Count);
         Assert.Equal(3, result.LocationType.Count);
          Assert.Equal(3, result.SchoolBusInvolved.Count);
       Assert.Equal(3, result.SchoolZone.Count);
       Assert.Equal(3, result.PersonCount.Count);
            Assert.Equal(3, result.VehicleCount.Count);
           Assert.Equal(3, result.FatalCount.Count);
       Assert.Equal(3, result.InjuryCount.Count);
      Assert.Equal(3, result.PedestrianCount.Count);
         Assert.Equal(3, result.PedestrianDeathCount.Count);
       Assert.Equal(3, result.PoliceReportedLatitude.Count);
  Assert.Equal(3, result.PoliceReportedLongitude.Count);
       Assert.Equal(3, result.SecondaryResponderLatitude.Count);
         Assert.Equal(3, result.SecondaryResponderLongitude.Count);
         Assert.Equal(3, result.PhantomVehicleInvolved.Count);
         Assert.Equal(3, result.SpeedLimit.Count);
    Assert.Equal(3, result.StreetName.Count);
            Assert.Equal(3, result.ImpairedDriver.Count);
            }
            finally
    {
     File.Delete(zipPath);
            }
 }

     #endregion

        #region Error Handling Tests

   [Fact]
     public void LoadCrashData_FileNotFound_ThrowsFileNotFoundException()
     {
          // Arrange
         var csvContent = CreateValidCsvContent();
          var zipPath = CreateTestZipFile(csvContent, "actual_file.csv");

    try
     {
           // Act & Assert
       Assert.Throws<FileNotFoundException>(() =>
        CrashDateController.LoadCrashData(zipPath, "nonexistent.csv"));
      }
            finally
            {
       File.Delete(zipPath);
   }
        }

[Fact]
        public void LoadCrashData_InvalidZipPath_ThrowsException()
        {
   // Act & Assert
            Assert.Throws<FileNotFoundException>(() =>
  CrashDateController.LoadCrashData("nonexistent.zip", "data.csv"));
        }

        [Fact]
        public void LoadCrashData_RowWithLessThan30Fields_SkipsRow()
        {
            // Arrange
  var csv = new StringBuilder();
   csv.AppendLine("CrashRecordNumber,District,CrashCounty,Municipality,CrashDate,CrashSceneLighting,Weather,RoadCondition,CollisionType,RelationToRoad,IntersectionType,TrafficControlDeviceType,UrbanRural,LocationType,SchoolBusInvolved,SchoolZone,PersonCount,VehicleCount,FatalCount,InjuryCount,PedestrianCount,PedestrianDeathCount,PoliceReportedLatitude,PoliceReportedLongitude,SecondaryResponderLatitude,SecondaryResponderLongitude,PhantomVehicleInvolved,SpeedLimit,StreetName,ImpairedDriver");
    csv.AppendLine("1001,District1,Allegheny"); // Only 3 fields - should be skipped
            csv.AppendLine("1002,District2,Allegheny,West Mifflin,2023-02-20 09:15:00,2,1,1,2,200,3,4,5,6,false,true,3,1,1,2,1,1,40.4406,-79.9959,40.4408,-79.9960,true,25,Forbes Ave,true"); // Valid row

     var zipPath = CreateTestZipFile(csv.ToString());

            try
   {
         // Act
                var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

         // Assert - only 1 valid row should be loaded
      Assert.Equal(1, result.CrashRecordNumber.Count);
         Assert.Equal(1002, result.CrashRecordNumber[0]);
          }
      finally
            {
  File.Delete(zipPath);
         }
        }

        #endregion

        #region Data Integrity Tests

     [Fact]
        public void LoadCrashData_ValidCsv_PreservesDataIntegrity()
  {
    // Arrange
         var csvContent = CreateValidCsvContent();
            var zipPath = CreateTestZipFile(csvContent);

   try
    {
      // Act
     var result = CrashDateController.LoadCrashData(zipPath, "test_data.csv");

     // Assert - verify complete first record
Assert.Equal(1001, result.CrashRecordNumber[0]);
 Assert.Equal("District1", result.District[0]);
                Assert.Equal("Allegheny", result.CrashCounty[0]);
    Assert.Equal("West Mifflin", result.Municipality[0]);
          Assert.Equal(new DateTime(2023, 1, 15, 14, 30, 0), result.CrashDate[0]);
     Assert.Equal((byte)1, result.CrashSceneLighting[0]);
        Assert.Equal((byte)2, result.Weather[0]);
       Assert.Equal((byte)3, result.RoadCondition[0]);
     Assert.Equal((byte)4, result.CollisionType[0]);
         Assert.Equal((ushort)100, result.RelationToRoad[0]);
       Assert.Equal((byte)5, result.IntersectionType[0]);
  Assert.Equal((byte)6, result.TrafficControlDeviceType[0]);
     Assert.Equal((byte)7, result.UrbanRural[0]);
    Assert.Equal((byte)8, result.LocationType[0]);
                Assert.True(result.SchoolBusInvolved[0]);
     Assert.False(result.SchoolZone[0]);
  Assert.Equal((byte)2, result.PersonCount[0]);
    Assert.Equal((byte)2, result.VehicleCount[0]);
     Assert.Equal((byte)0, result.FatalCount[0]);
                Assert.Equal((byte)1, result.InjuryCount[0]);
     Assert.Equal((byte)0, result.PedestrianCount[0]);
         Assert.Equal((byte)0, result.PedestrianDeathCount[0]);
      Assert.Equal(40.3623, result.PoliceReportedLatitude[0], precision: 4);
   Assert.Equal(-79.8664, result.PoliceReportedLongitude[0], precision: 4);
   Assert.Equal(40.3625, result.SecondaryResponderLatitude[0], precision: 4);
      Assert.Equal(-79.8665, result.SecondaryResponderLongitude[0], precision: 4);
    Assert.False(result.PhantomVehicleInvolved[0]);
        Assert.Equal((byte)35, result.SpeedLimit[0]);
             Assert.Equal("Lebanon Church Rd", result.StreetName[0]);
       Assert.False(result.ImpairedDriver[0]);
       }
        finally
       {
                File.Delete(zipPath);
      }
        }

#endregion
    }
}
