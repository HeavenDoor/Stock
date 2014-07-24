#ifndef XMLUTILS_H
#define XMLUTILS_H
#include <QObject>
#include <QList>
#include <QHash>
#include <QVariant>
#include <QMap>
#include <QString>
#include <QStringList>

class XmlUtils : public QObject
{
	Q_OBJECT
public slots:
	static QList<QHash<QString,QString>> readXml(QString fileName);
	static bool writeXml(QString fileName, QList<QHash<QString,QString>> list);
	static QVariantList ReadXml(QString fileName);
	static bool WriteXml(QString fileName, QVariantList list);
	static bool isElementExist(QString& fileName, QString& tagName, QString& tagContent);//判断指定标签,指定内容是否存在
	static bool appendItem(QString&fileName, QHash<QString, QString>& listItem );//在XML的根标记下增加一个
	static void removeItem(QString& fileName, QString& tagName, QString& tagValue);
public:
	virtual ~XmlUtils(){}
};
#endif