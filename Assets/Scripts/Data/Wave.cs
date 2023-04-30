using System.Collections.Generic;

public class Wave
{
    public PizzaType[] Delivery { get; set; }
    public float Time { get; set; }

    public Wave(PizzaType[] delivery, float time)
    {
        Delivery = delivery;
        Time = time;
    }
}
