package com.stockside.model;

import com.lidroid.xutils.db.annotation.Column;
import com.lidroid.xutils.db.annotation.Foreign;
import com.lidroid.xutils.db.annotation.Id;
import com.lidroid.xutils.db.annotation.Table;
import com.lidroid.xutils.db.annotation.Transient;

import com.stockside.entity.*;

@Table(name = "DaysChangeRate")
public class DaysChangeRate
{
	@Column(column = "StockCode")
	private String StockCode;
	
	@Column(column = "StockName")
	private String StockName;
	
	@Column(column = "ClosePrice")
	private double ClosePrice;
	
	@Column(column = "FluctuateRate")
	private double FluctuateRate;
	
	@Column(column = "ChangeRate")
	private double ChangeRate;
	
	@Column(column = "ChangerateMain")
	private boolean ChangerateMain;
	
	@Column(column = "TradeDays")
	private int TradeDays;
	
	public String getStockCode()
	{
		return StockCode;
	}	
	public void setStockCode(String stockcode)
	{
		StockCode = stockcode;
	}
	
	public String getStockName()
	{
		return StockName;
	}	
	public void setStockName(String stockname)
	{
		StockName = stockname;
	}
	
	public double getClosePrice()
	{
		return ClosePrice;
	}	
	public void setClosePrice(double closeprice)
	{
		ClosePrice = closeprice;
	}
	
	public double getFluctuateRate()
	{
		return FluctuateRate;
	}	
	public void setFluctuateRate(double fluctuaterate)
	{
		FluctuateRate = fluctuaterate;
	}
	
	public double getChangeRate()
	{
		return ChangeRate;
	}	
	public void setChangeRate(double changerate)
	{
		ChangeRate = changerate;
	}
	
	public boolean getChangerateMain()
	{
		return ChangerateMain;
	}	
	public void setChangerateMain(boolean changeratemain)
	{
		ChangerateMain = changeratemain;
	}
	
	public int getTradeDays()
	{
		return TradeDays;
	}	
	public void setTradeDays(int tradedays)
	{
		TradeDays = tradedays;
	}
	
	public static DaysChangeRate ConvertTo(StockTradeEntity.StockTradeResult.StockTrade item)
	{
		DaysChangeRate rate = new DaysChangeRate();
		rate.setStockCode(item.get_StockCode());
		rate.setStockName(item.get_StockName());
		rate.setClosePrice(item.get_ClosePrice());
		rate.setFluctuateRate(item.get_FluctuateRate());
		rate.setChangeRate(item.get_ChangeRate());
		rate.setChangerateMain(item.get_ChangerateMain());
		rate.setTradeDays(item.get_TradeDays());
		return rate;
	}
}
