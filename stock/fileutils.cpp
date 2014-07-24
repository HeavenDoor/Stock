#include "stdafx.h"
#include "fileutils.h"
#include "stringutils.h"

#include <QDir>
#include <QFileInfo>
#include <QTextStream>
#include <QDataStream>
#include <QLibrary>
#include <QFileDialog>

// QLibrary *qzrLib = NULL;
// typedef int (__stdcall *ExecCommandType)(const wchar_t *commandLine, const wchar_t *logFileName);
// ExecCommandType execCommandFunction = NULL;

// typedef int (__stdcall *ExtractFileType)(const wchar_t *commandLine, const wchar_t *logFileName, unsigned int &unpackSize, char *content);
// ExtractFileType extractFileFunction;

FileUtils::FileUtils() : QObject()
{

}

FileUtils::~FileUtils()
{
}

QString appPath;

QString FileUtils::get_AppPath()
{
	if (appPath.isEmpty())
	{
		QString exeFileName = QApplication::applicationFilePath();
		QFileInfo file(exeFileName);
		appPath = file.canonicalPath(); // 去掉..后的，如E:/trd/product/Rokh/bin/
	}
	return appPath;
}

QString FileUtils::getFullPath(const QString &fileName)
{
	if(QDir::isAbsolutePath(fileName))
	{
		return fileName;	
	}
	QString appPath = FileUtils::get_AppPath();
	return QString(appPath + "/" + fileName);	
}

bool FileUtils::existsFullPath(const QString &fileName)
{
	QFileInfo file(fileName);
	return file.exists();
}

bool FileUtils::exists(const QString &fileName)
{
	QString newFileName = getFullPath(fileName);		
	return existsFullPath(newFileName);
}

QString FileUtils::readAllTextFullPath(const QString &fileName)
{
	QFile file(fileName);
	file.open(QIODevice::ReadOnly);
	QTextStream stream(&file);
	QString contents = stream.readAll();
	file.close();
	return contents;
}

QString FileUtils::readAllText(const QString &fileName)
{
	QString newFileName = getFullPath(fileName);		
	return readAllTextFullPath(newFileName);
}

void checkInit()
{
// 	if (!qzrLib)
// 	{ 
// 		QString libFileName = FileUtils::getFullPath("7zr");
// 		qzrLib = new QLibrary(libFileName);
// 		execCommandFunction = (ExecCommandType)qzrLib->resolve("execCommand");
// 		extractFileFunction = (ExtractFileType)qzrLib->resolve("extractFile");
// 	}
}

QString getLogFileName()
{
	return FileUtils::getFullPath("data/7zr.elf");
}

int FileUtils::execCommand(const QString &commandLine)
{
	/*checkInit();
	if (!execCommandFunction)
	{
		//context()->throwError("7zr库没找到 execCommand");
		return 1;
	}
	if (commandLine.isEmpty())
	{
		//context()->throwError("commandLine 参数为空");
		return 1;
	}
	QString logFileName = getLogFileName();
	int result = execCommandFunction(commandLine.utf16(), logFileName.utf16());
	if (result != 0)
	{
		QString log = readAllText(logFileName);
		QString error = QString("压缩解压缩出错，返回值 %0，日志：\n%1").arg(result).arg(log);
		//context()->throwError(error);
	}*/
	return 0;
}

bool FileUtils::checkExists(const QString &fileName)
{
	if (!existsFullPath(fileName))
	{
		QString error = QString("文件不存在：%0").arg(fileName);
		//context()->throwError(error);
		return false;
	}
	return true;
}

void FileUtils::zipFile(const QString &zipFileName, const QString &srcFileName)
{
	QString newSrcFileName = getFullPath(srcFileName);
	if (!checkExists(newSrcFileName))
	{
		return;
	}
	QString newZipFileName = getFullPath(zipFileName);
	QString commandLine = QString("7zr a \"%0\" \"%1\"").arg(newZipFileName).arg(newSrcFileName);
	execCommand(commandLine);
}

void FileUtils::unzip(const QString &zipFileName, const QString &outputDir)
{
	QString newZipFileName = getFullPath(zipFileName);
	if (!checkExists(newZipFileName))
	{
		return;
	}
	QString newOutputDir = getFullPath(outputDir);
	 // 如 "7zr x \"-oE:/trd/product/Rokh/bin/data/test7z\" \"E:/trd/product/Rokh/bin/tests/test1.7z\""
	QString commandLine = QString("7zr x \"-o%1\" \"%0\"").arg(newZipFileName).arg(newOutputDir);
	execCommand(commandLine);
}

