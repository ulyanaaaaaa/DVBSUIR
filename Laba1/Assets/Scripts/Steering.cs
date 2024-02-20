using UnityEngine;

public class Steering
{
   public float Angular; //поворот
   public Vector3 Linear; //перемещение

   public Steering()
   {
      Angular = 0;
      Linear = new Vector3();
   }
}
