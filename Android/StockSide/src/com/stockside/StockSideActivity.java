package com.stockside;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.ActionBar;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SimpleCursorAdapter.ViewBinder;
import android.R.bool;
import android.R.integer;
import android.R.string;
import android.content.Entity;
import android.content.Intent;
import android.database.DatabaseUtils;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.os.Build;
import android.view.Window;

import com.lidroid.xutils.HttpUtils;
import com.lidroid.xutils.ViewUtils;
import com.lidroid.xutils.db.sqlite.Selector;
import com.lidroid.xutils.db.sqlite.WhereBuilder;
import com.lidroid.xutils.exception.DbException;
import com.lidroid.xutils.exception.HttpException;
import com.lidroid.xutils.http.RequestParams;
import com.lidroid.xutils.http.ResponseInfo;
import com.lidroid.xutils.http.ResponseStream;
import com.lidroid.xutils.http.callback.RequestCallBack;
import com.lidroid.xutils.http.client.HttpRequest;
import com.lidroid.xutils.http.client.HttpRequest.HttpMethod;
import com.lidroid.xutils.util.PreferencesCookieStore;
import com.lidroid.xutils.util.LogUtils;
import com.lidroid.xutils.view.ResType;
import com.lidroid.xutils.view.annotation.ResInject;
import com.lidroid.xutils.view.annotation.ViewInject;
import com.lidroid.xutils.view.annotation.event.OnClick;
import com.lidroid.xutils.DbUtils;

import com.stockside.common.MD5;
import com.stockside.common.XmlDeal;
import com.stockside.common.XmlUtil;
import com.stockside.entity.BaseResult;
import com.stockside.entity.StockItemEntity;
import com.stockside.entity.StockTradeEntity;
import com.stockside.model.*;
import com.stockside.data.*;

