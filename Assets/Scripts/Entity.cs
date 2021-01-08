using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Entity : NetworkBehaviour
{
    new public string name;
    public string description;

    public float health;
    public float maxHealth;
}
