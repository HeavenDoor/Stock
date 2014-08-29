crasdfa error

CREATE TABLE STOCK (
  STOCKNAME CHAR(50)  NOT NULL , 
  STOCKCODE CHAR(50) PRIMARY KEY);


CREATE TABLE USER (
  USERNAME CHAR(50)  PRIMARY KEY, 
  PASSWORD CHAR(50)  NOT NULL, 
  EMAIL CHAR(50) NOT NULL,
  PHONENUMBER CHAR(50) NOT NULL,
  PHONEID CHAR(100) NOT NULL,
  REGTIME DATETIME NOT NULL
 );

		
--drop TABLE USER
INSERT INTO USER VALUES('shenghai', '123465','test', 'test')
  
CREATE TABLE LASTUPDATE(
  LASTUPDATETIME DATE PRIMARY KEY
  );
  --drop TABLE lastupdate
  SELECT * FROM lastupdate
  INSERT INTO lastupdate VALUES('2014-8-15')
  INSERT INTO lastupdate VALUES('2014-7-15')
  INSERT INTO lastupdate VALUES('2014-7-25')
  
  UPDATE lastupdate SET LASTUPDATETIME='2014-5-13'
  DELETE FROM lastupdate
  
CREATE TABLE HOLIDAYS (
  HOLIDAY DATE PRIMARY KEY
);


INSERT INTO HOLIDAYS VALUES('2013-12-30'),('2013-12-31'),('2014-1-1'),('2014-1-30'),('2014-1-31'),('2014-2-3'),('2014-2-4'),('2014-2-5');
INSERT INTO HOLIDAYS VALUES('2014-4-7'),('2014-5-1'),('2014-5-2'),('2014-6-2'),('2014-9-8'),('2014-10-1'),('2014-10-2'),('2014-10-3');
INSERT INTO HOLIDAYS VALUES('2014-10-4'),('2014-10-5'),('2014-10-6'),('2014-10-7');
    
SELECT * FROM HOLIDAYS ORDER BY HOLIDAY DESC
DELETE FROM HOLIDAYS

CREATE TABLE TRADEDATE (
  STOCKTRADEDATE DATE PRIMARY KEY
);


SELECT * FROM TRADEDATE ORDER BY STOCKTRADEDATE DESC
--DROP TABLE TRADEDATE STOCKTRADEDATE>2014/8/8
DELETE FROM TRADEDATE WHERE STOCKTRADEDATE>2014/8/8

INSERT INTO TRADEDATE('2013-12-30')
INSERT INTO TRADEDATE('2013-12-29')
INSERT INTO TRADEDATE('2013-12-28')
INSERT INTO TRADEDATE('2013-12-27')
INSERT INTO TRADEDATE('2013-12-26')

SELECT * FROM TRADEDATE ORDER BY STOCKTRADEDATE DESC LIMIT 1

  
CREATE TABLE VALIDATION (
  PHONEID CHAR(100) NOT NULL,
  VALIDATIONCODE CHAR(10) NOT NULL,
  REGTIME DATETIME NOT NULL
);

SELECT * FROM VALIDATION
--drop TABLE VALIDATION

CREATE TABLE STOCKITEM(
 STOCKDATE DATE NOT NULL,
  STOCKCODE CHAR(50) NOT NULL,
  STOCKNAME CHAR(50) NOT NULL,  
  OPENPRICE DOUBLE NOT NULL,
  CLOSEPRICE  DOUBLE NOT NULL,
  HIGHESTPRICE DOUBLE NOT NULL,
  LOWESTPRICE DOUBLE NOT NULL,
  FLUCTUATEMOUNT DOUBLE NOT NULL, -- 涨跌额
  FLUCTUATERATE DOUBLE NOT NULL, -- 涨跌幅
  CHANGERATE DOUBLE NOT NULL, -- 换手率
  TRADEVOLUME DOUBLE NOT NULL, -- 成交量
  TRADEMOUNT DOUBLE NOT NULL, -- 成交金额
  TOATLMARKETCAP DOUBLE NOT NULL, -- 总市值
  CIRCULATIONMARKETCAP DOUBLE NOT NULL, -- 流通市值
  CONSTRAINT pk_a_ss PRIMARY KEY(`STOCKDATE`,`STOCKCODE`)
  );
  
