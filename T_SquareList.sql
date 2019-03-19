-- Script Date: 18/03/2019 15:19  - ErikEJ.SqlCeScripting version 3.5.2.80
CREATE TABLE [T_SquareList] (
  [SquareId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [PlayerId] bigint NOT NULL
, [SquareNumber] bigint NOT NULL
, [SmallTokenQty] bigint NOT NULL
, [BigTokenQty] bigint NOT NULL
, CONSTRAINT [FK_T_SquareList_0_0] FOREIGN KEY ([PlayerId]) REFERENCES [T_Player] ([SquareId]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