public class StockSideActivity extends ActionBarActivity implements View.OnClickListener
{
	private ActionBar actionBar;
	private DbUtils db;
	private String email;
	private String pwd;
	private boolean hasLogin;
	private boolean hasUpdate;
	private WaitDialog progressDialog;
	private JobState jobState;
	private final int tradeDays[] = {2, 3, 5, 10, 15, 30, 45, 60};

	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_stock_side); 	
		hasLogin = false;
		hasUpdate = true;
		jobState = new JobState();
		db = DbUtils.create(this);

		try
    	{
    		List<User> users = db.findAll(Selector.from(User.class)); 
    		if( users == null || users.size() != 1)
    		{
    			hasLogin = false;
    		}
    		else 
    		{
    			email = users.get(0).getEmail();
    			pwd = users.get(0).getPassword();
    			hasLogin = true;
    			//db.deleteAll(users);
    		}
    	} 
    	catch (DbException e) 
    	{  
    		e.printStackTrace();  
    	}     

		
		if(!hasLogin)
		{
			Intent intent = new Intent();  
		    intent.setClass(StockSideActivity.this, LoginActivity.class);  
		    startActivity(intent);  
		    finish();
		}
		else
		{
			//jobState.set_loadFinish();
			autoLogin();
		}
		new LoadViewTask().execute();
		
        initView();		
		initData();
		
		
		
	}
	
	private void loadDbData()
	{
		DataControl.LoadDataFromDB(db);
	}
	
	private void loadProgramData()
	{
		loadDailyChangeRate();
		loadDailyFluctuateRate();
		loadAbsoluteRecentDaysData();
		/*for(int trade : tradeDays)
		{
			loadDaysChangeRateAndFluctuateRate(trade, true);
			loadDaysChangeRateAndFluctuateRate(trade, false);
		}*/
		
	}
	
	private void loadLastUpdate()
	{
		HttpUtils http = new HttpUtils();
		RequestParams params = new RequestParams();
        params.addBodyParameter("email", email);
        params.addBodyParameter("passWord", pwd);

        String url = this.getString(R.string.service) + "GetLastUpdate";
        http.send(HttpRequest.HttpMethod.POST,
                url,
                params,
                new RequestCallBack<String>() {

                    @Override
                    public void onStart() 
                    {
                    } 

                    @Override
                    public void onLoading(long total, long current, boolean isUploading) 
                    {
                    }

                    @Override
                    public void onSuccess(ResponseInfo<String> responseInfo) 
                    {
                    	String kkString = XmlDeal.DealXml(responseInfo.result);
                    	BaseResult result = XmlUtil.toBean(kkString, BaseResult.class);
                    	if(result.get_LogicBase().getErrorID() != 0 || result.get_LogicBase().getErrorType() != 0 )
                    	{
                    		jobState.setFinish();
                    		Intent intent = new Intent();  
                		    intent.setClass(StockSideActivity.this, LoginActivity.class);  
                		    startActivity(intent);  
                		    finish();
                    	}
                    	
                    	LastUpdate lastUpdate = new LastUpdate();
                    	lastUpdate.setLastUpdate(result.get_LogicBase().getReturnMessage());
                    	try
            	    	{
                    		List<LastUpdate> list = db.findAll(Selector.from(LastUpdate.class));
                    		if(list == null)
                    		{
                    			hasUpdate = true;
                    			db.save(lastUpdate);
                    		}
                    		else
                    		{
                    			if(list.size() == 0) 
                    				hasUpdate = true;
                    			else if(list.get(0).getLastUpdate() != result.get_LogicBase().getReturnMessage())
                    				hasUpdate = true;
                    			else hasUpdate = false;
                    		
                    			db.deleteAll(list);
                    			db.save(lastUpdate);
                    		}
            	    	}
                		catch (DbException e) 
            	    	{  
            	    		e.printStackTrace();  
            	    	}
                    	if(hasUpdate)
                    	{
                    		loadProgramData();
                    	}
                    	else
                    	{
                    		loadDbData();
                    		progressDialog.dismiss();
                    	}
                    }

                    @Override
                    public void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    }
                });
	}
	
	private void loadDaysChangeRateAndFluctuateRate(int days, boolean ischangerate)
	{
		/*HttpUtils http = new HttpUtils();
		final int tradedays = days;
		final boolean changerate = ischangerate;
		RequestParams params = new RequestParams();
        params.addBodyParameter("email", email);
        params.addBodyParameter("passWord", pwd);
        params.addBodyParameter("days", String.valueOf(days));
        String url;
        if(ischangerate)
        	url = this.getString(R.string.service) + "GetRecentDaysChangeRate";
        else
        	url = this.getString(R.string.service) + "GetRecentDaysFluctuateRate";
        http.send(HttpRequest.HttpMethod.POST,
                url,
                params,
                new RequestCallBack<String>() {
                    @Override
                    public synchronized void onStart() 
                    {
                    } 
                    @Override
                    public synchronized void onLoading(long total, long current, boolean isUploading) 
                    {
                    }
                    @Override
                    public synchronized void onSuccess(ResponseInfo<String> responseInfo) 
                    {                    
                    	String kkString = XmlDeal.DealXml(responseInfo.result);
                    	//Log.i("days"+tradedays,kkString);
                    	StockTradeEntity result = XmlUtil.toBean(kkString, StockTradeEntity.class);
                    	if(result.get_StockTradeResult().get_ErrorID() != 0 || result.get_StockTradeResult().get_ErrorType() != 0 )
                    	{
                    		jobState.setFinish();
                    		Intent intent = new Intent();  
                		    intent.setClass(StockSideActivity.this, LoginActivity.class);  
                		    startActivity(intent);  
                		    finish();
                    	}
                    	clearDaysTradeData(tradedays, changerate);
                    	Log.i("clearDaysTradeData "+tradedays,"changerate" + changerate );
                    	
                    	List<DaysChangeRate> changeRates = new ArrayList<DaysChangeRate>(); 
                    	DaysChangeRate rate;
                    	for( StockTradeEntity.StockTradeResult.StockTrade item : result.get_StockTradeResult().get_StockTradeitems() )
                    	{
                    		rate = DaysChangeRate.ConvertTo(item);
                    		changeRates.add(rate);
                    		try
                	    	{
                    			db.save(rate);
                	    	}
                    		catch (DbException e) 
                	    	{  
                	    		e.printStackTrace();  
                	    	} 
                    	}
                    	switch(tradedays)
                    	  {
                    	  case 2:
                    		  if(changerate)
                    		  {
                    			  DataControl.getInstance().set_DaysChangeRates2(changeRates);
                    			  jobState.set_loadDaysChangeRate2(true);
                    		  }
                    		  else
                    		  {
                    			  DataControl.getInstance().set_DaysFluctuateRates2(changeRates);
                    			  jobState.set_loadDaysFluctuateRate2(true);
                    		  }
                    		  break;
                    	  case 3:
                    		  if(changerate)
                    		  {
                    			  DataControl.getInstance().set_DaysChangeRates3(changeRates);
                    			  jobState.set_loadDaysChangeRate3(true);
                    		  }
                    		  else
                    		  {
                    			  DataControl.getInstance().set_DaysFluctuateRates3(changeRates);
                    			  jobState.set_loadDaysFluctuateRate3(true);
                    		  }
                    		  break;
                    	  case 5:
                    		  if(changerate)
                    		  {
                    			  DataControl.getInstance().set_DaysChangeRates5(changeRates);
                    			  jobState.set_loadDaysChangeRate5(true);
                    		  }
                    		  else
                    		  {
                    			  DataControl.getInstance().set_DaysFluctuateRates5(changeRates);
                    			  jobState.set_loadDaysFluctuateRate5(true);
                    		  }
                    		  break;
                    	  case 10:
                    		  if(changerate)
                    		  {
                    			  DataControl.getInstance().set_DaysChangeRates10(changeRates);
                    			  jobState.set_loadDaysChangeRate10(true);
                    		  }
                    		  else
                    		  {
                    			  DataControl.getInstance().set_DaysFluctuateRates10(changeRates);
                    			  jobState.set_loadDaysFluctuateRate10(true);
                    		  }
                    		  break;
                    	  case 15:
                    		  if(changerate)
                    		  {
                    			  DataControl.getInstance().set_DaysChangeRates15(changeRates);
                    			  jobState.set_loadDaysChangeRate15(true);
                    		  }
                    		  else
                    		  {
                    			  DataControl.getInstance().set_DaysFluctuateRates15(changeRates);
                    			  jobState.set_loadDaysFluctuateRate15(true);
                    		  }
                    		  break;
                    	  case 30:
                    		  if(changerate)
                    		  {
                    			  DataControl.getInstance().set_DaysChangeRates30(changeRates);
                    			  jobState.set_loadDaysChangeRate30(true);
                    		  }
                    		  else
                    		  {
                    			  DataControl.getInstance().set_DaysFluctuateRates30(changeRates);
                    			  jobState.set_loadDaysFluctuateRate30(true);
                    		  }
                    		  break;
                    	  case 45:
                    		  if(changerate)
                    		  {
                    			  DataControl.getInstance().set_DaysChangeRates45(changeRates);
                    			  jobState.set_loadDaysChangeRate45(true);
                    		  }
                    		  else
                    		  {
                    			  DataControl.getInstance().set_DaysFluctuateRates45(changeRates);
                    			  jobState.set_loadDaysFluctuateRate45(true);
                    		  }
                    		  break;
                    	  case 60:
                    		  if(changerate)
                    		  {
                    			  DataControl.getInstance().set_DaysChangeRates60(changeRates);
                    			  jobState.set_loadDaysChangeRate60(true);
                    		  }
                    		  else
                    		  {
                    			  DataControl.getInstance().set_DaysFluctuateRates60(changeRates);
                    			  jobState.set_loadDaysFluctuateRate60(true);
                    		  }
                    		  break;
                    	  default:
                    		  break;
                    	  }

                    }

                    @Override
                    public synchronized void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    }
                });*/
	}
	
	private void clearDaysTradeData(int days, boolean ischangerate)
	{
		try
		{
			List<DaysChangeRate> list = db.findAll(Selector.from(DaysChangeRate.class).where(WhereBuilder.b("ChangerateMain","=",ischangerate).and("TradeDays","=",days)));
			if(list != null)
			{
				db.deleteAll(list);
			}
		}
		catch (DbException e) 
    	{  
    		e.printStackTrace();  
    	} 
	}
	
	private void clearDaysTradeData()
	{
		try
		{
			List<DaysChangeRate> list = db.findAll(Selector.from(DaysChangeRate.class));
			if(list != null)
			{
				db.deleteAll(list);
			}
		}
		catch (DbException e) 
    	{  
    		e.printStackTrace();  
    	} 
	}
	
	private void loadAbsoluteRecentDaysData()
	{
		HttpUtils http = new HttpUtils();

		RequestParams params = new RequestParams();
        params.addBodyParameter("email", email);
        params.addBodyParameter("passWord", pwd);
        String url = this.getString(R.string.service) + "GetAbsoluteRecentDaysData";
        http.send(HttpRequest.HttpMethod.POST,
                url,
                params,
                new RequestCallBack<String>() {
                    @Override
                    public synchronized void onStart() 
                    {
                    } 
                    @Override
                    public synchronized void onLoading(long total, long current, boolean isUploading) 
                    {
                    }
                    @Override
                    public synchronized void onSuccess(ResponseInfo<String> responseInfo) 
                    {                    
                    	String kkString = XmlDeal.DealXml(responseInfo.result);
                    	//Log.i("days"+tradedays,kkString);
                    	StockTradeEntity result = XmlUtil.toBean(kkString, StockTradeEntity.class);
                    	if(result.get_StockTradeResult().get_ErrorID() != 0 || result.get_StockTradeResult().get_ErrorType() != 0 )
                    	{
                    		jobState.setFinish();
                    		Intent intent = new Intent();  
                		    intent.setClass(StockSideActivity.this, LoginActivity.class);  
                		    startActivity(intent);  
                		    finish();
                    	}
                    	clearDaysTradeData();
                    	//Log.i("clearDaysTradeData "+tradedays,"changerate" + changerate );
                    	
                    	List<DaysChangeRate> changeRates = new ArrayList<DaysChangeRate>(); 
                    	DaysChangeRate rate;
                    	for( StockTradeEntity.StockTradeResult.StockTrade item : result.get_StockTradeResult().get_StockTradeitems() )
                    	{
                    		rate = DaysChangeRate.ConvertTo(item);
                    		changeRates.add(rate);
                    		try
                	    	{
                    			db.save(rate);
                	    	}
                    		catch (DbException e) 
                	    	{  
                	    		e.printStackTrace();  
                	    	} 
                    	}
                    	DataControl.getInstance().set_AbsoluteRecentDaysData(changeRates);
                    	loadDbData();
                    	jobState.set_loadAbsoluteRecentDaysData(true);
                    	
                    	if(jobState.getFinish())
                    	{
                    		progressDialog.dismiss();
                    	}
                    }

                    @Override
                    public synchronized void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    }
                });
	}
	
 	private void loadDailyChangeRate()
	{
		HttpUtils http = new HttpUtils();
		RequestParams params = new RequestParams();
        params.addBodyParameter("email", email);
        params.addBodyParameter("passWord", pwd);
        String url = this.getString(R.string.service) + "GetTodayChangeRate";
        http.send(HttpRequest.HttpMethod.POST,
                url,
                params,
                new RequestCallBack<String>() {
                    @Override
                    public void onStart() 
                    {
                    } 
                    @Override
                    public void onLoading(long total, long current, boolean isUploading) 
                    {
                    }
                    @Override
                    public void onSuccess(ResponseInfo<String> responseInfo) 
                    {
                    	String kkString = XmlDeal.DealXml(responseInfo.result);
                    	StockItemEntity result = XmlUtil.toBean(kkString, StockItemEntity.class);
                    	if(result.get_StockItemResult().get_ErrorID() != 0 || result.get_StockItemResult().get_ErrorType() != 0 )
                    	{
                    		jobState.setFinish();
                    		Intent intent = new Intent();  
                		    intent.setClass(StockSideActivity.this, LoginActivity.class);  
                		    startActivity(intent);  
                		    finish();
                    	}
                    	 
                    	try
            	    	{
                    		List<DailyChangeRate> list = db.findAll(Selector.from(DailyChangeRate.class));
                    		if(list != null)
                    		{
                    			db.deleteAll(list);
                    		}
            	    	}
                		catch (DbException e) 
            	    	{  
            	    		e.printStackTrace();  
            	    	} 
        	    		
                    	List<DailyChangeRate> changeRates = new ArrayList<DailyChangeRate>(); 
                    	DailyChangeRate rate;
                    	for( StockItemEntity.StockItemResult.StockItem item : result.get_StockItemResult().get_StockItems() )
                    	{
                    		rate = DailyChangeRate.ConvertTo(item);
                    		changeRates.add(rate);
                    		try
                	    	{
                    			db.save(rate);
                	    	}
                    		catch (DbException e) 
                	    	{  
                	    		e.printStackTrace();  
                	    	} 
                    	}
                    	DataControl.getInstance().set_DailyChangeRates(changeRates);
                    	jobState.set_loadDailyChangeRate(true);
                    	if(jobState.getFinish())
                    	{
                    		progressDialog.dismiss();
                    	}
                    }

                    @Override
                    public void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    }
                });
	}
	
	private void loadDailyFluctuateRate()
	{
		HttpUtils http = new HttpUtils();
		RequestParams params = new RequestParams();
        params.addBodyParameter("email", email);
        params.addBodyParameter("passWord", pwd);
        String url = this.getString(R.string.service) + "GetTodayFluctuateRate";
        http.send(HttpRequest.HttpMethod.POST,
                url,
                params,
                new RequestCallBack<String>() {
                    @Override
                    public void onStart() 
                    {
                    } 
                    @Override
                    public void onLoading(long total, long current, boolean isUploading) 
                    {
                    }
                    @Override
                    public void onSuccess(ResponseInfo<String> responseInfo) 
                    {
                    	String kkString = XmlDeal.DealXml(responseInfo.result);
                    	StockItemEntity result = XmlUtil.toBean(kkString, StockItemEntity.class);
                    	if(result.get_StockItemResult().get_ErrorID() != 0 || result.get_StockItemResult().get_ErrorType() != 0 )
                    	{
                    		jobState.setFinish();
                    		Intent intent = new Intent();  
                		    intent.setClass(StockSideActivity.this, LoginActivity.class);  
                		    startActivity(intent);  
                		    finish();
                    	}
                    	try
            	    	{
                    		List<DailyFluctuateRate> list = db.findAll(Selector.from(DailyFluctuateRate.class));
                    		if(list != null)
                    		{
                    			db.deleteAll(list);
                    		}
            	    	}
                		catch (DbException e) 
            	    	{  
            	    		e.printStackTrace();  
            	    	} 
                    	List<DailyFluctuateRate> fluctuateRates = new ArrayList<DailyFluctuateRate>(); 
                    	DailyFluctuateRate rate;
                    	for( StockItemEntity.StockItemResult.StockItem item : result.get_StockItemResult().get_StockItems() )
                    	{
                    		rate = DailyFluctuateRate.ConvertTo(item);
                    		fluctuateRates.add(rate);
                    		try
                	    	{
                    			db.save(rate);
                	    	}
                    		catch (DbException e) 
                	    	{  
                	    		e.printStackTrace();  
                	    	} 
                    	}
                    	DataControl.getInstance().set_DailyFluctuateRates(fluctuateRates);
                    	jobState.set_loadDailyFluctuateRate(true);
                    	if(jobState.getFinish())
                    	{
                    		progressDialog.dismiss();
                    	}
                    }

                    @Override
                    public void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    }
                });
	}
	
	private void autoLogin()
	{
		HttpUtils http = new HttpUtils();
		RequestParams params = new RequestParams();
        params.addBodyParameter("email", email);
        params.addBodyParameter("passWord", pwd);

        String url = this.getString(R.string.service) + "Login";
        http.send(HttpRequest.HttpMethod.POST,
                url,
                params,
                new RequestCallBack<String>() {

                    @Override
                    public void onStart() 
                    {
                    } 

                    @Override
                    public void onLoading(long total, long current, boolean isUploading) 
                    {
                    }

                    @Override
                    public void onSuccess(ResponseInfo<String> responseInfo) 
                    {
                    	String kkString = XmlDeal.DealXml(responseInfo.result);
                    	BaseResult result = XmlUtil.toBean(kkString, BaseResult.class);
                    	if(result.get_LogicBase().getErrorID() != 0 || result.get_LogicBase().getErrorType() != 0 )
                    	{
                    		jobState.setFinish();
                    		Intent intent = new Intent();  
                		    intent.setClass(StockSideActivity.this, LoginActivity.class);  
                		    startActivity(intent);  
                		    finish();
                    	}
                    	
                    	loadLastUpdate();
                    }

                    @Override
                    public void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    }
                });
	}
	
	private class LoadViewTask extends AsyncTask<Void, Integer, Void>
    {
    	//Before running code in the separate thread
		@Override
		protected void onPreExecute() 
		{
			progressDialog = new WaitDialog(StockSideActivity.this, R.style.CustomProgressDialog);
			progressDialog.show();
		}

		@Override
		protected Void doInBackground(Void... params) 
		{
			try 
			{
				//Get the current thread's token
				synchronized (this) 
				{
					int counter = 0;
					
					//boolean log = jobState.get_loginFinish();
					while(true)//  !jobState.get_loginFinish()
					{
						this.wait(850);
						counter++;
						publishProgress(counter*25);
					}
				}
			} 
			catch (InterruptedException e) 
			{
				e.printStackTrace();
			}
			return null;
		}

		@Override
		protected void onProgressUpdate(Integer... values) 
		{
		}

		@Override
		protected void onPostExecute(Void result) 
		{
			progressDialog.dismiss();
			jobState.reset();
			//loadfinish = false;
			//loginfinish = false;
		} 	
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
	
	@Override
    public boolean onCreateOptionsMenu(Menu menu) 
     {
		getMenuInflater().inflate(R.menu.stock_side, menu);         
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
		actionBar.setDisplayUseLogoEnabled(false);
	}
		
	private CharSequence updatetime()
	{
		return (new Date().toString());
	}
		
	private class JobState
	{
		private boolean loaddailychangerate;
		private boolean loaddailyfluctuaterate;
		private boolean loadabsoluterecentdaysdata;

		public JobState()
		{
			loaddailychangerate = false;
			loaddailyfluctuaterate = false;
			loadabsoluterecentdaysdata = false;
		}
		
		public void reset()
		{
			loaddailychangerate = false;
			loaddailyfluctuaterate = false;
			loadabsoluterecentdaysdata = false;
		}
		
		public boolean getFinish()
		{
			boolean finish = loaddailychangerate&&loaddailyfluctuaterate&&loadabsoluterecentdaysdata;
			return finish;
		}
		
		public void setFinish()
		{
			loaddailychangerate = true;
			loaddailyfluctuaterate = true;
			loadabsoluterecentdaysdata = true;
		}
		
		public boolean get_loadDailyChangeRate()
		{
			return loaddailychangerate;
		}
		public void set_loadDailyChangeRate(boolean value)
		{
			loaddailychangerate = value;
		}
		
		public boolean get_loadDailyFluctuateRate()
		{
			return loaddailyfluctuaterate;
		}
		public void set_loadDailyFluctuateRate(boolean value)
		{
			loaddailyfluctuaterate = value;
		}
		
		public boolean get_loadAbsoluteRecentDaysData()
		{
			return loadabsoluterecentdaysdata;
		}
		public void set_loadAbsoluteRecentDaysData(boolean value)
		{
			loadabsoluterecentdaysdata = value;
		}
	}

}
