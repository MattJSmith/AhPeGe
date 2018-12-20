#region Using Statements

using System;
using System.Collections.Generic;
using AhPeGe.GameProject.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework;

#endregion

namespace AhPeGe
{

    //An imaging consisting of a centre image and images in each direction
    public class SpriteAnimationCross : Sprite
    {
  
        public int frameWidth;
        private Vector2 positionOfMiddleImageInTexture;

        private float frameTimeDuration;
        private float elapsedFrameTime;
        private int frameIndex;
        private int frameCounter;
        public bool Paused { get; private set; }
        private Direction animationDirection;
        private int currentFrame;
        private int ImagesPerDirection;
        private Rectangle currentFrameRectangle;
       

        public SpriteAnimationCross(Texture2D texture, int imagesPerDirectionIncludingCenter, float frameTime, Vector2 startPosition) : base(texture)
        {
            ImagesPerDirection = imagesPerDirectionIncludingCenter;
            
            frameTimeDuration = frameTime;
            elapsedFrameTime = 0.0f;
            frameIndex = 0;
            frameCounter = 0;
            spriteEffects = SpriteEffects.None;
            colour = Color.White;
        
            Paused = true;
            this.frameWidth = GetFrameWidth(tiledImage.Width, ImagesPerDirection);
            position = startPosition;
            rectangle = new Rectangle((int)Origin().X, (int)Origin().Y, frameWidth, frameWidth);
            var frameWidthScaled = frameWidth;
            var centreImagePos = (frameWidthScaled * (ImagesPerDirection - 1));
            positionOfMiddleImageInTexture= new Vector2(centreImagePos, centreImagePos);

        }

        /// <summary>
        /// Gets a screen co-ordinate of the middle of the image.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMiddleOfImageScreenCoordinates() {
            var frameWidthScaled = frameWidth * scale;

            return new Vector2(frameWidthScaled /2, frameWidthScaled /2);
        }

        /// <summary>
        /// Centre of a single square of the tile.
        /// </summary>
        /// <returns></returns>
        public override Vector2 Origin()
        {
            return new Vector2(frameWidth / 2.0f, frameWidth / 2.0f);
        }

        public int GetFrameWidth(int entireTextureWidth, int imagesPerDirection)
        {
            var imageFrames = (imagesPerDirection * 2) - 1;

            return entireTextureWidth / imageFrames;
        }



        public void StartAnimationInDirection(Direction direction)
        {
            frameCounter = 0;
            Paused = false;
            animationDirection = direction;
            frameIndex = 0;
        }

        public int GetNextFrame(GameTime gameTime)
        {
            elapsedFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedFrameTime > frameTimeDuration)
            {
                // freezes on the last frame
                frameIndex = Math.Min(frameCounter, ImagesPerDirection - 1);

                if (frameCounter > ImagesPerDirection - 1) {
                    Paused = true;
                    frameIndex = 0;
                    return frameIndex;
                }

                 elapsedFrameTime = 0.0f;

                // Advance the frame index; stops on last frame
                frameCounter++;
                
            }

            return frameIndex;
        }

        public Rectangle GetFrameRectangleForCurrentFrame(int currentFrameIndex, Direction direction)
        {
            int cellWidth = frameWidth;
            int farLeftPixelOfSpriteImage = currentFrameIndex * cellWidth;


            int X = (int)positionOfMiddleImageInTexture.X;
            int Y = (int)positionOfMiddleImageInTexture.Y;

            switch (direction)
            {
                case Direction.Down:
                    Y = (int)positionOfMiddleImageInTexture.Y + farLeftPixelOfSpriteImage;
                    break;
                case Direction.Up:
                    Y = (int)positionOfMiddleImageInTexture.Y - farLeftPixelOfSpriteImage;
                    break;
                case Direction.Left:
                    X = (int)positionOfMiddleImageInTexture.X - farLeftPixelOfSpriteImage;
                    break;
                case Direction.Right:
                    X = (int)positionOfMiddleImageInTexture.X + farLeftPixelOfSpriteImage;
                    break;
            }

            return new Rectangle(X, Y, cellWidth, cellWidth);
        }

        public void Update(GameTime gameTime)
        {
            if (Paused) {
                currentFrameRectangle = GetFrameRectangleForCurrentFrame(0, animationDirection);
                return;
            }

            currentFrame = GetNextFrame(gameTime);

            // Calculate the source rectangle of the current frame
            currentFrameRectangle = GetFrameRectangleForCurrentFrame(currentFrame, animationDirection);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch,
             SpriteEffects spriteEffects)
        {
            Draw(gameTime, spriteBatch,position, spriteEffects);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch,
            Vector2 position, SpriteEffects spriteEffects)
        {
            // Draw the current frame.
            Vector2 orig = Origin();
   
            spriteBatch.Draw(tiledImage, position, currentFrameRectangle, colour, rotation, orig, scale, spriteEffects, 0.5f);
        }

    }
}