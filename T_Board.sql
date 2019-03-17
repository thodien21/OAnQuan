-- Script Date: 16/03/2019 22:24  - ErikEJ.SqlCeScripting version 3.5.2.80
CREATE TABLE [T_Board] (
  [BoardId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [PlayerId] bigint NOT NULL
, [Turn] bigint NOT NULL
, [Player2Pseudo] text NULL
, CONSTRAINT [FK_T_Board_0_0] FOREIGN KEY ([PlayerId]) REFERENCES [T_Player] ([BoardId]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
