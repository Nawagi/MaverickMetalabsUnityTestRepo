//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class FibonacciSequenceTests
{
    [Test]
    public void CheckFibonacciSequencePerRound()
    {
        GameObject gameObject = new GameObject();
        RoundManager roundManager = gameObject.AddComponent<RoundManager>();

        List<int> fibonacciSequenceList = new List<int>();

        #region fibonacciSequenceList items

        fibonacciSequenceList.Add(1);
        fibonacciSequenceList.Add(1);
        fibonacciSequenceList.Add(2);
        fibonacciSequenceList.Add(3);
        fibonacciSequenceList.Add(5);
        fibonacciSequenceList.Add(8);
        fibonacciSequenceList.Add(13);
        fibonacciSequenceList.Add(21);
        fibonacciSequenceList.Add(34);
        fibonacciSequenceList.Add(55);
        fibonacciSequenceList.Add(89);
        fibonacciSequenceList.Add(144);
        fibonacciSequenceList.Add(233);
        fibonacciSequenceList.Add(377);
        fibonacciSequenceList.Add(610);
        fibonacciSequenceList.Add(987);
        fibonacciSequenceList.Add(1597);
        fibonacciSequenceList.Add(2584);
        fibonacciSequenceList.Add(4181);
        fibonacciSequenceList.Add(6765);
        fibonacciSequenceList.Add(10946);
        fibonacciSequenceList.Add(17711);
        fibonacciSequenceList.Add(28657);
        fibonacciSequenceList.Add(46368);
        fibonacciSequenceList.Add(75025);
        fibonacciSequenceList.Add(121393);
        fibonacciSequenceList.Add(196418);

        #endregion fibonacciSequenceList items

        int currentFibonacciSequence;

        for (int i = 0; i < fibonacciSequenceList.Count; i++)
        {
            currentFibonacciSequence = roundManager.CheckFibonacciSequencePerRound();
            Assert.AreEqual(fibonacciSequenceList[i], currentFibonacciSequence);
            Debug.Log("Round " + (i + 1) + " - Expected: " + fibonacciSequenceList[i] + ";  Actual: " + currentFibonacciSequence);
        }
    }
}