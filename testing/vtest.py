# vtest.py
# tests versions
#
# vtest.py -1.5.9.10 to delete generated files after
# vtest.py 1.5.9.10 not to
#

import re
import os
import sys

numberMeanings = [ "major version", "minor version", "build", "revision" ]
delete = False

def checkRelNum(rn,delete):
    rn2 = [ str(x) for x in rn ]
    vers = ".".join(rn2)
    filename = "tempversion-" + vers + ".trizbort"
    try:
        file_write = open(filename, "w")
    except:
        raise Exception("Failed to open " + filename + " for writing, skipping test.")
        return
    file_write.write("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
    file_write.write("<trizbort version=\"" + vers + "\">")
    file_write.write("<info />\n<map />\n<settings />\n</trizbort>")
    file_write.close()
    os.system(filename)
    if delete is True:
        print "Deleting " + filename
        os.remove(filename)
        return

def try_versions(rn, delete):
    for y in range(0,len(rn)):
        if rn[y] > 0:
            print "decreasing " + numberMeanings[y]
            rn[y] = rn[y] - 1
            checkRelNum(rn,delete)
            rn[y] = rn[y] + 1
        else:
            print "Can't decrease " + numberMeanings[y]
        print "increasing " + numberMeanings[y]
        rn[y] = rn[y] + 1
        checkRelNum(rn, delete)
        rn[y] = rn[y] - 1
    return

# this is the build for when I first designed the script. You will want to change it.

version = "1.5.9.10"

if len(sys.argv) > 1:
    version = sys.argv[1]
    if re.match("^-", version):
        delete = True
        version = re.sub("^-", "", version)
        print "Deleting after."
    else:
        print "Use - before to delete files after."
else:
    print "Running default trizbort file of " + version + ". We may wish to change that to the current version with vers.py 1.9.8.4 or whatever."

release_numbers = version.split(".")

if len(release_numbers) != 4:
    print "A release number must be of the form a.b.c.d"
    exit()

release_numbers = [ int(x) for x in release_numbers ]

try_versions(release_numbers, delete)
