use master;
GO
--DROP DATABASE lClient1;
GO
CREATE DATABASE lgtest;
GO

USE lgtest;

IF OBJECT_ID('lists') IS NULL
CREATE TABLE lists (id INT NOT NULL PRIMARY KEY, lnum INT DEFAULT 0, [Name] VARCHAR(200) );

IF OBJECT_ID('listname') IS NULL
BEGIN
	CREATE TABLE listname (idList INT NOT NULL, id INT NOT NULL, id2 INT NOT NULL DEFAULT 0, isdel INT NOT NULL DEFAULT 0,
		[Name] VARCHAR(1000), Comment VARCHAR(MAX), name2 VARCHAR(1000), name3 VARCHAR(1000), chdate DATE , cuser INT  );
	ALTER TABLE listname ADD CONSTRAINT PK_listname PRIMARY KEY (idList, id, id2);
END;

IF OBJECT_ID('users') IS NULL
CREATE TABLE users (id INT NOT NULL Primary KEY, [name] VARCHAR(100), Shortname VARCHAR(100), Pass VARCHAR(50),
	isdel INT NOT NULL DEFAULT 0, tp  INT NOT NULL DEFAULT 0, AdminUser VARCHAR(100), SECLEVEL INT, OnlyOWn INT, CrDate DATE, chdate DATE , cuser INT );

IF OBJECT_ID('UserCond') IS NULL
BEGIN
	CREATE TABLE UserCond (idUser INT NOT NULL, idCond INT NOT NULL, id INT NOT NULL, id2 INT NOT NULL DEFAULT 0,
		[name] VARCHAR(1000), name2  VARCHAR(1000), cmnt VARCHAR(MAX) );
	ALTER TABLE UserCond ADD CONSTRAINT PK_UserCond PRIMARY KEY (idUser, idCond, id, id2);
END;
-- *********************************************************************************************************************
IF OBJECT_ID('lCust') IS NULL 
BEGIN 
	CREATE TABLE lCust (id INT  NOT NULL DEFAULT 0 , yr INT  NOT NULL DEFAULT 0 , isdel INT  NOT NULL DEFAULT 0 , product INT , lot VARCHAR(100), 
		supplier INT , Client INT , splace INT , warehouse INT , luser INT , delivery INT , lparam1 INT , lparam2 INT , lparam3 INT , 
		weightp FLOAT , cntp FLOAT , weight FLOAT , cnt FLOAT , weight_w FLOAT , cnt_w FLOAT , valplan FLOAT , valfact FLOAT , ETD_plan DATE , 
		ETD_FACT DATE , ETA_plan DATE , ETA_fact DATE , transi VARCHAR(1000) , vallogist FLOAT , pay1_sum FLOAT , pay1_bill VARCHAR(1000) , 
		pay1_dplan DATE , pay1_dfact DATE , pay2_sum FLOAT , pay2_bill VARCHAR(1000) , pay2_dplan DATE , pay2_dfact DATE , pay3_sum FLOAT , 
		pay3_bill VARCHAR(1000) , pay3_dplan DATE , pay3_dfact DATE , docs VARCHAR(1000) , doc_dplan DATE , doc_dfact DATE ,
		dparam1 DATE , dparam2 DATE , fparam1 FLOAT , fparam2 FLOAT , sparam1 VARCHAR(1000) , sparam2 VARCHAR(1000) , cmnt VARCHAR(MAX) , 
		lcmnt VARCHAR(MAX) , chdate DATE , cuser INT ); 
	ALTER TABLE lCust ADD CONSTRAINT PK_lCust PRIMARY KEY (id); 
