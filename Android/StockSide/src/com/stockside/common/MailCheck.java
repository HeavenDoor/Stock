package com.stockside.common;

import java.util.regex.Matcher; 
import java.util.regex.Pattern;

public class MailCheck
{
	public static boolean ISEemailFormat(String email) 
    { 
        boolean tag = true; 
        final String pattern1 = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$"; 
        final Pattern pattern = Pattern.compile(pattern1); 
        final Matcher mat = pattern.matcher(email); 
        if (!mat.find()) 
        { 
            tag = false; 
        } 
        return tag; 
    } 
}
