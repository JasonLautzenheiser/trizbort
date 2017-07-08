#allfont.pl
#
#requires no arguments
#
#goes through all fonts, or will once I get the code right
#
#currently just looks for fonts listed below
#creates fonttest-(fontname).trizbort, opens and lets you close it
#
#usage function shows what you can try
#
#Thanks to http://www.perlmonks.org/?node_id=663514 for code I shamelessly stole
#But it makes sense now, so learning, yay!
#

use POSIX;

use strict;
use warnings;

########################
#options
#

my $justPrintFonts = 0;
my $lowBound = 0;
my $upperBound = 0;
my $noFormatting = 0;
my $toQuit = 0;
my $quitAt = 0;

########################
#variables
#

my $fontCount = 0;

preProcessCmd();

$|=1;
use Win32API::Registry qw( :ALL );
my ($key, $uIndex, $name, $nameLength, $type, $data, $dataLength );
my %allfonts;
my $thisfont;

my $ok=RegOpenKeyEx( HKEY_LOCAL_MACHINE , "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts", 0, KEY_READ, $key );
if($key){
    my $uIndex=0;
    do{
         RegEnumValue ( $key, $uIndex, $name, $nameLength, [], $type, 
+$data, $dataLength );
         $uIndex++;
		 if ($name =~ /\(TrueType\)/) { $name =~ s/ +\(TrueType\)//g; }
		 $data =~ s/[^a-z]*$//gi;
		 $name =~ s/\\ / /g;
		 if (!$allfonts{"name"})
		 {
		 $allfonts{"$name"} = "$data";
		 }
         #print "$uIndex $name=$data.\r\n";
	}
     while(!regLastError());
    }
else { print "No key.\n"; }

RegCloseKey( $key );

for my $curfont (sort keys %allfonts)
{
  # font name is nicer looking/more descriptive than font file name, so use that

  $data = $allfonts{"$curfont"};

  # should we skip this font?

  if ($noFormatting)
  {
    if ($data =~ / (bold|italic)/i) { next; }
  }

  if ($lowBound) { if (lc($data) le lc($lowBound)) { next; } }
  if ($upperBound) { if (lc($data) ge lc($lowBound)) { next; } }

  if ($justPrintFonts) { $fontCount++; print "$fontCount: $curfont $data\n"; next; }

  if ($quitAt) { $toQuit++; if ($toQuit > $quitAt) { print "You've run your $quitAt tests. Finished!\n"; last; } }

  # ok, go through with processing it.
  my $outStr = "fonttest-$data.trizbort";
  my $params="";
  open(A, "unavailable-font.trizbort");
  open(B, ">$outStr");
  while ($a = <A>)
  {
    if ($a =~ /BADFONT/)
    {
      $a =~ s/BADFONT/$curfont/g;
      if (!$params)
      {
      if ($data =~ / bold/i) { $params .= " bold=\"yes\""; }
      if ($data =~ / italic/i) { $params .= " italic=\"yes\""; }
      }
      $a =~ s/>/$params>/; #only replace the first
    }
    print B $a;
  }
  close(A);
  close(B);
  `$outStr`;
  print "Erasing $outStr\n";
  unlink<"$outStr">;
  #may leave artifacts, so need cleanup at end
}
unlink<fonttest-*.trizbort>;

if ($quitAt)
{
  if ($toQuit < $quitAt) { print "You didn't get to the $quitAt fonts you wanted before quitting.\n"; }
  elsif ($toQuit == $quitAt) { print "You got through just the right amount of fonts before quitting.\n"; }
}

############################
#preprocessing here
#
#not many options, but where to start/end and even how many to do before quitting are options
#

sub preProcessCmd
{
  my $count = 0;
  while ($count <= $#ARGV)
  {
    $a = $ARGV[$count];
    $b = $ARGV[$count+1];
    for ($a)
    {
      /^-q$/ && do { $quitAt = $b; $toQuit = 0; $count+= 2; next; };
      /^-j$/ && do { $justPrintFonts = 1; $count++; next; };
      /^-l$/ && do { $lowBound = $b; $count += 2; next; };
      /^-u$/ && do { $upperBound = $b; $count += 2; next; };
      /^-nf$/ && do { $noFormatting = 1; $count++; next; };
	  /^-\?$/ && do { usage(); $count++; next; };
	  print "Invalid argument at entry $count: $a. Running USAGE.\n";
      usage();
    }
  }
  if ($lowBound && $upperBound && (lc($lowBound) ge lc($upperBound)))
  {
    die("You specified a lower bound greater than an upper bound. Bailing.");
  }
}

sub usage
{
 
print<<EOT;
-q = quit at font file # (2nd argument
-j = just print fonts
-l = low letter (or group, e.g. AND means nothing before AND)
-u = upper bound (or group, e.g. SCH means nothing after SCH)
-nf = no formattion
EOT
}