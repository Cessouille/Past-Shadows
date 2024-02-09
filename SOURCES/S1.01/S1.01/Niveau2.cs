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
    public class Niveau2 : GameScreen
    {
        private Game1 _myGame;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
        private TiledMapTileLayer decor;
        private Texture2D _background;
        public Niveau2(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("Level_Boss");
            _background = Content.Load<Texture2D>("mt_fugi2");
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclelvl3");
            decor = _tiledMap.GetLayer<TiledMapTileLayer>("decorlvl3");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw();
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_myGame.Boss, _myGame.PositionBoss);
            _myGame.SpriteBatch.Draw(_myGame.Perso, _myGame.Position);
            _myGame.SpriteBatch.End();
        }
    }
}