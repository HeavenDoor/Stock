/****************************************************************************
** Meta object code from reading C++ file 'dateutils.h'
**
** Created by: The Qt Meta Object Compiler version 63 (Qt 4.8.5)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "StdAfx.h"
#include "../../dateutils.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'dateutils.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 63
#error "This file was generated using the moc from 4.8.5. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_DateUtils[] = {

 // content:
       6,       // revision
       0,       // classname
       0,    0, // classinfo
       1,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      24,   19,   11,   10, 0x0a,

       0        // eod
};

static const char qt_meta_stringdata_DateUtils[] = {
    "DateUtils\0\0QString\0date\0"
    "dateTimeToStr(QDateTime)\0"
};

void DateUtils::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        Q_ASSERT(staticMetaObject.cast(_o));
        DateUtils *_t = static_cast<DateUtils *>(_o);
        switch (_id) {
        case 0: { QString _r = _t->dateTimeToStr((*reinterpret_cast< const QDateTime(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QString*>(_a[0]) = _r; }  break;
        default: ;
        }
    }
}

const QMetaObjectExtraData DateUtils::staticMetaObjectExtraData = {
    0,  qt_static_metacall 
};

const QMetaObject DateUtils::staticMetaObject = {
    { &QObject::staticMetaObject, qt_meta_stringdata_DateUtils,
      qt_meta_data_DateUtils, &staticMetaObjectExtraData }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &DateUtils::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *DateUtils::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *DateUtils::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_DateUtils))
        return static_cast<void*>(const_cast< DateUtils*>(this));
    return QObject::qt_metacast(_clname);
}

int DateUtils::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 1)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 1;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
