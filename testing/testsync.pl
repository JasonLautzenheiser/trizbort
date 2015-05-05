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
    if (!$trizfile{$thistriz}) { print "Orphaned trizbort file: \%TESTBASE\%\\$thistriz\n"; }
  }
}


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
      if (! -f "$a") { print "$a in $_[0] is not a valid file.\n"; }
      $trizfile{$a} = $_[0];
    }
  }
  close(A);
}