#include "StdAfx.h"
#include "dateutils.h"

QString DateUtils::dateTimeToStr(const QDateTime &date)
{
	return date.toString("yyyy-MM-dd HH:mm:ss"); // 标准格式
}
