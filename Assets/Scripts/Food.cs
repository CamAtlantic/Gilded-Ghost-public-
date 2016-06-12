using UnityEngine;
using System.Collections;

public class Food : Item {
    Needs needs;
    public float foodHungerValue = 75;

    public override void Awake()
    {
        base.Awake();
        needs = player.GetComponent<Needs>();
    }

    public override void Use()
    {
        base.Use();
        Eat();
    }

    void Eat()
    {
        needs.EatFood(foodHungerValue);
        PickUpItem.Reset();
        Destroy(gameObject);
    }

}
