from perlin_noise import PerlinNoise
from PIL import Image

noise1 = PerlinNoise(octaves=3)
# noise2 = PerlinNoise(octaves=6)
# noise3 = PerlinNoise(octaves=12)
# noise4 = PerlinNoise(octaves=24)


palette = Image.open('palette-1.png').palette
pal = palette.getdata()[1]

xpix, ypix = 128, 128

img = Image.new('L', size=(xpix, ypix), color='black')
height = []
colors = []
for i in range(img.size[0]):
    for j in range(img.size[1]):
        noise_val = noise1([i/xpix, j/ypix])
        # if noise_val < 0:
        #   print(noise_val)
        # noise_val += 0.5 * noise2([i/xpix, j/ypix])
        # noise_val += 0.25 * noise3([i/xpix, j/ypix])
        # noise_val += 0.125 * noise4([i/xpix, j/ypix])

        value = (int)(noise_val * 255)

        height.append(value)

        color_index = (int)(noise_val * len(pal))
        colors.append(color_index)


img.putdata(height)
img.save('maps/terrain-1.png')


img_color = Image.new('P', size=(xpix, ypix), color='black')
img_color.palette = palette
img_color.putdata(colors)
img_color.save('maps/terrain-1W.png')
