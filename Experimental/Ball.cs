using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Experimental
{
    class Ball : GameObject
    {
        #region Member Variables
        internal static new Texture2D _Texture;
        private Vector2 _Velocity;
        private Vector2 _Acceleration;
        private Vector2 _GraphicsOffset;
        public float Radius;
        #endregion

        #region Getters and Setters
        public static Texture2D Texture { get { return _Texture; } set { _Texture = value; } }
        public Vector2 Velocity { get { return _Velocity; } set { _Velocity = value; } }
        public Vector2 Acceleration { get { return _Acceleration; } set { _Acceleration = value; } }
        #endregion

        #region Constructors and Deconstructors
        public Ball(bool startActive = true, bool startVisible = true) : base(startActive, startVisible)
        {
            Radius = _Texture.Width / 2;
            _GraphicsOffset = new Vector2(Radius, Radius);
        }
        #endregion

        #region Member Methods
        internal override void Update(float dt)
        {
            _Velocity += _Acceleration * dt;
            _Position += _Velocity * dt;
        }
        internal override void Draw(SpriteBatch spriteBatchToDrawTo)
        {
            spriteBatchToDrawTo.Draw(_Texture, _Position - _GraphicsOffset , _Color);  
        }
        public void BounceUp() { _Velocity.Y = -Math.Abs(.8f * _Velocity.Y); }
        public void BounceDown() { _Velocity.Y = Math.Abs(.8f * _Velocity.Y); }
        public void BounceLeft() { _Velocity.X = -Math.Abs(.6f * _Velocity.X); }
        public void BounceRight() { _Velocity.X = Math.Abs(.6f * _Velocity.X); }

        #endregion
    }
}
