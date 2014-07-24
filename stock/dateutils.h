#ifndef DATEUTILS_H
#define DATEUTILS_H

#include <QDateTime>

class DateUtils : public QObject
{
	Q_OBJECT

public slots:
	static QString dateTimeToStr(const QDateTime &date);
public:
	virtual ~DateUtils(){}
};

#endif // DATEUTILS_H
