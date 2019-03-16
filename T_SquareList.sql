-- Script Date: 12/03/2019 13:29  - ErikEJ.SqlCeScripting version 3.5.2.80
CREATE TABLE [T_SquareList] (
  [SquareId] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  [BoardId] INTEGER NOT NULL,
  [SmallTokenQty] INTEGER NOT NULL,
  [BigTokenQty] INTEGER NOT NULL,
  FOREIGN KEY (BoardId) REFERENCES T_Board (SquareId)
);