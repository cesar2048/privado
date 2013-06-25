#!/usr/bin/perl
use strict;

# subroutine declarations
sub apply_transformations;
sub generate_features;

# global variables
our($octave, $annotator, $audio_dir, $temp_dir);
$annotator 	= "G:\\Enrique\\Documentos\\Univer~1\\Semestres\\Privado\\software\\sonic-annotator-1.0-win32\\sonic-annotator.exe";
$octave 	= "C:\\Program Files\\Octave\\3.2.4_gcc-4.4.0\\bin\\octave-3.2.4.exe";
$audio_dir 	= "datos/";
$temp_dir	= "temp/";

sub main
{
	my(@filters, $filterOk, $fileName, $filter, $output_file);
	@filters 		= ("tranquilo.*wav", "enojado.*wav");
	$output_file	= "${temp_dir}/features.csv";
	
	unlink($output_file);
	
	#
	# search files that match the filters
	#
	opendir my($dh), $audio_dir or die "Couldn't open dir '$audio_dir': $!";
	my @files = readdir $dh;
	closedir $dh;
	
	foreach (@files) {
		$filterOk = 0;
		$fileName = $_;
		
		foreach(@filters) {
			$filter = $_;
			if ( $fileName =~ $filter) {
				$filterOk = 1;
				last;
			}
		}
		
		if ( $filterOk ) {
			 apply_transformations($output_file, $fileName, $audio_dir, $temp_dir);
			
		}
	}
}

sub apply_transformations
{
	my ($output_file, $fileName, $audio_dir, $temp_dir) = @_;
	my ($input_file, $file_mfcc, $file_noisi, $file_pitch);
	my ($transf_desc, $command, $label, $key, $value, $regexp, %labels);
	
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
	
	# generate label from the file name
	$label = 0;
	%labels = ("enojado" => 1, "tranquilo" => 2 );
	while ( ($key, $value) = each(%labels)){
		 $regexp = "$key.*";
		 if ( $fileName =~ $regexp ) {
			$label = $value;
			last;
		 }
	}
	
	# -------- extract low level data with sonic-annotator ---------------
	
	#$command	= " \"${annotator}\" -t ${transf_desc} \"${input_file}\" -w rdf";
	$command	= " \"${annotator}\" -t ${transf_desc} \"${input_file}\" -w csv --csv-basedir ${temp_dir} --csv-force 2> sonic-log.txt";
	system ($command);
	
	# -------- octave script, adds the features to the output file --------
	$command	= " \"${octave}\" -q funciones\\getEstadistica.m \"$output_file\" \"$file_pitch\" \"$file_mfcc\" $label ";
	system ($command);
	
	# deleting original files
	#unlink($file_mfcc);
	unlink($file_noisi);
	#unlink($file_pitch);
}


main();

