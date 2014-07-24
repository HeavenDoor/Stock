#include "stdafx.h"
#include "stock.h"
#include "xmlutils.h"
#include "clientdb.h"
#include <QDebug>
#include <QLabel>

stock::stock(QWidget *parent, Qt::WFlags flags) : QWidget(parent, flags)
{
	setGeometry(400,200, 800,600);
	qDebug() << "Available drivers:";  
	QStringList drivers = QSqlDatabase::drivers();  
	foreach(QString driver, drivers)  
		qDebug() << "/t" << driver;  
	QVariantList li;
	QStringList a;
	a.push_back("Name");
	QStringList b;
	b.push_back("Code");
	li.insert(0, a);
	li.insert(1, b);
 	ClientDb db;
 	db.set_DbName("C:\\Users\\shenghai\\Desktop\\stock\\Debug\\stock.db");
 	db.connectDb();
	/*QVariantList vlist = XmlUtils::ReadXml("C:\\Users\\SH\\Desktop\\stock\\stock\\st.xml");//C:\\Users\\SH\\Desktop\\stock\\stock\\

	for (int i = 0; i < vlist.length(); i++)
	{
		QString code = vlist.at(i).toMap().value("f").toString();
		QString name = vlist.at(i).toMap().value("n").toString();
		QVariantMap m;
		m.insert("Name",name);
		m.insert("Code", code);
		db.insert("StockCode",li,m);
	}*/

 }

stock::~stock()
{

}


void stock::startQuery(QString redirect_url)  
{  
    QNetworkRequest request;  
    QString url;  
    // 如果是重定向请求， 则直接指向位置， 否则拼字符串  
    if (redirect_url.length() != 0)  
    {  
        url = redirect_url;  
    }else  
    {  
        //url = "http://www.nuihq.com/" + query_word;  
    }  
    // 设定url  
    request.setUrl(QUrl(url));  
    // 设定请求头  
//     request.setRawHeader("Host", "quote.eastmoney.com");  
//     request.setRawHeader("User-Agent", "Mozilla/5.0 (X11; Linux x86_64; rv:7.0.1) Gecko/20100101 Firefox/7.0.1");  
//     request.setRawHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");  
//     request.setRawHeader("Accept-Language", "zh-cn,zh;q=0.5");  
//     // TODO:使用gzip  
//     request.setRawHeader("Accept-Encoding", "deflate");  
//     request.setRawHeader("Accept-Charset", "utf-8;q=0.7,*;q=0.7");  
//     request.setRawHeader("Connection", "keep-alive");  
  
    // 使用get方式发起请求  
	qnam = new QNetworkAccessManager();  
    reply = qnam->get(request);  
    connect(reply, SIGNAL(finished()), this, SLOT(slot_httpFinished()));  
} 

void stock::slot_httpFinished()  
{  
	int http_status = reply->attribute(QNetworkRequest::HttpStatusCodeAttribute).toInt();  

	reply->close();  
	reply->deleteLater();  

	// 判断是否是重定向  
	if (http_status == 302)  
	{  
		startQuery(reply->rawHeader("Location"));  
	}else  
	{  
		QString reply_content = QString::fromUtf8(reply->readAll());  
		if (reply->error() == QNetworkReply::NoError)  
		{  
			//saveToFile(reply_content);  
		}else  
		{  
			//qDebug() << "ERROR:" << query_word << " CODE:" << reply->error();  
		}  
	}  
} 
