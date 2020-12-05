#!/bin/bash

set -e

dir=`dirname "$0"`

cd "$dir"
time python img.py

#for f in terrain-1.png terrain-1W.png; do
#  mogrify -scale 1024 maps/$f
#done
