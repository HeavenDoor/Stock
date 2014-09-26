package com.stockside.model;

import com.lidroid.xutils.db.annotation.Column;
import com.lidroid.xutils.db.annotation.Foreign;
import com.lidroid.xutils.db.annotation.Id;
import com.lidroid.xutils.db.annotation.Table;
import com.lidroid.xutils.db.annotation.Transient;

@Table(name = "LastUpdate")
public class LastUpdate
{
	@Id(column = "LastUpdate")
	private String LastUpdate;
	
	public String getLastUpdate()
	{
		return LastUpdate;
	}
	
	public void setLastUpdate(String value)
	{
		LastUpdate = value;
	}
}
