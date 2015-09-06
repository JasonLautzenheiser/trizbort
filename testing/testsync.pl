#testsync.pl
#
#makes sure all trizbort files are in some batch file or other
#also checks if a trizbort file is used in more than one test case

processCmdLine();

opendir(DIR, ".");

my @triz = readdir(DIR);

close(DIR);

for $batfile (@triz)
{
  if ($batfile =~ /\.bat/i)
  {
    getTriz($batfile);
  }
}

for $thistriz (@triz)
{
  if ($thistriz =~ /\.trizbort/i)
  {
    if (!$trizfile{$thistriz}) { print "Orphaned trizbort file: \%TESTBASE\%\\$thistriz\n"; $orphanedFiles++; }
  }
}

if ((!$orphanedFiles) && (!$badFiles))
{
  print "Hooray! No orphaned *.trizbort files, and no bad references in .bat files!\n";
  if ($runZipAfter)
  {
  print "Updating the zip file...\n";
  `7z a all-test-files.zip \*.trizbort \*.bat`;
  print "Zip file update complete.\n";
  }
}
elsif ($orphanedFiles == 0) { print "There are no orphaned files in the test directory, but there are $badFiles bad files in various batch files.\n"; }
elsif ($badFiles == 0) { print "There are no bad files, but there are $orphanedFiles orphaned files in the test directory.\n"; }
else { print "$orphanedFiles orphaned files in the test directory, and $badFiles bad files in the batch files.\n"; } 

sub getTriz
{
  print "Reading $_[0]\n";
  open(A, "$_[0]");
  while ($a = <A>)
  {
    if ($a =~ /^::/) { next; }
    if ($a =~ /\.trizbort/)
    {
      chomp($a);
      $a =~ s/.*\\//g;
      if (! -f "$a") { print "$a in $_[0] is not a valid file.\n"; $badFiles++; }
	  
	  #This may not be the best coding, but we may want a way to overlook for one instance that we are reusing a batch file.
	  #So while warning{$a} and skip aren't defined, we may find a way to do so later. This isn't a problem with relatively noncomplex test cases, but it may be.
	  if (($trizfile{$a}) && (!$warning{$a}))
	  {
	    if (!$skip)
		{
	    print ("WARNING: $a is used twice.\n");
		}
	  }
      $trizfile{$a} = $_[0];
    }
  }
  close(A);
}

##########################
#processCmdLine
#
#looks for valid, or invalid, command line parameters

sub processCmdLine
{
my $count = 0;
while ($count <= $#ARGV)
{
  $a = @ARGV[$count];
  
  for ($a)
  {
    /-r/ && do { $runZipAfter = 1; $count++; next; };
	/-\?/ && do { usage(); };
	print "Invalid argument # $count: $a\n";
	usage();
  }

}
}


##########################
#usage
#
#tells the user the CMD line parameters
#

sub usage
{
print<<EOT;
USAGE
The only flag right now is -r to run the zip file creator after.
EOT

exit;
}
