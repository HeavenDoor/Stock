#include "stdafx.h"
#include "xmlutils.h"
#include <QFile>
#include <QDomDocument>
#include <QTextStream>
#include <Windows.h>

QList<QHash<QString,QString>> XmlUtils::readXml(QString fileName)
{
	QFile xmlFile(fileName);
	QDomDocument doc;
	QList<QHash<QString,QString>> list;
	QHash<QString,QString> hash;

	if (!xmlFile.open(QIODevice::ReadOnly))
	{
		return list;//配置文件打开失败
	}
	if (!doc.setContent(&xmlFile))
	{
		int m = GetLastError();
		xmlFile.close();
		return list;
	}
	QDomElement root = doc.documentElement();
	QDomNodeList item = root.childNodes();
	list.clear();
	hash.clear();
	for (int i = 0; i < item.size(); ++i)
	{
		QDomNodeList node = item.at(i).childNodes();
		for (int j = 0; j < node.size(); ++j)
		{
			hash.insert(node.at(j).nodeName(), node.at(j).toElement().text());
		}
		list.append(hash);
		hash.clear();
	}
	return list;
}

bool XmlUtils::writeXml(QString fileName, QList<QHash<QString,QString>> list)
{
	QStringList keys;
	//keys = list.at(0).keys();
	QFile xmlFile(fileName);
	QDomDocument doc;
	QDomElement root = doc.createElement("config");
	doc.appendChild(root);
	for (int i = 0; i < list.size(); ++i)
	{
		QHash<QString, QString> temp;
		temp = list.at(i);
		keys = temp.keys();
		QDomElement item = doc.createElement("item");
		root.appendChild(item);
		for (int j = 0; j < keys.size(); ++j)
		{
			QDomElement node = doc.createElement(keys.at(j));
			node.appendChild(doc.createTextNode(temp.value(keys.at(j))));
			item.appendChild(node);
		}
		root.appendChild(item);
		item.clear();
	}
	xmlFile.close();
	if (!xmlFile.open(QIODevice::WriteOnly))
	{
		return false;//配置文件写入失败
	}
	QTextStream out(&xmlFile);
	doc.save(out, 1);
	xmlFile.close();
	return true;
}

QVariantList XmlUtils::ReadXml(QString fileName)
{
	QVariantList mapList;
	QList<QHash<QString, QString>> hashList = readXml(fileName);
	if (hashList.length()<1)
	{
		return mapList;
	}
	QStringList keys;
	for (int i = 0; i < hashList.size(); ++i)
	{
		QHash<QString, QString> hashtemp;
		QMap<QString, QVariant> maptemp;
		hashtemp = hashList.at(i);
		keys = hashtemp.keys();
		for (int j = 0; j < hashtemp.size(); ++j)
		{
			maptemp.insert(keys[j], hashtemp.value(keys[j]));
		}
		mapList.append(maptemp);
	}

	return mapList;
}

bool XmlUtils::WriteXml(QString fileName, QVariantList list)
{
	QList<QVariantMap> mapList;
	QList<QHash<QString, QString>> hashList;
	QStringList keys;

	for (int i = 0; i < list.size(); ++i)
	{
		QVariant variant = list.at(i);
		QVariantMap *map;
		map = (QVariantMap *)&variant;
		mapList.append(*map);
	}

	for (int i = 0; i < mapList.size(); ++i)
	{
		QHash<QString, QString> hashtemp;
		QVariantMap maptemp;
		maptemp = mapList.at(i);
		keys = maptemp.keys();
		for (int j = 0; j < maptemp.size(); ++j)
		{
			hashtemp.insert(keys[j], maptemp.value(keys[j]).toString());
		}
		hashList.append(hashtemp);
	}
	return writeXml(fileName, hashList);
}

bool XmlUtils::isElementExist(QString& fileName, QString& tagName, QString& tagContent)
{
	QFile xmlFile(fileName);
	QDomDocument doc;
	QList<QHash<QString,QString>> list = readXml(fileName);

	if(list.isEmpty())
		return false;
	for(QList<QHash<QString, QString>>::iterator it  = list.begin(); it!=list.end(); ++it)
	{
		if(it->value(tagName) == tagContent)
		{
			return true;
		}
	}
	return false;
}

bool XmlUtils::appendItem(QString& fileName, QHash<QString, QString>& listItem )
{
	if(listItem.isEmpty())
		return false;
	QList<QHash<QString,QString>> list = readXml(fileName);
	list.append(listItem);
	return writeXml(fileName, list);
}

void XmlUtils::removeItem(QString& fileName, QString& tagName, QString& tagValue)
{
	QList<QHash<QString,QString>> list = readXml(fileName);
	if(list.isEmpty())
		return ;

	for(QList<QHash<QString, QString>>::iterator it  = list.begin(); it!=list.end(); ++it)
	{
		if(it->value(tagName) == tagValue)
		{
			list.removeOne(*it);
			writeXml(fileName, list);
			return;
		}
	}
}