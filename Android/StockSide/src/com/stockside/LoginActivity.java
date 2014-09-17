package com.stockside;

import android.app.Activity;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.view.Menu;
import android.view.MenuItem;


import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import android.support.v7.app.ActionBarActivity;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SimpleCursorAdapter.ViewBinder;
import android.R.integer;
import android.R.string;
import android.database.DatabaseUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.os.Build;
import android.view.Window;
import android.content.*;
import android.widget.*;
import android.view.*;

import com.lidroid.xutils.HttpUtils;
import com.lidroid.xutils.ViewUtils;
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
import com.stockside.device.PhoneDevice;
import com.stockside.entity.BaseResult;

import org.apache.http.impl.cookie.BasicClientCookie;


import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

import com.stockside.model.*;

public class LoginActivity extends Activity implements View.OnClickListener
{
	private TextView register; //ActionBar
	private Button btn;
	private EditText mail;
	private EditText secret;
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		String yy = "";
		if(savedInstanceState != null)
		  yy = savedInstanceState.getString("mail");
		Bundle bundle = getIntent().getExtras();

		mail = (EditText)this.findViewById(R.id.edit_mailbox);
		if(bundle != null)
		{
			mail.setText(bundle.getString("mail")); 
		}
		secret = (EditText)this.findViewById(R.id.login_edit_password);
		btn = (Button)this.findViewById(R.id.stockside_login);		
		register = (TextView)this.findViewById(R.id.registUser);
		
		String url =this.getString(R.string.service);
		
		
		{
			DbUtils db = DbUtils.create(this);
			
	    	User user = new User(); //这里需要注意的是User对象必须有id属性，或者有通过@ID注解的属性
	    	user.setDefault(true);
	    	user.setEmail("866058573@qq.com");
	    	user.setPassword("123465");
	    	
	    	//user.setId(20);
	    	try
	    	{
	    		//db.dropDb();
	    		db.save(user); // 使用saveBindingId保存实体时会为实体的id赋值
	    	} 
	    	catch (DbException e) 
	    	{  
	    		e.printStackTrace();  
	    	} 
	    	

		}
	}
		
	public void onLoginClicked(View v)
	{
		register.setText(this.getString(R.string.registeruser));
		HttpUtils http = new HttpUtils();
		RequestParams params = new RequestParams();
		
		String md5PWD = MD5.GenMD5(secret.getText().toString());
		
        params.addBodyParameter("email", mail.getText().toString());
        params.addBodyParameter("passWord", md5PWD);
        
        
        
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
                    		register.setText(result.get_LogicBase().getReturnMessage());
                    		return;
                    	}
                    	
                    	Intent intent = new Intent();  
                        intent.setClass(LoginActivity.this, StockSideActivity.class);  
                        startActivity(intent);  
                        finish();
                    	
                    }


                    @Override
                    public void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    }
                });
	}
	
	public void onRegisterClicked(View v)
	{
		Intent intent = new Intent();  
        intent.setClass(LoginActivity.this, RegisterActivity.class);  
        startActivity(intent);  
        finish();//停止当前的Activity,如果不写,则按返回键会跳转回原来的Activity 
	}
	
	public void onClick(View v) 
	{
	
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		getMenuInflater().inflate(R.menu.login, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item)
	{
		int id = item.getItemId();
		if (id == R.id.action_settings)
		{
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
}
