using System.Collections.Generic;
using DatabaseCopier.Library.Database;

namespace DatabaseCopier
{
    internal class Source
    {
        /* -- Run this script to generate the TableSources
DECLARE @Dependencies TABLE (TableName NVARCHAR(200), DependentOn NVARCHAR(200))

INSERT INTO @Dependencies
SELECT
	TableName = FK.TABLE_NAME,
	DependentOn = PK.TABLE_NAME
FROM 
	INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
	INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
	INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
	INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
	INNER JOIN (
		SELECT i1.TABLE_NAME, i2.COLUMN_NAME
		FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
		INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
		WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
	) PT ON PT.TABLE_NAME = PK.TABLE_NAME


SELECT 'list.Add(TableSource.ToTableSource("{ Name: ''' + T.TABLE_NAME + ''',TableDependencies:[' + ISNULL(D.Depends,'') + ']}"));'
FROM INFORMATION_SCHEMA.TABLES T
	LEFT JOIN 
	(
	SELECT TableName, Depends = 
		STUFF((SELECT ', ' + '''' + DependentOn + ''''
			   FROM @Dependencies b 
			   WHERE a.TableName = b.TableName
			  FOR XML PATH('')), 1, 2, '')
	FROM @Dependencies a
	GROUP BY TableName
	) D on T.TABLE_NAME = D.TableName
WHERE T.TABLE_TYPE='BASE TABLE'         
         */
        public static IList<TableSource> TableDefinitions()
        {
            var list = new List<TableSource>();

            list.Add(TableSource.ToTableSource("{ Name: 'DeliveryReceipt',TableDependencies:['Customer', 'Order']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'DeliveryReceiptDetail',TableDependencies:['DeliveryReceipt', 'ProductType']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'Order',TableDependencies:['OrderStatus']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'OrderDetail',TableDependencies:['Order', 'ProductType']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'OrderStatus',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'PickupSlip',TableDependencies:['Customer']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'PickupSlipDetail',TableDependencies:['PickupSlip', 'ProductType']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'ProductType',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'RefStatus',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'ReturnReceipt',TableDependencies:['Customer']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'ReturnReceiptDetail',TableDependencies:['ProductType', 'ReturnReceipt']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'Settings',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'UserAuth',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'UserOAuthProvider',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'DeliveryReceiptLedger',TableDependencies:['DeliveryReceipt']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'SalesInvoice',TableDependencies:['Customer']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'SalesInvoiceDetail',TableDependencies:['DeliveryReceiptLedger', 'SalesInvoice']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'InventoryLedgerType',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'InventoryLedger',TableDependencies:['Customer', 'InventoryLedgerType', 'ProductType']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'ExpenseCategory',TableDependencies:[]}"));
            list.Add(TableSource.ToTableSource("{ Name: 'ExpenseType',TableDependencies:['ExpenseCategory']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'Expense',TableDependencies:['ExpenseType']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'Customer',TableDependencies:['CustomerType']}"));
            list.Add(TableSource.ToTableSource("{ Name: 'CustomerType',TableDependencies:[]}"));

            return list;


        }
    }
}