#!/bin/bash

for f in *.csv
do
    filename=$(basename "$f")
    extension="${filename##*.}"
    filename="${filename%.*}"
    echo $filename
    mongoimport --host=mongodb:27017 -d Northwind -c "$filename" --type csv --file "$f" --headerline
done