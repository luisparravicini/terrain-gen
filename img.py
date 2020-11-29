from perlin_noise import PerlinNoise
from PIL import Image
import math

noise1 = PerlinNoise(octaves=3)
noise2 = PerlinNoise(octaves=6)
noise3 = PerlinNoise(octaves=12)
noise4 = PerlinNoise(octaves=24)


palette_img = Image.open('palette-1.png')
pal = [palette_img.getpixel((0, y)) for y in range(palette_img.size[1])]
pal.reverse()

width = height = 128
water_level = 0.35

img = Image.new('L', size=(width, height), color='black')
height_data = []
colors = []
sqrt_two = math.sqrt(2)
min_value = sqrt_two / 2
for x in range(img.size[0]):
    for y in range(img.size[1]):
        noise_val = noise1([x / width, y / height])
        noise_val += 0.5 * noise2([x / width, y / height])
        noise_val += 0.25 * noise3([x / width, y / height])
        noise_val += 0.125 * noise4([x / width, y / height])

        # normalize value to [0..1]
        noise_val += min_value
        noise_val /= sqrt_two

        value = (int)(noise_val * 255)

        height_data.append(value)

        if noise_val < water_level:
            noise_val = 0
        color_index = (int)(noise_val * len(pal))
        colors.append(pal[color_index])


img.putdata(height_data)
img.save('maps/terrain-1.png')


img_color = Image.new('RGB', size=(width, height), color='black')
img_color.putdata(colors)
img_color.convert('P', palette=Image.ADAPTIVE, colors=64)
img_color.save('maps/terrain-1W.png')
