package com.stockside.entity;

import java.util.ArrayList;
import java.util.List;

import com.stockside.entity.BaseResult.LogicBase;
import com.stockside.entity.StockItemEntity.StockItemResult;
import com.stockside.entity.StockItemEntity.StockItemResult.StockItem;
import com.thoughtworks.xstream.annotations.XStreamAlias;
import com.thoughtworks.xstream.annotations.XStreamAsAttribute;
import com.thoughtworks.xstream.annotations.XStreamConverter;
import com.thoughtworks.xstream.annotations.XStreamImplicit;
import com.thoughtworks.xstream.annotations.XStreamOmitField;

@XStreamAlias("string")
public class StockTradeEntity
{
	@XStreamAlias("Stockitem_Changerate_FluctuaterateResult")
	private StockTradeResult result;
	public StockTradeResult get_StockTradeResult() 
    {
        return result;
    }
    public void set_StockTradeResult(StockTradeResult value) 
    {
        this.result = value;
    }
    
    public static class StockTradeResult
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
	    
	    @XStreamAlias("Stockitem_Changerate_Fluctuaterates")
		private List<StockTrade> StockTradeitems;
	    
		public List<StockTrade> get_StockTradeitems() 
	    {
	        return StockTradeitems;
	    }
	    public void set_StockTradeitems(List<StockTrade> value) 
	    {
	        this.StockTradeitems = value;
	    }
	    
	    @XStreamAlias("Stockitem_Changerate_Fluctuaterate")
		public static class StockTrade
		{
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
		    
		    @XStreamAlias("ChangerateMain")
		    private boolean ChangerateMain;
		    public boolean get_ChangerateMain()
		    {
		    	return ChangerateMain;
		    }
		    public void set_ChangerateMain(boolean value)
		    {
		    	this.ChangerateMain = value;
		    }
		    
		    @XStreamAlias("TradeDays")
		    private int TradeDays;
		    public int get_TradeDays()
		    {
		    	return TradeDays;
		    }
		    public void set_TradeDays(int value)
		    {
		    	this.TradeDays = value;
		    }
		}
    }
}
