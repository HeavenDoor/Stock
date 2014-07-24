/****************************************************************************
** Meta object code from reading C++ file 'xmlutils.h'
**
** Created by: The Qt Meta Object Compiler version 63 (Qt 4.8.5)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../xmlutils.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'xmlutils.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 63
#error "This file was generated using the moc from 4.8.5. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_XmlUtils[] = {

 // content:
       6,       // revision
       0,       // classname
       0,    0, // classinfo
       7,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      50,   41,   10,    9, 0x0a,
      86,   72,   67,    9, 0x0a,
     148,   41,  135,    9, 0x0a,
     165,   72,   67,    9, 0x0a,
     224,  196,   67,    9, 0x0a,
     285,  267,   67,    9, 0x0a,
     356,  330,    9,    9, 0x0a,

       0        // eod
};

static const char qt_meta_stringdata_XmlUtils[] = {
    "XmlUtils\0\0QList<QHash<QString,QString> >\0"
    "fileName\0readXml(QString)\0bool\0"
    "fileName,list\0"
    "writeXml(QString,QList<QHash<QString,QString> >)\0"
    "QVariantList\0ReadXml(QString)\0"
    "WriteXml(QString,QVariantList)\0"
    "fileName,tagName,tagContent\0"
    "isElementExist(QString&,QString&,QString&)\0"
    "fileName,listItem\0"
    "appendItem(QString&,QHash<QString,QString>&)\0"
    "fileName,tagName,tagValue\0"
    "removeItem(QString&,QString&,QString&)\0"
};

void XmlUtils::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        Q_ASSERT(staticMetaObject.cast(_o));
        XmlUtils *_t = static_cast<XmlUtils *>(_o);
        switch (_id) {
        case 0: { QList<QHash<QString,QString> > _r = _t->readXml((*reinterpret_cast< QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QList<QHash<QString,QString> >*>(_a[0]) = _r; }  break;
        case 1: { bool _r = _t->writeXml((*reinterpret_cast< QString(*)>(_a[1])),(*reinterpret_cast< QList<QHash<QString,QString> >(*)>(_a[2])));
            if (_a[0]) *reinterpret_cast< bool*>(_a[0]) = _r; }  break;
        case 2: { QVariantList _r = _t->ReadXml((*reinterpret_cast< QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QVariantList*>(_a[0]) = _r; }  break;
        case 3: { bool _r = _t->WriteXml((*reinterpret_cast< QString(*)>(_a[1])),(*reinterpret_cast< QVariantList(*)>(_a[2])));
            if (_a[0]) *reinterpret_cast< bool*>(_a[0]) = _r; }  break;
        case 4: { bool _r = _t->isElementExist((*reinterpret_cast< QString(*)>(_a[1])),(*reinterpret_cast< QString(*)>(_a[2])),(*reinterpret_cast< QString(*)>(_a[3])));
            if (_a[0]) *reinterpret_cast< bool*>(_a[0]) = _r; }  break;
        case 5: { bool _r = _t->appendItem((*reinterpret_cast< QString(*)>(_a[1])),(*reinterpret_cast< QHash<QString,QString>(*)>(_a[2])));
            if (_a[0]) *reinterpret_cast< bool*>(_a[0]) = _r; }  break;
        case 6: _t->removeItem((*reinterpret_cast< QString(*)>(_a[1])),(*reinterpret_cast< QString(*)>(_a[2])),(*reinterpret_cast< QString(*)>(_a[3]))); break;
        default: ;
        }
    }
}

const QMetaObjectExtraData XmlUtils::staticMetaObjectExtraData = {
    0,  qt_static_metacall 
};

const QMetaObject XmlUtils::staticMetaObject = {
    { &QObject::staticMetaObject, qt_meta_stringdata_XmlUtils,
      qt_meta_data_XmlUtils, &staticMetaObjectExtraData }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &XmlUtils::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *XmlUtils::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *XmlUtils::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_XmlUtils))
        return static_cast<void*>(const_cast< XmlUtils*>(this));
    return QObject::qt_metacast(_clname);
}

int XmlUtils::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 7)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 7;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
