# Currency Converter API
  This is a REST API for converting between currencies, built with .NET 6.

#Features
-  Convert between different currency pairs
-  Get list of supported currencies and latest exchange rates
-  Request logging to SQLite database
-  Swagger UI documentation
-  Basic authentication
-  Custom exception handling middleware
-  Repository pattern for data access
-  Dependency injection
-  Endpoints
-  Convert Currency
-  Converts a source currency amount to a target currency.

#Usage
- Update appsettings.json with any API keys
- Build and run the API
- Access endpoints via https://localhost:7032 or http://localhost:5191
- See Swagger UI for documentation

```
-- Create DATABASE in you're local in SQL Server Management Studio
 Database Name: CurrencyTestDB

#Database
-- Run this script in your local SQL Server Management Studio
-- Create the "User" table
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
([Id] ASC)
)
-- Insert sample data into the "User" table
INSERT INTO dbo.[User]
SELECT  'testuser1', 'testuser1' UNION ALL
SELECT  'testuser2', 'testuser2' UNION ALL
SELECT  'testuser3', 'testuser3'

-- Create the "RequestLog" table
CREATE TABLE [dbo].[RequestLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [varchar](5000) NULL,
	[Method] [varchar](10) NULL,
	[RequestBody] [varchar](max) NULL,
	[StatusCode] [int] NULL,
	[Timestamp] [datetime] NULL,
 CONSTRAINT [PK_RequestLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) 

```


#Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- Swashbuckle (Swagger)
- Dependency injection is used throughout for loose coupling
- Repositories implement the repository pattern
- Custom middlware for consistent exception handling
- Input validation middleware for bad requests
- Serilog for request logging
