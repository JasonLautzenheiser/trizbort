#testsync.pl
#
#makes sure all trizbort files are in some batch file or other
#also checks if a trizbort file is used in more than one test case

use Win32::Clipboard;
use strict;
use warnings;

#########################
#options
my $runZipAfter = 0;
my $sendToClip = 0;

#########################
#variables
my %trizfile;
my $orphanedFiles = 0;
my $badFiles = 0;
my $clip;
my $clipString = "";

processCmdLine();

opendir(DIR, ".");

my @triz = readdir(DIR);

close(DIR);

for my $batfile (@triz)
{
  if ($batfile =~ /\.bat/i)
  {
    getTriz($batfile);
  }
}

for my $thistriz (@triz)
{
  if ($thistriz =~ /\.trizbort/i)
  {
    if (!$trizfile{$thistriz})
	{
	  print "Orphaned trizbort file: \%TESTBASE\%\\$thistriz\n";
	  $orphanedFiles++;
	  if ($sendToClip) { $clipString .= "echo TEST DESCRIPTION HERE\n\%TESTBASE\%\\$thistriz\n"; }
	}
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
  else
  {
  print "You may wish to rerun with -r to commit the updated zip and bat files to all-test-files.zip.\n";
  }
}
else
{
  if ($orphanedFiles == 0) { print "There are no orphaned files in the test directory, but there are $badFiles bad file(s) in various batch files.\n"; }
  elsif ($badFiles == 0) { print "There are no bad files, but you have $orphanedFiles orphaned file(s) in the test directory.\n"; }
  else { print "$orphanedFiles orphaned file(s) in the test directory, and $badFiles bad file(s) in the batch files.\n"; } 
  if ($orphanedFiles)
  {
    if ($clipString)
	{
      $clip = Win32::Clipboard::new();
      $clip->Set("$clipString");
	  print "Clipboard string set to orphaned files.\n";
	}
	else
	{
	  print "Use -s to set the clipboard to the orphaned files.\n";
	}
  }
  if ($runZipAfter) { print "Fix the orphaned file issue in order to update the ZIP file.\n"; }
}

sub getTriz
{
  my %warning;
  my $skip = 0;
  print "Reading $_[0]\n";
  open(A, "$_[0]");

  while ($a = <A>)
  {
    if ($a =~ /^::/) { next; }
	if ($a =~ /rem skip/i) { $skip = 1; next; }
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
		else
		{
		$skip = 0;
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
    /-s/ && do { $sendToClip = 1; $count++; next; };
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
-r runs the zip file creator after.
-s sets the missing file text to the clipboard.
-? shows this without printing usage, but that doesn't really count.
EOT

exit;
}
