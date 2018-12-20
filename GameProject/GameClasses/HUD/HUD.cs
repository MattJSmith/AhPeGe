using AhPeGe.GameProject.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace AhPeGe.GameProject.GameClasses.HUD
{
    /// <summary>
    /// The overlay of information for the player
    /// </summary>
    public class HUD
    {
        Message message;
        /// <summary>
        /// Eventually make a contentmanager factory class which makes all these constuctors?
        /// </summary>
        /// <param name="font"></param>
        public HUD(SpriteFont font)
        {
            message = new Message(font);
        }

        public HUD()
        {
            message = new Message();
        }

        public void Update(string mapCoordinates) {
            message.SetMessage(mapCoordinates);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var messagePosition = new Vector2(500, 100);
            message.Draw(spriteBatch, messagePosition);
        }
    }
}
