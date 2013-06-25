#!/usr/bin/perl
#
#	Comments:
#

sub main
{
	$temp_dir 		= "temp";
	$first_wav 		= "";
	$features_file = "$temp_dir/features.csv";
	$predict_file  = "$temp_dir/predictions.csv";
	
	# make sure directory "temp" exists
	unless(-d $temp_dir){
		mkdir $temp_dir or die;
	}
	
	# delete previous files
	unlink($predict_file);
	
	#
	# call octave
	#
	
	$octave 	= "C:\\Program Files\\Octave\\3.2.4_gcc-4.4.0\\bin\\octave-3.2.4.exe";
	$command	= " \"${octave}\" -q funciones\\main.m ";
	system ($command);
}

main();
