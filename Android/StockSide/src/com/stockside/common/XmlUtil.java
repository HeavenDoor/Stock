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
 * 输出xml和解析xml的工具类
 *@ClassName:XmlUtil
 *@author: chenyoulong  Email: chen.youlong@payeco.com
 *@date :2012-9-29 上午9:51:28
 *@Description:TODO
 */
public class XmlUtil
{
	/**
     * java 转换成xml
     * @Title: toXml 
     * @Description: TODO 
     * @param obj 对象实例
     * @return String xml字符串
     */
    public static String toXml(Object obj){
        XStream xstream=new XStream();
//      XStream xstream=new XStream(new DomDriver()); //直接用jaxp dom来解释
//      XStream xstream=new XStream(new DomDriver("utf-8")); //指定编码解析器,直接用jaxp dom来解释
         
        ////如果没有这句，xml中的根元素会是<包.类名>；或者说：注解根本就没生效，所以的元素名就是类的属性
        xstream.processAnnotations(obj.getClass()); //通过注解方式的，一定要有这句话
        return xstream.toXML(obj);
    }
     
    /**
     *  将传入xml文本转换成Java对象
     * @Title: toBean 
     * @Description: TODO 
     * @param xmlStr
     * @param cls  xml对应的class类
     * @return T   xml对应的class类的实例对象
     * 
     * 调用的方法实例：PersonBean person=XmlUtil.toBean(xmlStr, PersonBean.class);
     */
    public static <T> T  toBean(String xmlStr,Class<T> cls)
    {
        //注意：不是new Xstream(); 否则报错：java.lang.NoClassDefFoundError: org/xmlpull/v1/XmlPullParserFactory
        XStream xstream=new XStream(new DomDriver());
        xstream.processAnnotations(cls);
        //@SuppressWarnings("unchecked")
		T obj=(T)xstream.fromXML(xmlStr);
        return obj;         
    } 

}
