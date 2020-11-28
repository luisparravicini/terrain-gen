# import matplotlib.pyplot as plt
from perlin_noise import PerlinNoise
from PIL import Image

# noise = PerlinNoise(octaves=10, seed=1)
# xpix, ypix = 100, 100
# pic = [[noise([i/xpix, j/ypix]) for j in range(xpix)] for i in range(ypix)]

# plt.imshow(pic, cmap='gray')
# plt.show()

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

# pic = []
# for i in range(xpix):
#     row = []
#     for j in range(ypix):
#         noise_val =         noise1([i/xpix, j/ypix])
#         noise_val += 0.5  * noise2([i/xpix, j/ypix])
#         noise_val += 0.25 * noise3([i/xpix, j/ypix])
#         noise_val += 0.125* noise4([i/xpix, j/ypix])

#         row.append(noise_val)
#     pic.append(row)

# plt.imshow(pic, cmap='gray')
# plt.show()


img.save('VoxelSpace/maps/terrain-1.png')
