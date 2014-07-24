#ifndef CLIENTDB_H
#define CLIENTDB_H

#include <QtSql>
#include <QSqlDatabase>
#include <QSqlError>
#include <QSqlQuery>
#include <QtSql/QSqlQuery>

class ClientDb : public QObject
{
	Q_OBJECT
	Q_PROPERTY(bool FieldNameToLower READ get_FieldNameToLower WRITE set_FieldNameToLower)
	Q_PROPERTY(QVariant LastInsertedId READ get_LastInsertedId)
	Q_PROPERTY(QString DbName READ get_DbName WRITE set_DbName)

public slots:
	void beginTransaction();
	void commitTransaction();
	void rollbackTransaction();
	int insert(const QString &tableName, const QVariantList &fields, const QVariantMap &values);
	int update(const QString &tableName, const QString &primaryKey, const QVariantList &fields, const QVariantMap &values);
	QVariantList select(const QString &sql);
	int del(const QString &tableName, const QString &field, const QVariant &value);
	QVariant selectFirstRow(const QString &sql);
	QVariant selectSingleRow(const QString &sql);
	int executeNonQuerySQL(const QString &sql);
	void batchExecute(const QVariantList &sqlList);
	void addParameter(const QString &parameterName, const QVariant &value);

public:
	ClientDb();
	virtual ~ClientDb();

	bool get_FieldNameToLower();
	void set_FieldNameToLower(const bool value);
	QVariant get_LastInsertedId();
	QString get_DbName();
	void set_DbName(const QString &dbName);

	void connectDb();
	void closeDb();
private:
	QString dbName;
	QVariant lastInsertedId;
	QVariantList parameterList;
	QVariantList valueList;
	QSqlDatabase db;
	bool toLower;
	bool hasBegunTransaction;
	bool checkHasBegunTransaction();
	bool checkPrepare(QSqlQuery &query, const QString &sql);
	bool checkExec(QSqlQuery &query);
	bool throwQueryError(const QSqlQuery &query, const QString &action);
	int exec(QSqlQuery &query);
	void bindQueryValue(QSqlQuery &query);
};

#endif // BUTTON_H
