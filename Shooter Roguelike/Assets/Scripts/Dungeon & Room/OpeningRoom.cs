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
	//Nome da abertura (se � abertura esquerda ou direita, por exemplo)
	public string openingName;
	//Posica��o da abertura no mapa
	//representa o local aonde um quarto, cadidato a conectar com essa abertura,
	//deve ser posicionado
	public Vector3 position;
	//Indica o lado dessa abertura
	//se esta a esquerda do centro do quarto, por exemplo,
	//seu valor ser� (-1, 0)
	public Vector2Int side;
	//Posic�o no grid da abertura e do quarto que ser� conectado com ela
	public Vector2Int sideGridPosition;
	//Indica se essa abertura est� livre ou ja foi conectada/fechada
	public bool isFree;
}
