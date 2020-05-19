using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Experimental
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        internal GraphicsDeviceManager _Graphics;
        internal SpriteBatch _SpriteBatch;
        private Ball _Ball;
        private Random _Rand = new Random();
        private int _WindowHeight;
        private int _WindowWidth;


        public Game1()
        {
            _Graphics = new GraphicsDeviceManager(this);
            _WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            _WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            _Graphics.PreferredBackBufferWidth = _WindowWidth;
            _Graphics.PreferredBackBufferHeight = _WindowHeight;
            _Graphics.ApplyChanges();
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Ball.Texture = Content.Load<Texture2D>("ball");
            _Ball = new Ball();
            _Ball.Position = new Vector2(_Ball.Radius, _Ball.Radius);
            _Ball.Velocity = new Vector2(15f, 0f);
            _Ball.Acceleration = new Vector2(0f, 9.8f);
            _Ball.Color = Color.White;
                


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
             
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            GameObject.UpdateActive(.2f);

            if ((_Ball.Position.Y + _Ball.Radius) > _WindowHeight) { _Ball.Position = new Vector2(_Ball.Position.X, _WindowHeight - _Ball.Radius); _Ball.BounceUp(); }
            if ((_Ball.Position.Y - _Ball.Radius) < 0) { _Ball.Position = new Vector2(_Ball.Position.X, 0); _Ball.BounceDown(); } 
            if ((_Ball.Position.X + _Ball.Radius) > _WindowWidth) _Ball.BounceLeft();
            if ((_Ball.Position.X - _Ball.Radius) < 0) _Ball.BounceRight();

            


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _SpriteBatch.Begin();
            GameObject.DrawVisible(_SpriteBatch);
            _SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
