using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

/**
* Script for holding AI configuration. maxTime is ressponsible for updating the enemy, maxDistance for maximal distance of enemy from player
**/
public class AiAgentConfig : ScriptableObject
{
    //maximal time when to update the position of the player
    public float maxTime = 1.0f;
    //maximal distance how close an enemy can come near the player
    public float maxDistance = 1.0f;
}
