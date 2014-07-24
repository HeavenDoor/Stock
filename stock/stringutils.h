#ifndef STRINGUTILS_H
#define STRINGUTILS_H

class StringUtils : public QObject
{
	Q_OBJECT
public slots:
	static QString md5(const QString &text);
	static QString getPinyinCode(const QString &str);
	static QString getPinyinCode(const QString &str, int maxLength);
	// ����Guid��36�ֽڳ��ȵ��ַ�����
	static QString newGuid();
	static QString simpleEncrypt(const QString &str);
	static QString simpleDecrypt(const QString &str);
public:
	virtual ~StringUtils(){}
	static QStringList splitString(const QString &str, const QString &spit);
};

#endif // STRINGUTILS_H
