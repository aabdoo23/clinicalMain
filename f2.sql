CREATE DATABASE ClinicalBaseClasses;

USE ClinicalBaseClasses;
# Admin 
CREATE TABLE Admin (
    userID INT NOT NULL PRIMARY KEY,
    userName VARCHAR(40),
    password VARCHAR(40),
    role INT
);
CREATE TABLE Patient (
    PatientID INT PRIMARY KEY,
    Name VARCHAR(100),
    Address VARCHAR(255),
    BirthDate DATE,
    PhoneNumber VARCHAR(15),
    Gender varchar (10)
);

CREATE TABLE PhysioTherapist (
    PhysioTherapistID INT PRIMARY KEY,
    UserID int,
    Name VARCHAR(100),
    DateOfBirth DATETIME,
    ContactInfo text,
    NationalID VARCHAR(30),
    HireDate DATETIME,
    Address VARCHAR(255)

);


# Attendance 
CREATE TABLE Attendance (
    ID INT NOT NULL PRIMARY KEY,
    PhysioTherapistID INT,
    IsPresent BOOLEAN,
    foreign key (PhysioTherapistID) references PhysioTherapist (PhysioTherapistID)
);


# Billing record
 CREATE TABLE Billing_record (
     BillingID INT NOT NULL PRIMARY KEY,
     DATE date,
     Amount decimal(10, 2),
     Description VARCHAR(255)
 );
# diagnosis


# Drug
CREATE TABLE Drug (
    DrugID INT NOT NULL PRIMARY KEY,
    Name VARCHAR(40),
    Frequency INT
);

#Employee

CREATE TABLE Diagnosis (
    DiagnosisID INT PRIMARY KEY,
    PatientID INT,
    PhysioTherapistID INT,
    VisitID INT,
    DiagnosisDate DATE,
    DiagnosisText TEXT,
    FOREIGN KEY (PatientID)
        REFERENCES Patient (PatientID),
    FOREIGN KEY (PhysioTherapistID)
        REFERENCES PhysioTherapist (PhysioTherapistID),
    FOREIGN KEY (VisitID)
        REFERENCES Visit (VisitID)
);


CREATE TABLE Employee (
    EmployeeID INT NOT NULL PRIMARY KEY,
    Name VARCHAR(100),
    DateOfBirth DATE,
    ContactInfo text,
    Role VARCHAR(50),
    NationalID VARCHAR(30),
    HireDate DATETIME
);


#Medical record 
CREATE TABLE MedicalRecord (
    RecordID INT NOT NULL PRIMARY KEY,
    PatientID INT,
    PhysioTherapistID int,
    RecordDate DATETIME,
    Description text,
    Type text,
    foreign key (PatientID) references Patient (PatientID)
);



# Message
CREATE TABLE Message (
    MessageID INT NOT NULL PRIMARY KEY,
    SenderID INT,
    RecipientID INT,
    Content TEXT,
    TimeStamp DATETIME
);


CREATE TABLE Prescription (
    PrescriptionID INT PRIMARY KEY,
    PhysioTherapistID INT,
    PatientID INT,
    VisitID INT,
    FOREIGN KEY (PhysioTherapistID) REFERENCES PhysioTherapist(PhysioTherapistID),
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (VisitID) REFERENCES Visit(VisitID)
);

# bos 3aleha (internal ?)
CREATE TABLE Report (
    ReportId INT PRIMARY KEY,
    PhysioTherapistID INT,
    PatientID INT,
    Type VARCHAR(40),
    Content TEXT,
    VisitID INT,
    Date DATETIME,
    FOREIGN KEY (PhysioTherapistID) REFERENCES PhysioTherapist(PhysioTherapistID),
    FOREIGN KEY (PatientId) REFERENCES Patient(PatientID),
    FOREIGN KEY (VisitID) REFERENCES Visit(VisitID)
);

CREATE TABLE Room (
    RoomID INT PRIMARY KEY,
    Capacity INT,
    RoomType text,
    IsAvailable BOOLEAN
);


CREATE TABLE TreatmentPlan(
	Patient_ID int,
    Plan_ID int primary key,
    Description text,
    progress int,
	foreign key (patient_ID) references Patient (PatientID)
);

CREATE TABLE Visit (
	VisitID int primary key,
	PatientID int,
    PhysioTherapistID int,
    Date datetime,
    reason text,
    doctorNotes TEXT,
    RoomID int,
    foreign key (PatientID) references Patient(PatientID),
    foreign key (PhysioTherapistID) references PhysioTherapist(PhysioTherapistID),
	foreign key (RoomID) references Room (RoomID)
    );
        


