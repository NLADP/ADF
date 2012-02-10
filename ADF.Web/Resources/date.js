<script language='javascript'>
// Check only date keys are pressed (0-9) or -
// use in onkeypress event
function date_keys() 
{
    var intCode;
    
    intCode = window.event.keyCode;
    if ( (intCode < "0".charCodeAt(0)) || (intCode > "9".charCodeAt(0)) )
    {
		if (intCode != "-".charCodeAt(0))
		{
			window.event.cancelBubble = true;
			window.event.returnValue = false;
		}  
    }
}

// Check only date&time keys are pressed (0-9) or - or :
// use in onkeypress event
function datetime_keys() 
{
    var intCode;
    
    intCode = window.event.keyCode;
    
    if ( (intCode < "0".charCodeAt(0)) || (intCode > "9".charCodeAt(0)) )
    {
		if (intCode != "-".charCodeAt(0) && intCode != ":".charCodeAt(0) && intCode != " ".charCodeAt(0))
		{
			window.event.cancelBubble = true;
			window.event.returnValue = false;
		}  
    }
}
       
// Check if entered value is a valid date (dd-mm-yyyy)
// Use in onblur event
function check_field_date()
{
    var strNewDate;

    if (window.event.srcElement.value == "")
    {
	    return;
    }
    strNewDate = formatDate(window.event.srcElement.value);
    if (strNewDate != "")
    {
		window.event.srcElement.value = strNewDate;
    }
    else
    {
	    alert("Datum is niet geldig");
		window.event.srcElement.focus();
		window.event.cancelBubble = true;
		window.event.returnValue = false;
		window.event.srcElement.select();
    }
}
//
// Format a date into dd-mm-yyyy format
// returncode is a formatted or empty string if
// date was invalid. Function contains some intelligence
// to change string to a valid date
//
function formatDate(strDate)
{
    var sepPos;
	var strYear, strMonth, strDay;
	var intYear, intMonth, intDay;
	var intSepPos;
	var strFormattedDate;
  
	var dtmDate
    var intCenturyBreak;
    
    intCenturyBreak = 20;
    
	strFormattedDate = "";
	intSepPos = strDate.search("-");
	if (intSepPos < 0)
	{
		// No seperator, just add them
		switch(strDate.length)
		{
			case 4:
				// no year entered
				strDate = strDate.substr(0, 2) + "-" + strDate.substr(2,2) + "-" + new Date().getFullYear();
				break;
			case 6:
				// only 2 digits for year
				var strLeadYear;
				strLeadYear = (Number(strDate.substr(4,2)) > intCenturyBreak) ? "19" : "20";
				strDate = strDate.substr(0, 2) + "-" + strDate.substr(2,2) + "-" + strLeadYear + strDate.substr(4,2);
				break;
			case 8:
				// no dashes
				strDate = strDate.substr(0, 2) + "-" + strDate.substr(2,2) + "-" + strDate.substr(4,4);
				break;
			default:
				return "";
		}
		intSepPos = strDate.search("-");
	}
  
	strDay = strDate.substr(0, intSepPos);
	strDate = strDate.substr(intSepPos + 1);
	intSepPos = strDate.search("-");
	if (intSepPos < 0)
	{
		// no second seperator
		strMonth = strDate
		strYear = new Date().getFullYear().toString();
	}
	else
	{
		strMonth = strDate.substr(0, intSepPos);
		strYear = strDate.substr(intSepPos + 1);
		// Hoeveel posities is jaar, 2 dan aanvullen
		if (strYear.length <= 2)
		{
			var strLeadYear;
			
			strLeadYear = (Number(strYear) > intCenturyBreak) ? "19" : "20";
			strYear = strLeadYear + strYear;
		}
	}


	if ((strDay == "") || (strMonth == "") || (strYear == ""))
	{
		return "";
	}

	intDay = Number(strDay);
	intMonth = Number(strMonth);
	intYear = Number(strYear);
	dtmDate = new Date(intYear, intMonth-1, intDay, 0, 0, 0, 0);

	if ((dtmDate.getFullYear() != intYear) || ((dtmDate.getMonth()+1) != intMonth) || (dtmDate.getDate() != intDay))
	{
		return "";
	}
	  
	strFormattedDate = "";
	if (intDay < 10)
	{
		strFormattedDate = strFormattedDate + "0";
	}
	strFormattedDate = strFormattedDate + intDay.toString() + "-";
	  
	if (intMonth < 10)
	{
		strFormattedDate = strFormattedDate + "0"
	}
	strFormattedDate = strFormattedDate + intMonth.toString() + "-" + intYear.toString();

	return strFormattedDate;
}

