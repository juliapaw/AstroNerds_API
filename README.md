# AstroNerds_API
AstroNerds project is an application for generating daily horoscopes related to astrology. The project consists of three database tables that store information about zodiacs, horoscopes, and PDF content of horoscope files.

Database Tables

Table zodiacs
The zodiacs table stores information about zodiacs, such as date ranges, zodiac names, and descriptions.

Table horoscope
The horoscope table stores information about daily horoscopes, such as date ranges, current date, description, compatibility, mood, lucky number, and zodiac name.

Table dailyHoroscopePdfContent
The dailyHoroscopePdfContent table stores the binary content of daily horoscope PDF files, along with the zodiac name as the primary key.

Sample query creating three tables zodiacs, horoscope and dailyHoroscopePdfContent and populating them with sample data:

-- Creating a table of zodiacs
CREATE TABLE zodiacs (
  Date_start DATE NOT NULL,
  Date_end DATE NOT NULL,
  ZodiacName NVARCHAR(50) NOT NULL,
  Description NVARCHAR(50) NOT NULL
);

-- Filling the zodiacs table with sample data
INSERT INTO zodiacs (Date_start, Date_end, ZodiacName, Description)
VALUES ('2023-03-21', '2023-04-19', 'Aries', 'Aries description'),
       ('2023-04-20', '2023-05-20', 'Taurus', 'Taurus description'),
       ('2023-05-21', '2023-06-20', 'Gemini', 'Gemini description');

-- Creating a horoscope table
CREATE TABLE horoscope (
  Date_range NVARCHAR(100) NOT NULL,
  Current_date DATE NOT NULL,
  Description NVARCHAR(MAX) NOT NULL,
  Compatibility NVARCHAR(100) NOT NULL,
  Mood VARCHAR(50) NOT NULL,
  Lucky_Number TINYINT NOT NULL,
  ZodiacName NVARCHAR(50) NOT NULL
);

-- Filling the horoscope table with sample data
INSERT INTO horoscope (Date_range, Current_date, Description, Compatibility, Mood, Lucky_Number, ZodiacName)
VALUES ('March 21 - April 19', '2023-04-10', 'Aries horoscope description', 'Leo', 'Positive', 5, 'Aries'),
       ('April 20 - May 20', '2023-04-10', 'Taurus horoscope description', 'Cancer', 'Neutral', 7, 'Taurus'),
       ('May 21 - June 20', '2023-04-10', 'Gemini horoscope description', 'Libra', 'Negative', 3, 'Gemini');

-- Create dailyHoroscopePdfContent table
CREATE TABLE dailyHoroscopePdfContent (
  ZodiacName NVARCHAR(50) PRIMARY KEY,
  PdfContent VARBINARY(MAX) NOT NULL
);

-- Filling the dailyHoroscopePdfContent table with sample data
INSERT INTO dailyHoroscopePdfContent (ZodiacName, PdfContent)
VALUES ('Aries', 0x255044462D312E350D0A25E2E3CFD30D0A312030206F626A0D0A3C3C2F4C656E6774682033363730203020522F46696C7465722F466C6174654465636F6465292F46696C7465722F466C6174654465636F64653E3E0D0A73747265616D0D0A789C3D0A0A313061303061306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130303030306130
