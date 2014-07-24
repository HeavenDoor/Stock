/****************************************************************************
** Meta object code from reading C++ file 'clientdb.h'
**
** Created by: The Qt Meta Object Compiler version 63 (Qt 4.8.5)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../../clientdb.h"
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'clientdb.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 63
#error "This file was generated using the moc from 4.8.5. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
static const uint qt_meta_data_ClientDb[] = {

 // content:
       6,       // revision
       0,       // classname
       0,    0, // classinfo
      12,   14, // methods
       3,   74, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: signature, parameters, type, tag, flags
      10,    9,    9,    9, 0x0a,
      29,    9,    9,    9, 0x0a,
      49,    9,    9,    9, 0x0a,
      99,   75,   71,    9, 0x0a,
     175,  140,   71,    9, 0x0a,
     241,  237,  224,    9, 0x0a,
     279,  257,   71,    9, 0x0a,
     318,  237,  309,    9, 0x0a,
     342,  237,  309,    9, 0x0a,
     367,  237,   71,    9, 0x0a,
     403,  395,    9,    9, 0x0a,
     450,  430,    9,    9, 0x0a,

 // properties: name, type, flags
     486,  481, 0x01095003,
     503,  309, 0xff095001,
     526,  518, 0x0a095003,

       0        // eod
};

static const char qt_meta_stringdata_ClientDb[] = {
    "ClientDb\0\0beginTransaction()\0"
    "commitTransaction()\0rollbackTransaction()\0"
    "int\0tableName,fields,values\0"
    "insert(QString,QVariantList,QVariantMap)\0"
    "tableName,primaryKey,fields,values\0"
    "update(QString,QString,QVariantList,QVariantMap)\0"
    "QVariantList\0sql\0select(QString)\0"
    "tableName,field,value\0"
    "del(QString,QString,QVariant)\0QVariant\0"
    "selectFirstRow(QString)\0"
    "selectSingleRow(QString)\0"
    "executeNonQuerySQL(QString)\0sqlList\0"
    "batchExecute(QVariantList)\0"
    "parameterName,value\0addParameter(QString,QVariant)\0"
    "bool\0FieldNameToLower\0LastInsertedId\0"
    "QString\0DbName\0"
};

void ClientDb::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        Q_ASSERT(staticMetaObject.cast(_o));
        ClientDb *_t = static_cast<ClientDb *>(_o);
        switch (_id) {
        case 0: _t->beginTransaction(); break;
        case 1: _t->commitTransaction(); break;
        case 2: _t->rollbackTransaction(); break;
        case 3: { int _r = _t->insert((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QVariantList(*)>(_a[2])),(*reinterpret_cast< const QVariantMap(*)>(_a[3])));
            if (_a[0]) *reinterpret_cast< int*>(_a[0]) = _r; }  break;
        case 4: { int _r = _t->update((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QString(*)>(_a[2])),(*reinterpret_cast< const QVariantList(*)>(_a[3])),(*reinterpret_cast< const QVariantMap(*)>(_a[4])));
            if (_a[0]) *reinterpret_cast< int*>(_a[0]) = _r; }  break;
        case 5: { QVariantList _r = _t->select((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QVariantList*>(_a[0]) = _r; }  break;
        case 6: { int _r = _t->del((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QString(*)>(_a[2])),(*reinterpret_cast< const QVariant(*)>(_a[3])));
            if (_a[0]) *reinterpret_cast< int*>(_a[0]) = _r; }  break;
        case 7: { QVariant _r = _t->selectFirstRow((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QVariant*>(_a[0]) = _r; }  break;
        case 8: { QVariant _r = _t->selectSingleRow((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< QVariant*>(_a[0]) = _r; }  break;
        case 9: { int _r = _t->executeNonQuerySQL((*reinterpret_cast< const QString(*)>(_a[1])));
            if (_a[0]) *reinterpret_cast< int*>(_a[0]) = _r; }  break;
        case 10: _t->batchExecute((*reinterpret_cast< const QVariantList(*)>(_a[1]))); break;
        case 11: _t->addParameter((*reinterpret_cast< const QString(*)>(_a[1])),(*reinterpret_cast< const QVariant(*)>(_a[2]))); break;
        default: ;
        }
    }
}

const QMetaObjectExtraData ClientDb::staticMetaObjectExtraData = {
    0,  qt_static_metacall 
};

const QMetaObject ClientDb::staticMetaObject = {
    { &QObject::staticMetaObject, qt_meta_stringdata_ClientDb,
      qt_meta_data_ClientDb, &staticMetaObjectExtraData }
};

#ifdef Q_NO_DATA_RELOCATION
const QMetaObject &ClientDb::getStaticMetaObject() { return staticMetaObject; }
#endif //Q_NO_DATA_RELOCATION

const QMetaObject *ClientDb::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->metaObject : &staticMetaObject;
}

void *ClientDb::qt_metacast(const char *_clname)
{
    if (!_clname) return 0;
    if (!strcmp(_clname, qt_meta_stringdata_ClientDb))
        return static_cast<void*>(const_cast< ClientDb*>(this));
    return QObject::qt_metacast(_clname);
}

int ClientDb::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 12)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 12;
    }
#ifndef QT_NO_PROPERTIES
      else if (_c == QMetaObject::ReadProperty) {
        void *_v = _a[0];
        switch (_id) {
        case 0: *reinterpret_cast< bool*>(_v) = get_FieldNameToLower(); break;
        case 1: *reinterpret_cast< QVariant*>(_v) = get_LastInsertedId(); break;
        case 2: *reinterpret_cast< QString*>(_v) = get_DbName(); break;
        }
        _id -= 3;
    } else if (_c == QMetaObject::WriteProperty) {
        void *_v = _a[0];
        switch (_id) {
        case 0: set_FieldNameToLower(*reinterpret_cast< bool*>(_v)); break;
        case 2: set_DbName(*reinterpret_cast< QString*>(_v)); break;
        }
        _id -= 3;
    } else if (_c == QMetaObject::ResetProperty) {
        _id -= 3;
    } else if (_c == QMetaObject::QueryPropertyDesignable) {
        _id -= 3;
    } else if (_c == QMetaObject::QueryPropertyScriptable) {
        _id -= 3;
    } else if (_c == QMetaObject::QueryPropertyStored) {
        _id -= 3;
    } else if (_c == QMetaObject::QueryPropertyEditable) {
        _id -= 3;
    } else if (_c == QMetaObject::QueryPropertyUser) {
        _id -= 3;
    }
#endif // QT_NO_PROPERTIES
    return _id;
}
QT_END_MOC_NAMESPACE
