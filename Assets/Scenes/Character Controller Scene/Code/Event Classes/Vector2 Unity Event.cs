using UnityEngine;
using UnityEngine.Events;

//  This is a variation of the UnityEvent class that accepts a float as an argument
//  This allows us to use these events in the Inspect like normal Unity Events
[System.Serializable]
public class Vector2UnityEvent : UnityEvent<Vector2> { }
