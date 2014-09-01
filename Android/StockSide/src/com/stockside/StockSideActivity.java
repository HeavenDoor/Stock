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


public class StockSideActivity extends ActionBarActivity implements View.OnClickListener
{
	private ActionBar actionBar;
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		
		super.onCreate(savedInstanceState);

		setContentView(R.layout.activity_stock_side); 
		
        initView();
		
		initData();
		
	}
	
	@Override
    public boolean onCreateOptionsMenu(Menu menu) 
     {
		getMenuInflater().inflate(R.menu.stock_side, menu);   
		/*MenuItem actionItemset = menu.add("settings");
         actionItemset.setShowAsAction(MenuItem.SHOW_AS_ACTION_ALWAYS);
         actionItemset.setIcon(R.drawable.setting);
         
         MenuItem actionItemsearch = menu.add("Search");
         actionItemsearch.setShowAsAction(MenuItem.SHOW_AS_ACTION_ALWAYS);
         actionItemsearch.setIcon(R.drawable.search);*/
         
        return true;

    }
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) 
	{
		int id = item.getItemId();
	        if (id == R.id.action_setting) {
	            return true;
	        }
	        else 
		if( id == R.id.action_search)
	    {
			int num;
			num = 0;
			num++;
	            //this.finish();
	    }
		return super.onOptionsItemSelected(item);
	}
	
	private void initView()
	{
		//得到ActionBar
		actionBar = getSupportActionBar();
	}
    
    private void initData()
	{
    	//actionBar.setTitle("StockSide");
    	actionBar.setDisplayShowHomeEnabled(false);
    	actionBar.setDisplayHomeAsUpEnabled(false);
		//设置ActionBar标题不显示
		actionBar.setDisplayShowTitleEnabled(true);
		
		//设置ActionBar的背景
		//actionBar.setBackgroundDrawable(getResources().getDrawable(R.drawable.actionbar_gradient_bg));
		
		//设置ActionBar左边默认的图标是否可用
		actionBar.setDisplayUseLogoEnabled(false);
		
		//设置导航模式为Tab选项标签导航模式
		//actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_TABS);
		
		//设置ActinBar添加Tab选项标签
		//actionBar.addTab(actionBar.newTab().setText("TAB1").setTabListener(new MyTabListener<FragmentPage1>(this,FragmentPage1.class)));
		//actionBar.addTab(actionBar.newTab().setText("TAB2").setTabListener(new MyTabListener<FragmentPage2>(this,FragmentPage2.class)));
		//actionBar.addTab(actionBar.newTab().setText("TAB3").setTabListener(new MyTabListener<FragmentPage3>(this,FragmentPage3.class)));
				
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
		 
		
	}
	private CharSequence updatetime()
	{
		return (new Date().toString());
	}
	

}
