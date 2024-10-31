using UnityEngine;

namespace SaveAndLoad
{
  public class SerializableVector3
  {
    public float x;
    public float y;
    public float z;


    public SerializableVector3()
    {
      x = 0;
      y = 0;
      z = 0;
    }

    public SerializableVector3(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public SerializableVector3(Vector3 vector)
    {
      x = vector.x;
      y = vector.y;
      z = vector.z;
    }

    public Vector3 ToVector3()
    {
      return new Vector3(x, y, z);
    }
  }
}