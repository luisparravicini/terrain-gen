from perlin_noise import PerlinNoise
from PIL import Image

noise1 = PerlinNoise(octaves=3)
# noise2 = PerlinNoise(octaves=6)
# noise3 = PerlinNoise(octaves=12)
# noise4 = PerlinNoise(octaves=24)


palette = Image.open('palette-1.png').palette
pal = palette.getdata()[1]

width = height = 128

img = Image.new('L', size=(width, height), color='black')
height_data = []
colors = []
for x in range(img.size[0]):
    for y in range(img.size[1]):
        noise_val = noise1([x / width, y / height])
        # if noise_val < 0:
        #   print(noise_val)
        # noise_val += 0.5 * noise2([x/width, y/height])
        # noise_val += 0.25 * noise3([x/width, y/height])
        # noise_val += 0.125 * noise4([x/width, y/height])

        value = (int)(noise_val * 255)

        height_data.append(value)

        color_index = (int)(noise_val * len(pal))
        colors.append(color_index)


img.putdata(height_data)
img.save('maps/terrain-1.png')


img_color = Image.new('P', size=(width, height), color='black')
img_color.palette = palette
img_color.putdata(colors)
img_color.save('maps/terrain-1W.png')
