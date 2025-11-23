# TestDataParsing - Unit Test Documentation

## Overview
Comprehensive unit test suite for validating CSV crash data parsing and type integrity in the CrashDateHeatMap project.

## Test Coverage

### ✅ All 13 Tests Passing

### Test Categories

#### 1. **Data Type Validation Tests** (7 tests)
Tests that verify each field type is correctly parsed from CSV:

- `LoadCrashData_ValidCsv_ParsesIntFieldsCorrectly`
  - Validates `CrashRecordNumber` (int) parsing
  - Confirms correct count and type

- `LoadCrashData_ValidCsv_ParsesStringFieldsCorrectly`
  - Validates `District`, `CrashCounty`, `Municipality`, `StreetName` (string?) parsing
  - Tests nullable string handling

- `LoadCrashData_ValidCsv_ParsesDateTimeFieldsCorrectly`
  - Validates `CrashDate` (DateTime) parsing
  - Ensures proper date/time conversion

- `LoadCrashData_ValidCsv_ParsesByteFieldsCorrectly`
  - Validates all byte fields: `CrashSceneLighting`, `Weather`, `RoadCondition`, `CollisionType`, `IntersectionType`, `TrafficControlDeviceType`, `UrbanRural`, `LocationType`, `PersonCount`, `VehicleCount`, `FatalCount`, `InjuryCount`, `PedestrianCount`, `PedestrianDeathCount`, `SpeedLimit`
  - Confirms byte range adherence

- `LoadCrashData_ValidCsv_ParsesUshortFieldsCorrectly`
  - Validates `RelationToRoad` (ushort) parsing
  - Tests unsigned short integer handling

- `LoadCrashData_ValidCsv_ParsesBooleanFieldsCorrectly`
  - Validates `SchoolBusInvolved`, `SchoolZone`, `PhantomVehicleInvolved`, `ImpairedDriver` (bool) parsing
  - Tests true/false conversion

- `LoadCrashData_ValidCsv_ParsesDoubleFieldsCorrectly`
  - Validates `PoliceReportedLatitude`, `PoliceReportedLongitude`, `SecondaryResponderLatitude`, `SecondaryResponderLongitude` (double) parsing
  - Confirms floating-point precision (4 decimal places)

#### 2. **Schema Validation Tests** (2 tests)

- `CrashDateSchema_Initialization_CreatesAllListsWithZeroCount`
  - Validates that all 30 list fields are initialized (not null)
  - Confirms empty state on construction

- `LoadCrashData_ValidCsv_AllListsHaveEqualCounts`
  - Ensures all 30 lists have matching record counts
  - Validates data consistency across all fields

#### 3. **Error Handling Tests** (3 tests)

- `LoadCrashData_FileNotFound_ThrowsFileNotFoundException`
  - Tests behavior when CSV file doesn't exist in zip
  - Validates proper exception throwing

- `LoadCrashData_InvalidZipPath_ThrowsException`
  - Tests behavior when zip file path is invalid
  - Validates file system error handling

- `LoadCrashData_RowWithLessThan30Fields_SkipsRow`
  - Tests that malformed CSV rows (< 30 fields) are skipped
  - Validates defensive parsing

#### 4. **Data Integrity Tests** (1 test)

- `LoadCrashData_ValidCsv_PreservesDataIntegrity`
- End-to-end validation of a complete record
  - Confirms all 30 fields are correctly mapped from CSV to schema
  - Tests exact value preservation across all data types

## Test Data Structure

### Sample CSV Format (30 fields)
```csv
CrashRecordNumber,District,CrashCounty,Municipality,CrashDate,CrashSceneLighting,Weather,RoadCondition,CollisionType,RelationToRoad,IntersectionType,TrafficControlDeviceType,UrbanRural,LocationType,SchoolBusInvolved,SchoolZone,PersonCount,VehicleCount,FatalCount,InjuryCount,PedestrianCount,PedestrianDeathCount,PoliceReportedLatitude,PoliceReportedLongitude,SecondaryResponderLatitude,SecondaryResponderLongitude,PhantomVehicleInvolved,SpeedLimit,StreetName,ImpairedDriver
```

### Test Records
Three representative records covering:
- West Mifflin (Lebanon Church Rd)
- Pittsburgh (Forbes Ave)
- McKeesport (Curry Hollow Rd)

## Running the Tests

### Command Line
```bash
dotnet test CrashDateHeatMap.Server\CrashDateHeatMap.Server.csproj
```

### Visual Studio
- Open Test Explorer (Test > Test Explorer)
- Click "Run All Tests"

### With Detailed Output
```bash
dotnet test CrashDateHeatMap.Server\CrashDateHeatMap.Server.csproj --logger "console;verbosity=detailed"
```

## Test Framework & Dependencies

- **xUnit** 2.9.3 - Test framework
- **xunit.runner.visualstudio** 3.1.5 - Visual Studio test adapter
- **Microsoft.NET.Test.Sdk** 18.0.1 - .NET test platform
- **Target Framework**: .NET 8.0

## Key Features

✅ **Type Safety** - Every field type is validated  
✅ **Defensive Parsing** - Handles malformed rows gracefully  
✅ **Temporary File Management** - Tests create and clean up zip files  
✅ **Real-World Data** - Uses actual Pittsburgh-area street names and coordinates  
✅ **Complete Coverage** - All 30 schema fields tested  
✅ **Error Scenarios** - Tests missing files and invalid data  

## Future Enhancements

Consider adding:
- Performance tests for large CSV files (100k+ rows)
- Tests for edge cases (null values, extreme coordinates, date boundaries)
- Integration tests with actual compressed `.csv.gz` files from Assets
- Parameterized tests using `[Theory]` for boundary values
- Mock tests for file I/O operations
- Tests for `RecordProducer()` method when implemented

## Notes

- Tests use temporary files in `Path.GetTempPath()` - automatically cleaned up
- Each test is isolated (creates its own test data)
- Tests are fast (~3 seconds for full suite)
- No external dependencies or test data files required

---

**Last Updated**: November 23 2025  
**Author**: Jeramaya Gramby, All Rights Reserved.
