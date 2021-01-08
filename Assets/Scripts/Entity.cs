using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Entity : NetworkBehaviour
{
    new public string name = "Entity";
    public string description;
    public float maxHealth;
    public float health;
}