END;
IF OBJECT_ID('lCustLog') IS NULL 
BEGIN 
	CREATE TABLE lCustLog (id INT  NOT NULL DEFAULT 0 , yr INT  NOT NULL DEFAULT 0 , isdel INT  NOT NULL DEFAULT 0 , product INT , lot VARCHAR(100),
		supplier INT , Client INT , splace INT , warehouse INT , luser INT , delivery INT , lparam1 INT , lparam2 INT , lparam3 INT , 
		weightp FLOAT , cntp FLOAT , weight FLOAT , cnt FLOAT , weight_w FLOAT , cnt_w FLOAT , valplan FLOAT , valfact FLOAT , 
		ETD_plan DATE , ETD_FACT DATE , ETA_plan DATE , ETA_fact DATE , transi VARCHAR(1000) , vallogist FLOAT , pay1_sum FLOAT ,
		pay1_bill VARCHAR(1000) , pay1_dplan DATE , pay1_dfact DATE , pay2_sum FLOAT , pay2_bill VARCHAR(1000) , pay2_dplan DATE , 
		pay2_dfact DATE , pay3_sum FLOAT , pay3_bill VARCHAR(1000) , pay3_dplan DATE , pay3_dfact DATE , docs VARCHAR(1000) , 
		doc_dplan DATE , doc_dfact DATE , dparam1 DATE , dparam2 DATE , fparam1 FLOAT , fparam2 FLOAT , sparam1 VARCHAR(1000) , 
		sparam2 VARCHAR(1000) , cmnt VARCHAR(MAX) , lcmnt VARCHAR(MAX) , chdate DATE , cuser INT , idint INT NOT NULL, actime VARCHAR(8), 
		compname VARCHAR(500), actn VARCHAR(100) ); 
	ALTER TABLE lCustLog ADD CONSTRAINT PK_lCustLog PRIMARY KEY (id, idint); 
END;

IF OBJECT_ID('lras') IS NULL
BEGIN
	CREATE TABLE lras (id INT NOT NULL, idint INT NOT NULL, isdel INT NOT NULL DEFAULT 0, yr INT, classras INT, nameras INT, 
		company INT, rasn VARCHAR(1000), val FLOAT, nn VARCHAR(100), dplan DATE, dfact DATE, culprit VARCHAR(1000), rval FLOAT,
		rdate DATE, cmnt VARCHAR(MAX), chdate DATE, cuser INT);
	ALTER TABLE lras ADD CONSTRAINT PK_LRAS PRIMARY KEY (id, idint); 
END;

GO

CREATE OR ALTER TRIGGER Trigger_lCustLog ON lCust 
	FOR UPDATE AS 
BEGIN 
	DECLARE @isDEL int, @actn VARCHAR(50), @id INT; 
	SELECT @isDEL = isDel, @id = id FROM inserted; 
	IF (@isDEL IS NOT NULL) 
	BEGIN 
		SET @actn = CASE WHEN @isDEL = 1 THEN 'Удален' 
		WHEN @isDEL = 2 THEN 'Отправлен в архив' 
		WHEN NOT EXISTS (SELECT 1 FROM lCustLog WHERE id = @id) THEN 'Создан' 
		ELSE 'Изменен' 
					END; 
	END;
	
	INSERT INTO lCustLog (id, yr, isdel, product, lot, supplier, Client, splace, warehouse, luser, 
		delivery, lparam1, lparam2, lparam3, weightp, cntp, weight, cnt, weight_w, cnt_w, valplan, valfact, ETD_plan, 
		ETD_FACT, ETA_plan, ETA_fact, transi, vallogist, pay1_sum, pay1_bill, pay1_dplan, pay1_dfact, pay2_sum, pay2_bill, 
		pay2_dplan, pay2_dfact, pay3_sum, pay3_bill, pay3_dplan, pay3_dfact, docs, doc_dplan, doc_dfact, dparam1, dparam2, 
		fparam1, fparam2, sparam1, sparam2, cmnt, lcmnt, chdate, cuser, actime , compname, actn, idint ) 
 
	SELECT id, yr, isdel, product, lot, supplier, Client, splace, warehouse, luser, delivery, lparam1, lparam2, lparam3, weightp, 
		cntp, weight, cnt, weight_w, cnt_w, valplan, valfact, ETD_plan, ETD_FACT, ETA_plan, ETA_fact, transi, vallogist, pay1_sum, pay1_bill,
		pay1_dplan, pay1_dfact, pay2_sum, pay2_bill, pay2_dplan, pay2_dfact, pay3_sum, pay3_bill, pay3_dplan, pay3_dfact, docs, doc_dplan, 
		doc_dfact, dparam1, dparam2, fparam1, fparam2, sparam1, sparam2, cmnt, lcmnt, chdate, cuser, FORMAT(GETDATE(), 'HH.mm.ss') , 
		HOST_NAME() , @actn, 
		CASE WHEN NOT EXISTS (SELECT 1 FROM lCustLog WHERE id = @id) THEN 1 ELSE (SELECT MAX(idint)+1 FROM lCustLog WHERE id = @id) END 
	FROM lCust WHERE id = @id; 
