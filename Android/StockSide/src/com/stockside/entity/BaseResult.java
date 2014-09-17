package com.stockside.entity;

import com.stockside.entity.ValidationCode.ValidationCodeResult;
import com.thoughtworks.xstream.annotations.XStreamAlias;
import com.thoughtworks.xstream.annotations.XStreamAsAttribute;
import com.thoughtworks.xstream.annotations.XStreamConverter;
import com.thoughtworks.xstream.annotations.XStreamImplicit;
import com.thoughtworks.xstream.annotations.XStreamOmitField;

@XStreamAlias("string")
public class BaseResult
{
	@XStreamAlias("LogicBase")
	private LogicBase result;
	public LogicBase get_LogicBase() 
    {
        return result;
    }
    public void set_LogicBase(LogicBase value) 
    {
        this.result = value;
    }
    
    public static class LogicBase 
	{
    	@XStreamAlias("ErrorType")
		private int errorType;
		public int getErrorType() 
	    {
	        return errorType;
	    }
	    public void setErrorType(int value) 
	    {
	        this.errorType = value;
	    }
		
		@XStreamAlias("ErrorID")
		private int errorID;
		public int getErrorID() 
	    {
	        return errorID;
	    }
	    public void setErrorID(int value) 
	    {
	        this.errorID = value;
	    }
	    
		@XStreamAlias("ReturnMessage")
		private String returnMessage;
		public String getReturnMessage() 
	    {
	        return returnMessage;
	    }
	    public void setReturnMessage(String value) 
	    {
	        this.returnMessage = value;
	    }
	}
}
