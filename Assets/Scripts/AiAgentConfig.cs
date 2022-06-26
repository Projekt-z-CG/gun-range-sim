using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

/**
* Script for holding AI configuration. maxTime is ressponsible for updating the enemy, maxDistance for maximal distance of enemy from player
**/
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
}
