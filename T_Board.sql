-- Script Date: 12/03/2019 13:29  - ErikEJ.SqlCeScripting version 3.5.2.80
CREATE TABLE [T_Board] (
  [BoardId] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  [PlayerId] INTEGER NOT NULL,
  [Turn] INTEGER NOT NULL,
  [FullName] text NULL,
  FOREIGN KEY (PlayerId) REFERENCES T_Player (BoardId)
);