END;

GO
IF OBJECT_ID('lLog') IS NULL
BEGIN
	CREATE TABLE lLog (LogDt DATE NOT NULL, id INT NOT NULL, ProgUser INT, Stats INT, isdel INT NOT NULL DEFAULT 0,
		oper INT, Param1 INT, Param2 INT, WinUser VARCHAR(100), CompName VARCHAR(200), param3 VARCHAR(200), LogTime VARCHAR(10) );
	ALTER TABLE lLog ADD CONSTRAINT PK_lLog PRIMARY KEY (LogDt, id);
END;

IF NOT EXISTS (SELECT 1 FROM users WHERE id = 0) 
	INSERT INTO users (id, [name], Shortname, tp, pass, CrDate, OnlyOWn, SECLEVEL)
	VALUES (0, 'Administrator', 'Administrator', 0, 'nov456', CAST(GETDATE() AS DATE), 0, 0);

IF NOT EXISTS (SELECT 1 FROM lists WHERE id=1) INSERT INTO lists (id, [name]) VALUES (1, 'Продукт');
IF NOT EXISTS (SELECT 1 FROM lists WHERE id=2) INSERT INTO lists (id, [name]) VALUES (2, 'Поставщик');
IF NOT EXISTS (SELECT 1 FROM lists WHERE id=3) INSERT INTO lists (id, [name]) VALUES (3, 'Покупатель');
IF NOT EXISTS (SELECT 1 FROM lists WHERE id=4) INSERT INTO lists (id, [name]) VALUES (4, 'Пункт отправки');
IF NOT EXISTS (SELECT 1 FROM lists WHERE id=5) INSERT INTO lists (id, [name]) VALUES (5, 'Пункт прихода');
IF NOT EXISTS (SELECT 1 FROM lists WHERE id=6) INSERT INTO lists (id, [name]) VALUES (6, 'Условия поставки');
IF NOT EXISTS (SELECT 1 FROM lists WHERE id=7) INSERT INTO lists (id, [name]) VALUES (7, 'Классификация расходов');
IF NOT EXISTS (SELECT 1 FROM lists WHERE id=8) INSERT INTO lists (id, [name]) VALUES (8, 'РАСХОДЫ');
IF NOT EXISTS (SELECT 1 FROM lists WHERE id=20) INSERT INTO lists (id, [name], lnum) VALUES (20, 'Перевозчик', 1);

IF NOT EXISTS (SELECT 1 FROM listname WHERE idList=6) INSERT INTO listname (idList, id, [name])
	VALUES (6, 1, 'Доставка'), (6, 2, 'Самовывоз');

IF NOT EXISTS (SELECT 1 FROM listname WHERE idList=8) INSERT INTO listname (idList, id, [name], id2)
	VALUES (8, 1, 'Перевозчик', 20);

IF NOT EXISTS (SELECT 1 FROM listname WHERE idList=7) INSERT INTO listname (idList, id, [name], id2)
	VALUES (7, 1, 'Включенные в СС', 0), (7, 2, 'Доп.расход', 0) ;

GO

CREATE OR ALTER VIEW GetCustParam AS
SELECT t.id AS id, t.yr AS yr, t.isdel AS isdel, t.lot AS lot, 
	(SELECT l.[name] FROM listname l WHERE l.idList=1 AND l.id=t.product) AS product,
	(SELECT l.[name] FROM listname l WHERE l.idList=2 AND l.id=t.supplier) AS supplier,
	(SELECT l.[name] FROM listname l WHERE l.idList=3 AND l.id=t.client) AS client,		
	(SELECT l.[name] FROM listname l WHERE l.idList=4 AND l.id=t.splace) AS splase,
	(SELECT l.[name] FROM listname l WHERE l.idList=5 AND l.id=t.warehouse) AS warehouse,
	(SELECT l.[name] FROM listname l WHERE l.idList=6 AND l.id=t.delivery) AS delivery,
	(SELECT u.shortname FROM users u WHERE u.id=t.luser) AS luser,
	(CASE WHEN t.etd_plan IS NULL THEN 'БЕЗ ПЛАНА' WHEN t.etd_fact IS NULL THEN 'Запланирован'
                WHEN t.eta_fact IS NULL THEN 'В пути' ELSE 'Выгружен' END) AS stat

