using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Helpers;
using Engines;

namespace Screens
{
    class SplashScreen : DrawableGameComponent
    {
        Texture2D _tx;
        public bool Active { get; set; }

        public Texture2D Tx
        {
            get
            {
                return _tx;
            }

            set
            {
                _tx = value;
            }
        }
        public Song BackingTrack { get; set; }
        public SoundEffectInstance  SoundPlayer { get; set; }
        public Vector2 Position { get; set; }

        public Keys ActivationKey;


        public SplashScreen(Game game, Vector2 pos, Texture2D tx, Song sound, Keys key) : base(game)
        {
            game.Components.Add(this);
            _tx = tx;
            BackingTrack = sound;
            Position = pos;
            ActivationKey = key;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputEngine.IsKeyPressed(ActivationKey))
                Active = !Active;
            if (Active)
            {
                if (BackingTrack != null && MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(BackingTrack);
                }
            }
            if (!Active)
            {
                if (MediaPlayer.State == MediaState.Playing && MediaPlayer.Queue.ActiveSong.Name == BackingTrack.Name)
                {
                    MediaPlayer.Stop();
                    // Could do resume and Pause if you want Media player state
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            if (spriteBatch == null) return;
            if (Active)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(_tx, new Rectangle(Position.ToPoint(), new Point(
                    this.Game.GraphicsDevice.Viewport.Bounds.Width,
                    this.Game.GraphicsDevice.Viewport.Bounds.Height)), Color.White);
                spriteBatch.End();

            }
            base.Draw(gameTime);
        }
    }
}
