using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Experimental
{

    class GameObject
    {
        public enum States // All posible general states an object can be in
        {
            Active = 0b_0000_0001, // Active objects are updated each cycle
            Visible = 0b_0000_0010, // Visible objects are drawn each frame
        }

        #region Member Variables
        internal Texture2D _Texture; // Object Texture
        internal Vector2 _Position; // 2d Position Vector
        internal Color _Color; // Color object
        internal byte _State; // State flags
        internal static List<GameObject> _ActiveObjects; // List of all active objects
        internal static List<GameObject> _VisibleObjects; // List of all visible objects
        internal static List<GameObject> _AllObjects; // List of all objects
        #endregion

        #region Getters and Setters
        public Vector2 Position { get { return _Position; } set { _Position = value; } } // 2d Position vector
        public Color Color { get { return _Color; } set { _Color = value; } } // Color object
        public Texture2D Texture { get { return _Texture; } set { _Texture = value; } } //Texture object
        #endregion

        #region Constructors and Deconstructors
        public GameObject(bool startActive = false, bool startVisible = false) // Creates the object and adds it to the list of existing objects
        {
            _ActiveObjects = new List<GameObject> { };
            _VisibleObjects = new List<GameObject> { };
            _AllObjects = new List<GameObject> { };

            if (startActive) { Activate(); }
            if (startVisible) { Show(); }
            _AllObjects.Add(this);
        }
        ~GameObject() // Deactivates, Hides, and Destroys the object
        {
            Deactivate();
            Hide();
            _AllObjects.Remove(this);

        }
        #endregion

        #region Methods that edit state
        public void Activate() // Adds object to the list of active objects
        {
            if (_ChangeState(States.Active, true))
            {
                _ActiveObjects.Add(this);
            }
        } 
        public void Deactivate() // Removes object from the list of active objects 
        {
            if (_ChangeState(States.Active, false))
            {
                _ActiveObjects.Remove(this);
            }
        }
        public void Show() // Adds object to the list of Visible objects
        {
            if (_ChangeState(States.Visible, true))
            {
                _VisibleObjects.Add(this);
            }
        }
        public void Hide() // Removes the object from the list of Visible objects
        {
            if (_ChangeState(States.Visible, true))
            {
                _VisibleObjects.Remove(this);
            }
        }
        private bool _ChangeState(States stateToChange, bool valueToSet) // changes stateToChange to valueToSet. Returns true if successful, false if redundent
        {
            if (Convert.ToBoolean(_State & (byte)stateToChange) != valueToSet)
            {
                _State ^= (byte)stateToChange;
                return true;
            }
            return false;
        }
        #endregion

        #region Member Methods
        public static int UpdateActive(float dt) // Updates all active objects
        {
            foreach (GameObject item in _ActiveObjects)
            {
                item.Update(dt);
            }
            return _ActiveObjects.Count;
        }
        internal virtual void Update(float dt) { } // Update code
        public static int DrawVisible(SpriteBatch spriteBatchToDrawTo) 
        {
            foreach (GameObject item in _VisibleObjects)
            {
                item.Draw(spriteBatchToDrawTo);
            }
            return _VisibleObjects.Count;
        }
        internal virtual void Draw(SpriteBatch spriteBatchToDrawTo) { }
        #endregion

    }
}

