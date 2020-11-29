set -e

dir=`dirname "$0"`
mkdir -p "$dir"/images

echo delete
rm -f "$dir"/images/*.png

python "$dir"/anim.py

echo convert
ffmpeg -i "$dir"/images/out%03d.png -y -c:v libx264 -r 25 -pix_fmt yuv420p terrain.mp4
