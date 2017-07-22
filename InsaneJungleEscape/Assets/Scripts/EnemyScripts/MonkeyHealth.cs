using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyHealth : MonoBehaviour {
    [SerializeField]
    private int Health;

    public int HP
    {
        get
        {
            return Health;
        }
        set
        {
            Health = value;
        }
    }
}
