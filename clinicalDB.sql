CREATE DATABASE Clinical;

USE Clinical;

CREATE TABLE User (
	  firstName varchar(30) not null,
    lastname varchar (30) not null,
    userID int not null primary key,
    gender boolean,
	  hireDate date,
    birthdate date,
    address text,
    phoneNumber varchar(12),
    email varchar(100),
    nationalID varchar(30)    
);
CREATE TABLE Room (
    roomID INT NOT NULL,
    roomNumber INT NOT NULL,
    equipmentIDs TEXT,
    PRIMARY KEY (roomID)
);

CREATE TABLE AttendanceRecord (
	recordID varchar(30) NOT NULL PRIMARY KEY,
	timeStamp DATE,
	userID int,
	present BOOLEAN,
    FOREIGN KEY (userID) REFERENCES User(userID)
);
CREATE TABLE ChatGroup (
    chatGroupID INT NOT NULL ,
    usersIDs TEXT NOT NULL,
    chatGroupName VARCHAR(100),
    PRIMARY KEY (chatGroupID)
);

CREATE TABLE Drug (
    drugID INT NOT NULL AUTO_INCREMENT,
    drugName VARCHAR(100) NOT NULL,
    normalDosage VARCHAR(255),
    notes TEXT,
    PRIMARY KEY (drugID)
);

CREATE TABLE Equipment (
    equipmentID INT NOT NULL,
    equipmentName VARCHAR(100) NOT NULL,
    equipmentFunction TEXT,
    latestMaintenanceDate DATE,
    toCheck BOOLEAN,
    PRIMARY KEY (equipmentID)
);

CREATE TABLE ChatRoom (
    chatRoomID INT NOT NULL ,
    firstUserID int NOT NULL,
    secondUserID int NOT NULL,
    chatRoomName VARCHAR(100),
    PRIMARY KEY (chatRoomID),
    FOREIGN KEY (firstUserID) REFERENCES User(userID),
    FOREIGN KEY (secondUserID) REFERENCES User(userID));
    
    CREATE TABLE ChatMessage (
    messageID INT NOT NULL,
    senderID int NOT NULL,
    chatRoomID INT NOT NULL,
    messageContent TEXT,
    timeStamp datetime,
    PRIMARY KEY (messageID),
    FOREIGN KEY (senderID) REFERENCES User(userID),
    FOREIGN KEY (chatRoomID) REFERENCES ChatRoom(chatRoomID)
);

CREATE TABLE Patient (
    patientID INT NOT NULL,
    firstName VARCHAR(30) NOT NULL,
    lastName VARCHAR(30) NOT NULL,
    birthdate DATE,
    gender BOOLEAN,
    phoneNumber VARCHAR(12),
    email VARCHAR(100),
    address TEXT,
    chronicDiseasesIDs TEXT,
    previousInjuriesIDs TEXT,
    referred BOOLEAN,
    previouslyTreated BOOLEAN,
    height DECIMAL(5,2),
    weight DECIMAL(5,2),
    dueAmount DECIMAL(10,2),
    physicianID int,
	FOREIGN KEY (physicianID) REFERENCES User(userID),

    PRIMARY KEY (patientID)
);

CREATE TABLE ChronicDisease (
    chronicDiseaseID INT NOT NULL ,
    chronicDiseaseName VARCHAR(100) NOT NULL,
    description TEXT,
    PRIMARY KEY (chronicDiseaseID)
);

CREATE TABLE Exercise (
    exerciseID INT NOT NULL ,
    exerciseName VARCHAR(100) NOT NULL,
    explanationLink TEXT, 
    notes TEXT,
    PRIMARY KEY (exerciseID)
);

CREATE TABLE ChatGroupRelation (
    groupMemberID INT NOT NULL,
    chatGroupID INT NOT NULL,
    userID int NOT NULL,
    PRIMARY KEY (groupMemberID),
    FOREIGN KEY (chatGroupID) REFERENCES ChatGroup(chatGroupID),
    FOREIGN KEY (userID) REFERENCES User(userID)
);


CREATE TABLE Injury (
    injuryID INT NOT NULL ,
    injuryName TEXT NOT NULL,
    injuryLocation TEXT NOT NULL,
    severity INT,
    treatmentPlanID INT,
    PRIMARY KEY (injuryID)
);

