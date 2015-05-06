#testsync.pl
#
#makes sure all trizbort files are in some batch file or other

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

if ((!$orphanedFiles) && (!$badFiles)) { print "Hooray! No orphaned *.trizbort files, and no bad references in .bat files!\n"; }
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
      $trizfile{$a} = $_[0];
    }
  }
  close(A);
}