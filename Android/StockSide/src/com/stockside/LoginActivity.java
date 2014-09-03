package com.stockside;

import android.app.Activity;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.view.Menu;
import android.view.MenuItem;


import java.util.Date;

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
import android.os.Build;
import android.view.Window;
import android.content.*;
import android.widget.*;
import android.view.*;

public class LoginActivity extends Activity implements View.OnClickListener
{
	private TextView register; //ActionBar
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);

		setContentView(R.layout.activity_login);
		
		register=(TextView)this.findViewById(R.id.registUser);
		register.setOnClickListener(new View.OnClickListener(){    
            @Override  
            public void onClick(View v) {  
                Intent intent = new Intent();  
                intent.setClass(LoginActivity.this, RegisterActivity.class);  
                startActivity(intent);  
                finish();//停止当前的Activity,如果不写,则按返回键会跳转回原来的Activity        
            }  
              
        });
		if (true)
		{
			/*Intent intent = new Intent();  
	        intent.setClass(LoginActivity.this, StockSideActivity.class);  
	        startActivity(intent);  
	        finish();*/
		}
		/*Button btnLogin =(Button)findViewById(R.id.stockside_login);
		btnLogin.setOnClickListener(new View.OnClickListener(){    
            @Override  
            public void onClick(View v) {  
                Intent intent = new Intent();  
                intent.setClass(LoginActivity.this, StockSideActivity.class);  
                startActivity(intent);  
                finish();//停止当前的Activity,如果不写,则按返回键会跳转回原来的Activity        
            }  
              
        }); */
	}
	
	public void onLoginClicked(View v)
	{
		Intent intent = new Intent();  
        intent.setClass(LoginActivity.this, StockSideActivity.class);  
        startActivity(intent);  
        finish();//停止当前的Activity,如果不写,则按返回键会跳转回原来的Activity 
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
		int ItemId = v.getId();//获取组件的id值
		Intent intent = new Intent();
		switch (ItemId) 
		{
		case R.id.stockside_login:
			  
            intent.setClass(LoginActivity.this, StockSideActivity.class);  
            startActivity(intent);  
            finish();//停止当前的Activity,如果不写,则按返回键会跳转回原来的Activity  
            break;
        default:
        	break;
            	
		}
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.login, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item)
	{
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings)
		{
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
}
