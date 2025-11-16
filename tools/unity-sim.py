import pygame
import json

pygame.init()
screen = pygame.display.set_mode((800, 600))
pygame.display.set_caption("AR Forage Sim")

try:
    with open('roots.json', 'r') as f:
        roots = json.load(f)
except:
    roots = []

running = True
while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
        if event.type == pygame.MOUSEBUTTONDOWN:
            print(f"Sim Harvest: {roots[3]['branch'] if len(roots) > 3 else 'Unknown'}")
    screen.fill((0, 255, 0))
    pygame.display.flip()

pygame.quit()