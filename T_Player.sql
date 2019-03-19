-- Script Date: 19/03/2019 14:42  - ErikEJ.SqlCeScripting version 3.5.2.80
DROP TABLE [T_Player];
CREATE TABLE [T_Player] (
  [PlayerId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Pseudo] text NOT NULL
, [Password] text NOT NULL
, [FullName] text NOT NULL
, [IsAdmin] bigint NULL
, [IsDisabled] bigint NULL
, [WinGameQty] bigint NOT NULL
, [DrawGameQty] bigint NOT NULL
, [LoseGameQty] bigint NOT NULL
);
