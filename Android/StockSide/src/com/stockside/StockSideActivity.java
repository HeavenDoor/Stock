package com.stockside;

import java.util.Date;

import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.ActionBar;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SimpleCursorAdapter.ViewBinder;
import android.R.integer;
import android.R.string;
import android.database.DatabaseUtils;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.os.Build;
import android.view.Window;
import java.util.Date;

public class StockSideActivity extends ActionBarActivity implements View.OnClickListener
{
	Button btn;
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		
		super.onCreate(savedInstanceState);
		
		
		this.requestWindowFeature(Window.FEATURE_CUSTOM_TITLE);
		setContentView(R.layout.activity_stock_side); 
		getWindow().setFeatureInt(Window.FEATURE_CUSTOM_TITLE, R.layout.title);
		
		btn = (Button)findViewById(R.id.button);
		btn.setOnClickListener( this );
		/*{
			
			@Override
			public void onClick(View arg0)
			{
				btn.setText("Äãµã»÷ÁËButton");   
				
			}
		});*/
		//setContentView(btn); // R.layout.activity_stock_side

	}
	
	@Override
	protected void onStart()
	{
		super.onStart();
	}
	
	@Override 
	protected void onStop()
	{
		super.onStop();
	}
	
	@Override
	protected void onPause()
	{
		super.onPause();
	}
	
	@Override 
	protected void onResume()
	{
		super.onResume();
	}
	
	@Override
	protected void onRestart()
	{
		super.onRestart();
	}
	@Override
	protected void onDestroy()
	{
		super.onDestroy();
	}
	
	@Override
	public void onClick(View arg0)
	{
		btn.setText(updatetime());   
		
	}
	private CharSequence updatetime()
	{
		return (new Date().toString());
	}
	
	/**
	 * A placeholder fragment containing a simple view.
	 */
	public static class PlaceholderFragment extends Fragment 
	{

		public PlaceholderFragment() {}

		@Override
		public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
		{
			View rootView = inflater.inflate(R.layout.fragment_stock_side, container, false);
			return rootView;
		}
	}

}
