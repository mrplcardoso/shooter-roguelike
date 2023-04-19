using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.EventCommunication;
using Utility.Random;

public class DungeonLevel : MonoBehaviour
{
	public static DungeonLevel dungeon {  get; private set; }

	//Grid criado por c�digo
	//Guarda o GameObject Grid que mant�m os quartos
	[SerializeField]
	Grid levelGrid;
	//n�mero de c�lulas (tiles) que um quarto ocupado em cada eixo
	//ou seja, o tamanho de um quarto.
	[SerializeField]
	Vector2Int roomCells;
	public static Vector2Int tileCells { get; private set; }
	//Guarda metaade do tamanho de um quarto
	public static Vector2Int halfTileCell { get; private set; }
	//Guarda o tamanho de cada tile do grid
	[SerializeField]
	float cellSize;
	public static float gridCellSize { get; private set; }

	public List<DungeonRoom> roomPrefabs;
	//Lista de todos os quartos do mapa
	[SerializeField]
	List<DungeonRoom> level;
	//Lista dos quartos do caminho principal
	[SerializeField]
	List<DungeonRoom> mainPath;
	//Lista de caminhos alternativos
	[SerializeField]
	List<List<DungeonRoom>> sidePaths;
	//Vari�vel que guarda o tamanho do caminho principal
	//Definido pelo Inspector
	[SerializeField]
	int numberOfMainRooms;
	[SerializeField]
	int numberOfSideRooms;
	[SerializeField]
	int depthOfSidePaths;

	public RandomStream randomStream
	{ get; private set; }
	[SerializeField]
	int seed;

	const int limitTries = 100;

	void Awake()
	{
		DungeonLevel[] d = FindObjectsOfType<DungeonLevel>();
		for (int i = 0; i < d.Length; i++)
		{
			if (d[i] != this)
			{ Destroy(this); return; }
		}
		dungeon = this;
	}

	private void Start()
	{
		CreateRandomStream();
		CreateGrid();
		level = new List<DungeonRoom>();
		MainPath();
		DepthPath();

		StartCoroutine(PublishGeneration());
	}

	IEnumerator PublishGeneration()
	{
		yield return null;
		CloseRooms();
		yield return null;
		EventHub.Publish(EventList.RoomGenerationCompleted);
	}

	void MainPath()
	{
		int tries = 0;
		while (mainPath == null || mainPath.Count != numberOfMainRooms)
		{
			ClearPath(mainPath);
			mainPath = ExpandRoom(numberOfMainRooms);
			tries++;
			if (tries >= limitTries)
			{ print("limite excedido"); break; }
		}
		for (int i = 0; i < mainPath.Count; ++i)
		{ level.Add(mainPath[i]); }
	}

	void DepthPath()
	{
		sidePaths = new List<List<DungeonRoom>>();
		sidePaths.Add(mainPath);
		for (int depth = 0; depth < depthOfSidePaths; ++depth)
		{
			sidePaths.Add(new List<DungeonRoom>());
			SidePath(sidePaths[depth], sidePaths[depth + 1]);
		}
	}

	void SidePath(List<DungeonRoom> path, List<DungeonRoom> nextPath)
	{
		for (int i = 0; i < path.Count - 1; ++i)
		{
			if (path[i].numberOfOpenings < 1)
			{ continue; }

			TrySidePath(path[i]);
			for (int j = 0; j < path[i].sidePath.Count; ++j)
			{
				level.Add(path[i].sidePath[j]);
				nextPath.Add(path[i].sidePath[j]);
			}
		}
	}

	void TrySidePath(DungeonRoom room)
	{
		int tries = 0;
		while (room.sidePath == null || room.sidePath.Count != numberOfSideRooms)
		{
			ClearPath(room.sidePath);
			room.sidePath = ExpandPath(room, numberOfSideRooms);
			++tries;
			if (tries >= limitTries)
			{ break; }
		}
	}

	List<DungeonRoom> ExpandRoom(int numberOfRooms)
	{
		DungeonRoom tile = Instantiate(roomPrefabs[randomStream.nextInt(0, roomPrefabs.Count)],
			levelGrid.transform);
		tile.inGridPosition = new Vector2Int(0, 0);
		numberOfRooms--;

		return ExpandPath(tile, numberOfRooms);
	}

