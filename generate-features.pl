#!/usr/bin/perl
use strict;

# subroutine declarations
sub apply_transformations;
sub generate_features;

# global variables
our($octave, $annotator, $audio_dir, $temp_dir);
$annotator 	= "sonic-annotator.exe";
$octave 	= "octave.exe";
$audio_dir 	= "data/";
$temp_dir	= "temp/";

sub main
{
	my($filterOk, $fileName, $filter, $output_file, %labels, $key, $value, $label);
	%labels 		= ("A" => 1, "D" => 2, "E" => 3 );
	$output_file	= "${temp_dir}/features.csv";
	
	unlink($output_file);
	
	#
	# search files that match the filters
	#
	opendir my($dh), $audio_dir or die "Couldn't open dir '$audio_dir': $!";
	my @files = readdir $dh;
	closedir $dh;
	
	foreach (@files) {
		$label = 0;
		$fileName = $_;
		
		while ( ($key, $value) = each(%labels)){
			 if ( $fileName =~ "${key}.*wav" ) {
				$label = $value;
				last;
			 }
		}
		
		# label = 0 doesn't exists, it means the file is excluded
		if ( $label ) {	
			 apply_transformations($label, $output_file, $fileName, $audio_dir, $temp_dir);
		}
	}
}

sub apply_transformations
{
	my ($label, $output_file, $fileName, $audio_dir, $temp_dir) = @_;
	my ($input_file, $file_mfcc, $file_noisi, $file_pitch);
	my ($transf_desc, $command, $key, $value, $regexp, %labels);
	
	print("$fileName\n");
	$input_file		= "${audio_dir}${fileName}";
	$fileName 		=~ s/.wav$//;
	$file_mfcc		= "${temp_dir}${fileName}_vamp_qm-vamp-plugins_qm-mfcc_coefficients.csv";
	$file_noisi		= "${temp_dir}${fileName}_vamp_vamp-aubio_aubiosilence_noisy.csv";
	$file_pitch		= "${temp_dir}${fileName}_vamp_vamp-aubio_aubiopitch_frequency.csv";
	$transf_desc	= "transform-descriptor.n3";
	
	unless (-e $input_file) {
		print("Error: wav file '$input_file' does not exists\n");
		return;
	}
	unless (-e $transf_desc) {
		print("Error: transformations file '$transf_desc' does not exists\n");
		return;
	}

	# -------- extract low level data with sonic-annotator ---------------
	
	$command	= " \"${annotator}\" -t ${transf_desc} \"${input_file}\" -w csv --csv-basedir ${temp_dir} --csv-force 2> sonic-log.txt";
	system ($command);
	
	# -------- octave script, adds the features to the output file --------
	$command	= " \"${octave}\" -q funciones\\getEstadistica.m \"$output_file\" \"$file_pitch\" \"$file_mfcc\" $label ";
	system ($command);
	
	# deleting original files
	unlink($file_mfcc);
	unlink($file_noisi);
	unlink($file_pitch);
}


main();

