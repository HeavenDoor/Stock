/****************************************************************************
** Meta object code from reading C++ file 'stringutils.h'
**
** Created by: The Qt Meta Object Compiler version 63 (Qt 4.8.5)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../stringutils.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'stringutils.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 63
#error "This file was generated using the moc from 4.8.5. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_StringUtils[] = {

 // content:
       6,       // revision
       0,       // classname
       0,    0, // classinfo
       6,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      26,   21,   13,   12, 0x0a,
      43,   39,   13,   12, 0x0a,
      80,   66,   13,   12, 0x0a,
     107,   12,   13,   12, 0x0a,
     117,   39,   13,   12, 0x0a,
     140,   39,   13,   12, 0x0a,

       0        // eod
};

static const char qt_meta_stringdata_StringUtils[] = {
    "StringUtils\0\0QString\0text\0md5(QString)\0"
    "str\0getPinyinCode(QString)\0str,maxLength\0"
    "getPinyinCode(QString,int)\0newGuid()\0"
    "simpleEncrypt(QString)\0simpleDecrypt(QString)\0"
};

void StringUtils::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        Q_ASSERT(staticMetaObject.cast(_o));
        StringUtils *_t = static_cast<StringUtils *>(_o);
        switch (_id) {
        case 0: { QString _r = _t->md5((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 1: { QString _r = _t->getPinyinCode((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 2: { QString _r = _t->getPinyinCode((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< int(*)>(_a[2])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 3: { QString _r = _t->newGuid();
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 4: { QString _r = _t->simpleEncrypt((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        case 5: { QString _r = _t->simpleDecrypt((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        default: ;
        }
    }
}

const QMetaObjectExtraData StringUtils::staticMetaObjectExtraData = {
    0,  qt_static_metacall 
};

const QMetaObject StringUtils::staticMetaObject = {
    { &QObject::staticMetaObject, qt_meta_stringdata_StringUtils,
      qt_meta_data_StringUtils, &staticMetaObjectExtraData }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &StringUtils::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *StringUtils::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *StringUtils::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_StringUtils))
        return static_cast<void*>(const_cast< StringUtils*>(this));
    return QObject::qt_metacast(_clname);
}

int StringUtils::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 6)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 6;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
