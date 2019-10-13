-- Creating table 'Appointment'
CREATE TABLE [dbo].[Appointment] (
[AppointmentId] int IDENTITY(1,1) NOT NULL,
[FirstName] nvarchar(max) NOT NULL,
[LastName] nvarchar(max) NOT NULL,
[Title] nvarchar(max),
[PhoneNo] int NOT NULL,
[NoOfPeople] int NOT NULL,
[Date] date NOT NULL,
PRIMARY KEY (AppointmentId)
);
GO

-- Creating table 'Restaurant'
CREATE TABLE [dbo].[Restaurant] (
[RestaurantId] int IDENTITY(1,1) NOT NULL,
[Name] nvarchar(max) NOT NULL,
[Description] nvarchar(max) NOT NULL,
[Longitude] int NOT NULL,
[Latitude] int NOT NULL
PRIMARY KEY (RestaurantId)
);
GO