using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Random;

public static class PublicData
{
	public static RandomStream randomStream { get; private set; }
	public static int numberOfMainRooms = 10;
	public static int numberOfSideRooms = 1;
	public static int depthOfSidePaths = 1;
	public static int currentLevel = 1;

	public static void Initialize(int seed)
	{
		randomStream = new RandomStream(seed);
	}

	public static void AddLevel()
	{
		currentLevel++;
		
	}
}
