#allfont.pl
#
#requires no arguments
#
#goes through all fonts, or will once I get the code right
#
#currently just looks for fonts listed below
#creates fonttest-(fontname).trizbort, opens and lets you close it
#
#future adds: start at certain place in alphabet

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
         print "$uIndex $name=$data.\r\n";
	}
     while(!regLastError());
    }
else { print "No key.\n"; }
RegCloseKey( $key );

for $curfont (sort keys %allfonts)
{
  $data = $allfonts{"$curfont"};
  #print "$curfont $data\n"; next;
  my $outStr = "fonttest-$data.trizbort";
  open(A, "unavailable-font.trizbort");
  open(B, ">$outStr");
  while ($a = <A>)
  {
    if ($a =~ /BADFONT/) { $a =~ s/BADFONT/$curfont/g; }
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