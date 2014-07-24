#include "stdafx.h"
#include "clientdb.h"
#include "fileutils.h"
#include "dateutils.h"

ClientDb::ClientDb()
{
	hasBegunTransaction = false;
	toLower = false;//是否把返回结果的字段名转成小写，默认为 false
}

ClientDb::~ClientDb()
{
}

QVariant ClientDb::get_LastInsertedId()
{
	return this->lastInsertedId;
}

bool ClientDb::get_FieldNameToLower()
{
	return toLower;
}

void ClientDb::set_FieldNameToLower(const bool value)
{
	this->toLower = value;
}

QString ClientDb::get_DbName()
{
	return this->dbName;
}

void ClientDb::set_DbName(const QString &dbName)
{
	this->dbName = dbName;
	this->connectDb();
}

void ClientDb::connectDb()
{
	if(this->db.isOpen())
	{
		return;
	}
		
	QString dbFileDir = FileUtils::getFullPath(this->dbName);
	if(!FileUtils::existsFullPath(dbFileDir))
	{
		//throwError(tr("数据库 %0 不存在").arg(this->dbName));
		return;
	}
	db = QSqlDatabase::addDatabase("QSQLITE");

	qDebug() << "Available drivers:";  
	QStringList drivers = QSqlDatabase::drivers();  
	foreach(QString driver, drivers)  
		qDebug() << "/t" << driver;  


	db.setDatabaseName(dbFileDir);
	if (!db.open()) 
	{
		//throwError(tr("数据库 %0 连接失败，%1").arg(dbName).arg(db.lastError().text()));
		int m = 0;
	}
}

void ClientDb::closeDb()
{
	this->db.close();
	QSqlDatabase::removeDatabase(db.connectionName());
}

void ClientDb::beginTransaction()
{
	db.transaction();
	this->hasBegunTransaction = true;
}

bool ClientDb::checkHasBegunTransaction()
{
	if(!hasBegunTransaction)
	{
		//throwError("还没有 beginTransaction");
	}
	return hasBegunTransaction;
}

void ClientDb::commitTransaction()
{
	if(!checkHasBegunTransaction())
		return;
	db.commit();
	hasBegunTransaction = false;
}

void ClientDb::rollbackTransaction()
{
	if(!checkHasBegunTransaction())
		return;
	db.rollback();
	hasBegunTransaction = false;
}

bool ClientDb::throwQueryError(const QSqlQuery &query, const QString &action)
{
	//throwError(tr("%0，%1 SQL：\n%2").arg(query.lastError().text()).arg(action).arg(query.lastQuery()));
	return false;
}

bool ClientDb::checkExec(QSqlQuery &query)
{
	if (!query.exec())
	{
		return throwQueryError(query, "执行");
	}
	return true;
}

int ClientDb::exec(QSqlQuery &query)
{
	if (!checkExec(query))
	{
		return -1;
	}
	return query.numRowsAffected();
}

void ClientDb::bindQueryValue(QSqlQuery &query)
{
	if(this->parameterList.length() == 0) return;
	for(int i = 0; i < parameterList.length(); i++)
	{
		query.bindValue(parameterList[i].toString(),  valueList[i]);
	}
	this->parameterList.clear();
	this->valueList.clear();
}

QVariant formatValue(const QVariant &value)
{
	if (value.type() == QVariant::DateTime)
	{
		return DateUtils::dateTimeToStr(value.toDateTime());
	}
	return value;
}

int ClientDb::insert(const QString &tableName, const QVariantList &fields, const QVariantMap &values)
{
// 	if(throwArgumentNull(tableName.isEmpty(), "tableName"))
// 	{
// 		return -1;
// 	}
// 	if(throwArgumentNull(fields.isEmpty(), "fields"))
// 	{
// 		return -1;
// 	}
// 	if(throwArgumentNull(values.isEmpty(), "values"))
// 	{
// 		return -1;
// 	}

	QString fieldsStr1;
	QString fieldsStr2;
	int fieldCount = fields.length();
	for(int i = 0; i < fieldCount; i++)
	{
		if (i > 0)
		{
			fieldsStr1.append(",");
			fieldsStr2.append(",");
		}
		QString fieldName = fields[i].toString();
		fieldsStr1.append(fieldName);
		fieldsStr2.append("@").append(fieldName);
	}
	QString sql = QString("insert into %0 (%1) values (%2)").arg(tableName).arg(fieldsStr1).arg(fieldsStr2);

	QSqlQuery query = db.exec();
	if (!checkPrepare(query, sql))
	{
		//return -1;
	}

	for(int i = 0; i < fieldCount; i++)
	{
		QString fieldName = fields[i].toString();
		QVariant value = values.value(fieldName);
		query.bindValue(fieldName, formatValue(value));
	}

	int result = exec(query);
	this->lastInsertedId = query.lastInsertId();// 取最后插入的 Id
	return result;
}

