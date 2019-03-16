-- Script Date: 12/03/2019 13:29  - ErikEJ.SqlCeScripting version 3.5.2.80
CREATE TABLE [T_Pool] (
  [PoolId] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  [BoardId] INTEGER NOT NULL,
  [PlayerNumber] INTEGER NOT NULL,
  [SmallTokenQty] INTEGER NOT NULL,
  [BigTokenQty] INTEGER NOT NULL,
  FOREIGN KEY (BoardId) REFERENCES T_Board (PoolId)
);