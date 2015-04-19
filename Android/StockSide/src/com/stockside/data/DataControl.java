package com.stockside.data;

import java.util.List;

import com.lidroid.xutils.DbUtils;
import com.lidroid.xutils.db.sqlite.Selector;
import com.lidroid.xutils.db.sqlite.WhereBuilder;
import com.lidroid.xutils.exception.DbException;

import com.stockside.model.*;

public class DataControl
{
	private static DataControl uniqueInstance = null;
	private static User user = new User();
	private static List<DailyChangeRate> dailyChangeRates;
	private static List<DailyFluctuateRate> dailyFluctuateRates;
	private static List<DaysChangeRate> daysChangeRates2;
	private static List<DaysChangeRate> daysFluctuateRates2;
	private static List<DaysChangeRate> daysChangeRates3;
	private static List<DaysChangeRate> daysFluctuateRates3;
	private static List<DaysChangeRate> daysChangeRates5;
	private static List<DaysChangeRate> daysFluctuateRates5;
	private static List<DaysChangeRate> daysChangeRates10;
	private static List<DaysChangeRate> daysFluctuateRates10;
	private static List<DaysChangeRate> daysChangeRates15;
	private static List<DaysChangeRate> daysFluctuateRates15;
	private static List<DaysChangeRate> daysChangeRates30;
	private static List<DaysChangeRate> daysFluctuateRates30;
	private static List<DaysChangeRate> daysChangeRates45;
	private static List<DaysChangeRate> daysFluctuateRates45;
	private static List<DaysChangeRate> daysChangeRates60;
	private static List<DaysChangeRate> daysFluctuateRates60;
	
    private DataControl() 
    {
       // Exists only to defeat instantiation.
    }
 
    public static DataControl getInstance() 
    {
       if (uniqueInstance == null) 
       {
           uniqueInstance = new DataControl();
       }
       return uniqueInstance;
    }
    
    public static void LoadDataFromDB(DbUtils db)
    {
    	try
    	{
    		dailyChangeRates = db.findAll(Selector.from(DailyChangeRate.class));
    		dailyFluctuateRates = db.findAll(Selector.from(DailyFluctuateRate.class));
    		daysChangeRates2 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",true)).where(WhereBuilder.b("TradeDays","=",2)));
    		daysFluctuateRates2 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",false)).where(WhereBuilder.b("TradeDays","=",2)));
    		
    		daysChangeRates3 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",true)).where(WhereBuilder.b("TradeDays","=",3)));
    		daysFluctuateRates3 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",false)).where(WhereBuilder.b("TradeDays","=",3)));
    		
    		daysChangeRates5 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",true)).where(WhereBuilder.b("TradeDays","=",5)));
    		daysFluctuateRates5 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",false)).where(WhereBuilder.b("TradeDays","=",5)));
    		
    		daysChangeRates10 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",true)).where(WhereBuilder.b("TradeDays","=",10)));
    		daysFluctuateRates10 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",false)).where(WhereBuilder.b("TradeDays","=",10)));
    		
    		daysChangeRates15 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",true)).where(WhereBuilder.b("TradeDays","=",15)));
    		daysFluctuateRates15 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",false)).where(WhereBuilder.b("TradeDays","=",15)));
    		
    		daysChangeRates30 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",true)).where(WhereBuilder.b("TradeDays","=",30)));
    		daysFluctuateRates30 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",false)).where(WhereBuilder.b("TradeDays","=",30)));
    		
    		daysChangeRates45 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",true)).where(WhereBuilder.b("TradeDays","=",45)));
    		daysFluctuateRates45 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",false)).where(WhereBuilder.b("TradeDays","=",45)));
    		
    		daysChangeRates60 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",true)).where(WhereBuilder.b("TradeDays","=",60)));
    		daysFluctuateRates60 = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",false)).where(WhereBuilder.b("TradeDays","=",60)));
    	}
    	catch (DbException e) 
    	{  
    		e.printStackTrace();  
    	} 
    }
    
    public static void set_User(User u)
    {
    	user = u;
    }
    
    public static User get_User()
    {
    	return user;
    }
    
    public static void set_DailyChangeRates(List<DailyChangeRate> rates)
    {
    	dailyChangeRates = rates;
    }
    
    public static List<DailyChangeRate> get_DailyChangeRates()
    {
    	return dailyChangeRates;
    }
    
    public static void set_DailyFluctuateRates(List<DailyFluctuateRate> rates)
    {
    	dailyFluctuateRates = rates;
    }
    
    public static List<DailyFluctuateRate> get_setDailyFluctuateRates()
    {
    	return dailyFluctuateRates;
    }
    
    
    public static void set_AbsoluteRecentDaysData(List<DaysChangeRate> rates)
    {
    	
    }
    