	List<DungeonRoom> ExpandPath(DungeonRoom start, int numberOfExpansion)
	{
		List<DungeonRoom> pathList = new List<DungeonRoom>();
		if (!level.Contains(start))
		{ pathList.Add(start); }
		DungeonRoom tile = start;
		DungeonRoom tempTile;
		int randomSide;
		int randomRoom;
		int tries = 0;

		for (int i = 0; i < numberOfExpansion; ++i)
		{
			randomSide = RandomFreeOpening(tile);

			if (!FreeGridPosition(tile.openings[randomSide].sideGridPosition, pathList))
			{
				++tries;
				if (tries >= limitTries)
				{ return pathList; }
				--i;
				continue;
			}

			randomRoom = randomStream.nextInt(0, roomPrefabs.Count);
			tries = 0;
			while (!IsValidRoom(tile.openings[randomSide], randomRoom, pathList))
			{
				tries++;
				if (tries >= limitTries)
				{ return pathList; }
				randomRoom = randomStream.nextInt(0, roomPrefabs.Count);
			}

			tempTile = CreateRoom(tile.openings[randomSide], randomRoom);
			tile.openings[randomSide].isFree = false;
			tile = tempTile;
			pathList.Add(tile);
		}
		return pathList;
	}

	DungeonRoom CreateRoom(OpeningRoom opening, int prefabIndex)
	{
		DungeonRoom t = Instantiate(roomPrefabs[prefabIndex], opening.position,
				Quaternion.identity, levelGrid.transform);
		t.openings.Find(x => x.side == (opening.side * -1)).isFree = false;
		t.inGridPosition = opening.sideGridPosition;
		return t;
	}

	//Sorteia uma abertura v�lida (livre)
	int RandomFreeOpening(DungeonRoom room)
	{
		if (room.openings.Find(x => x.isFree) == null)
		{ return 0; }
		int randomSide = randomStream.nextInt(0, room.openings.Count);
		while (!room.openings[randomSide].isFree)
		{
			randomSide = randomStream.nextInt(0, room.openings.Count);
		}
		return randomSide;
	}

	//Verifica se o quarto sorteado � v�lido
	bool IsValidRoom(OpeningRoom backOpening, int randomRoom, List<DungeonRoom> tempList)
	{
		bool valid = false;
		DungeonRoom tempRoom = Instantiate(roomPrefabs[randomRoom],
			backOpening.position, Quaternion.identity, levelGrid.transform);

		//Se suas aberturas possuem espa�o para parear com novos quartos...
		if (tempRoom.numberOfOpenings > 1)
		{
			//Se possui abertura para o quarto anterior...
			if (HasConnection(tempRoom.openings, backOpening.side))
			{
				tempRoom.inGridPosition = backOpening.sideGridPosition;
				//Se o quarto sorteado N�O se encontra na posi��o de um quarto ja existente
				if (FreeGridPosition(tempRoom.openings, tempList))
				{
					valid = true;
				}
			}
		}
		Destroy(tempRoom.gameObject);
		return valid;
	}

	//Verifica se existe alguma abertura na lista que seja conect�vel com uma outra abertura
	bool HasConnection(List<OpeningRoom> openings, Vector2Int side)
	{
		for (int i = 0; i < openings.Count; ++i)
		{
			if (!openings[i].isFree)
				continue;
			if (openings[i].side == (side * -1))
			{
				return true;
			}
		}
		return false;
	}

	//Verfica se j� existe uma sala na mesma posi��o de grid das aberturas do par�metro
	bool FreeGridPosition(List<OpeningRoom> openings, List<DungeonRoom> tempList)
	{
		int n = 0;
		for (int i = 0; i < openings.Count; ++i)
		{
			if (!openings[i].isFree)
				continue;
			if (level.Find(x => x.inGridPosition == openings[i].sideGridPosition) != null ||
				tempList.Find(x => x.inGridPosition == openings[i].sideGridPosition) != null)
			{
				++n;
			}
		}
		if (n > 1)
			return false;
		return true;
	}

	//Verfica se j� existe uma sala na mesma posi��o de grid do par�metro
	bool FreeGridPosition(Vector2Int openingGridPosition, List<DungeonRoom> tempList)
	{
		if (level.Find(x => x.inGridPosition == openingGridPosition) != null ||
			tempList.Find(x => x.inGridPosition == openingGridPosition) != null)
		{
			return false;
		}
		return true;
	}

	void CreateGrid()
	{
		GameObject g = new GameObject("Grid Level");
		levelGrid = g.AddComponent<Grid>();
		levelGrid.transform.position = Vector3.forward * 7;
		levelGrid.cellSize = Vector2.one * cellSize;

		gridCellSize = cellSize;
		tileCells = roomCells;
		halfTileCell = roomCells / 2;

		numberOfMainRooms = PublicData.currentMainPath;
		numberOfSideRooms = PublicData.numberOfSideRooms;
		depthOfSidePaths = PublicData.depthOfSidePaths;
	}

	void CreateRandomStream()
	{
		if(PublicData.randomStream == null)
		{ PublicData.Initialize(seed); }

		randomStream = PublicData.randomStream;
		seed = randomStream.seed;
	}

	void ClearPath(List<DungeonRoom> path, bool nullOnly = false)
	{
		if (path == null)
			return;

		while (path.Count > 0)
		{
			if (path[path.Count - 1] != null && !nullOnly)
			{
				Destroy(path[path.Count - 1].gameObject);
			}
			path.RemoveAt(path.Count - 1);
		}
		path.Clear();
	}

