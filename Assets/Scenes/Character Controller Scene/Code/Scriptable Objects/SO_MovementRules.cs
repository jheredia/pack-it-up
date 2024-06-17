using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data/Movement Rules", order = 1)]
public class SO_MovementRules : ScriptableObject
{
    public float topSpeed = 10;
    public float acceleration = 1f;
    public float momentum = 0f;
}
