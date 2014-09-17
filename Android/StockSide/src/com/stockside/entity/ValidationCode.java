package com.stockside.entity;

import android.R.integer;

import com.thoughtworks.xstream.annotations.XStreamAlias;
import com.thoughtworks.xstream.annotations.XStreamAsAttribute;
import com.thoughtworks.xstream.annotations.XStreamConverter;
import com.thoughtworks.xstream.annotations.XStreamImplicit;
import com.thoughtworks.xstream.annotations.XStreamOmitField;

@XStreamAlias("string")
public class ValidationCode
{
	@XStreamAlias("ValidationCodeResult")
	private ValidationCodeResult result;
	public ValidationCodeResult get_ValidationCodeResult() 
    {
        return result;
    }
    public void set_ValidationCodeResult(ValidationCodeResult value) 
    {
        this.result = value;
    }
    
    
	
	public static class ValidationCodeResult 
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
	    
		@XStreamAlias("Image")
		private String image;
		public String getImage() 
	    {
	        return image;
	    }
	    public void setImage(String value) 
	    {
	        this.image = value;
	    }
	}
	
}