	void CloseRooms()
	{
		Vector2Int side;

		for (int i = 0; i < level.Count; ++i)
		{
			for (int j = 0; j < level[i].openings.Count; ++j)
			{
				if (MustCloseOpening(level[i].openings[j]))
				{
					side = level[i].openings[j].side;
					if(side.x == 1)
					{
						//Asphalt
						Vector3Int topNeighbour = new Vector3Int(halfTileCell.x - 2, 1);
						Vector3Int bottomNeighbour = new Vector3Int(halfTileCell.x - 2, -2);
						SetTile(bottomNeighbour, level[i], 0, new Vector3Int(halfTileCell.x - 2, 0));
						SetTile(topNeighbour, level[i], 0, new Vector3Int(halfTileCell.x - 2, -1));

						//Water
						topNeighbour = new Vector3Int(halfTileCell.x - 1, 1);
						bottomNeighbour = new Vector3Int(halfTileCell.x - 1, -2);
						SetTile(bottomNeighbour, level[i], 1, new Vector3Int(halfTileCell.x - 1, 0));
						SetTile(topNeighbour, level[i], 1, new Vector3Int(halfTileCell.x - 1, -1));
					}
					if(side.x == -1)
					{
						//Asphalt
						Vector3Int topNeighbour = new Vector3Int(-halfTileCell.x + 1, 1);
						Vector3Int bottomNeighbour = new Vector3Int(-halfTileCell.x + 1, -2);
						SetTile(bottomNeighbour, level[i], 0, new Vector3Int(-halfTileCell.x + 1, 0));
						SetTile(topNeighbour, level[i], 0, new Vector3Int(-halfTileCell.x + 1, -1));

						//Water
						topNeighbour = new Vector3Int(-halfTileCell.x, 1);
						bottomNeighbour = new Vector3Int(-halfTileCell.x, -2);
						SetTile(bottomNeighbour, level[i], 1, new Vector3Int(-halfTileCell.x, 0));
						SetTile(topNeighbour, level[i], 1, new Vector3Int(-halfTileCell.x, -1));
					}
					if(side.y == 1)
					{
						//Asphalt
						Vector3Int topNeighbour = new Vector3Int(1, halfTileCell.y - 2);
						Vector3Int bottomNeighbour = new Vector3Int(-2, halfTileCell.y - 2);
						SetTile(bottomNeighbour, level[i], 0, new Vector3Int(0, halfTileCell.y - 2));
						SetTile(topNeighbour, level[i], 0, new Vector3Int(-1, halfTileCell.y - 2));

						//Water
						topNeighbour = new Vector3Int(1, halfTileCell.y - 1);
						bottomNeighbour = new Vector3Int(-2, halfTileCell.y - 1);
						SetTile(bottomNeighbour, level[i], 1, new Vector3Int(0, halfTileCell.y - 1));
						SetTile(topNeighbour, level[i], 1, new Vector3Int(-1, halfTileCell.y - 1));
					}
					if(side.y == -1)
					{
						//Asphalt
						Vector3Int topNeighbour = new Vector3Int(1, -halfTileCell.y + 1);
						Vector3Int bottomNeighbour = new Vector3Int(-2, -halfTileCell.y + 1);
						SetTile(bottomNeighbour, level[i], 0, new Vector3Int(0, -halfTileCell.y + 1));
						SetTile(topNeighbour, level[i], 0, new Vector3Int(-1, -halfTileCell.y + 1));

						//Water
						topNeighbour = new Vector3Int(1, -halfTileCell.y);
						bottomNeighbour = new Vector3Int(-2, -halfTileCell.y);
						SetTile(bottomNeighbour, level[i], 1, new Vector3Int(0, -halfTileCell.y));
						SetTile(topNeighbour, level[i], 1, new Vector3Int(-1, -halfTileCell.y));
					}
				}
			}
		}
	}

	void SetTile(Vector3Int neighbour, DungeonRoom level, int mapIndex, Vector3Int newPosition)
	{
		Sprite sprite = level.roomTileMap[mapIndex].GetTile<UnityEngine.Tilemaps.Tile>(neighbour).sprite;
		Tile newTile = ScriptableObject.CreateInstance<Tile>();
		newTile.sprite = sprite;
		level.roomTileMap[mapIndex].SetTile(newPosition, newTile);
	}

	bool MustCloseOpening(OpeningRoom opening)
	{
		if (opening.isFree || (!opening.isFree &&
					level.Find(x => x.inGridPosition == opening.sideGridPosition) == null))
		{
			return true;
		}
		return false;
	}

	public Vector2 RandomRoomPosition()
	{
		DungeonRoom room = level[RandomStream.NextInt(0, level.Count)];
		if(room.enemyCapacity == 0)
		{ return Vector2.down * 5000; }
		room.enemyCapacity--;
		return room.transform.position;
	}
}
