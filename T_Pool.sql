-- Script Date: 19/03/2019 09:45  - ErikEJ.SqlCeScripting version 3.5.2.80
CREATE TABLE [T_Pool] (
  [PoolId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [PlayerId] bigint NOT NULL
, [PlayerNumber] bigint NOT NULL
, [SmallTokenQty] bigint NOT NULL
, [BigTokenQty] bigint NOT NULL
, CONSTRAINT [FK_T_SquareList_0_0] FOREIGN KEY ([PlayerId]) REFERENCES [T_Player] ([PoolId]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
