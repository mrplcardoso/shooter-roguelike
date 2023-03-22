using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Classe que descreve e abstrai quartos das dungeons aleat�rias
 * Esse quartos s�o representados no Unity por GameObjects vazios
 * que cont�m como filhos dois tilemaps
 */
public class DungeonRoom : MonoBehaviour
{
	public List<OpeningRoom> openings;
	public List<DungeonRoom> sidePath;

	public Tilemap[] roomTileMap { get; private set; }

	//GridPosition representa a posi��o do quarto no grid/grafo de quartos
	[SerializeField]
	Vector2Int gridPosition;
	public Vector2Int inGridPosition
	{
		get { return gridPosition; }
		set
		{
			gridPosition = value;
			for (int i = 0; i < openings.Count; ++i)
			{
				openings[i].sideGridPosition = OpeningGridPosition(openings[i].position);
			}
		}
	}

	/*Retorna o n�mero de aberturas, ou seja
	 o n�mero de objetos "OpeningRoom", 
	que possuem isFree verdadeiro*/
	public int numberOfOpenings
	{
		get { return openings.FindAll(x => x.isFree).Count; }
	}

	public int enemyCapacity;

	private void Awake()
	{
		UpdateOpeningsPosition();
		roomTileMap = GetComponentsInChildren<Tilemap>();
		enemyCapacity = 2;
	}

	public void UpdateOpeningsPosition()
	{
		for (int i = 0; i < openings.Count; ++i)
		{
			openings[i].position = OpeningPosition(openings[i].side);
		}
	}

	//Retorna a posi��o do mundo de uma abertura,
	//a partir do lado que representa a abertura
	/*F�rmula: posi��o do quarto + lado * tamanho de um tile * quantidade
	 de tiles que o quarto ocupa no eixo
	Essa formula descreve cada eixo
	*/
	public Vector3 OpeningPosition(Vector2Int openingSide)
	{
		return transform.position +
			new Vector3(openingSide.x * DungeonLevel.gridCellSize * DungeonLevel.tileCells.x,
			openingSide.y * DungeonLevel.gridCellSize * DungeonLevel.tileCells.y);
	}

	//Retorna a posi��o do mundo de uma abertura,
	//a partir do seu �ndice na lista de aberturas
	public Vector3 OpeningPosition(int index)
	{
		return transform.position +
			new Vector3(openings[index].side.x * DungeonLevel.gridCellSize * DungeonLevel.tileCells.x,
			openings[index].side.y * DungeonLevel.gridCellSize * DungeonLevel.tileCells.y);
	}

	//Retorna a posi��o de grid de uma abertura, a partir de sua posi��o no mundo
	public Vector2Int OpeningGridPosition(Vector3 openingPosition)
	{
		return gridPosition +
			Vector2Int.FloorToInt((openingPosition - transform.position).normalized);
	}

	//Retorna a posi��o de grid de uma abertura, a partir de seu �ndice na lista de aberturas
	public Vector2Int OpeningGridPosition(int index)
	{
		return gridPosition +
			Vector2Int.FloorToInt((openings[index].position - transform.position).normalized);
	}
}
