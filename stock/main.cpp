#include "stdafx.h"
#include "stock.h"
#include <QtGui/QApplication>
#include <QTranslator>
#include <QTextCodec>
#include <QTextStream>
#include <QFile>
#include <QFileInfo>
#include <QDir>
#include <QDialogButtonBox>
#include <QProcess>
#include <QSettings>
#include <QtPlugin>
Q_IMPORT_PLUGIN(qsqlite)

void initCodec();
int main(int argc, char *argv[])
{
	initCodec();
	QApplication a(argc, argv);
	stock w;
	w.show();
	w.startQuery("http://quote.eastmoney.com/stocklist.html#sz");
	return a.exec();
}


void initCodec()
{
	QTextCodec *codec = QTextCodec::codecForName("System"); //GB2312
	QTextCodec::setCodecForLocale(codec);
	QTextCodec::setCodecForCStrings(codec);
	QTextCodec::setCodecForTr(codec);

	QTranslator *translator = new QTranslator(); // 必须用指针，否则被释放就无效了
	translator->load("qt_zh_CN", QApplication::applicationDirPath(), NULL, NULL);
	qApp->installTranslator(translator);
}