FROM lCust t;

GO

CREATE OR ALTER TRIGGER Trigger_ObserverU ON users
	FOR INSERT, UPDATE, DELETE AS
BEGIN
	DECLARE @u INT;
	SELECT @u = cuser FROM inserted
	IF (SELECT SecLevel FROM users u WHERE u.id=@u) > 0
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR ('Недостаточно прав для редактирования данных!', 16,1);
	END;

	SELECT @u = cuser FROM deleted
	IF (SELECT SecLevel FROM users u WHERE u.id=@u) > 0
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR ('Недостаточно прав для редактирования данных!', 16,1);
	END;
END;

GO

CREATE OR ALTER TRIGGER Trigger_ObserverL ON listname
	FOR INSERT, UPDATE, DELETE AS
BEGIN
	DECLARE @u INT;
	SELECT @u = cuser FROM inserted
	IF (SELECT SecLevel FROM users u WHERE u.id=@u) >= 2
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR ('Недостаточно прав для редактирования данных!', 16,1);
	END;

	SELECT @u = cuser FROM deleted
	IF (SELECT SecLevel FROM users u WHERE u.id=@u) >= 2
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR ('Недостаточно прав для редактирования данных!', 16,1);
	END;
END;

GO

CREATE OR ALTER TRIGGER Trigger_ObserverC ON lCust
	FOR INSERT, UPDATE, DELETE AS
BEGIN
	DECLARE @u INT;
	SELECT @u = cuser FROM inserted
	IF (SELECT SecLevel FROM users u WHERE u.id=@u) >= 2
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR ('Недостаточно прав для редактирования данных!', 16,1);
	END;

	SELECT @u = cuser FROM deleted
	IF (SELECT SecLevel FROM users u WHERE u.id=@u) >= 2
	BEGIN
		ROLLBACK TRANSACTION;
		RAISERROR ('Недостаточно прав для редактирования данных!', 16,1);
	END;
END;

GO

