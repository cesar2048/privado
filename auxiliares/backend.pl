#!/usr/bin/perl
use strict;
use IO::Socket;

sub main
{
	my $sock = new IO::Socket::INET(
		LocalHost => '127.0.0.1',
		LocalPort => '7070',
		Proto => 'tcp',
		Listen => 1,
		Reuse => 1);
		
	die "Could not create socket: $!\n" unless $sock;
	print ("listening...\n");
	
	while ( 1 ) {
		print ("wait for client\n");
	
		my $client = $sock->accept();
		my $data = "";
		while(<$client>) {
			$data .= $_;
			
			# if ( $data =~ ".*policy-file-request.*") {
				# printf("policy file requested\n");
				
				# print $client "<?xml version=\"1.0\"?><cross-domain-policy>";
				# print $client "<allow-access-from domain=\"*\" to-ports=\"*\" />";
				# print $client "</cross-domain-policy>\0\0";
				# $data = "";
			# }
		}
		
		my $bytes = length($data);
		printf("total bytes $bytes\n\n");
		
		if ( $bytes > 0 ) {
			# write the wav file
			open FILE, ">salida.wav" or die $!;
			binmode(FILE);
			print FILE $data;
			close FILE;
		}
		
	}
	close($sock);
}


main();

