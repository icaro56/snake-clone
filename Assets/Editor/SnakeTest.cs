using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class SnakeTest 
{
    [Test]
    public void IncreaseSpeedTest()
    {
        var gObject = new GameObject();
        gObject.AddComponent<Snake>();
        var snake = gObject.GetComponent<Snake>();

        var speed = snake.speed;
        var waitTime = snake.waitTime;
   
        snake.increaseSpeed();

        Assert.AreEqual(snake.speed, speed * 1.05f);
        Assert.AreNotEqual(speed, snake.speed);

        float newWaitTime = (speed * waitTime) / snake.speed;

        Assert.AreEqual(snake.waitTime, newWaitTime);
        Assert.AreNotEqual(waitTime, snake.waitTime);
    }
}
