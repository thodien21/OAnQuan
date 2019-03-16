-- Script Date: 16/03/2019 12:43  - ErikEJ.SqlCeScripting version 3.5.2.80
CREATE TABLE [T_Player] (
  [PlayerId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Pseudo] text NOT NULL
, [Password] text NOT NULL
, [isAdmin] bigint NULL
, [FullName] text NULL
, [WinGameQty] bigint NULL
, [DrawGameQty] bigint NULL
, [LoseGameQty] bigint NULL
);