CREATE OR ALTER FUNCTION LpayFUNC(@yr int)
	RETURNS @pf TABLE ( yr INT, id INT, isdel INT, stat VARCHAR(500), lot VARCHAR(100), luser VARCHAR(500), 
	client VARCHAR(500), supplier VARCHAR(500), product VARCHAR(500), valfact FLOAT, 
	delivery VARCHAR(500), splace VARCHAR(500), warehouse VARCHAR(500),  
	etd_plan DATE, etd_fact DATE, eta_fact DATE, 
	 dplan DATE, dfact DATE, sumfact FLOAT, 
	 weightp FLOAT, [weight] FLOAT,  cmnt VARCHAR(MAX), pay_stat VARCHAR(100), pay_name VARCHAR(100)
) AS
BEGIN

	DECLARE @id int, @pstat varchar(100), @i INT;
	DECLARE @pmt varchar(100), @sum1 float, @sum2 float, @dplan DATE;
	DECLARE @dfact DATE,  @pr float, @bsum1 float, @bfact DATE, @bsum2 FLOAT, @bplan DATE, @bp FLOAT;
	
	DECLARE PayCur CURSOR FOR
		SELECT s.id FROM lCust s 
			WHERE (s.isdel=0 OR s.isdel=2)
			AND ((s.pay1_sum is not NULL and s.pay1_sum<>0) OR (s.pay2_sum is not NULL and s.pay2_sum<>0) OR
			(s.pay3_sum is not NULL and s.pay3_sum<>0)) AND s.id>0 AND s.yr=@yr
			ORDER BY (case when etd_plan is null AND etd_fact is null THEN 1000 WHEN etd_fact is not null THEN DATEPART(wk, etd_fact)	
				ELSE DATEPART(wk, etd_plan) END) DESC;

	OPEN payCur;
	FETCH NEXT FROM PayCur INTO @id;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		SELECT @i=CASE WHEN pay1_sum IS NOT NULL AND pay1_sum<>0 THEN 1 ELSE 0 END, @dplan=pay1_dplan, @dfact=pay1_dfact,
			@sum1=pay1_sum FROM lCust WHERE id=@id;
		IF @i = 1
		BEGIN
			SET @pstat = CASE WHEN @dplan IS NULL AND @dfact IS NULL THEN 'Не оплачен'
				WHEN @dfact IS NULL AND @dplan >= CAST(GETDATE() AS DATE) THEN 'Не оплачен'
				WHEN @dfact IS NULL AND @dplan < CAST(GETDATE() AS DATE) THEN 'Просрочен'
				WHEN @dfact IS NOT NULL THEN 'Оплачен' END;

			INSERT INTO @pf (id, sumfact, pay_name, pay_stat, dplan, dfact) VALUES (@id, @sum1, 'ОПЛАТА 1', @pstat, @dplan, @dfact);
		END;

		SELECT @i=CASE WHEN pay2_sum IS NOT NULL AND pay2_sum<>0 THEN 1 ELSE 0 END, @dplan=pay2_dplan, @dfact=pay2_dfact,
			@sum1=pay2_sum FROM lCust WHERE id=@id;
		IF @i = 1
		BEGIN
			SET @pstat = CASE WHEN @dplan IS NULL AND @dfact IS NULL THEN 'Не оплачен'
				WHEN @dfact IS NULL AND @dplan >= CAST(GETDATE() AS DATE) THEN 'Не оплачен'
				WHEN @dfact IS NULL AND @dplan < CAST(GETDATE() AS DATE) THEN 'Просрочен'
				WHEN @dfact IS NOT NULL THEN 'Оплачен' END;

			INSERT INTO @pf (id, sumfact, pay_name, pay_stat, dplan, dfact) VALUES (@id, @sum1, 'ОПЛАТА 2', @pstat, @dplan, @dfact);
		END;
		
		SELECT @i=CASE WHEN pay3_sum IS NOT NULL AND pay3_sum<>0 THEN 1 ELSE 0 END, @dplan=pay3_dplan, @dfact=pay3_dfact,
			@sum1=pay3_sum FROM lCust WHERE id=@id;
		IF @i = 1
		BEGIN
			SET @pstat = CASE WHEN @dplan IS NULL AND @dfact IS NULL THEN 'Не оплачен'
				WHEN @dfact IS NULL AND @dplan >= CAST(GETDATE() AS DATE) THEN 'Не оплачен'
				WHEN @dfact IS NULL AND @dplan < CAST(GETDATE() AS DATE) THEN 'Просрочен'
				WHEN @dfact IS NOT NULL THEN 'Оплачен' END;

			INSERT INTO @pf (id, sumfact, pay_name, pay_stat, dplan, dfact) VALUES (@id, @sum1, 'ОПЛАТА 3', @pstat, @dplan, @dfact);
		END;
       
		FETCH NEXT FROM PayCur INTO @id;
	END;

	CLOSE payCur;
	DEALLOCATE payCur;

	UPDATE ct SET ct.stat=(CASE WHEN t.etd_plan IS NULL THEN 'БЕЗ ПЛАНА' WHEN t.etd_fact IS NULL THEN 'Запланирован' 
                WHEN t.eta_fact IS NULL THEN 'В пути' ELSE 'Выгружен' END), ct.lot=t.lot, 
				ct.luser=(SELECT [shortname] FROM users WHERE id=t.luser), 
				ct.client=(SELECT [name] FROM listname WHERE idList=3 AND id=t.client),
				ct.supplier=(SELECT [name] FROM listname WHERE idList=2 AND id=t.supplier),
				ct.product=(SELECT [name] FROM listname WHERE idList=1 AND id=t.product),
				ct.valfact=t.valfact, 
				ct.delivery=(SELECT [name] FROM listname WHERE idList=6 AND id=t.delivery),
				ct.splace=(SELECT [name] FROM listname WHERE idList=4 AND id=t.splace), 
				ct.warehouse=(SELECT [name] FROM listname WHERE idList=5 AND id=t.warehouse), 
				ct.etd_plan=t.etd_plan, ct.etd_fact=t.etd_fact, ct.eta_fact=t.eta_fact,
				ct.weightp=t.weightp, ct.weight=t.weight, ct.cmnt=t.cmnt, ct.isdel=t.isdel
				FROM (SELECT * FROM @pf) ct INNER JOIN lCust t ON ct.id = t.id;

	RETURN;
END;