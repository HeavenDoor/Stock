package com.stockside.device;

import android.app.Activity;
import android.os.Build;
import android.os.Bundle;
import android.telephony.CellLocation;     
import android.telephony.PhoneStateListener;     
import android.telephony.TelephonyManager; 

public class PhoneDevice extends  Activity
{
	public static String GetPhoneID(Activity act)
	{
		TelephonyManager tm = (TelephonyManager) act.getSystemService(TELEPHONY_SERVICE);
		return tm.getDeviceId();
	}
	
	public static String GetPhoneNumber(Activity act)
	{
		TelephonyManager tm = (TelephonyManager) act.getSystemService(TELEPHONY_SERVICE);
		return tm.getLine1Number();
	}
	
	public static String GetPhoneName()
	{
		return Build.MODEL;
	}
}
