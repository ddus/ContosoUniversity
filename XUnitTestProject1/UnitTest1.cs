using System;
using Xunit;

using MyUtils;

namespace XUnitTestProject1
{
  public class UnitTest1
  {
    [Fact]
    public void Test1()
    {
      byte[] rights = null;
      byte[] newRights = RightsTools.SetRight(rights, 12, true);
    }
  }
}
