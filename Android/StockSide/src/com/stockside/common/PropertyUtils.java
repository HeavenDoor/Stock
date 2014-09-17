package com.stockside.common;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;


public class PropertyUtils
{
	private static Properties getrojectConfigProperties() 
	{  
        Properties props = new Properties();  
        InputStream in = PropertyUtils.class.getResourceAsStream("project.properties");  

        try 
        {  
            props.load(in);  
        } 
        catch (IOException e) 
        {  
            e.printStackTrace();  
        }  
        return props;  
    }
	
	public static String getWebServiceAddr()
	{
		return getrojectConfigProperties().getProperty("service"); 
	}
}