// Check if entered value is a valid datetime (dd-MM-yyyy HH:mm)
// Use in onblur event
function check_field_datetime()
{
    var strNewDate;

    if (window.event.srcElement.value == "")
    {
	    return;
    }
    strNewDate = formatDateTime(window.event.srcElement.value);
    if (strNewDate != "")
    {
		window.event.srcElement.value = strNewDate;
    }
    else
    {
	    alert("Datum-tijd is niet geldig");
		window.event.srcElement.focus();
		window.event.cancelBubble = true;
		window.event.returnValue = false;
		window.event.srcElement.select();
    }
}


//
// Format a date into dd-MM-yyyy HH:mm format
// returncode is a formatted or empty string if
// date was invalid. Function contains some intelligence
// to change string to a valid date
//
function formatDateTime(strDate)
{
    var sepPos;
	var strYear, strMonth, strDay, strHour, strMin;
	var intYear, intMonth, intDay, intHour, intMin;
	var intSepPos, intSepPosTime;
	var strFormattedDate;
  
	var dtmDate
    var intCenturyBreak;
    var strTime;
    
    strTime = " 12:00"
    
    intCenturyBreak = 20;
    
	strFormattedDate = "";
	intSepPos = strDate.search("-");
	if (intSepPos < 0)
	{
		// No seperator, just add them
		switch(strDate.length)
		{
			case 4:
				// no year entered
				strDate = strDate.substr(0, 2) + "-" + strDate.substr(2,2) + "-" + new Date().getFullYear() + strTime;
				break;
			case 6:
				// only 2 digits for year
				var strLeadYear;
				strLeadYear = (Number(strDate.substr(4,2)) > intCenturyBreak) ? "19" : "20";
				strDate = strDate.substr(0, 2) + "-" + strDate.substr(2,2) + "-" + strLeadYear + strDate.substr(4,2) + strTime;
				break;
			case 8:
				// no dashes
				strDate = strDate.substr(0, 2) + "-" + strDate.substr(2,2) + "-" + strDate.substr(4,4) + strTime;
				break;
			default:
				return "";
		}
		intSepPos = strDate.search("-");
	}
  
    intSepPosTime = strDate.search(" ");
    if(intSepPosTime >= 0)
    {
        intSepPosTime = strDate.search(":");
        if(intSepPosTime < 0)
        {
            return "";
        }
    }
    else
    {
        strDate = strDate + strTime;
    }
    
	strDay = strDate.substr(0, intSepPos);
	strDate = strDate.substr(intSepPos + 1);
	intSepPos = strDate.search("-");

	if (intSepPos < 0)
	{
		// no second separator
		strMonth = strDate
		strYear = new Date().getFullYear().toString();
	}
	else
	{
		strMonth = strDate.substr(0, intSepPos);
		intSepPosTime = strDate.search(" ");
		
		strYear = strDate.substr(intSepPos + 1, intSepPosTime - intSepPos - 1);
		
		
		// Hoeveel posities is jaar, 2 dan aanvullen
		if (strYear.length <= 2)
		{
			var strLeadYear;
			
			strLeadYear = (Number(strYear) > intCenturyBreak) ? "19" : "20";
			strYear = strLeadYear + strYear;
		}
	}

	intSepPos = strDate.search(" ");
	
	strTime = strDate.substr(intSepPos + 1);
	arrTime = strTime.split(":");
	
	if(arrTime.length < 2)
	{
	    return "";
	}
	
	strHour = arrTime[0];
	strMin = arrTime[1];

    intHour = Number(strHour);
    intMin = Number(strMin);
    
    
    if(intHour < 0 || intHour > 23 || intMin < 0 || intMin > 59) 
    {
        return "";
    }

	if ((strDay == "") || (strMonth == "") || (strYear == ""))
	{
		return "";
	}

	intDay = Number(strDay);
	intMonth = Number(strMonth);
	intYear = Number(strYear);

	dtmDate = new Date(intYear, intMonth-1, intDay, 0, 0, 0, 0);

	    
	if ((dtmDate.getFullYear() != intYear) || ((dtmDate.getMonth()+1) != intMonth) || (dtmDate.getDate() != intDay))
	{
		return "";
	}
	  
	  
      
	  
	strFormattedDate = "";
	if (intDay < 10)
	{
		strFormattedDate = strFormattedDate + "0";
	}
	strFormattedDate = strFormattedDate + intDay.toString() + "-";
	  
	if (intMonth < 10)
	{
		strFormattedDate = strFormattedDate + "0";
	}
	
	strFormattedDate = strFormattedDate + intMonth.toString() + "-" + intYear.toString();
	
	strFormattedDate = strFormattedDate + " ";
	if (intHour < 10)
	{
	    strHour = "0" + intHour.toString();
	}
	strFormattedDate = strFormattedDate + strHour;

	strFormattedDate = strFormattedDate + ":";
	if (intMin < 10)
	{
	    strMin = "0" + intMin.toString();
	}
	strFormattedDate = strFormattedDate + strMin;
	

	return strFormattedDate;
}
</script>