    public static void set_DaysChangeRates2(List<DaysChangeRate> rates)
    {
    	daysChangeRates2 = rates;
    }   
    public static List<DaysChangeRate> get_DaysChangeRates2(List<DaysChangeRate> rates)
    {
    	return daysChangeRates2;
    }
    
    public static void set_DaysFluctuateRates2(List<DaysChangeRate> rates)
    {
    	daysFluctuateRates2 = rates;
    }   
    public static List<DaysChangeRate> get_DaysFluctuateRates2(List<DaysChangeRate> rates)
    {
    	return daysFluctuateRates2;
    }
    
    public static void set_DaysChangeRates3(List<DaysChangeRate> rates)
    {
    	daysChangeRates3 = rates;
    }    
    public static List<DaysChangeRate> get_DaysChangeRates3(List<DaysChangeRate> rates)
    {
    	return daysChangeRates3;
    }
    
    public static void set_DaysFluctuateRates3(List<DaysChangeRate> rates)
    {
    	daysFluctuateRates3 = rates;
    }   
    public static List<DaysChangeRate> get_DaysFluctuateRates3(List<DaysChangeRate> rates)
    {
    	return daysFluctuateRates3;
    }
    
    public static void set_DaysChangeRates5(List<DaysChangeRate> rates)
    {
    	daysChangeRates5 = rates;
    }  
    public static List<DaysChangeRate> get_DaysChangeRates5(List<DaysChangeRate> rates)
    {
    	return daysChangeRates5;
    }
    
    public static void set_DaysFluctuateRates5(List<DaysChangeRate> rates)
    {
    	daysFluctuateRates5 = rates;
    }   
    public static List<DaysChangeRate> get_DaysFluctuateRates5(List<DaysChangeRate> rates)
    {
    	return daysFluctuateRates5;
    }
    
    public static void set_DaysChangeRates10(List<DaysChangeRate> rates)
    {
    	daysChangeRates10 = rates;
    }  
    public static List<DaysChangeRate> get_DaysChangeRates10(List<DaysChangeRate> rates)
    {
    	return daysChangeRates10;
    }
    
    public static void set_DaysFluctuateRates10(List<DaysChangeRate> rates)
    {
    	daysFluctuateRates10 = rates;
    }   
    public static List<DaysChangeRate> get_DaysFluctuateRates10(List<DaysChangeRate> rates)
    {
    	return daysFluctuateRates10;
    }
    
    public static void set_DaysChangeRates15(List<DaysChangeRate> rates)
    {
    	daysChangeRates15 = rates;
    }  
    public static List<DaysChangeRate> get_DaysChangeRates15(List<DaysChangeRate> rates)
    {
    	return daysChangeRates15;
    }
    
    public static void set_DaysFluctuateRates15(List<DaysChangeRate> rates)
    {
    	daysFluctuateRates15 = rates;
    }   
    public static List<DaysChangeRate> get_DaysFluctuateRates15(List<DaysChangeRate> rates)
    {
    	return daysFluctuateRates15;
    }
    
    public static void set_DaysChangeRates30(List<DaysChangeRate> rates)
    {
    	daysChangeRates30 = rates;
    }  
    public static List<DaysChangeRate> get_DaysChangeRates30(List<DaysChangeRate> rates)
    {
    	return daysChangeRates30;
    }
    
    public static void set_DaysFluctuateRates30(List<DaysChangeRate> rates)
    {
    	daysFluctuateRates30 = rates;
    }   
    public static List<DaysChangeRate> get_DaysFluctuateRates30(List<DaysChangeRate> rates)
    {
    	return daysFluctuateRates30;
    }
    
    public static void set_DaysChangeRates45(List<DaysChangeRate> rates)
    {
    	daysChangeRates45 = rates;
    }  
    public static List<DaysChangeRate> get_DaysChangeRates45(List<DaysChangeRate> rates)
    {
    	return daysChangeRates45;
    }
    
    public static void set_DaysFluctuateRates45(List<DaysChangeRate> rates)
    {
    	daysFluctuateRates45 = rates;
    }   
    public static List<DaysChangeRate> get_DaysFluctuateRates45(List<DaysChangeRate> rates)
    {
    	return daysFluctuateRates45;
    }
    public static void set_DaysChangeRates60(List<DaysChangeRate> rates)
    {
    	daysChangeRates60 = rates;
    }  
    public static List<DaysChangeRate> get_DaysChangeRates60(List<DaysChangeRate> rates)
    {
    	return daysChangeRates60;
    }
    
    public static void set_DaysFluctuateRates60(List<DaysChangeRate> rates)
    {
    	daysFluctuateRates60 = rates;
    }   
    public static List<DaysChangeRate> get_DaysFluctuateRates60(List<DaysChangeRate> rates)
    {
    	return daysFluctuateRates60;
    }
}
