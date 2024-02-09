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
    public class Niveau1 : GameScreen
    {
        private Game1 _myGame;

        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
        private TiledMapTileLayer decor;
        private Texture2D _background;
        public Niveau1(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("Level_Forest");
            _background = Content.Load<Texture2D>("background1");
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("platform-lvl1");
            decor = _tiledMap.GetLayer<TiledMapTileLayer>("décorlvl1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
            _myGame.SpriteBatch.Draw(_myGame.Ennemi1, _myGame.PositionEnnemi1);
            _myGame.SpriteBatch.Draw(_myGame.Ennemi2, _myGame.PositionEnnemi2);
            _myGame.SpriteBatch.Draw(_myGame.Ennemi3, _myGame.PositionEnnemi3);
            _myGame.SpriteBatch.Draw(_myGame.Perso, _myGame.Position);
            _myGame.SpriteBatch.End();
            _tiledMapRenderer.Draw();
        }
    }
}
