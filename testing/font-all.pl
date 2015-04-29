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

$allfonts{"Arial"} = 1;
#$allfonts{"Times New Roman"} = 1;
#$allfonts{"Comic Sans MS"} = 1;

for $thisfont (sort keys %allfonts)
{
  $outStr = "fonttest-$thisfont.trizbort";
  open(A, "unavailable-font.trizbort");
  open(B, ">$outStr");
  while ($a = <A>)
  {
    if ($a =~ /BADFONT/) { $a =~ s/BADFONT/$thisfont/g; }
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