from PIL import Image
import numpy as np
from perlin_numpy import generate_fractal_noise_2d


palette_img = Image.open('palette-1.png')
pal = [palette_img.getpixel((0, y)) for y in range(palette_img.size[1])]
pal.reverse()

width = height = 128
water_level = 0.35

img = Image.new('L', size=(width, height), color='black')
height_data = []
colors = []

# np.random.seed(0)
noise = generate_fractal_noise_2d((128, 128), (8, 8), 5)

# values come in the [-1, 1] range
# noise = np.clip(noise + 1, 0, 1)

height_data = (noise * 255).astype(int)


colors = (noise * len(pal)).astype(int)


def convert_to_palette(x):
    global water_level
    global pal

    if x < water_level:
        x = 0
    x = min(len(pal) - 1, x)
    return pal[x]


colors = list(map(convert_to_palette, colors.flatten().tolist()))

img.putdata(height_data.flatten())
img.save('maps/terrain-1.png')


img_color = Image.new('RGB', size=(width, height), color='black')
img_color.putdata(colors)
img_color.convert('P', palette=Image.ADAPTIVE, colors=64)
img_color.save('maps/terrain-1W.png')
