package com.stockside.common;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Base64;

public class BitmapConvert
{
	public static Bitmap stringtoBitmap(String string) 
	{
		// ���ַ���ת����Bitmap����
		Bitmap bitmap = null;
		try 
		{
			byte[] bitmapArray;
			bitmapArray = Base64.decode(string, Base64.DEFAULT);
			bitmap = BitmapFactory.decodeByteArray(bitmapArray, 0, bitmapArray.length);
		} 
		catch (Exception e) 
		{
			e.printStackTrace();
		}
		return bitmap;
	}
}
