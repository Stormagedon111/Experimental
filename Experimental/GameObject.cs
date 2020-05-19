using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Experimental
{

    class GameObject
    {
        internal Vector2 _Position;

        internal Color _Color;
        internal byte _State;
        internal static List<GameObject> _ActiveObjects; // List of all active objects
        internal static List<GameObject> _VisibleObjects; // List of all visible objects
        internal static List<GameObject> _AllObjects; // List of all objects

        public enum States // All posible general states an object can be in
        {
            Active = 0b_0000_0001, // Active objects are updated each cycle
            Visible = 0b_0000_0010, // Visible objects are drawn each frame
        }

        #region // constructors and deconstructors
        GameObject() // Creates the object and adds it to the list of existing objects
        {
            _AllObjects.Add(this);
        }
        ~GameObject() // Deactivates, Hides, and Destroys the object
        {
            Deactivate();
            Hide();
            _AllObjects.Remove(this);

        }
        #endregion

        #region // Getters and Setters
        public Vector2 Position() { return _Position; } // Get the 2d position vector
        public void Position(Vector2 position) { _Position = position; } //Set the 2d position vector using a Vector2 object
        public void Position(float xPosition, float yPosition) { _Position = new Vector2(xPosition, yPosition); } // Set the 2d position using descrete x and y floats
        public Color Color() { return _Color; } // Get the Color object assigned to the object
        public void Color(Color color) { _Color = color; } // Set the color object
        #endregion

        #region // Methods that edit the state of the object
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
    }
}

