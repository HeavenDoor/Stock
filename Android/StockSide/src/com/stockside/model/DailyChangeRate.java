package com.stockside.model;

import com.lidroid.xutils.db.annotation.Column;
import com.lidroid.xutils.db.annotation.Foreign;
import com.lidroid.xutils.db.annotation.Id;
import com.lidroid.xutils.db.annotation.Table;
import com.lidroid.xutils.db.annotation.Transient;

import com.stockside.entity.*;

@Table(name = "DailyChangeRate")
public class DailyChangeRate
{
	@Id(column = "StockCode")
	private String StockCode;
	
	@Column(column = "StockDate")
	private String StockDate;
	
	@Column(column = "StockName")
	private String StockName;
	
	@Column(column = "OpenPrice")
	private double OpenPrice;
	
	@Column(column = "ClosePrice")
	private double ClosePrice;
	
	@Column(column = "HighestPrice")
	private double HighestPrice;
	
	@Column(column = "LowestPrice")
	private double LowestPrice;
	
	@Column(column = "FluctuateMount")
	private double FluctuateMount;
	
	@Column(column = "FluctuateRate")
	private double FluctuateRate;
	
	@Column(column = "ChangeRate")
	private double ChangeRate;
	
	@Column(column = "TradeVolume")
	private double TradeVolume;
	
	@Column(column = "TradeMount")
	private double TradeMount;
	
	@Column(column = "ToatlMarketCap")
	private double ToatlMarketCap;
	
	@Column(column = "CirculationMarketCap")
	private double CirculationMarketCap;
	
	
	public String getStockCode()
	{
		return StockCode;
	}	
	public void setStockCode(String stockcode)
	{
		StockCode = stockcode;
	}
	
	public String getStockDate()
	{
		return StockDate;
	}	
	public void setStockDate(String stockdate)
	{
		StockDate = stockdate;
	}
	
	public String getStockName()
	{
		return StockName;
	}	
	public void setStockName(String stockname)
	{
		StockName = stockname;
	}
	
	public double getOpenPrice()
	{
		return OpenPrice;
	}	
	public void setOpenPrice(double openprice)
	{
		OpenPrice = openprice;
	}
	
	public double getClosePrice()
	{
		return ClosePrice;
	}	
	public void setClosePrice(double closeprice)
	{
		ClosePrice = closeprice;
	}
	
	public double getHighestPrice()
	{
		return HighestPrice;
	}	
	public void setHighestPrice(double highestprice)
	{
		HighestPrice = highestprice;
	}
	
	public double getLowestPrice()
	{
		return LowestPrice;
	}	
	public void setLowestPrice(double lowestprice)
	{
		LowestPrice = lowestprice;
	}
	
	public double getFluctuateMount()
	{
		return FluctuateMount;
	}	
	public void setFluctuateMount(double fluctuatemount)
	{
		FluctuateMount = fluctuatemount;
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
	
	public double getTradeVolume()
	{
		return TradeVolume;
	}	
	public void setTradeVolume(double tradevolume)
	{
		TradeVolume = tradevolume;
	}
	
	public double getTradeMount()
	{
		return TradeMount;
	}	
	public void setTradeMount(double trademount)
	{
		TradeMount = trademount;
	}
	
	public double getToatlMarketCap()
	{
		return ToatlMarketCap;
	}	
	public void setToatlMarketCap(double toatlmarketcap)
	{
		ToatlMarketCap = toatlmarketcap;
	}
	
	public double getCirculationMarketCap()
	{
		return CirculationMarketCap;
	}	
	public void setCirculationMarketCap(double circulationmarketcap)
	{
		CirculationMarketCap = circulationmarketcap;
	}

	public static DailyChangeRate ConvertTo(StockItemEntity.StockItemResult.StockItem item)
	{
		DailyChangeRate rate = new DailyChangeRate();
		rate.setStockCode(item.get_StockCode());
		rate.setStockDate(item.get_StockDate());
		rate.setStockName(item.get_StockName());
		rate.setOpenPrice(item.get_OpenPrice());
		rate.setClosePrice(item.get_ClosePrice());
		rate.setHighestPrice(item.get_HighestPrice());
		rate.setLowestPrice(item.get_LowestPrice());
		rate.setFluctuateMount(item.get_FluctuateMount());
		rate.setFluctuateRate(item.get_FluctuateRate());
		rate.setChangeRate(item.get_ChangeRate());
		rate.setTradeVolume(item.get_TradeVolume());
		rate.setTradeMount(item.get_TradeMount());
		rate.setToatlMarketCap(item.get_ToatlMarketCap());
		rate.setCirculationMarketCap(item.get_CirculationMarketCap());
		return rate;
	}
}
