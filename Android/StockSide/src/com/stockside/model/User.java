package com.stockside.model;


import com.lidroid.xutils.db.annotation.Column;
import com.lidroid.xutils.db.annotation.Foreign;
import com.lidroid.xutils.db.annotation.Id;
import com.lidroid.xutils.db.annotation.Table;
import com.lidroid.xutils.db.annotation.Transient;

@Table(name = "User")
public class User //extends EntityBase
{
	//@primary(column = "Email")
	@Id(column = "Email")
	private String Email;
	
	@Column(column = "Password")
	
	private String Password;
	
	@Column(column = "ISDefault")
	private Boolean ISDefault;
	
	public String getEmail()
	{
		return Email;
	}
	
	public void setEmail(String email)
	{
		Email = email;
	}
		
	public String getPassword()
	{
		return Password;
	}
	
	public void setPassword(String password)
	{
		Password = password;
	}
	
	public Boolean getDefaule()
	{
		return ISDefault;
	}
	
	public void setDefault(Boolean value)
	{
		ISDefault = value;
	}
	
	@Override
	public String toString() 
	{
		return "";
	}
}
