using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework;

namespace MouseHunt
{
    public class DebugTools
    {
        public void DebugRectangle(SpriteBatch spriteBatch, Rectangle position)
        {
            #if DEBUG
            var rect = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
            spriteBatch.Draw(rect, position, Color.White);
            #endif
        }
    }
}
