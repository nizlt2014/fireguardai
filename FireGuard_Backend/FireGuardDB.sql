CREATE TABLE Sites (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    City NVARCHAR(100),
    Status NVARCHAR(50),
    MonthlyRevenue DECIMAL(18,2)
);

INSERT INTO Sites(Name, City, Status, MonthlyRevenue)
VALUES
('ABC Mall','Indore','Healthy',25000),
('XYZ Factory','Bhopal','Warning',42000),
('City Hospital','Ujjain','Healthy',31000);

Select * from Sites;

CREATE TABLE Alerts (
    Id INT IDENTITY PRIMARY KEY,
    SiteId INT,
    Severity NVARCHAR(20),
    IsResolved BIT DEFAULT 0
);

CREATE TABLE Renewals (
    Id INT IDENTITY PRIMARY KEY,
    SiteId INT,
    DueDate DATE,
    IsCompleted BIT DEFAULT 0
);

INSERT INTO Alerts (SiteId, Severity, IsResolved)
VALUES
(1,'Critical',0),
(2,'Critical',0),
(3,'Warning',0),
(1,'Critical',1);

INSERT INTO Renewals (SiteId, DueDate, IsCompleted)
VALUES
(1,'2026-05-10',0),
(2,'2026-05-15',0),
(3,'2026-06-01',1);


CREATE TABLE Users (
    Id INT IDENTITY PRIMARY KEY,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Role NVARCHAR(50) NOT NULL
);

INSERT INTO Users (Email, PasswordHash, Role)
VALUES ('admin@fireguard.com',
'$2a$11$N9qo8uLOickgx2ZMRZo5i.ejZAg/P6MqxsVXni4eWh05rq6ArlTcK',
'Admin');

UPDATE Users
SET PasswordHash = '$2a$11$QdIzMXKcCUJ1tUgneGnUm.9l6azWMAImJZTJtT5i4z5koOCMuKAIG'
WHERE Email = 'admin@fireguard.com';

Select * from Users;

CREATE TABLE Devices (
    Id INT PRIMARY KEY IDENTITY,
    SiteName NVARCHAR(100),
    DeviceName NVARCHAR(100),
    DeviceType NVARCHAR(50),
    Status NVARCHAR(30),
    LastSeen DATETIME,
    BatteryLevel INT
);

INSERT INTO Devices
(SiteName, DeviceName, DeviceType, Status, LastSeen, BatteryLevel)
VALUES
('ABC Mall','Smoke Detector #22','Detector','Critical',GETDATE(),22),
('Nova Tower','Hydrant Pump #3','Pump','Warning',GETDATE(),61),
('Sunrise Mall','Exit Alarm Panel','Panel','Healthy',GETDATE(),92);

SELECT * FROM Devices;

CREATE TABLE Technicians (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Status NVARCHAR(30),      -- Available / On Job / Travelling
    Phone NVARCHAR(20),
    Skill NVARCHAR(100)
);

CREATE TABLE Jobs (
    Id INT PRIMARY KEY IDENTITY,
    TechnicianId INT,
    SiteName NVARCHAR(100),
    JobStatus NVARCHAR(30),   -- Assigned / In Progress / Completed
    ETAMinutes INT,
    JobsToday INT,
    FOREIGN KEY (TechnicianId) REFERENCES Technicians(Id)
);

INSERT INTO Technicians (Name, Status, Phone, Skill)
VALUES
('Ravi Kumar','On Job','9999991111','Hydrant'),
('Arjun Patel','Travelling','9999992222','Alarm Systems'),
('Sameer Khan','Available','9999993333','Extinguishers');

INSERT INTO Jobs (TechnicianId, SiteName, JobStatus, ETAMinutes, JobsToday)
VALUES
(1,'ABC Mall','In Progress',28,4),
(2,'Nova Tower','Assigned',41,3),
(3,'-','Available',0,2);


SELECT t.Id,
       t.Name,
       t.Status,
       t.Skill,
       ISNULL(j.SiteName,'-') SiteName,
       ISNULL(j.ETAMinutes,0) ETAMinutes,
       ISNULL(j.JobsToday,0) JobsToday
FROM Technicians t
LEFT JOIN Jobs j ON t.Id = j.TechnicianId

ALTER TABLE Sites
ADD RenewalDate DATETIME NULL;

UPDATE Sites
SET RenewalDate = DATEADD(day,15,GETDATE())
WHERE Id = 1;

UPDATE Sites
SET RenewalDate = DATEADD(day,45,GETDATE())
WHERE Id = 2;