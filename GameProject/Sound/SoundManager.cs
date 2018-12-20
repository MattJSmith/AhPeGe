using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MouseHunt
{
    public class SoundManager
    {
        //private SoundEffect playerHit;

        public SoundManager(ContentManager Content)
        {
            //playerHit = Content.Load<SoundEffect>(ContentLocations.Player + "MouseHit");
        }


        private void PlaySound(SoundEffect soundEffect, float volumne = 1, bool loop = false)
        {
            var instance = soundEffect.CreateInstance();

            instance.Volume = volumne;

            instance.IsLooped = loop;

            instance.Play();
        }
    }
}
