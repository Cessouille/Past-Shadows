using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace S1._01
{
    public class Victory : GameScreen
    {
        private Game1 _myGame;

        private Texture2D _background;
        private Texture2D _boutonRejouer;
        private Texture2D _boutonQuitter;

        private Vector2 _backgroundPos;
        private Vector2 _posRejouer;
        private Vector2 _posQuitter;
        public Victory(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            _backgroundPos = new Vector2(300, 150);
            _posRejouer = new Vector2(400, 400);
            _posQuitter = new Vector2(750, 400);

            base.Initialize();
        }
        public override void LoadContent()
        {
            _background = Content.Load<Texture2D>("Victory");
            _boutonRejouer = Content.Load<Texture2D>("boutonRejouer");
            _boutonQuitter = Content.Load<Texture2D>("boutonQuitter2");

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_background, _backgroundPos, Color.White);
            _myGame.SpriteBatch.Draw(_boutonRejouer, _posRejouer, Color.White);
            _myGame.SpriteBatch.Draw(_boutonQuitter, _posQuitter, Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}