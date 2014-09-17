package com.stockside;

import android.app.Activity;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.view.Menu;
import android.view.MenuItem;


import java.io.IOException;
import java.io.InputStream;
import java.util.Date;
import java.util.Properties;

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
import android.graphics.*;
import android.util.*;

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
import org.apache.http.impl.cookie.BasicClientCookie;

import com.thoughtworks.xstream.XStream;  
import com.thoughtworks.xstream.io.HierarchicalStreamWriter;  
import com.thoughtworks.xstream.io.json.JettisonMappedXmlDriver;  
import com.thoughtworks.xstream.io.json.JsonHierarchicalStreamDriver;  
import com.thoughtworks.xstream.io.json.JsonWriter;

import com.stockside.entity.*;
import com.stockside.entity.ValidationCode.ValidationCodeResult;
import com.thoughtworks.xstream.XStream;
import com.thoughtworks.xstream.io.xml.DomDriver;

import com.stockside.common.*;
import com.stockside.device.*;

public class RegisterActivity extends Activity implements View.OnClickListener
{

	private Button btn;
	private ImageView image;
	private EditText mail;
	private EditText secret;
	private EditText vertify;
	private TextView errorText;
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_register);
		btn = (Button)this.findViewById(R.id.stockside_register);
		mail = (EditText)this.findViewById(R.id.register_edit_mailbox);
		secret = (EditText)this.findViewById(R.id.register_edit_password);
		vertify = (EditText)this.findViewById(R.id.register_edit_vertify);	
		image = (ImageView)this.findViewById(R.id.register_img);
		errorText = (TextView)this.findViewById(R.id.error_register);
		
		onNextImgClicked(null); // 获得验证码
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		getMenuInflater().inflate(R.menu.register, menu);
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
	
	public void onClick(View v)
	{
		
	}
	
	public void onRegisterClicked(View v)
	{
		errorText.setText("");
		if( !MailCheck.ISEemailFormat(mail.getText().toString()))
		{
			errorText.setText("邮箱格式错误");
			return;
		}
		if( secret.getText().toString().isEmpty() )
		{
			errorText.setText("未填写密码");
			return;
		}
		HttpUtils http = new HttpUtils();
		RequestParams params = new RequestParams();
		final String phoneID = PhoneDevice.GetPhoneID(this);
		final String phoneNumber = PhoneDevice.GetPhoneNumber(this);
		final String deviceName = PhoneDevice.GetPhoneName();

		String md5PWD = MD5.GenMD5(secret.getText().toString());
		
		params.addBodyParameter("userName", deviceName);	
		params.addBodyParameter("email", mail.getText().toString());
		params.addBodyParameter("pwd", md5PWD);
		params.addBodyParameter("validationCode", vertify.getText().toString());
		params.addBodyParameter("phone", phoneNumber);
		params.addBodyParameter("phoneID", phoneID);
		
		
		String url = this.getString(R.string.service) + "RegisterUser";
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
                    		errorText.setText(result.get_LogicBase().getReturnMessage());
                    		return;
                    	}
                    	
                    	Intent intent = new Intent();  
                        intent.setClass(RegisterActivity.this, LoginActivity.class); 
                    	Bundle bundle = new Bundle();  
                        bundle.putString("mail", mail.getText().toString());  
                        intent.putExtras(bundle);  
                        startActivity(intent);  
                        finish();
                    }


                    @Override
                    public void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    	//btn.setText(phoneNumber);
                    	int m = 0; 
                    	m++;
                    }
                });
	}
	
	public void onNextImgClicked(View v)
	{
		HttpUtils http = new HttpUtils();
		RequestParams params = new RequestParams();
		final String phoneID = PhoneDevice.GetPhoneID(this);
		final String phoneNumber = PhoneDevice.GetPhoneNumber(this);
        params.addBodyParameter("phoneID", phoneID);
        String app_name = this.getString(R.string.app_name);
        String md5 = MD5.GenMD5(app_name + phoneID + app_name);
        params.addBodyParameter("password", md5);

        String url = this.getString(R.string.service) + "GetValidationCode";
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
                    	//xstream.processAnnotations(ValidationCode.class);
                       	String kkString = XmlDeal.DealXml(responseInfo.result);
                    	//ValidationCode bb = (ValidationCode)xstream.fromXML(kkString);
                       	ValidationCode bb = XmlUtil.toBean(kkString, ValidationCode.class);
                    	Bitmap bmp = BitmapConvert.stringtoBitmap(bb.get_ValidationCodeResult().getImage());
                    	image.setImageBitmap(bmp);
                    	
                    	//btn.setText(phoneNumber);
                    	
                    	/*Intent intent = new Intent();  
                        intent.setClass(RegisterActivity.this, StockSideActivity.class);  
                        startActivity(intent);  
                        finish();//停止当前的Activity,如果不写,则按返回键会跳转回原来的Activity*/
                    }


                    @Override
                    public void onFailure(HttpException error, String msg) 
                    {
                    	//btn.setText(msg);
                    	//btn.setText(phoneNumber);
                    	int m = 0; 
                    	m++;
                    }
                });
	
	}
}
