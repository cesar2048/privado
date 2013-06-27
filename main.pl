#!/usr/bin/perl
#
#	Comments:
#
$octave 	= "octave.exe";

sub main
{
	$temp_dir 		= "temp";
	$first_wav 		= "";
	$features_file 	= "$temp_dir/features.csv";
	$predict_file  	= "$temp_dir/predictions.csv";
	$theta			= "nn_theta.dat";
	
	# make sure directory "temp" exists
	unless(-d $temp_dir){
		mkdir $temp_dir or die;
	}
	
	# delete previous files
	unlink($predict_file);
	unlink($theta);
	
	#
	# call octave
	#
	
	$command	= " \"${octave}\" -q funciones\\main.m ";
	system ($command);
}

main();
