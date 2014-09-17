package com.stockside.common;

public class XmlDeal
{
	public static String DealXml(String xmlString) 
	{	
		xmlString = xmlString.replace("&lt;?xml version=\"1.0\" encoding=\"utf-8\"?&gt;", "");
		xmlString = xmlString.replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
		xmlString = xmlString.replace("\n", "");
		xmlString = xmlString.replaceAll("&lt;", "<");
		xmlString = xmlString.replaceAll("&gt;", ">");
		return xmlString;
	}
}
