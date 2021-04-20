SELECT Lot.LotNumber, Inventory.ItemName 
FROM Lot
INNER JOIN Lot_Inventory ON Lot.Id = Lot_Inventory.LotId
INNER JOIN Inventory ON Lot_Inventory.InventoryId = Inventory.Id