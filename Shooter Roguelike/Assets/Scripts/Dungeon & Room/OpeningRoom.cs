using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*A classe OpeningRoom define e abstrai a abertura
 * de um quarto (DungeonRoom)
 */
[Serializable]
public class OpeningRoom
{
	//Nome da abertura (se é abertura esquerda ou direita, por exemplo)
	public string openingName;
	//Posicação da abertura no mapa
	//representa o local aonde um quarto, cadidato a conectar com essa abertura,
	//deve ser posicionado
	public Vector3 position;
	//Indica o lado dessa abertura
	//se esta a esquerda do centro do quarto, por exemplo,
	//seu valor será (-1, 0)
	public Vector2Int side;
	//Posicão no grid da abertura e do quarto que será conectado com ela
	public Vector2Int sideGridPosition;
	//Indica se essa abertura está livre ou ja foi conectada/fechada
	public bool isFree;
}