bool ClientDb::checkPrepare(QSqlQuery &query, const QString &sql)
{
	QString ss = "\"insert into StockCode (Name,Code)\" \"values (@Name,@Code)\"";
	if (!query.prepare(sql))
	{
		return throwQueryError(query, "准备");
	}
	return true;
}


int ClientDb::update(const QString &tableName, const QString &primaryKey, const QVariantList &fields, const QVariantMap &values)
{
// 	if(throwArgumentNull(tableName.isEmpty(), "tableName"))
// 	{
// 		return -1;
// 	}
// 	if(throwArgumentNull(primaryKey.isEmpty(), "primaryKey"))
// 	{
// 		return -1;
// 	}
// 	if(throwArgumentNull(fields.isEmpty(), "fields"))
// 	{
// 		return -1;
// 	}
// 	if(throwArgumentNull(values.isEmpty(), "values"))
// 	{
// 		return -1;
// 	}

	QString fieldsStr;
	QString whereStr;
	int filedCount = fields.length();
	for(int i = 0; i < filedCount; i++)
	{
		if (i > 0)
		{
			fieldsStr.append(",");
		}
		QString fieldName = fields[i].toString();
		fieldsStr.append(fieldName).append("=@").append(fieldName);
	}
	whereStr.append(primaryKey).append("=@").append(primaryKey);
	QString sql = QString("update %0 set %1 where %2").arg(tableName).arg(fieldsStr).arg(whereStr);

	QSqlQuery query = db.exec();
	if (!checkPrepare(query, sql))
	{
		return -1;
	}
	for(int i = 0; i < filedCount; i++)
	{
		QString fieldName = fields[i].toString();
		QVariant value = values.value(fieldName);
		query.bindValue(fieldName, formatValue(value));
	}
	query.bindValue(primaryKey, values.value(primaryKey));
	return exec(query);
}

QVariantList ClientDb::select(const QString &sql)
{
	QVariantList list;
	if(sql.indexOf("select", 0, Qt::CaseInsensitive) < 0)
	{
		return list;
	}

	this->connectDb();

	QSqlQuery query = db.exec();
	query.setForwardOnly(true);
	if (!checkPrepare(query, sql))
	{
		return list;
	}
	this->bindQueryValue(query);
	if (!checkExec(query))
	{
		return list;
	}
	QStringList *keys = new QStringList();
	QString fieldName;
	QString key;
	for(int i = 0; i < query.record().count(); i++)
	{
		fieldName = query.record().fieldName(i);
		key = toLower ? fieldName.toLower() : fieldName;
		keys->append(key);
	}
	while(query.next())
	{
		QVariantMap row;
		for(int i = 0; i < query.record().count(); i++)
		{			
			row.insert(keys->at(i), query.value(i));
		}
		list.append(row);
	}
	delete(keys);
	return list;
}

QVariant ClientDb::selectFirstRow(const QString &sql)
{
	QVariantList list = select(sql);
	return list.count() > 0 ? list.value(0) : QVariant();
}

QVariant ClientDb::selectSingleRow(const QString &sql)
{
	QVariantList list = select(sql);
	int count = list.count();
	if (count != 1)
	{
		//throwError(QString("执行SQL“%0”时期待返回一行，现在返回了 %1 行").arg(sql).arg(count));
		return QVariant();
	}
	return list[0];
}

int ClientDb::del(const QString &tableName, const QString &field, const QVariant &value)
{
// 	if(throwArgumentNull(tableName.isEmpty(), "tableName"))
// 	{
// 		return -1;
// 	}
// 	if(throwArgumentNull(field.isEmpty(), "fields"))
// 	{
// 		return -1;
// 	}
// 	if(throwArgumentNull(value.isNull(), "value"))
// 	{
// 		return -1;
// 	}

	QString whereStr = QString("%0=@%1").arg(field).arg(field);
	QString sql = QString("delete from %0 where %1").arg(tableName).arg(whereStr);

	QSqlQuery query = db.exec();
	if (!checkPrepare(query, sql))
	{
		return -1;
	}
	query.bindValue(field, value);
	return exec(query);
}

int ClientDb::executeNonQuerySQL(const QString &sql)
{
// 	if(throwArgumentNull(sql.isEmpty(), "sql"))
// 	{
// 		return -1;
// 	}

	QSqlQuery query = db.exec();
	if (!checkPrepare(query, sql))
	{
		return -1;
	}
	this->bindQueryValue(query);
	return exec(query);
}

void ClientDb::batchExecute(const QVariantList &sqlList)
{
 	for ( int i = 0; i < sqlList.size(); i++)
 	{
 		this->db.exec(sqlList.at(i).toString());
 	}
}

void ClientDb::addParameter(const QString &parameterName, const QVariant &value)
{
	this->parameterList.append(parameterName);
	this->valueList.append(value);
}