CREATE TABLE TreatmentPlan (
    planID INT NOT NULL,
    planName VARCHAR(100) NOT NULL,
    planTime INT NOT NULL,
    injuryID INT,
    price DECIMAL(10,2),
    notes TEXT,
    PRIMARY KEY (planID),
    FOREIGN KEY (injuryID) REFERENCES Injury(injuryID)
);


CREATE TABLE DrugRelation (
    drugRelationID INT NOT NULL,
    patientID INT NOT NULL,
    drugName VARCHAR(100) NOT NULL,
    PRIMARY KEY (drugRelationID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
);


CREATE TABLE Visit (
    visitID INT NOT NULL,
    userID int not null,
    patientID INT NOT NULL,
    packageID INT,
    timeStamp DATETIME,
    roomID INT,
    type TEXT NOT NULL,
    therapistNotes TEXT,
    PRIMARY KEY (visitID),
    FOREIGN KEY (UserID) REFERENCES User(userID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (packageID) REFERENCES TreatmentPlan(planID),
    FOREIGN KEY (roomID) REFERENCES Room(roomID)
);



CREATE TABLE Prescription (
    prescriptionID INT NOT NULL,
    timeStamp DATETIME,
    patientID INT NOT NULL,
    userID int NOT NULL,
    visitID INT,
    PRIMARY KEY (prescriptionID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (userID) REFERENCES User(userID),
    FOREIGN KEY (visitID) REFERENCES Visit(visitID)
);

CREATE TABLE IssueDrug (
    issueID INT NOT NULL,
    drugID INT NOT NULL,
    prescriptionID INT NOT NULL,
    patientID INT NOT NULL,
    frequency TEXT,
    notes TEXT,
    PRIMARY KEY (issueID),
    FOREIGN KEY (drugID) REFERENCES Drug(drugID),
    FOREIGN KEY (prescriptionID) REFERENCES Prescription(prescriptionID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
);

CREATE TABLE IssueExercise (
    issueID INT NOT NULL,
    exerciseID INT NOT NULL,
    patientID INT NOT NULL,
    prescriptionID INT NOT NULL,
    frequency TEXT,
    notes TEXT,
    PRIMARY KEY (issueID),
    FOREIGN KEY (exerciseID) REFERENCES Exercise(exerciseID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID),
    FOREIGN KEY (prescriptionID) REFERENCES Prescription(prescriptionID)
);



CREATE TABLE IssuedDrugRelation (
    issueDrugRelationID INT NOT NULL,
    prescriptionID INT NOT NULL,
    drugID INT NOT NULL,
    PRIMARY KEY (issueDrugRelationID),
    FOREIGN KEY (prescriptionID) REFERENCES Prescription(prescriptionID),
    FOREIGN KEY (drugID) REFERENCES Drug(drugID)
);

CREATE TABLE IssuedExerciseRelation (
    prescriptionID INT NOT NULL,
    IssuedExerciseRelationID INT NOT NULL,
    exerciseID INT NOT NULL,
    PRIMARY KEY (IssuedExerciseRelationID),
    FOREIGN KEY (prescriptionID) REFERENCES Prescription(prescriptionID),
    FOREIGN KEY (exerciseID) REFERENCES Exercise(exerciseID)
);

CREATE TABLE MedicalRecord (
    recordID INT NOT NULL,
    type TEXT,
    timeStamp DATETIME,
    report TEXT,
    images INT, 					#	##########BYSHOF 
    visitID INT,
    patientID INT,
    physicianNotes TEXT,
    PRIMARY KEY (recordID),
    FOREIGN KEY (visitID) REFERENCES Visit(visitID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
);


CREATE TABLE BillingRecord (
    billingID INT NOT NULL,
    billingAmount DECIMAL(10,2) NOT NULL,
    treatmentPlanID INT,
    patientID INT NOT NULL,
    PRIMARY KEY (billingID),
    FOREIGN KEY (treatmentPlanID) REFERENCES TreatmentPlan(planID),
    FOREIGN KEY (patientID) REFERENCES Patient(patientID)
);