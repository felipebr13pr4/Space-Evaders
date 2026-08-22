using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyPattern
{
    public List<Movement> Movements = new List<Movement>();
}

[Serializable]
public class Movement
{
    public Vector2 Direction;
    public int RepeatAmount;
}