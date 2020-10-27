var counter = 0;
var isPaused = true;



var Index;






//$("#pause, #resume").hide();
//$("#days, #hours, #first-divider, #second-divider").hide();
//$("#start").show();





var t = window.setInterval(function () {
	
	if (!isPaused) {
		counter++;
		var s = counter;
		convertSeconds(Math.floor(s));
	}
}, 1000);



function forward(Index_) {	


	Index = Index_;
	counter++;
	var s = counter;
	convertSeconds(Math.floor(s));

	//Caltime(Index_)
}

function backward(Index_) {

	Index = Index_;
	if (counter != 0) {
		counter--;
		var s = counter;
		convertSeconds(Math.floor(s));

	}
//	Caltime(Index_)
	

}


function Caltime(Index_) {


	var Minutes = $('#Minutes_' + Index_ + '').text();
	var Seconds = $('#Seconds_' + Index_ + '').text();


	var Time_Practical = '00:' + Minutes + ':' + Seconds;
	var a = Time_Practical.split(':');
	var ActualTime_Seconds = (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);



	var Min_time = $('#Min_Time_' + Index_ + '').text();
	Min_time = '00:' + Min_time
	var b = Min_time.split(':');
	var Min_time_Seconds = (+b[0]) * 60 * 60 + (+b[1]) * 60 + (+b[2]);

	var Max_time = $('#Max_Time_' + Index_ + '').text();
	Max_time = '00:' + Max_time
	var c = Max_time.split(':');
	var Max_time_Seconds = (+c[0]) * 60 * 60 + (+c[1]) * 60 + (+c[2]);


	if ((ActualTime_Seconds < Max_time_Seconds) && (ActualTime_Seconds > Min_time_Seconds)) {
		$('#ActualTime_' + Index_).removeClass("bg-danger ").addClass("bg-success text-white");

	//	$('#Minutes_' + Index_ + '').removeClass("text-white");
    //		$('#Seconds_' + Index_ + '').removeClass("text-white");

	} else{

		$('#ActualTime_' + Index_).removeClass("bg-success text-white").addClass("bg-danger text-white");

		$('#Minutes_' + Index_ + '').addClass("text-white");
		$('#Seconds_' + Index_ + '').addClass("text-white");
		$('#LB_ActualTime_' + Index_ + '').addClass("text-white");

		
	}


}
 
	


	


	

// Button Click Events

//$("#start").click(function () {
//	startClock();
//	$(this).hide();
//	$("#pause").show();
//	$("#reset, #stop").show();
//	$("#DIV_Forward_and_Back").hide();

//});


function start(Index_) {
	
	Index = Index_;
	reset(Index_)
	startClock();

	
	$('#BTN_Save_' + Index).hide();

	$('#start_' + Index).hide();
	$("#pause_" + Index).show();
	$("#reset_" + Index + ", #stop_" + Index + "").show();



	$("#BTN_Backward_" + Index + ", #BTN_Forward_" + Index).hide();
}

function pause(Index_) {
	
	Index = Index_;
	$('#BTN_Save_' + Index).hide();
	pauseClock();
	$("#pause_" + Index).hide();
	$("#resume_" + Index).show();
}
function resume(Index_) {
	Index = Index_;
	$('#BTN_Save_' + Index).hide();
	resumeClock();
	$("#resume_" + Index).hide();
	$("#pause_" + Index).show();
}

function reset(Index_) {
	debugger
	Index = Index_;
	resetClock();
	$('#BTN_Save_' + Index).hide();
	$("#resume_" + Index + "").hide();
	$("#pause_" + Index + "").show();
}

function stop(Index_) {
	
	Index = Index_;
	pauseClock();
	//resetClock();
	$('#BTN_Save_' + Index).show();
	$("#pause_" + Index + ", #resume_" + Index + "").hide();

	$("#start_" + Index ).show();

	$("#BTN_Backward_" + Index+", #BTN_Forward_" + Index).show();


	$("#reset_" + Index + ", #stop_" + Index + "").hide();
}




//$("#pause").click(function (){
//	pauseClock();
//	$("#pause_" + Index ).hide();
//	$("#resume_" + Index ).show();
//});

//$("#resume").click(function (){
//	resumeClock();
//	$("#resume_" + Index).hide();
//	$("#pause_" + Index).show();
//});

//$("#reset").click(function (){
//	resetClock();
//	$("#resume_" + Index + "").hide();
//	$("#pause_" + Index + "").show();
//});

//$("#stop").click(function () {
//	pauseClock();
//	//resetClock();
//	$("#pause_" + Index + ", #resume_" + Index+"").hide();
//	$("#start_" + Index + ", #DIV_Forward_and_Back_" + Index + "").show();
//	$("#reset_" + Index + ", #stop_" + Index + "").hide();

//	//$().css("opacity", "0");
//});


function startClock() { 
	isPaused = false;
}
function pauseClock() { 
	isPaused = true;
}
function resumeClock() { isPaused = false; }


function resetClock() {

	counter = 0;
	$("#days_" + Index).html("00");
	$("#hours_" + Index).html("00");
	$("#Minutes_" + Index).html("00");
	$("#Seconds_"+Index).html("00");
}
function stopClock() {
	resetClock();
	isPaused = true;
}

function convertSeconds(s) {
	
	var days = Math.floor(s / 86400)
	var hours = Math.floor((s % 86400) / 3600);
	var minutes = Math.floor(((s % 86400) % 3600) / 60);
	var seconds = ((s % 86400) % 3600) % 60;
	
	if (days		< 10) {days 	 = "0" + days}
	if (hours 	< 10) {hours 	 = "0" + hours;}
	if (minutes < 10) {minutes = "0" + minutes;}
	if (seconds < 10) {seconds = "0" + seconds;}
	
	$("#days_" + Index).html(days);
	$("#hours_" + Index).html(hours);
	$("#Minutes_" + Index).html(minutes);
	$("#Seconds_" + Index).html(seconds);
	
	if (days == 0 && hours == 0) {
		$("#days_" + Index + ", #hours_" + Index+"").hide();
		$("#first-divider, #second-divider").hide();
	} else if (days == 0) {
		$("#days").hide();
		$("#hours").show();
		$("#second-divider").show();
	} else {
		$("p, .divider").show();
	}
	Caltime(Index) 

}