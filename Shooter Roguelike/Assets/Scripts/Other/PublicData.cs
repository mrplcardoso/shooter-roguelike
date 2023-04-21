using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Random;

public static class PublicData
{
	public const int initialMainPath = 5;
	public const int maxMainPath = 100;
	public const int numberOfSideRooms = 1;
	public const int depthOfSidePaths = 1;
	public const int minLevelSpawns = 5;

	public static RandomStream randomStream { get; private set; }
	public static int currentMainPath { get; private set; } = 20;
	public static int currentLevel { get; private set; }

	public static int enemiesPerLevel { get { return (minLevelSpawns * currentLevel) + currentMainPath; } }
	public static int itensPerLevel { get { return 1 + (int)(enemiesPerLevel * 0.2f); } }

	public static int totalSeconds = 7500;
	public static int totalShoots = 100;
	public static int totalItens = 200;
	public static int seedUsed = 654; 

	public static void Initialize(int seed)
	{
		randomStream = new RandomStream(seed);
		currentMainPath = initialMainPath;
		currentLevel = 1;

		totalSeconds = (int)Time.realtimeSinceStartup;
		totalShoots = 0;
		totalItens = 0;
		seedUsed = seed;
	}

	public static void AddLevel()
	{
		currentLevel++;
		currentMainPath += initialMainPath;
	}
}
