using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTools
{
  public static class RightsTools
  {
    public static bool IsRight(byte[] rights, int index)
    {
      if (rights == null)
        return false;
      int byteIndex = index / 8;
      if (rights.Length <= byteIndex)
        return false;
      int bitIndex = index % 8;
      byte byt = rights[byteIndex];
      return (byt & (1 << bitIndex)) > 0;
    }

    public static byte[] SetRight(byte[] rights, int index, bool flag)
    {
      int byteIndex = index / 8;
      int bitIndex = index % 8;
      int rightLength = rights == null ? 0 : rights.Length;
      byte[] newRights = new byte[Math.Max(rightLength, byteIndex + 1)];
      if (rightLength > 0)
      {
        Array.Copy(rights, newRights, rightLength);
      }
      byte byt = newRights[byteIndex];
      byte newByt = (byte)(1 << bitIndex);
      byt = (byte)(flag ? byt | newByt : byt ^ newByt);
      newRights[byteIndex] = byt;
      return newRights;
    }
  }
}
