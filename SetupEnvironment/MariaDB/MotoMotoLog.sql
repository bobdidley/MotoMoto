-- Logging SQL Code
CREATE TABLE Level (
    levelName VARCHAR(50) NOT NULL UNIQUE,
    CONSTRAINT Level_PK PRIMARY KEY (levelName)
);
CREATE TABLE Category (
    categoryName VARCHAR(100) NOT NULL UNIQUE,
    CONSTRAINT Category_Pk PRIMARY KEY (categoryName)
);
CREATE TABLE Log (
    logId INT NOT NULL AUTO_INCREMENT,
    categoryName VARCHAR(100) NOT NULL,
    levelName VARCHAR(100) NOT NULL,
    timeStamp TIME NOT NULL,
    userID INT NOT NULL,
    DSCRIPTION VARCHAR(1000) NOT NULL,
    CONSTRAINT Log_Category_FK FOREIGN KEY (categoryName) REFERENCES Category (categoryName),
    CONSTRAINT Log_Level_FK FOREIGN KEY (levelName) REFERENCES Level (levelName),
    CONSTRAINT Log_PK PRIMARY KEY (logId)
);
INSERT INTO Level
VALUES ("Info"),
    ("Debug"),
    ("Warning"),
    ("Error");
INSERT INTO Category
VALUES ("View"),
    ("Business"),
    ("Server"),
    ("Data"),
    ("Data Store");