-- 当日涨幅
CREATE TABLE STOCKITEM_DAILYFLUCTUATERATE(
  STOCKDATE DATE NOT NULL,
  STOCKCODE CHAR(50) NOT NULL,
  STOCKNAME CHAR(50) NOT NULL,  
  OPENPRICE DOUBLE NOT NULL,
  CLOSEPRICE  DOUBLE NOT NULL,
  HIGHESTPRICE DOUBLE NOT NULL,
  LOWESTPRICE DOUBLE NOT NULL,
  FLUCTUATEMOUNT DOUBLE NOT NULL, -- 涨跌额
  FLUCTUATERATE DOUBLE NOT NULL, -- 涨跌幅
  CHANGERATE DOUBLE NOT NULL, -- 换手率
  TRADEVOLUME DOUBLE NOT NULL, -- 成交量
  TRADEMOUNT DOUBLE NOT NULL, -- 成交金额
  TOATLMARKETCAP DOUBLE NOT NULL, -- 总市值
  CIRCULATIONMARKETCAP DOUBLE NOT NULL, -- 流通市值
  CONSTRAINT pk_a_ss PRIMARY KEY(`STOCKDATE`,`STOCKCODE`)
  );
  SELECT * FROM STOCKITEM_DAILYFLUCTUATERATE ORDER BY FLUCTUATERATE DESC

-- 当日换手率
CREATE TABLE STOCKITEM_DAILYCHANGERATE(
  STOCKDATE DATE NOT NULL,
  STOCKCODE CHAR(50) NOT NULL,
  STOCKNAME CHAR(50) NOT NULL,  
  OPENPRICE DOUBLE NOT NULL,
  CLOSEPRICE  DOUBLE NOT NULL,
  HIGHESTPRICE DOUBLE NOT NULL,
  LOWESTPRICE DOUBLE NOT NULL,
  FLUCTUATEMOUNT DOUBLE NOT NULL, -- 涨跌额
  FLUCTUATERATE DOUBLE NOT NULL, -- 涨跌幅
  CHANGERATE DOUBLE NOT NULL, -- 换手率
  TRADEVOLUME DOUBLE NOT NULL, -- 成交量
  TRADEMOUNT DOUBLE NOT NULL, -- 成交金额
  TOATLMARKETCAP DOUBLE NOT NULL, -- 总市值
  CIRCULATIONMARKETCAP DOUBLE NOT NULL, -- 流通市值
  CONSTRAINT pk_a_ss PRIMARY KEY(`STOCKDATE`,`STOCKCODE`)
);

SELECT * FROM STOCKITEM_DAILYFLUCTUATERATE ORDER BY CHANGERATE DESC


CREATE TABLE STOCKITEM_CHANGERATE_FLUCTUATERATE (
  STOCKCODE CHAR(50) NOT NULL,
  STOCKNAME CHAR(50) NOT NULL, 
  CLOSEPRICE  DOUBLE NOT NULL,
  FLUCTUATERATE DOUBLE NOT NULL, -- 涨跌幅
  CHANGERATE DOUBLE NOT NULL, -- 换手率
  CHANGERATEMAIN BOOL NOT NULL, -- 计算换手率还是涨幅 TRUE 换手率  FALSE 涨幅
  TRADEDAYS INT NOT NULL  -- 交易日
);


SELECT * FROM STOCKITEM_CHANGERATE_FLUCTUATERATE  WHERE TRADEDAYS=3 AND CHANGERATEMAIN=TRUE ORDER BY CHANGERATE DESC

SELECT * FROM STOCKITEM WHERE STOCKCODE='002640'

DROP TABLE STOCKITEM_CHANGERATE_FLUCTUATERATE

SELECT * FROM STOCKITEM_DAILYCHANGERATE ORDER BY CHANGERATE DESC
SELECT * FROM STOCKITEM WHERE STOCKDATE='2014/8/8' AND CHANGERATE!=0  ORDER BY CHANGERATE ASC LIMIT 0,15
  
  
ALTER TABLE stockitem ADD INDEX stockitem_index ( `STOCKDATE`,`STOCKCODE` );

SELECT * FROM stockitem ORDER BY stockcode ASC, stockdate DESC 

DROP INDEX stockitem_index ON stockitem 

SELECT STOCKCODE,STOCKNAME,SUM(CHANGERATE) AS RATE FROM STOCKITEM GROUP BY stockcode ORDER BY RATE DESC LIMIT 0,15

SELECT STOCKCODE, (SELECT CHANGERATE FROM stockitem ) AS price FROM STOCKITEM GROUP BY stockcode ORDER BY RATE DESC LIMIT 0,15

SELECT COUNT(*) FROM stock

