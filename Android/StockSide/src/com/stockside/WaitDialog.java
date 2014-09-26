package com.stockside;

import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.os.Bundle;
import android.app.ActionBar.LayoutParams;
import android.view.animation.Animation;
import android.view.animation.LinearInterpolator;
import android.view.animation.RotateAnimation;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;


public class WaitDialog extends Dialog 
{
		private TextView textView;
			
		public WaitDialog(Context context, int id) 
		{
			super(context, id);
			
			setTitle(null);
			setCancelable(false);
			setOnCancelListener(null);
			setContentView(R.layout.wheel);
			
		}
			
		@Override
		public void show() 
		{
			super.show();
		}
}
	

