using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework;

namespace AhPeGe
{
   public class CollisionSprite
    {
        public Rectangle bounds;
        public SpriteAnimation sprite;

        public Vector2 spriteOffset;

        public CollisionSprite(Texture2D tiledTexture, Rectangle Bounds, Vector2 newSpriteOffset)
        {
            sprite = new SpriteAnimation(tiledTexture, 0.2f, true);

            bounds = Bounds;
            spriteOffset = newSpriteOffset;

            bounds.Offset((int)newSpriteOffset.X, (int)newSpriteOffset.Y);
        }
        public CollisionSprite(Texture2D tiledTexture)
        {
            sprite = new SpriteAnimation(tiledTexture, 0.2f, true);

            bounds = tiledTexture.Bounds;
            spriteOffset = Vector2.Zero;

            bounds.Offset((int)spriteOffset.X, (int)spriteOffset.Y);
        }
        public void UpdateByNewPosition(Vector2 newPosition)
        {
            bounds.X = (int)newPosition.X;
            bounds.Y = (int)newPosition.Y;
        }

        public void UpdateByOffset(Vector2 offset)
        {
            bounds.Offset((int)offset.X,(int)offset.Y);
        }
        
        public bool boundingBoxIntersection(CollisionSprite b)
        {
            // check if two Rectangles intersect
            return (bounds.Right > b.bounds.Left && bounds.Left < b.bounds.Right &&
                    bounds.Bottom > b.bounds.Top && bounds.Top < b.bounds.Bottom);
        }

    public bool visiblePixelCollision(CollisionSprite b)
        {
            if (boundingBoxIntersection(b))
            {
                // only needed for the fast version
                uint[] bitsA = new uint[sprite.rectangle.Width * sprite.rectangle.Height];
                sprite.tiledImage.GetData<uint>(bitsA);
                
                uint[] bitsB = new uint[b.sprite.rectangle.Width * b.sprite.rectangle.Height];
            b.sprite.tiledImage.GetData<uint>(bitsB);
                // end of only needed for the fast version

                int x1 = Math.Max(bounds.X, b.bounds.X);
                int x2 = Math.Min(bounds.X + bounds.Width, b.bounds.X + b.bounds.Width);

                int y1 = Math.Max(bounds.Y, b.bounds.Y);
                int y2 = Math.Min(bounds.Y + bounds.Height, b.bounds.Y + b.bounds.Height);

                for (int y = y1; y < y2; ++y)
                {
                    for (int x = x1; x < x2; ++x)
                    {
                        // FAST and unitelligable version

                        if (((bitsA[(x - bounds.X) + (y - bounds.Y) * sprite.tiledImage.Width] & 0xFF000000) >> 24) > 20 &&
                            ((bitsB[(x - b.bounds.X) + (y - b.bounds.Y) * b.sprite.tiledImage.Width] & 0xFF000000) >> 24) > 20)
                            return true;      
                    }
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            sprite.Draw(gameTime, spriteBatch, bounds.Center(), SpriteEffects.None);
        }
    }

    }