SELECT bb.STOCKCODE ,bb.STOCKNAME,(SELECT ss.CLOSEPRICE FROM stockitem ss WHERE ss.stockdate='2014/8/8' AND ss.stockcode=bb.STOCKCODE) AS CLOSEPRICE,SUM(CHANGERATE) AS CHANGERATE, SUM(FLUCTUATERATE) AS FLUCTUATERATE FROM STOCKITEM bb WHERE STOCKDATE >='2014-8-7' GROUP BY stockcode ORDER BY FLUCTUATERATE DESC LIMIT 0,15

SELECT * FROM STOCKITEM WHERE stockcode='600399' ORDER BY stockdate DESC 
SELECT CLOSEPRICE FROM stockitem WHERE stockdate='2014/8/8'

SELECT * FROM TRADEDATE ORDER BY STOCKTRADEDATE DESC LIMIT 1000

SELECT B.STOCKCODE, B.STOCKNAME,(SELECT S.CLOSEPRICE FROM STOCKITEM S WHERE S.STOCKDATE='2014/8/8' AND S.STOCKCODE=B.STOCKCODE) AS CLOSEPRICE,SUM(CHANGERATE) AS CHANGERATE, SUM(FLUCTUATERATE) AS FLUCTUATERATE FROM STOCKITEM B WHERE B.STOCKDATE >='2014-8-7' GROUP BY B.STOCKCODE ORDER BY FLUCTUATERATE ASC LIMIT 0,15

SELECT * FROM STOCKITEM_CHANGERATE_FLUCTUATERATE WHERE CHANGERATEMAIN=FALSE AND TRADEDAYS=2 ORDER BY CHANGERATE DESC
SELECT * FROM STOCKITEM_CHANGERATE_FLUCTUATERATE WHERE CHANGERATEMAIN=TRUE AND TRADEDAYS=3 ORDER BY CHANGERATE DESC


SELECT * FROM STOCKITEM_CHANGERATE_FLUCTUATERATE WHERE CHANGERATEMAIN=FALSE AND TRADEDAYS=2

DELETE FROM STOCKITEM_CHANGERATE_FLUCTUATERATE WHERE CHANGERATEMAIN=FALSE AND AND TRADEDAYS=2

CREATE TABLE test (
  testname CHAR(50),
  testcode CHAR(50),
  testtext CHAR(50),
  CONSTRAINT pk_a_ss PRIMARY KEY(STOCKDATE,STOCKCODE)
  
);  

INSERT INTO test VALUES('a','b','c')
INSERT INTO test VALUES('a','d','c')
INSERT INTO test VALUES('e','b','c')

INSERT INTO test VALUES('a','b','q')
SELECT * FROM test












  
INSERT INTO Stock VALUES('通达股份','sz002560');
INSERT INTO Stock VALUES('华电国际','sh600027');
INSERT INTO Stock VALUES('国海证券','sz000750');

SELECT * FROM Stock
SELECT * FROM USER
SELECT * FROM lastupdate
SELECT * FROM STOCKITEM

ALTER  TABLE USER RENAME TO "YS"
DROP TABLE stockcode
--DROP TABLE STOCKITEM



UPDATE STOCK SET STOCKNAME='江淮动力' WHERE STOCKCODE='sz000750'


DELETE  FROM stock

SELECT * FROM stock WHERE stockcode='600004'



SELECT * FROM STOCKITEM WHERE STOCKDATE='2008/12/26'

INSERT INTO STOCKITEM VALUES('2008-12-26','002560','通达股份',1,1,1,1,11,1,11,1,1,11,11);
INSERT INTO STOCKITEM VALUES(1,1,1,1,11,1,11,1,1,11,11);

SELECT * FROM STOCKITEM WHERE STOCKCODE='002560'

DELETE FROM stockitem
INSERT  INTO `stockitem`(`STOCKDATE`,`STOCKCODE`,`STOCKNAME`,`OPENPRICE`,`CLOSEPRICE`,`HIGHESTPRICE`,`LOWESTPRICE`,`FLUCTUATEMOUNT`,`FLUCTUATERATE`,`CHANGERATE`,`TRADEVOLUME`,`TRADEMOUNT`,`TOATLMARKETCAP`,`CIRCULATIONMARKETCAP`) VALUES ('2014-08-08','000001','平安银行',10.53,10.54,10.62,10.46,0.04,0.381,0.8304,55565200,1204180,12041800,70524400)

SELECT * FROM STOCKITEM  WHERE stockdate='2014/8/8'  ORDER BY FLUCTUATERATE DESC LIMIT [0,15]

SELECT * FROM STOCKITEM WHERE stockdate='2014/8/8'  ORDER BY FLUCTUATERATE DESC LIMIT 0,5

SELECT * FROM STOCKITEM WHERE STOCKDATE='2014/8/8' ORDER BY FLUCTUATERATE DESC LIMIT 0,15