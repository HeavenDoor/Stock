package com.stockside.entity;

import java.util.ArrayList;
import java.util.List;

import com.stockside.entity.BaseResult.LogicBase;
import com.thoughtworks.xstream.annotations.XStreamAlias;
import com.thoughtworks.xstream.annotations.XStreamAsAttribute;
import com.thoughtworks.xstream.annotations.XStreamConverter;
import com.thoughtworks.xstream.annotations.XStreamImplicit;
import com.thoughtworks.xstream.annotations.XStreamOmitField;

@XStreamAlias("string")
public class StockItemEntity
{
	@XStreamAlias("StockItemResult")
	private StockItemResult result;
	public StockItemResult get_StockItemResult() 
    {
        return result;
    }
    public void set_StockItemResult(StockItemResult value) 
    {
        this.result = value;
    }
    
    
    public static class StockItemResult
    {
    	@XStreamAlias("ErrorType")
		private int errorType;
		public int get_ErrorType() 
	    {
	        return errorType;
	    }
	    public void set_ErrorType(int value) 
	    {
	        this.errorType = value;
	    }
		
		@XStreamAlias("ErrorID")
		private int errorID;
		public int get_ErrorID() 
	    {
	        return errorID;
	    }
	    public void set_ErrorID(int value) 
	    {
	        this.errorID = value;
	    }
	    
		@XStreamAlias("ReturnMessage")
		private String returnMessage;
		public String get_ReturnMessage() 
	    {
	        return returnMessage;
	    }
	    public void set_ReturnMessage(String value) 
	    {
	        this.returnMessage = value;
	    }
	    
	    @XStreamAlias("StockItems")
		private List<StockItem> stockitems;
	    
		public List<StockItem> get_StockItems() 
	    {
	        return stockitems;
	    }
	    public void set_StockItems(List<StockItem> value) 
	    {
	        this.stockitems = value;
	    }

		@XStreamAlias("StockItem")
		public static class StockItem
		{
		    @XStreamAlias("StockDate")
		    private String StockDate;
		    public String get_StockDate()
		    {
		    	return StockDate;
		    }
		    public void set_StockDate(String value)
		    {
		    	this.StockDate = value;
		    }
		    
		    @XStreamAlias("StockCode")
		    private String StockCode;
		    public String get_StockCode()
		    {
		    	return StockCode;
		    }
		    public void set_StockCode(String value)
		    {
		    	this.StockCode = value;
		    }
		    
		    @XStreamAlias("StockName")
		    private String StockName;
		    public String get_StockName()
		    {
		    	return StockName;
		    }		    
		    public void set_StockName(String value)
		    {
		    	this.StockName = value;
		    }
		    
		    @XStreamAlias("OpenPrice")
		    private double OpenPrice;
		    public double get_OpenPrice()
		    {
		    	return OpenPrice;
		    }
		    public void set_OpenPrice(double value)
		    {
		    		this.OpenPrice = value;
		    }
		    
		    @XStreamAlias("ClosePrice")
		    private double ClosePrice;
		    public double get_ClosePrice()
		    {
		    	return ClosePrice;
		    }
		    public void set_ClosePrice(double value)
		    {
		    	this.ClosePrice = value;
		    }
		    
		    @XStreamAlias("HighestPrice")
		    private double HighestPrice;
		    public double get_HighestPrice()
		    {
		    	return HighestPrice;
		    }		    
		    public void set_HighestPrice(double value)
		    {
		    	this.HighestPrice = value;
		    }
		    	
		    @XStreamAlias("LowestPrice")
		    private double LowestPrice;
		    public double get_LowestPrice()
		    {
		    	return LowestPrice;
		    }
		    public void set_LowestPrice(double value)
		    {
		    	this.LowestPrice = value;
		    }
		    
		    @XStreamAlias("FluctuateMount")
		    private double FluctuateMount;
		    public double get_FluctuateMount()
		    {
		    	return FluctuateMount;
		    }
		    public void set_FluctuateMount(double value)
		    {
		    	this.FluctuateMount = value;
		    }
		    
		    @XStreamAlias("FluctuateRate")
		    private double FluctuateRate;
		    public double get_FluctuateRate()
		    {
		    	return FluctuateRate;
		    }
		    public void set_FluctuateRate(double value)
		    {
		    	this.FluctuateRate = value;
		    }
		    
		    @XStreamAlias("ChangeRate")
		    private double ChangeRate;
		    public double get_ChangeRate()
		    {
		    	return ChangeRate;
		    }
		    public void set_ChangeRate(double value)
		    {
		    	this.ChangeRate = value;
		    }
		    
		    @XStreamAlias("TradeVolume")
		    private double TradeVolume;
		    public double get_TradeVolume()
		    {
		    	return TradeVolume;
		    }
		    public void set_TradeVolume(double value)
		    {
		    	this.TradeVolume = value;
	    	}
	    	
		   	@XStreamAlias("TradeMount")
		   	private double TradeMount;
		   	public double get_TradeMount()
		   	{
		    	return TradeMount;
		    }		    	
		   	public void set_TradeMount(double value)
	    	{
	    		this.TradeMount = value;
		   	}
		   	
		   	@XStreamAlias("ToatlMarketCap")
		   	private double ToatlMarketCap;
		    public double get_ToatlMarketCap()
		    {
		    	return ToatlMarketCap;
		    }
		    public void set_ToatlMarketCap(double value)
		    {
		    	this.ToatlMarketCap = value;
		    }
		    	
		    @XStreamAlias("CirculationMarketCap")
		    private double CirculationMarketCap;
		    public double get_CirculationMarketCap()
		    {
		    	return CirculationMarketCap;
		    }
		    public void set_CirculationMarketCap(double value)
		    {
		    	this.CirculationMarketCap = value;
		    }
		    
		    @XStreamAlias("ISStopped")
		    private boolean ISStopped;
		    public boolean get_IsStoppedp()
		    {
		    	return ISStopped;
		    }
		    public void set_IsStopped(boolean value)
		    {
		    	this.ISStopped = value;
		    }
		}
    }
}