bool FileUtils::extractFile(const QString &zipFileName, const QString &extractFileName, QByteArray *content)
{
	/*QString newZipFileName = getFullPath(zipFileName);
	if (!existsFullPath(newZipFileName))
	{
		QString error = QString("压缩文件不存在：%0").arg(newZipFileName);
		return false;
	}
	checkInit();
	if (!extractFileFunction)
	{
		LogUtils::error("7zr库没找到 extractFile");
		return false;
	}
	QString logFileName = getLogFileName();

	QString commandLine = QString("7zr l \"%0\" \"%1\"").arg(newZipFileName).arg(extractFileName);
	unsigned int listUnpackSize;
	int result = extractFileFunction(commandLine.utf16(), logFileName.utf16(), listUnpackSize, NULL);
	if (result != 0 || listUnpackSize == 0)
	{
		QString log = readAllText(logFileName);
		QString error = QString("取解压字节数出错，返回值 %0、返回字节数 %1，日志：\n%2").arg(result).arg(listUnpackSize).arg(log);
		LogUtils::error(error);
		return false;
	}

	commandLine = QString("7zr x \"%0\" \"%1\"").arg(newZipFileName).arg(extractFileName);
	content->resize(listUnpackSize);
	unsigned int unpackSize;
	result = extractFileFunction(commandLine.utf16(), logFileName.utf16(), unpackSize, content->data());
	if (result != 0 || unpackSize == 0 || unpackSize != listUnpackSize)
	{
		QString log = readAllText(logFileName);
		QString error = QString("解压缩出错，返回值 %0，取到字节数 %1，返回字节数 %2，日志：\n%3").arg(result)
			.arg(listUnpackSize).arg(unpackSize).arg(log);
		LogUtils::error(error);
		return false;
	}*/
	return true;
}

QString FileUtils::selectFile(const QString &filter)
{	
// 	Popup::setCanHidePopup(false);
// 	QString path = QFileDialog::getOpenFileName(0, "选择文件", QDir::currentPath(), filter);
// 	Popup::setCanHidePopup(true);
// 	return path;
	return "";
}

QString FileUtils::selectFile(const QString &fileName, const QString &filter)
{	
// 	Popup::setCanHidePopup(false);
// 	QString path = QFileDialog::getSaveFileName(QApplication::activePopupWidget(), "请选择文件保存位置", fileName, filter);
// 	Popup::setCanHidePopup(true);
// 	return path;
return "";
}

void FileUtils::copyFile(const QString &src, const QString &dst)
{
	if (existsFullPath(dst))
	{
		QFile::remove(dst);
	}
	bool isSucceed = QFile::copy(src, dst);
	if (!isSucceed)
	{
		//context()->throwError("保存模板文件失败");
	}
}

void FileUtils::saveResourceToFile(const QString &url, const QString &fileName)
{
// 	if (!ResourceUtils::saveToFile(url, fileName))
// 	{
// 		//context()->throwError("保存模板文件失败");
// 	}
}

void FileUtils::makeDir(const QString &fileName)
{
	QDir dir;
	bool isSucceed = dir.mkdir(fileName);
	if (!isSucceed)
	{
		//context()->throwError("创建目录失败");
	}
}

void FileUtils::deleteDir(const QString& fileDir)
{
	QDir dir(QDir::currentPath());
	if (!dir.exists(fileDir)) return;
	QDir fdir(fileDir);
	QFileInfoList fileList = fdir.entryInfoList();
	if (fileList.count() > 2)
	{
		for (int i = 0; i < fileList.count(); ++i)
		{
			const QFileInfo file = fileList.at(i);
			if (file.fileName() == "." || file.fileName() == "..")
			{
				continue;
			}
			if (file.isDir())
			{
				deleteDir(fileDir + "/" + file.fileName());
			}
			else
			{
				QString filePath = file.absoluteFilePath();
				QFile::remove(filePath);
			}
		}
	}		
	dir.rmdir(fileDir);
}

void FileUtils::deleteFile(const QString& fileName)
{
	QDir dir(QDir::currentPath());
	if (!dir.exists(fileName)) return;
	QFileInfo fileInfo(fileName);
	QString filePath = fileInfo.absoluteFilePath();
	QFile::remove(filePath);
}

QString FileUtils::fileMd5(const QString & filePath)
{
	QFile theFile(filePath);

	if(!theFile.open(QIODevice::ReadOnly))
	{
		theFile.close();
		return "";
	}
	QTextStream iStream(&theFile);
	QString strMd5 =  StringUtils::md5(iStream.readAll());
	theFile.close();
	return strMd5;
}

QStringList FileUtils::getAllFileName(const QString & fileName)
{
	QString newFileName = getFullPath(fileName);
	QDir dir(QDir::currentPath());
	QStringList list;
	if (dir.exists(fileName))
	{
		QDir fdir(fileName);
		QFileInfoList fileList = fdir.entryInfoList();
		for (int i = 2; i < fileList.count(); i++)
		{
			list.append(fileList.at(i).fileName());
		}
	}
	return list;
}