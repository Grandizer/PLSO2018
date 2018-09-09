CREATE PROCEDURE [dbo].[GetEnumTriplet]
(
	@Enumeration NVARCHAR(25),
	@EnumName    NVARCHAR(25) = NULL,
	@EnumValue   INT = NULL
)
AS
BEGIN

	DECLARE @SQL     NVARCHAR(250)
	DECLARE @PDs     NVARCHAR(100) = N'@OutValue INT OUTPUT, @OutDisplay NVARCHAR(30) OUTPUT, @OutName NVARCHAR(25) OUTPUT'
	DECLARE @Value   INT = 0
	DECLARE @Display NVARCHAR(30)
	DECLARE @Name    NVARCHAR(25)

	IF (@EnumName IS NOT NULL)
		SET @SQL = 'SELECT @OutValue = e.ID, @OutDisplay = e.Description, @OutName = e.Name FROM ref.' + @Enumeration + ' AS e WHERE e.Name = ' + '''' + @EnumName + ''''
	ELSE IF (@EnumValue IS NOT NULL)
		SET @SQL = 'SELECT @OutValue = e.ID, @OutDisplay = e.Description, @OutName = e.Name FROM ref.' + @Enumeration + ' AS e WHERE e.ID = ' + CAST(@EnumValue AS NVARCHAR)

	EXECUTE sp_executesql
		@SQL, @PDs, @OutValue = @Value OUTPUT, @OutDisplay = @Display OUTPUT, @OutName = @Name OUTPUT;

	-- Test Content

	SELECT
		@Value       AS Value,
		@Display     AS DisplayName,
		@Name        AS KeyName,
		@Enumeration AS EnumerationName

/*
GetEnumTriplet 'LocationType', 'Lat Long' -- Wrong
GetEnumTriplet 'LocationType', 'LatLong', 3 -- Will default to looking up LatLong and skip looking up 3
GetEnumTriplet 'LocationType', 'LatLong'
GetEnumTriplet 'LocationType', NULL, 2
*/
END
