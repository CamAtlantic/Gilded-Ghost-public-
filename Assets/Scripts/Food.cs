using UnityEngine;
using System.Collections;

public class Food : Item {

    public float foodHungerValue = 75;

    public override void Use()
    {
        base.Use();
        Eat();
    }

    void Eat()
    {
        Needs.EatFood(foodHungerValue);
        PickUpItem.Reset();
        Destroy(gameObject);
    }

}
