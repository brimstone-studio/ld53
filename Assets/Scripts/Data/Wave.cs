using System.Collections.Generic;

public class Wave
{
    public DeliveryPizza[] Delivery { get; set; }
    public float Time { get; set; }

    public Wave(PizzaType[] delivery, float time)
    {
        Delivery = new DeliveryPizza[delivery.Length];
        for (var i = 0; i < delivery.Length; i++)
        {
            Delivery[i] = new DeliveryPizza() { Type = delivery[i], Delivered = false, PickedUp = false};
        }
        Time = time;
    }
}

public class DeliveryPizza
{
    public PizzaType Type { get; set; }
    public bool PickedUp { get; set; }
    public bool Delivered { get; set; }
}