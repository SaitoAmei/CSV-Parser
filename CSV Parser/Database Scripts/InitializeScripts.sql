CREATE DATABASE CsvParserData;
go
CREATE TABLE OrdersHistory
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TpepPickupDatetime DATETIME NOT NULL,
    TpepDropoffDatetime DATETIME NOT NULL,
    PassengerCount INT NOT NULL,
    TripDistance FLOAT NOT NULL,
    StoreAndFwdFlag NVARCHAR(3),
    PULocationID INT NOT NULL,
    DOLocationID INT NOT NULL,
    FareAmount DECIMAL NOT NULL,
    TipAmount DECIMAL NOT NULL
);

go
CREATE NONCLUSTERED INDEX IX_PULocationID_TripDistance_TpepDuration
ON OrdersHistory (PULocationID, TripDistance);
