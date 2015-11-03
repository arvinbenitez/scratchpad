﻿IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='SmsMessage')
BEGIN
	CREATE TABLE SmsMessage
	(
		MessageId INT NOT NULL IDENTITY PRIMARY KEY,
		RemoteMessageId INT NOT NULL,
		SourcePhoneNumber NVARCHAR(30) NOT NULL,
		ReceivingPhoneNumber NVARCHAR(30) NOT NULL,
		ContactName NVARCHAR(1024) NOT NULL,
		[Message] NVARCHAR(1024) NOT NULL,
		[TimeStamp] NVARCHAR(100) NULL,
		[Status] INT NULL,
		Created DATETIME NOT NULL DEFAULT GETDATE(),
		Updated DATETIME NULL
	)
END