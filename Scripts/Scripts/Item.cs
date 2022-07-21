using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, coin, Grenade, Heart, Weapon };
    public Type type;
    public int value;
    
}
