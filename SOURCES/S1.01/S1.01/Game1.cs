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

    public class Game1 : Game
    {
        public const int LARGEUR_FENETRE = 1500;
        public const int HAUTEUR_FENETRE = 844;

        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        private readonly ScreenManager _screenManager;

        private AnimatedSprite _perso;
        private AnimatedSprite _ennemi1;
        private AnimatedSprite _ennemi2;
        private AnimatedSprite _ennemi3;
        private AnimatedSprite _boss;

        private Texture2D _ecranAccueil;
        private Texture2D _boutonJouer;
        private Texture2D _boutonQuitter;

        private Vector2 _persoPosition;
        private Vector2 _ennemiPosition1;
        private Vector2 _ennemiPosition2;
        private Vector2 _ennemiPosition3;
        private Vector2 _bossPosition;
        private Vector2 _boutonJouerPos;
        private Vector2 _boutonQuitterPos;

        private string animation;
        private string animationEnnemi1;
        private string animationEnnemi2;
        private string animationEnnemi3;
        private string animationBoss;

        private int _vitessePerso;
        private int _vitesseEnnemi;

        private int compteur;
        private int compteurE1;
        private int compteurE2;
        private int compteurE3;
        private int compteurBoss;
        private int i;

        private bool _isOnGround;
        private bool _jumping;
        private bool _attacking;
        private bool _attackingBoss;
        private bool _isOnGroundBoss;
        private bool _fleeing;
        private bool _jouer;
        private bool _quitter;

        private string cote;
        private string coteE1;
        private string coteE2;
        private string coteE3;
        private string coteBoss;

        private TiledMap _tiledMap1;
        private TiledMapTileLayer map1Layer;
        private TiledMap _tiledMap2;
        private TiledMapTileLayer map2Layer;
        private Vector2 backgroundPos;

        private int pv;
        private int pvE1;
        private int pvE2;
        private int pvE3;
        private int pvBoss;
        private int scene;

        private double chronometre;
        private double chronometreE;
        private double chronometreBoss;

        private MouseState _currentMouse;
        private MouseState _previousMouse;
        public SpriteBatch SpriteBatch
        {
            get
            {
                return this._spriteBatch;
            }

            set
            {
                this._spriteBatch = value;
            }
        }
        public AnimatedSprite Perso
        {
            get
            {
                return this._perso;
            }
            set
            {
                this._perso = value;
            }
        }
        public Vector2 Position
        {
            get
            {
                return this._persoPosition;
            }

            set
            {
                this._persoPosition = value;
            }
        }
        public AnimatedSprite Ennemi1
        {
            get
            {
                return this._ennemi1;
            }
            set
            {
                this._ennemi1 = value;
            }
        }
        public AnimatedSprite Ennemi2
        {
            get
            {
                return this._ennemi2;
            }
            set
            {
                this._ennemi2 = value;
            }
        }
        public AnimatedSprite Ennemi3
        {
            get
            {
                return this._ennemi3;
            }
            set
            {
                this._ennemi3 = value;
            }
        }
        public AnimatedSprite Boss
        {
            get
            {
                return this._boss;
            }
            set
            {
                this._boss = value;
            }
        }
        public Vector2 PositionEnnemi1
        {
            get
            {
                return this._ennemiPosition1;
            }

            set
            {
                this._ennemiPosition1 = value;
            }
        }
        public Vector2 PositionEnnemi2
        {
            get
            {
                return this._ennemiPosition2;
            }

            set
            {
                this._ennemiPosition2 = value;
            }
        }
        public Vector2 PositionEnnemi3
        {
            get
            {
                return this._ennemiPosition3;
            }

            set
            {
                this._ennemiPosition3 = value;
            }
        }
        public Vector2 PositionBoss
        {
            get
            {
                return this._bossPosition;
            }

            set
            {
                this._bossPosition = value;
            }
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = LARGEUR_FENETRE;
            _graphics.PreferredBackBufferHeight = HAUTEUR_FENETRE;
            _graphics.ApplyChanges();

            _persoPosition = new Vector2(40, _graphics.GraphicsDevice.Viewport.Height - 150);
            _bossPosition = new Vector2(1160, _graphics.GraphicsDevice.Viewport.Height - 150);
            _ennemiPosition1 = new Vector2(900, 85);
            _ennemiPosition2 = new Vector2(850, 375);
            _ennemiPosition3 = new Vector2(1321, 260);
            _boutonJouerPos = new Vector2(1143, 276);
            _boutonQuitterPos = new Vector2(1143, 385);

            _vitessePerso = 100;
            _vitesseEnnemi = 50;

            animation = "idled";
            animationEnnemi1 = "marcheg";
            animationEnnemi2 = "marched";
            animationEnnemi3 = "marched";
            animationBoss = "idled";

            compteur = 1;
            compteurE1 = -1;
            compteurE2 = 1;
            compteurE3 = 1;

            i = 0;

            pv = 3;
            pvE1 = 1;
            pvE2 = 1;
            pvE3 = 1;
            pvBoss = 5;

            chronometre = 0;
            chronometreE = 0;

            cote = "d";
            coteE1 = "g";
            coteE2 = "d";
            coteE3 = "d";
            coteBoss = "g";

            _isOnGround = false;
            _jumping = false;
            _attacking = false;
            _attackingBoss = false;
            _isOnGroundBoss = false;
            _fleeing = false;
            _jouer = false;
            _quitter = false;

            scene = 1;

            backgroundPos = new Vector2(0, 0);
            Rectangle _personnage = new Rectangle((int)_persoPosition.X, (int)_persoPosition.Y, 40, 59);
            Rectangle _attaque = new Rectangle((int)_persoPosition.X + 40, (int)_persoPosition.Y, 50, 59);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tiledMap1 = Content.Load<TiledMap>("Level_Forest");
            map1Layer = _tiledMap1.GetLayer<TiledMapTileLayer>("platform-lvl1");
            _tiledMap2 = Content.Load<TiledMap>("Level_Boss");
            map2Layer = _tiledMap2.GetLayer<TiledMapTileLayer>("obstaclelvl3");

            _ecranAccueil = Content.Load<Texture2D>("accueil");
            _boutonJouer = Content.Load<Texture2D>("boutonJouer");
            _boutonQuitter = Content.Load<Texture2D>("boutonQuitter");

            SpriteSheet personnage = Content.Load<SpriteSheet>("perso.sf", new JsonContentLoader());
            SpriteSheet araignee = Content.Load<SpriteSheet>("spider.sf", new JsonContentLoader());
            SpriteSheet boss = Content.Load<SpriteSheet>("spritesheet-boss.sf", new JsonContentLoader());

            _perso = new AnimatedSprite(personnage);
            _ennemi1 = new AnimatedSprite(araignee);
            _ennemi2 = new AnimatedSprite(araignee);
            _ennemi3 = new AnimatedSprite(araignee);
            _boss = new AnimatedSprite(boss);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_quitter || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * _vitessePerso;
            float walkSpeedE = deltaSeconds * _vitesseEnnemi;
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Rectangle _personnage = new Rectangle((int)_persoPosition.X, (int)_persoPosition.Y, 40, 59);
            Rectangle _attaque = new Rectangle(0, 0, 50, 59);
            Rectangle _allonge = new Rectangle(0, 0, 50, 59);

            #region Personnage

            if ((pv == 0 || _persoPosition.Y > _graphics.GraphicsDevice.Viewport.Height) && scene != 1)
            {
                animation = "vraie mort" + cote;
                pv = -1;
                scene = 5;
                LoadScreenGameOver();
            }

            if (pv > 0)
            {
                if (compteur == 1)
                    cote = "d";
                else
                    cote = "g";

                if (_isOnGround && !_attacking)
                {
                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        _jumping = true;
                        _isOnGround = false;
                    }
                    if (pv != 0)
                        animation = "idle" + cote;
                    if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Q))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap1.TileWidth - 0.80);
                        ushort ty1 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight);
                        ushort ty2 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 1);
                        ushort ty3 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 2);
                        animation = "marcheg";
                        compteur = -1;
                        if (!(IsCollision(tx, ty1)) && !(IsCollision(tx, ty2)) && !(IsCollision(tx, ty3)))
                            _persoPosition.X -= walkSpeed;
                        if (_persoPosition.X < 0)
                            _persoPosition.X += walkSpeed;
                    }
                    else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                    {
                        animation = "marched";
                        compteur = 1;
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap1.TileWidth + 0.80);
                        ushort ty1 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight);
                        ushort ty2 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 1);
                        ushort ty3 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 2);
                        if (!(IsCollision(tx, ty1)) && !(IsCollision(tx, ty2)) && !(IsCollision(tx, ty3)))
                            _persoPosition.X += walkSpeed;
                        if (_persoPosition.X > _graphics.GraphicsDevice.Viewport.Width)
                            _persoPosition.X -= walkSpeed;
                    }
                }

                if (_jumping)
                {
                    ushort tx1 = (ushort)(_persoPosition.X / _tiledMap1.TileWidth);
                    ushort tx2 = (ushort)(_persoPosition.X / _tiledMap1.TileWidth - 0.80);
                    ushort tx3 = (ushort)(_persoPosition.X / _tiledMap1.TileWidth + 0.80);
                    ushort ty1 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight);
                    ushort ty2 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 1);
                    ushort ty3 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 2);
                    ushort ty4 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 2.5);
                    if (compteur == 1)
                    {
                        if (!(IsCollision(tx1, ty1)) && !(IsCollision(tx2, ty1)))
                        {
                            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                            {
                                ushort tx = (ushort)(_persoPosition.X / _tiledMap1.TileWidth + 0.50);
                                if (!(IsCollision(tx3, ty1)) && !(IsCollision(tx3, ty2)) && !(IsCollision(tx3, ty3)) && !(IsCollision(tx3, ty4)) && !(IsCollision(tx, ty4)) && !(IsCollision(tx, ty3)) && !(IsCollision(tx, ty2)) && !(IsCollision(tx, ty1)))
                                    _persoPosition.X += walkSpeed;
                            }
                            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Q))
                            {
                                ushort tx = (ushort)(_persoPosition.X / _tiledMap1.TileWidth + 0.50);
                                if (!(IsCollision(tx3, ty1)) && !(IsCollision(tx3, ty2)) && !(IsCollision(tx3, ty3)) && !(IsCollision(tx3, ty4)) && !(IsCollision(tx, ty4)) && !(IsCollision(tx, ty3)) && !(IsCollision(tx, ty2)) && !(IsCollision(tx, ty1)))
                                    _persoPosition.X -= walkSpeed;
                            }
                            if (i == 32)
                            {
                                _jumping = false;
                                i = 0;
                            }
                            animation = "saut" + cote;
                            _persoPosition.Y -= 2.50f;
                            i++;
                        }
                        else
                            _jumping = false;
                    }
                    else if (compteur != 1)
                    {
                        if (!(IsCollision(tx1, ty1)) && !(IsCollision(tx3, ty1)))
                        {
                            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                            {
                                ushort tx = (ushort)(_persoPosition.X / _tiledMap1.TileWidth + 0.50);
                                if (!(IsCollision(tx3, ty1)) && !(IsCollision(tx3, ty2)) && !(IsCollision(tx3, ty3)) && !(IsCollision(tx3, ty4)) && !(IsCollision(tx, ty4)) && !(IsCollision(tx, ty3)) && !(IsCollision(tx, ty2)) && !(IsCollision(tx, ty1)))
                                    _persoPosition.X += walkSpeed;
                            }
                            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Q))
                            {
                                ushort tx = (ushort)(_persoPosition.X / _tiledMap1.TileWidth - 0.50);
                                if (!(IsCollision(tx3, ty1)) && !(IsCollision(tx3, ty2)) && !(IsCollision(tx3, ty3)) && !(IsCollision(tx3, ty4)) && !(IsCollision(tx, ty4)) && !(IsCollision(tx, ty3)) && !(IsCollision(tx, ty2)) && !(IsCollision(tx, ty1)))
                                    _persoPosition.X -= walkSpeed;
                            }
                            if (i == 32)
                            {
                                _jumping = false;
                                i = 0;
                            }
                            animation = "saut" + cote;
                            _persoPosition.Y -= 2.50f;
                            i++;
                        }
                        else
                            _jumping = false;
                    }

                }

                else if (!(_jumping) && !(_isOnGround))
                {
                    ushort ty1 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight);
                    ushort ty2 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 1);
                    ushort ty3 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 2);
                    ushort ty4 = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 3);
                    if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap1.TileWidth + 0.80);
                        if (!(IsCollision(tx, ty1)) && !(IsCollision(tx, ty2)) && !(IsCollision(tx, ty3)) && !(IsCollision(tx, ty4)))
                            _persoPosition.X += walkSpeed;
                    }
                    else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Q))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap1.TileWidth - 0.80);
                        if (!(IsCollision(tx, ty1)) && !(IsCollision(tx, ty2)) && !(IsCollision(tx, ty3)) && !(IsCollision(tx, ty4)))
                            _persoPosition.X -= walkSpeed;
                    }
                    animation = "chute" + cote;
                    _persoPosition.Y += 2.50f;
                }

                ushort x1 = (ushort)(_persoPosition.X / _tiledMap1.TileWidth - 1);
                ushort x2 = (ushort)(_persoPosition.X / _tiledMap1.TileWidth);
                ushort x3 = (ushort)(_persoPosition.X / _tiledMap1.TileWidth + 1);
                ushort y = (ushort)(_persoPosition.Y / _tiledMap1.TileHeight + 2.75);

                if (compteur == 1)
                {
                    if (IsCollision(x1, y) || IsCollision(x2, y))
                        _isOnGround = true;
                    if (!(IsCollision(x1, y)) && !(IsCollision(x2, y)))
                        _isOnGround = false;
                }
                else if (compteur != 1)
                {
                    if (IsCollision(x2, y) || IsCollision(x3, y))
                        _isOnGround = true;
                    if (!(IsCollision(x2, y)) && !(IsCollision(x3, y)))
                        _isOnGround = false;
                }

                if (mouseState.LeftButton == ButtonState.Pressed || mouseState.RightButton == ButtonState.Pressed)
                    _attacking = true;

                if (mouseState.LeftButton == ButtonState.Released && mouseState.RightButton == ButtonState.Released)
                    _attacking = false;

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    animation = "coup simple " + cote;
                    if (compteur == 1)
                        _attaque = new Rectangle((int)_persoPosition.X + 40, (int)_persoPosition.Y, 50, 59);
                    else if (compteur != 1)
                        _attaque = new Rectangle((int)_persoPosition.X - 40, (int)_persoPosition.Y, 50, 59);
                }

                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    animation = "coup double " + cote;
                    if (compteur == 1)
                        _attaque = new Rectangle((int)_persoPosition.X + 40, (int)_persoPosition.Y, 50, 59);
                    else if (compteur != 1)
                        _attaque = new Rectangle((int)_persoPosition.X - 40, (int)_persoPosition.Y, 50, 59);
                }
                #endregion

                #region Niveau1
                if (scene == 2)
                {
                    Rectangle ennemi1 = new Rectangle((int)_ennemiPosition1.X, (int)_ennemiPosition1.Y, 20, 15);
                    Rectangle ennemi2 = new Rectangle((int)_ennemiPosition2.X, (int)_ennemiPosition2.Y, 20, 15);
                    Rectangle ennemi3 = new Rectangle((int)_ennemiPosition3.X, (int)_ennemiPosition3.Y, 20, 15);

                    if (pvE1 > 0)
                    {
                        ushort ex1 = (ushort)(_ennemiPosition1.X / _tiledMap1.TileWidth - 1);
                        ushort ex2 = (ushort)(_ennemiPosition1.X / _tiledMap1.TileWidth + 1);
                        ushort ey = (ushort)(_ennemiPosition1.Y / _tiledMap1.TileHeight + 1);
                        animationEnnemi1 = "marche" + coteE1;
                        if (compteurE1 == 1 && animationEnnemi1 != "vraie mort")
                        {
                            if (IsCollision(ex2, ey))
                                _ennemiPosition1.X += walkSpeedE;
                            else
                                compteurE1 = -1;
                        }
                        if (compteurE1 != 1)
                        {
                            if (IsCollision(ex1, ey))
                                _ennemiPosition1.X -= walkSpeedE;
                            else
                                compteurE1 = 1;
                        }

                        if (compteurE1 == 1)
                            coteE1 = "d";
                        else
                            coteE1 = "g";
                    }

                    else if (pvE1 <= 0)
                        animationEnnemi1 = "vraie mort";

                    if (pvE2 > 0)
                    {
                        ushort ex1 = (ushort)(_ennemiPosition2.X / _tiledMap1.TileWidth - 1);
                        ushort ex2 = (ushort)(_ennemiPosition2.X / _tiledMap1.TileWidth + 1);
                        ushort ey1 = (ushort)(_ennemiPosition2.Y / _tiledMap1.TileHeight + 1);
                        ushort ey2 = (ushort)(_ennemiPosition2.Y / _tiledMap1.TileHeight);
                        animationEnnemi2 = "marche" + coteE2;
                        if (compteurE2 == 1)
                        {
                            if (IsCollision(ex2, ey1) && !(IsCollision(ex2, ey2)))
                                _ennemiPosition2.X += walkSpeedE;
                            else
                                compteurE2 = -1;
                        }
                        if (compteurE2 != 1)
                        {
                            if (IsCollision(ex1, ey1) && !(IsCollision(ex1, ey2)))
                                _ennemiPosition2.X -= walkSpeedE;
                            else
                                compteurE2 = 1;
                        }

                        if (compteurE2 == 1)
                            coteE2 = "d";
                        else
                            coteE2 = "g";
                    }

                    else if (pvE2 <= 0)
                        animationEnnemi2 = "vraie mort";

                    if (pvE3 > 0)
                    {
                        ushort ex1 = (ushort)(_ennemiPosition3.X / _tiledMap1.TileWidth - 1);
                        ushort ex2 = (ushort)(_ennemiPosition3.X / _tiledMap1.TileWidth + 1);
                        ushort ey = (ushort)(_ennemiPosition3.Y / _tiledMap1.TileHeight + 1);
                        animationEnnemi3 = "marche" + coteE3;
                        if (compteurE3 == 1)
                        {
                            if (IsCollision(ex2, ey))
                                _ennemiPosition3.X += walkSpeedE;
                            else
                                compteurE3 = -1;
                        }
                        if (compteurE3 != 1)
                        {
                            if (IsCollision(ex1, ey))
                                _ennemiPosition3.X -= walkSpeedE;
                            else
                                compteurE3 = 1;
                        }

                        if (compteurE3 == 1)
                            coteE3 = "d";
                        else
                            coteE3 = "g";
                    }

                    else if (pvE3 <= 0)
                        animationEnnemi3 = "vraie mort";

                    if (_personnage.Intersects(ennemi1) && pv != 0 && pvE1 > 0)
                    {
                        animation = "degats" + cote;
                        if (chronometre == 0)
                        {
                            pv -= 1;
                            chronometre += deltaSeconds;
                        }
                    }

                    if (_personnage.Intersects(ennemi2) && pv != 0 && pvE2 > 0)
                    {
                        animation = "degats" + cote;
                        if (chronometre == 0)
                        {
                            pv -= 1;
                            chronometre += deltaSeconds;
                        }
                    }

                    if (_personnage.Intersects(ennemi3) && pv != 0 && pvE3 > 0)
                    {
                        animation = "degats" + cote;
                        if (chronometre == 0)
                        {
                            pv -= 1;
                            chronometre += deltaSeconds;
                        }
                    }

                    if (_attaque.Intersects(ennemi1) && pvE1 > 0)
                    {
                        animationEnnemi1 = "mort";
                        pvE1 -= 1;
                    }

                    if (_attaque.Intersects(ennemi2) && pvE2 > 0)
                    {
                        animationEnnemi2 = "mort";
                        pvE2 -= 1;
                    }

                    if (_attaque.Intersects(ennemi3) && pvE3 > 0)
                    {
                        animationEnnemi3 = "mort";
                        pvE3 -= 1;
                    }

                    if (chronometre != 0)
                    {
                        chronometre += deltaSeconds;
                        if (chronometre > 2)
                            chronometre = 0;
                    }

                    if (chronometreE != 0)
                    {
                        chronometreE += deltaSeconds;
                        if (chronometreE > 2)
                            chronometreE = 0;
                    }
                }

                #endregion

                #region Niveau Boss
                if (scene > 2)
                {
                    Rectangle _hitboxBoss = new Rectangle((int)_bossPosition.X, (int)_bossPosition.Y, 40, 59);
                    Rectangle _zone = new Rectangle((int)_persoPosition.X + 40, (int)_persoPosition.Y, 60, 59);

                    if (compteurBoss == 1)
                    {
                        coteBoss = "d";
                        _allonge = new Rectangle((int)_bossPosition.X + 40, (int)_bossPosition.Y, 50, 59);
                    }
                    else
                    {
                        coteBoss = "g";
                        _allonge = new Rectangle((int)_bossPosition.X - 40, (int)_bossPosition.Y, 50, 59);
                    }

                    ushort yb = (ushort)(_bossPosition.Y / _tiledMap1.TileHeight + 1.75);

                    if (compteurBoss == 1)
                    {
                        if (IsCollision(x1, yb) || IsCollision(x2, yb))
                            _isOnGroundBoss = true;
                        if (!(IsCollision(x1, yb)) && !(IsCollision(x2, yb)))
                            _isOnGroundBoss = false;
                    }
                    else if (compteurBoss != 1)
                    {
                        if (IsCollision(x2, yb) || IsCollision(x3, yb))
                            _isOnGroundBoss = true;
                        if (!(IsCollision(x2, yb)) && !(IsCollision(x3, yb)))
                            _isOnGroundBoss = false;
                    }

                    if (_isOnGroundBoss && !_attackingBoss && !_fleeing && pvBoss > 0)
                    {
                        if (_persoPosition.X < _bossPosition.X)
                        {
                            compteurBoss = -1;
                            _bossPosition.X -= walkSpeed;
                            if (_bossPosition.X < 0)
                                _bossPosition.X += walkSpeed;
                        }
                        else if (_persoPosition.X > _bossPosition.X)
                        {
                            compteurBoss = 1;
                            _bossPosition.X += walkSpeed;
                            if (_bossPosition.X > _graphics.GraphicsDevice.Viewport.Width)
                                _bossPosition.X -= walkSpeed;
                        }
                        animationBoss = "marche" + coteBoss;
                    }

                    if (!(_isOnGroundBoss))
                    {
                        animationBoss = "chute" + coteBoss;
                        _bossPosition.Y += 2.50f;
                    }

                    if (_personnage.Intersects(_allonge))
                        _attackingBoss = true;

                    if (!(_personnage.Intersects(_allonge)))
                        _attackingBoss = false;

                    if (_zone.Intersects(_hitboxBoss) && _attacking)
                        _fleeing = true;

                    if (!(_zone.Intersects(_hitboxBoss)))
                        _fleeing = false;

                    if (_fleeing && pvBoss > 0)
                    {
                        if (compteur == 1)
                        {
                            animationBoss = "marched";
                            _bossPosition.X += walkSpeed;
                        }
                        else if (compteur != 1)
                        {
                            animationBoss = "marcheg";
                            _bossPosition.X -= walkSpeed;
                        }
                    }

                    if (_attackingBoss && pv > 0 && !_fleeing && pvBoss > 0)
                    {
                        animationBoss = "attaque1" + coteBoss;
                        if (_allonge.Intersects(_personnage))
                        {
                            if (chronometre < 0.35)
                                animation = "degats" + cote;
                            if (chronometre == 0)
                            {
                                pv -= 1;
                                chronometre += deltaSeconds;
                            }
                        }
                    }

                    if (_attacking && _attaque.Intersects(_hitboxBoss))
                    {
                        if (chronometreBoss < 0.35)
                            animationBoss = "degats" + cote;
                        if (chronometreBoss == 0)
                        {
                            pvBoss -= 1;
                            chronometreBoss += deltaSeconds;
                        }
                    }

                    if (chronometre != 0)
                    {
                        chronometre += deltaSeconds;
                        if (chronometre > 2)
                            chronometre = 0;
                    }

                    if (chronometreBoss != 0)
                    {
                        chronometreBoss += deltaSeconds;
                        if (chronometreBoss > 2)
                            chronometreBoss = 0;
                    }

                    if (pv <= 0)
                        animationBoss = "idle" + coteBoss;

                    if (pvBoss <= 0)
                        animationBoss = "vraie mort" + coteBoss;
                }
            }
            #endregion

            if (pvBoss == 0)
            {
                pvBoss = -1;
                scene = 10;
                LoadScreenVictory();
            }

            if (_jouer == true || _persoPosition.X > 1550)
            {
                if (scene == 1)
                {
                    LoadScreen1();
                }
                else if (scene == 2)
                {
                    LoadScreen2();
                }
                _jouer = false;
                scene += 1;
            }

            if (scene == 1)
            {
                _previousMouse = _currentMouse;
                _currentMouse = Mouse.GetState();

                Rectangle boutonJouer = new Rectangle((int)_boutonJouerPos.X, (int)_boutonJouerPos.Y, 250, 84);
                Rectangle boutonQuitter = new Rectangle((int)_boutonQuitterPos.X, (int)_boutonQuitterPos.Y, 250, 84);

                Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

                if (mouseRectangle.Intersects(boutonJouer))
                {
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        _jouer = true;
                    }
                }

                if (mouseRectangle.Intersects(boutonQuitter))
                {
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        _quitter = true;
                    }
                }
            }

            if (scene == 5 || scene == 10)
            {

                _previousMouse = _currentMouse;
                _currentMouse = Mouse.GetState();

                Rectangle boutonRejouer = new Rectangle(400, 400, 70, 35);
                Rectangle boutonQuitter = new Rectangle(750, 400, 70, 35);

                Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

                if (mouseRectangle.Intersects(boutonRejouer))
                {
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        scene = 1;
                        pv = 3;
                        pvE1 = 1;
                        pvE2 = 1;
                        pvE3 = 1;
                        pvBoss = 5;
                        _jouer = true;
                    }
                }

                if (mouseRectangle.Intersects(boutonQuitter))
                {
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        _quitter = true;
                    }
                }
            }


            _perso.Play(animation);
            _ennemi1.Play(animationEnnemi1);
            _ennemi2.Play(animationEnnemi2);
            _ennemi3.Play(animationEnnemi3);
            _boss.Play(animationBoss);

            _perso.Update(deltaSeconds);
            _ennemi1.Update(deltaSeconds);
            _ennemi2.Update(deltaSeconds);
            _ennemi3.Update(deltaSeconds);
            _boss.Update(deltaSeconds);

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_ecranAccueil, backgroundPos, Color.White);
            _spriteBatch.Draw(_boutonJouer, _boutonJouerPos, Color.White);
            _spriteBatch.Draw(_boutonQuitter, _boutonQuitterPos, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private bool IsCollision(ushort x, ushort y)
        {
            TiledMapTile? tile;
            bool res;
            if (scene == 2)
            {
                if (map1Layer.TryGetTile(x, y, out tile) == false)
                    return res = false;
                if (!tile.Value.IsBlank)
                    return res = true;
                return res = false;
            }
            else
            {
                if (map2Layer.TryGetTile(x, y, out tile) == false)
                    return res = false;
                if (!tile.Value.IsBlank)
                    return res = true;
                return res = false;
            }
        }
        private void LoadScreen1()
        {
            _graphics.PreferredBackBufferWidth = 1585;
            _graphics.PreferredBackBufferHeight = 430;
            _graphics.ApplyChanges();
            _persoPosition = new Vector2(40, _graphics.GraphicsDevice.Viewport.Height - 150);
            _screenManager.LoadScreen(new Niveau1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen2()
        {
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 676;
            _graphics.ApplyChanges();
            _isOnGround = false;
            _jumping = false;
            _persoPosition = new Vector2(40, _graphics.GraphicsDevice.Viewport.Height / 2);
            _bossPosition = new Vector2(1160, _graphics.GraphicsDevice.Viewport.Height / 2);
            _screenManager.LoadScreen(new Niveau2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreenGameOver()
        {
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 676;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new GameOver(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreenVictory()
        {
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 676;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new Victory(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
    }
}