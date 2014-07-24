#ifndef FILEUTILS_H
#define FILEUTILS_H
#include <QObject>
class FileUtils : public QObject
{
	Q_OBJECT
public slots:
	void unzip(const QString &zipFileName, const QString &outputDir);
	void zipFile(const QString &zipFileName, const QString &srcFileName);
	static QString readAllText(const QString &fileName);
	static bool exists(const QString &fileName);
	static QString selectFile(const QString &filter);
	static QString selectFile(const QString &fileName, const QString &filter);
	static QString fileMd5(const QString & filePath);//计算文件的MD5值
	static QStringList getAllFileName(const QString & fileName);//得到目录下文件名 jk
	void copyFile(const QString &src,const QString &dst);
	void saveResourceToFile(const QString &url, const QString &fileName);
	void deleteDir(const QString& fileDir);
	void makeDir(const QString &fileName);//创建目录 qjl

public:
	FileUtils();
	virtual ~FileUtils();
	
	static QString get_AppPath();
	static QString getFullPath(const QString &fileName);
	static bool existsFullPath(const QString &fileName);
	static void deleteFile(const QString& fileName);
	bool checkExists(const QString &fileName);
	static bool extractFile(const QString &zipFileName, const QString &extractFileName, QByteArray *content);
	static QString readAllTextFullPath(const QString &fileName);	

private:
	int execCommand(const QString &commandLine);
};

#endif // FILEUTILS_H
