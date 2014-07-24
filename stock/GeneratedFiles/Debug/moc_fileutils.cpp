/****************************************************************************
** Meta object code from reading C++ file 'fileutils.h'
**
** Created by: The Qt Meta Object Compiler version 63 (Qt 4.8.5)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "StdAfx.h"
#include "../../fileutils.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'fileutils.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 63
#error "This file was generated using the moc from 4.8.5. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_FileUtils[] = {

 // content:
       6,       // revision
       0,       // classname
       0,    0, // classinfo
      12,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      33,   11,   10,   10, 0x0a,
      80,   56,   10,   10, 0x0a,
     122,  113,  105,   10, 0x0a,
     148,  113,  143,   10, 0x0a,
     171,  164,  105,   10, 0x0a,
     207,  191,  105,   10, 0x0a,
     244,  235,  105,   10, 0x0a,
     273,  113,  261,   10, 0x0a,
     305,  297,   10,   10, 0x0a,
     344,  331,   10,   10, 0x0a,
     388,  380,   10,   10, 0x0a,
     407,  113,   10,   10, 0x0a,

       0        // eod
};

static const char qt_meta_stringdata_FileUtils[] = {
    "FileUtils\0\0zipFileName,outputDir\0"
    "unzip(QString,QString)\0zipFileName,srcFileName\0"
    "zipFile(QString,QString)\0QString\0"
    "fileName\0readAllText(QString)\0bool\0"
    "exists(QString)\0filter\0selectFile(QString)\0"
    "fileName,filter\0selectFile(QString,QString)\0"
    "filePath\0fileMd5(QString)\0QStringList\0"
    "getAllFileName(QString)\0src,dst\0"
    "copyFile(QString,QString)\0url,fileName\0"
    "saveResourceToFile(QString,QString)\0"
    "fileDir\0deleteDir(QString)\0makeDir(QString)\0"
};

void FileUtils::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        Q_ASSERT(staticMetaObject.cast(_o));
        FileUtils *_t = static_cast<FileUtils *>(_o);
        switch (_id) {
        case 0: _t->unzip((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QString(*)>(_a[2]))); break;
        case 1: _t->zipFile((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QString(*)>(_a[2]))); break;
        case 2: { QString _r = _t->readAllText((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 3: { bool _r = _t->exists((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< bool*>(_a[0]) = _r; }  break;
        case 4: { QString _r = _t->selectFile((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 5: { QString _r = _t->selectFile((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QString(*)>(_a[2])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 6: { QString _r = _t->fileMd5((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 7: { QStringList _r = _t->getAllFileName((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QStringList*>(_a[0]) = _r; }  break;
        case 8: _t->copyFile((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QString(*)>(_a[2]))); break;
        case 9: _t->saveResourceToFile((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QString(*)>(_a[2]))); break;
        case 10: _t->deleteDir((*reinterpret_cast< const QString(*)>(_a[1]))); break;
        case 11: _t->makeDir((*reinterpret_cast< const QString(*)>(_a[1]))); break;
        default: ;
        }
    }
}

const QMetaObjectExtraData FileUtils::staticMetaObjectExtraData = {
    0,  qt_static_metacall 
};

const QMetaObject FileUtils::staticMetaObject = {
    { &QObject::staticMetaObject, qt_meta_stringdata_FileUtils,
      qt_meta_data_FileUtils, &staticMetaObjectExtraData }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &FileUtils::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *FileUtils::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *FileUtils::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_FileUtils))
        return static_cast<void*>(const_cast< FileUtils*>(this));
    return QObject::qt_metacast(_clname);
}

int FileUtils::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 12)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 12;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
