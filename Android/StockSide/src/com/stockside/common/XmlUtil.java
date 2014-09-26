package com.stockside.common;

import com.stockside.entity.ValidationCode.ValidationCodeResult;
import com.stockside.entity.StockItemEntity;
import com.thoughtworks.xstream.annotations.XStreamAlias;
import com.thoughtworks.xstream.annotations.XStreamAsAttribute;
import com.thoughtworks.xstream.annotations.XStreamConverter;
import com.thoughtworks.xstream.annotations.XStreamImplicit;
import com.thoughtworks.xstream.annotations.XStreamOmitField;
import com.thoughtworks.xstream.XStream;
import com.thoughtworks.xstream.io.xml.DomDriver;
/**
 * ���xml�ͽ���xml�Ĺ�����
 *@ClassName:XmlUtil
 *@author: chenyoulong  Email: chen.youlong@payeco.com
 *@date :2012-9-29 ����9:51:28
 *@Description:TODO
 */
public class XmlUtil
{
	/**
     * java ת����xml
     * @Title: toXml 
     * @Description: TODO 
     * @param obj ����ʵ��
     * @return String xml�ַ���
     */
    public static String toXml(Object obj){
        XStream xstream=new XStream();
//      XStream xstream=new XStream(new DomDriver()); //ֱ����jaxp dom������
//      XStream xstream=new XStream(new DomDriver("utf-8")); //ָ�����������,ֱ����jaxp dom������
         
        ////���û����䣬xml�еĸ�Ԫ�ػ���<��.����>������˵��ע�������û��Ч�����Ե�Ԫ���������������
        xstream.processAnnotations(obj.getClass()); //ͨ��ע�ⷽʽ�ģ�һ��Ҫ����仰
        return xstream.toXML(obj);
    }
     
    /**
     *  ������xml�ı�ת����Java����
     * @Title: toBean 
     * @Description: TODO 
     * @param xmlStr
     * @param cls  xml��Ӧ��class��
     * @return T   xml��Ӧ��class���ʵ������
     * 
     * ���õķ���ʵ����PersonBean person=XmlUtil.toBean(xmlStr, PersonBean.class);
     */
    public static <T> T  toBean(String xmlStr,Class<T> cls)
    {
        //ע�⣺����new Xstream(); ���򱨴�java.lang.NoClassDefFoundError: org/xmlpull/v1/XmlPullParserFactory
        XStream xstream=new XStream(new DomDriver());
        xstream.processAnnotations(cls);
        //@SuppressWarnings("unchecked")
		T obj=(T)xstream.fromXML(xmlStr);
        return obj;         
    } 

}
