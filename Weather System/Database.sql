
/**********************************************************************************************/
/*Tables.sql*/
/**********************************************************************************************/

CREATE TABLE [MEASUREMENT]
( 
	[MeasurementId]     int  NOT NULL  IDENTITY ( 1,1 ) Primary Key,
	[MeasurementName]   varchar(100)  NOT NULL UNIQUE,
	[MeasurementAlias]  varchar(100)  NULL,
	[Unit]    			varchar(50)  NULL
)
go

CREATE TABLE [MEASUREMENTDATA]
( 
	[MeasurementDataId]  int  NOT NULL  IDENTITY ( 1,1 ) Primary Key,
	[MeasurementId]      int  NOT NULL Foreign Key REFERENCES MEASUREMENT(MeasurementId),
	[MeasurementTimeStamp] datetime  NOT NULL ,
	[MeasurementValue]   float  NOT NULL 
)
go

	
/**********************************************************************************************/
/*GetMeasurementData.sql*/
/**********************************************************************************************/
IF EXISTS (SELECT name 
	   FROM   sysobjects 
	   WHERE  name = 'GetMeasurementData' 
	   AND 	  type = 'V')
	DROP VIEW GetMeasurementData
GO

CREATE VIEW GetMeasurementData
AS

SELECT
MEASUREMENTDATA.MeasurementDataId, 
MEASUREMENT.MeasurementId, 
MEASUREMENT.MeasurementName,
MEASUREMENT.MeasurementAlias,  
MEASUREMENTDATA.MeasurementTimeStamp, 
MEASUREMENTDATA.MeasurementValue,
MEASUREMENT.Unit 

FROM MEASUREMENTDATA 
INNER JOIN MEASUREMENT ON MEASUREMENTDATA.MeasurementId = MEASUREMENT.MeasurementId

GO
	

/*SaveMeasurementData.sql*/

IF EXISTS (SELECT name 
   FROM   sysobjects 
   WHERE  name = 'SaveMeasurementData' 
   AND   type = 'P')
DROP PROCEDURE SaveMeasurementData
GO

CREATE PROCEDURE SaveMeasurementData
@MeasurementName varchar(100),
@Unit varchar(50),
@MeasurementValue float
AS

DECLARE
@MeasurementId int

if not exists (select * from MEASUREMENT where MeasurementName = @MeasurementName)
	insert into MEASUREMENT (MeasurementName, Unit) values (@MeasurementName, @Unit)

select @MeasurementId = MeasurementId from MEASUREMENT where MeasurementName = @MeasurementName


insert into MEASUREMENTDATA (MeasurementId, MeasurementValue, MeasurementTimeStamp) values (@MeasurementId, @MeasurementValue, getdate())

GO