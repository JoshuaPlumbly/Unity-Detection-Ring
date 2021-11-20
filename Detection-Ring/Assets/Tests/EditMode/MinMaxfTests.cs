using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MinMaxfTests
{
    [Test]
    public void ConstructorWithPositive()
    {
        var minMaxf = new MinMaxf(1);
        Assert.AreEqual(minMaxf.Max, 1);
        Assert.AreEqual(minMaxf.Min, 1);
        Assert.AreEqual(minMaxf.GetRange(), 0);
    }

    [Test]
    public void ConstructorWithNegative()
    {
        var minMaxf = new MinMaxf(-1);
        Assert.AreEqual(minMaxf.Max, -1);
        Assert.AreEqual(minMaxf.Min, -1);
        Assert.AreEqual(minMaxf.GetRange(), 0);
    }

    [Test]
    public void ConstructorWithZero()
    {
        var minMaxf = new MinMaxf(0);
        Assert.AreEqual(minMaxf.Max, 0);
        Assert.AreEqual(minMaxf.Min, 0);
        Assert.AreEqual(minMaxf.GetRange(), 0);
    }

    [Test]
    public void Expand()
    {
        var minMaxf = new MinMaxf(float.NaN);
        minMaxf.Expand(1);
        minMaxf.Expand(-1);
        Assert.AreEqual(minMaxf.Max, 1);
        Assert.AreEqual(minMaxf.Min, -1);
        Assert.AreEqual(minMaxf.GetRange(), 2);
    }
}
