from perlin_noise import PerlinNoise
from PIL import Image

noise1 = PerlinNoise(octaves=3)
noise2 = PerlinNoise(octaves=6)
noise3 = PerlinNoise(octaves=12)
noise4 = PerlinNoise(octaves=24)

xpix, ypix = 100, 100
img = Image.new('L', size=(xpix, ypix), color='black')
for i in range(img.size[0]):
    for j in range(img.size[1]):
        noise_val = noise1([i/xpix, j/ypix])
        # noise_val += 0.5 * noise2([i/xpix, j/ypix])
        # noise_val += 0.25 * noise3([i/xpix, j/ypix])
        # noise_val += 0.125 * noise4([i/xpix, j/ypix])

        color = (int)(noise_val * 255)

        img.putpixel((i, j), color)

img.save('VoxelSpace/maps/terrain-1.png')
