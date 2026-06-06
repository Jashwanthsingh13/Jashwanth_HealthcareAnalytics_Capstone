-- Database Design (Normalized)

-- Database creattion
CREATE DATABASE HealthcareAnalyticsDB;

Use HealthcareAnalyticsDB;

-- Tables created for Each tasks !!
-- Roles table
CREATE TABLE Roles
(
    RoleId INT IDENTITY(1,1) PRIMARY KEY,

    RoleName NVARCHAR(50)
    NOT NULL UNIQUE
);

-- Users table

CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,

    Username NVARCHAR(100)
    NOT NULL UNIQUE,

    Email NVARCHAR(100)
    NOT NULL,

    PasswordHash NVARCHAR(MAX)
    NOT NULL,

    RoleId INT NOT NULL,

    CreatedDate DATETIME
    DEFAULT GETDATE(),

    CONSTRAINT FK_Users_Roles
    FOREIGN KEY(RoleId)
    REFERENCES Roles(RoleId)
);

--Patients table

CREATE TABLE Patients
(
    PatientId INT IDENTITY(1,1) PRIMARY KEY,

    FullName NVARCHAR(100)
    NOT NULL,

    Email NVARCHAR(100)
    NOT NULL,

    PhoneNumber NVARCHAR(15)
    NOT NULL,

    DateOfBirth DATE
    NOT NULL,

    Gender NVARCHAR(20)
    NOT NULL,

    Address NVARCHAR(250),

    CreatedDate DATETIME
    DEFAULT GETDATE()
);

-- Appointments table 

CREATE TABLE Appointments
(
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,

    PatientId INT NOT NULL,

    AppointmentDate DATETIME
    NOT NULL,

    Status NVARCHAR(30)
    NOT NULL,

    Notes NVARCHAR(500),

    CreatedDate DATETIME
    DEFAULT GETDATE(),

    CONSTRAINT FK_Appointments_Patients
    FOREIGN KEY(PatientId)
    REFERENCES Patients(PatientId)
);

--Rviews table

CREATE TABLE Reviews
(
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,

    PatientId INT NOT NULL,

    Rating INT NOT NULL,

    ReviewText NVARCHAR(500),

    Status NVARCHAR(30) DEFAULT 'Pending',

    CreatedDate DATETIME DEFAULT GETDATE(),

    FOREIGN KEY(PatientId)
    REFERENCES Patients(PatientId)
);


-- ActivityLogs Table
CREATE TABLE ActivityLogs
(
    ActivityId INT IDENTITY(1,1) PRIMARY KEY,

    ActivityDescription NVARCHAR(500),

    ActivityDate DATETIME DEFAULT GETDATE()
);

INSERT INTO ActivityLogs(ActivityDescription)
VALUES
('Patient John Smith Checked In'),
('Appointment Scheduled'),
('New Patient Added');


-- Feedbaks Table

CREATE TABLE Feedbacks
(
    FeedbackId INT IDENTITY(1,1) PRIMARY KEY,

    PatientId INT NOT NULL,

    Rating INT NOT NULL,

    Comments NVARCHAR(500),

    CreatedDate DATETIME DEFAULT GETDATE(),

    FOREIGN KEY(PatientId)
    REFERENCES Patients(PatientId)
);

-- Doctor's table

CREATE TABLE Doctors
(
    DoctorId INT IDENTITY(1,1) PRIMARY KEY,

    DoctorName NVARCHAR(100) NOT NULL,

    Specialization NVARCHAR(100),

    Email NVARCHAR(100),

    PhoneNumber NVARCHAR(20),

    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Imposrtant Information 

ALTER TABLE Appointments
ADD DoctorId INT;

ALTER TABLE Appointments
ADD CONSTRAINT FK_Appointments_Doctors
FOREIGN KEY (DoctorId)
REFERENCES Doctors(DoctorId);





-- Data Insertion Process
-- Insertion for Roles

INSERT INTO Roles(RoleName)
VALUES
('Admin'),
('Doctor'),
('Patient');

-- Insertion for Users

INSERT INTO Users
(
Username,
Email,
PasswordHash,
RoleId
)
VALUES
(
'admin',
'admin@healthcare.com',
'Admin123',
1
);

-- Patient data
INSERT INTO Patients
(
    FullName,
    Email,
    PhoneNumber,
    DateOfBirth,
    Gender,
    Address
)
VALUES
('Santosh Narwad', 'santosh@example.com', '9876543210', '2001-05-15', 'Male', 'Pune, Maharashtra'),

('Rahul Sharma', 'rahul@example.com', '9876543211', '1998-08-22', 'Male', 'Mumbai, Maharashtra'),

('Priya Patil', 'priya@example.com', '9876543212', '2000-03-10', 'Female', 'Nashik, Maharashtra'),

('Sneha Kulkarni', 'sneha@example.com', '9876543213', '1999-11-05', 'Female', 'Nagpur, Maharashtra'),

('Amit Deshmukh', 'amit@example.com', '9876543214', '1997-07-18', 'Male', 'Pimpri-Chinchwad, Maharashtra');

--Appointments

INSERT INTO Appointments
(
    PatientId,
    AppointmentDate,
    Status,
    Notes
)
VALUES
(
    1,
    '2026-06-15 10:30:00',
    'Scheduled',
    'Annual health checkup'
);

INSERT INTO Doctors
(
DoctorName,
Specialization,
Email,
PhoneNumber
)
VALUES
(
'Dr. Emily',
'Cardiology',
'emily@hospital.com',
'9999991111'
),

(
'Dr. Chen',
'Neurology',
'9999992222',
'9999992222'
),

(
'Dr. Lee',
'Orthopedics',
'9999993333',
'9999993333'
),

(
'Dr. Patel',
'Dermatology',
'9999994444',
'9999994444'
);

-- feddback tables insertion

INSERT INTO Feedbacks
(
PatientId,
Rating,
Comments
)
VALUES
(
1,
5,
'Excellent Service'
);

-- Reviews data entry

INSERT INTO Reviews
(
PatientId,
Rating,
ReviewText,
Status
)
VALUES
(
1,
5,
'Good Experience',
'Pending'
);

-- Upgrade Database
--Alter Appointments

ALTER TABLE Appointments
ADD CONSTRAINT CK_Appointments_Status
CHECK
(
Status IN
(
'Scheduled',
'Completed',
'Cancelled'
)
);

--Activity Logs

INSERT INTO ActivityLogs
(
ActivityDescription
)
VALUES
(
'Appointment Scheduled'
);

INSERT INTO Users
(
Username,
Email,
PasswordHash,
RoleId
)
VALUES
(
'doctor1',
'doctor@hospital.com',
'Doctor123',
2
);


-- Data Retrival from the tables 

SELECT * FROM Roles;

SELECT * FROM Users;

SELECT * FROM Patients;

SELECT * FROM Appointments;

SELECT * FROM Doctors;

SELECT TOP 5
ActivityDescription
FROM ActivityLogs
ORDER BY ActivityDate DESC;


SELECT COUNT(*)
FROM Patients;

SELECT COUNT(*)
FROM Appointments;

SELECT COUNT(*)
FROM Reviews
WHERE Status='Pending';

SELECT AVG(Rating)
FROM Feedbacks;

SELECT TOP 5 *
FROM ActivityLogs
ORDER BY ActivityDate DESC;