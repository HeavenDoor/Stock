#ifndef STOCK_H
#define STOCK_H

#include <QtGui/QWidget>

#include <QtNetwork/QNetworkAccessManager>
#include <QtNetwork/QNetworkRequest>
#include <QtNetwork/QNetworkReply>

class stock : public QWidget
{
	Q_OBJECT

public:
	stock(QWidget *parent = 0, Qt::WFlags flags = 0);
	~stock();
	void startQuery(QString redirect_url);
	QNetworkAccessManager* qnam;  
	QNetworkReply* reply; 
public slots:
	void slot_httpFinished();
};

#endif // STOCK_H
