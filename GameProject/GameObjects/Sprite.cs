#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework;

#endregion



namespace AhPeGe
{
    //Classes for a static image of any size
    //Animates Comic style tiled image. (Rectangle of squares, Far Left square 1st image, 2nd next to it... etc
    public class Sprite
    {
        public Texture2D tiledImage;
        public Rectangle rectangle; // used for collisions so set to public
        protected SpriteEffects spriteEffects;
        protected Color colour;
        protected float scale;
        protected float rotation;
        protected Vector2 position;

        public Sprite(Texture2D texture) // constructor for derived class
        {         
            tiledImage = texture;
            rectangle = new Rectangle(0, 0, tiledImage.Width, tiledImage.Height);
            spriteEffects = SpriteEffects.None;
            colour = Color.White;
            scale = 1;
            rotation = 0;
        }

        public Sprite(Texture2D texture, Rectangle newRectangle) //constructor for non - square images
        {
            tiledImage = texture;
            rectangle = newRectangle;
            spriteEffects = SpriteEffects.None;
            colour = Color.White;
            scale = 1;
            rotation = 0;
        }

        public virtual Vector2 Origin() // gets centre of image
        {
            return new Vector2(tiledImage.Width / 2.0f, tiledImage.Height / 2.0f);
        }

        public void setSpriteEffect(SpriteEffects sprEff)
        {
            spriteEffects = sprEff;
        }
        public void setScale(float newScale)
        {
            scale = newScale;
        }
        public void setSpriteColour(Color c)
        {
            colour = c;
        }
        public void setRotation(float newRotation)
        {
            rotation = newRotation;
        }
        public float getRotation()
        {
            return rotation;
        }

        public SpriteEffects getSpriteEffect()
        {
            return spriteEffects;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 newPosition, SpriteEffects spriteEffects)
        {
            // Calculate the source rectangle of the current frame

            // Draw the current frame.
            // (bigTexture, posOnScreen, sourceRect in big texture, col, rotation, origin, scale, effect, depth)
            Vector2 orig = Origin();
            position = newPosition;
            spriteBatch.Draw(tiledImage, newPosition, rectangle, colour, rotation, orig, scale, spriteEffects, 0.5f);
        }
    }

    //an animated strip of squares
    public class SpriteAnimation : Sprite
    {
        private float frameTimeDuration;
        private bool isLooping;
        private float elapsedFrameTime;
        private int frameIndex;
        private int frameCounter;
        public bool reversed;

        public SpriteAnimation(Texture2D texture,float frameTime, bool setupIsLooping) : base(texture)
        {   

            frameTimeDuration = frameTime;
            isLooping = setupIsLooping;
            elapsedFrameTime = 0.0f;
            frameIndex = 0;
            frameCounter = 0;
            spriteEffects = SpriteEffects.None;
            colour = Color.White;
            reversed = false;
            rectangle = new Rectangle(0, 0, tiledImage.Height, tiledImage.Height);
        }

        public override Vector2 Origin() // gets centre of one square in the timeline strip
        {
            return new Vector2(tiledImage.Height / 2.0f, tiledImage.Height / 2.0f);
        }

        public int FrameCount() // gets amount of images animated
        {
            return tiledImage.Width / tiledImage.Height;
        }

        public int getFrameCounter()
        {
            return frameCounter;
        }
        public void restartFowards()
        {
            frameCounter = 1;
            frameIndex = 0;
        }
        public void restartReverse()
        {
            reversed = true;
            frameIndex = FrameCount() - 1;
            frameCounter = FrameCount() - 1;
        }

        public int GetCurrentFrame(GameTime gameTime)
        {
            elapsedFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedFrameTime > frameTimeDuration)
            {
                if (!reversed)
                {
                    if (isLooping)
                    {
                        frameIndex = frameCounter % FrameCount();
                    }
                    else
                    {
                        // freezes on the last frame
                        frameIndex = Math.Min(frameCounter, FrameCount() - 1);
                    }
                    elapsedFrameTime = 0.0f;
                    // Advance the frame index; either loops or stops on last frame

                    frameCounter++;
                }
                else
                {
                    if (isLooping)
                    {
                        frameIndex = frameCounter % FrameCount();
                    }
                    else
                    {
                        // freezes on the last frame
                        frameIndex = Math.Max(frameCounter, 0);
                    }
                    elapsedFrameTime = 0.0f;
                    frameCounter--;
                }
            }
                return frameIndex;
        }

        public Rectangle GetFrameRectangleForCurrentFrame(int currentFrameIndex)
        {
            int cellWidth = tiledImage.Height;
            int leftMostPixel = currentFrameIndex * cellWidth;
            return new Rectangle(leftMostPixel, 0, cellWidth, cellWidth);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            var currentFrame = GetCurrentFrame(gameTime);

            // Calculate the source rectangle of the current frame
            var imageRectangle = GetFrameRectangleForCurrentFrame(currentFrame);
            
            // Draw the current frame.
            // (bigTexture, posOnScreen, sourceRect in big texture, col, rotation, origin, scale, effect, depth)
            Vector2 orig = Origin();
            
            spriteBatch.Draw(tiledImage, position, imageRectangle, colour,rotation, orig, scale, spriteEffects, 0.5f);
        }

    }
}