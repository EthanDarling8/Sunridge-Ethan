
SELECT LotNumber, Address, TaxId, STRING_AGG(ItemName, ', ') AS Lot_Inventory, STRING_AGG(FirstName, ', ') AS Lot_Owner
FROM Lot
INNER JOIN Lot_Inventory ON Lot.Id = Lot_Inventory.LotId
INNER JOIN Inventory ON Lot_Inventory.InventoryId = Inventory.Id
INNER JOIN Lot_Owner ON Lot.Id = Lot_Owner.Id
INNER JOIN AspNetUsers ON Lot_Owner.OwnerId = AspNetUsers.Id
GROUP BY LotNumber, Address, TaxId


SELECT LotNumber, STRING_AGG(FirstName, ', ') AS Lot_Owner
FROM Lot
INNER JOIN Lot_Owner ON Lot_Owner.LotId = Lot.Id
INNER JOIN AspNetUsers ON Lot_Owner.OwnerId = AspNetUsers.Id
GROUP BY LotNumber