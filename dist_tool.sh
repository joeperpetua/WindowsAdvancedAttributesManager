#!/bin/bash

bin_path="bin/publish"
dist_path=$(cat dist_path.conf)
ver=$(grep -oP '\b\d+\.\d+\.\d+\.\d+\b' "$bin_path"/index.html)

cp -r $bin_path $dist_path
cd $dist_path

# Delete old dist files
rm -r waam

# Rename new dist to waam
mv publish waam

# Publish
git add .
git commit -m "Update WAAM to $ver @dist_tool"
git push origin main