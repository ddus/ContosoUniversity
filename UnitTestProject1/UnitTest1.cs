using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyTools;

namespace UnitTestProject1
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestMethod1()
    {
      byte[] rights = null;

      Assert.IsFalse(RightsTools.IsRight(rights, 12));
      rights = RightsTools.SetRight(rights, 12, true);
      Assert.IsTrue(RightsTools.IsRight(rights, 12));
      Assert.IsFalse(RightsTools.IsRight(rights, 15));
      Assert.IsFalse(RightsTools.IsRight(rights, 23));
      Assert.IsFalse(RightsTools.IsRight(rights, 27));

      rights = RightsTools.SetRight(rights, 23, true);
      Assert.IsFalse(RightsTools.IsRight(rights, 15));
      Assert.IsTrue(RightsTools.IsRight(rights, 12));
      Assert.IsTrue(RightsTools.IsRight(rights, 23));
      Assert.IsFalse(RightsTools.IsRight(rights, 27));

      rights = RightsTools.SetRight(rights, 12, false);
      Assert.IsFalse(RightsTools.IsRight(rights, 15));
      Assert.IsFalse(RightsTools.IsRight(rights, 12));
      Assert.IsTrue(RightsTools.IsRight(rights, 23));
      Assert.IsFalse(RightsTools.IsRight(rights, 27));

      rights = RightsTools.SetRight(rights, 23, false);
      Assert.IsFalse(RightsTools.IsRight(rights, 15));
      Assert.IsFalse(RightsTools.IsRight(rights, 12));
      Assert.IsFalse(RightsTools.IsRight(rights, 23));
      Assert.IsFalse(RightsTools.IsRight(rights, 27));
    }
  }
}
