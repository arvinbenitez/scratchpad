--DROP TABLE Expense
--DROP TABLE ExpenseType

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ExpenseCategory')
BEGIN
	CREATE TABLE ExpenseCategory
	(ExpenseCategoryId INT NOT NULL IDENTITY PRIMARY KEY,
	 Name NVARCHAR(200) NOT NULL)

	SET IDENTITY_INSERT ExpenseCategory ON
	INSERT INTO ExpenseCategory (ExpenseCategoryId,Name)
	VALUES 
	(1,'Monthly Dues'),
	(2,'Phone Load'),
	(3,'Delivery Vehicle'),
	(4,'Container Seal/Sticker'),
	(5,'Deep Well/Machine Maintenance'),
	(6,'Cleaning Agents'),
	(7,'Food'),
	(8,'Others')
	SET IDENTITY_INSERT ExpenseCategory OFF
END

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ExpenseType')
BEGIN
	CREATE TABLE ExpenseType
	(
		ExpenseTypeId INT NOT NULL IDENTITY PRIMARY KEY,
		Name NVARCHAR(200) NOT NULL,
		ExpenseCategoryId INT NOT NULL,
		CONSTRAINT FK_ExpenseType_ExpenseCategory FOREIGN KEY (ExpenseCategoryId) REFERENCES ExpenseCategory(ExpenseCategoryId)
	 )

	INSERT INTO ExpenseType(ExpenseCategoryId, Name)
	VALUES
	(1,'Payroll'),
	(1,'Meralco'),
	(1,'Rent'),
	(1,'Machine Payment'),
	(1,'Investment Payment'),
	(1,'Maynilad'),
	(1,'Internet'),
	(2,'Globe'),
	(2,'Smart'),
	(3,'Honda Gas'),
	(3,'Honda Payment'),
	(3,'Honda Maintenance'),
	(3,'Multicab Gas'),
	(3,'Multicab Maintenance'),
	(4,'Slim Container'),
	(4,'Round Container'),
	(4,'Small Bottle'),
	(4,'Faucet Seal'),
	(4,'Big Mouth Seal'),
	(4,'Small Cap'),
	(4,'Round Seal'),
	(4,'Non Spill Cap'),
	(4,'Container Sticker'),
	(4,'Dispenser'),
	(5,'Water Lab Test'),
	(5,'Deep Well Maintenance'),
	(5,'Machine Maintenance'),
	(5,'Salt'),
	(5,'Filter'),
	(6,'Soap'),
	(6,'Zonrox'),
	(6,'Sponge'),
	(7,'Snack'),
	(7,'Lunch'),
	(8,'Delivery Receipt'),
	(8,'Construction'),
	(8,'Office Supplies'),
	(8,'Donation'),
	(8,'Business Tax/Permit'),
	(8,'Subd. Sticker'),
	(8,'Subd. Toll Fee'),
	(8,'Miscellaneous')
END


IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Expense')
BEGIN
	CREATE TABLE Expense
	(
		ExpenseId INT NOT NULL IDENTITY PRIMARY KEY,
		ExpenseDate DATETIME NOT NULL,
		ExpenseTypeId INT NOT NULL,
		Amount DECIMAL(9,2) NOT NULL,
		Remarks NVARCHAR(200),
		CONSTRAINT FK_Expense_ExpenseType FOREIGN KEY (ExpenseTypeId) REFERENCES ExpenseType(ExpenseTypeId)
	)
END
