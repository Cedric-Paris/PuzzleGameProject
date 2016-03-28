using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomElementOnMapGenerator {

	private enum ElementName{
		Vide,
		Chemin,
		Orientation,
		UpArrow,
		DownArrow,
		LeftArrow,
		RightArrow,
		Obstacle,
		Objectif,
		Start,
		End
	}

	private int nbCheminNul = 0;
	private List<Point2D> currentChemin;
	private bool cheminSansObjectif;
	private List<ElementName> listActions = new List<ElementName>();

	public Dictionary<string, int> getTabActionsPossible()
	{
		Dictionary<string, int> actions = new Dictionary<string , int>();
		actions.Add ("UpArrow", 0);
		actions.Add ("DownArrow", 0);
		actions.Add ("LeftArrow", 0);
		actions.Add ("RightArrow", 0);
		foreach(ElementName element in listActions)
		{
			switch(element)
			{
			case ElementName.UpArrow:
				actions["UpArrow"] += 1; 
				break;
			case ElementName.DownArrow:
				actions["DownArrow"] += 1; 
				break;
			case ElementName.LeftArrow:
				actions["LeftArrow"] += 1; 
				break;
			case ElementName.RightArrow:
				actions["RightArrow"] += 1; 
				break;
			}
		}
		Debug.Log ("Up:" +actions["UpArrow"]);
		Debug.Log ("Down:" + actions["DownArrow"]);
		return actions;
	}

	private int[,] ConvertTabInt(ElementName[,] tab, int hauteur, int largeur)
	{
		int[,] newTab = new int[hauteur, largeur];
		for (int i = 0; i<hauteur; i++)
		{
			for(int j=0; j<largeur; j++)
			{
				switch(tab[i,j])
				{
				case ElementName.Vide:
					newTab[i,j] = -1;
					break;
				case ElementName.Orientation:
					newTab[i,j] = -1;//-1
					break;
				case ElementName.Chemin:
					newTab[i,j] = -4;//-1
					break;
				case ElementName.Start:
					newTab[i,j] = 0;
					break;
				case ElementName.End:
					newTab[i,j] = 1;
					break;
				case ElementName.Obstacle:
					newTab[i,j] = 2;
					break;
				case ElementName.Objectif:
					newTab[i,j] = 3;
					break;
				default:
					newTab[i,j] = -1;
					break;
				}
			}
		}
		return newTab;
	}

	public int[,] GenerateRandom(int hauteur, int largeur, int nbChangeDir)
	{
		ElementName[,] tab = new ElementName[hauteur, largeur];
		Point2D debut = GenererCaseStartEnd(tab, hauteur, largeur);
		tab[debut.y, debut.x] = ElementName.Start;
		GenererCheminAvecObjectif(tab, debut, nbChangeDir, hauteur, largeur);
		GenererObstacle(tab, (int)((hauteur*largeur*15)/100), hauteur, largeur);
		return ConvertTabInt (tab, hauteur, largeur);
	}

	private void GenererCheminAvecObjectif(ElementName[,] tab, Point2D posDepart, int nbChangementDirection, int hauteur, int largeur)
	{
		Point2D pos = new Point2D(posDepart.x, posDepart.y);
		int distance, compteurSecurite;
		string direction, currentDir = "E";
		for (int i = 0; i <= nbChangementDirection; i++)
		{
			compteurSecurite = 0;
			do
			{
				compteurSecurite++;
				direction = ChoisirDirection(currentDir);
				distance = DefinirDistance(pos, direction, hauteur, largeur);
				if (compteurSecurite >= 40) break;
			}
			while (!VerifDirection(direction, pos, hauteur, largeur) || !VerifDistance(tab, pos, direction, distance, nbChangementDirection));
			if (compteurSecurite >= 40) continue;
			currentDir = direction;
			AjouterDirection(currentDir);
			pos = DrawChemin(tab, pos, direction, distance);
			GenererObjectifOnCurrentChemin(tab);
			tab[pos.y, pos.x] = ElementName.Orientation;
		}
		tab[pos.y, pos.x] = ElementName.End;
		listActions.Remove (listActions [0]);//On supprime le premier qui n'est pas une direction mais le sens de depart du joueur
	}
	
	private void GenererObstacle(ElementName[,] tab, int nbObstacle, int hauteur, int largeur)
	{
		int compteur = 0;
		while (compteur < nbObstacle)
		{
			Point2D posObs = new Point2D(0, 0);
			posObs.x = Random.Range(0, largeur);
			posObs.y = Random.Range(0, hauteur);
			if (tab[posObs.y, posObs.x] != ElementName.Vide)
				continue;
			tab[posObs.y, posObs.x] = ElementName.Obstacle;
			compteur++;
		}
	}
	
	private bool VerifDistance(ElementName[,] tab, Point2D pos, string direction, int distance, int chDirection)
	{
		if (distance == 1)
		{
			nbCheminNul++;
			if(nbCheminNul>chDirection/4)
				return false;
		}
		switch (direction)
		{
		case "O":
			if (tab[pos.y, pos.x - distance] != ElementName.Vide)// && tab[pos.y, pos.x - distance] != ElementName.Chemin)
				return false;
			break;
		case "N":
			if (tab[pos.y - distance, pos.x] != ElementName.Vide)// && tab[pos.y - distance, pos.x] != ElementName.Chemin)
				return false;
			break;
		case "E":
			if (tab[pos.y, pos.x + distance] != ElementName.Vide)// && tab[pos.y, pos.x + distance] != ElementName.Chemin)
				return false;
			break;
		case "S":
			if (tab[pos.y + distance, pos.x] != ElementName.Vide)// && tab[pos.y + distance, pos.x] != ElementName.Chemin)
				return false;
			break;
		}
		return true;
	}
	
	
	private bool VerifDirection(string direction, Point2D pos, int hauteur, int largeur)
	{
		if (direction == "O" && pos.x == 0)
			return false;
		if (direction == "N" && pos.y == 0)
			return false;
		if (direction == "S" && pos.y == hauteur - 1)
			return false;
		if (direction == "E" && pos.x == largeur - 1)
			return false;
		return true;
	}
	
	private string ChoisirDirection(string current)
	{
		//0->O   1->N  2->E   3->S
		int value, i;
		string dirInverse;
		string[] dirTab = new string[] {"O","N","E","S" };
		for (i = 0; i < 4; i++)
			if (dirTab[i] == current) break;
		if (i >= 2)
			dirInverse = dirTab[i - 2];
		else
			dirInverse = dirTab[i + 2];
		do
			value = Random.Range(0, 4);
		while (dirTab[value] == current || dirTab[value] == dirInverse);
		return dirTab[value];
	}

	private int DefinirDistance(Point2D pos, string direction, int hauteur, int largeur)
	{
		int distanceMax = 0;
		switch (direction)
		{
		case "O":
			distanceMax = pos.x;
			break;
		case "N":
			distanceMax = pos.y;
			break;
		case "E":
			distanceMax = largeur - (pos.x + 1);
			break;
		case "S":
			distanceMax = hauteur - (pos.y + 1);
			break;
		}
		return Random.Range(1, distanceMax+1);//base distanceMax+1
	}
	
	
	private Point2D DrawChemin(ElementName[,] tab, Point2D pos, string direction, int distance)
	{
		currentChemin = new List<Point2D>();
		switch (direction)
		{
		case "O":
			return DrawCheminOuest(tab, pos, distance);
		case "N":
			return DrawCheminNord(tab, pos, distance);
		case "E":
			return DrawCheminEst(tab, pos, distance);
		case "S":
			return DrawCheminSud(tab, pos, distance);
		}
		return pos;
	}
	
	private Point2D DrawCheminNord(ElementName[,] tab, Point2D pos, int distance)
	{
		for (int i = pos.y - 1; i >= pos.y - distance; i--)
		{
			tab [i, pos.x] = ElementName.Chemin;
			currentChemin.Add(new Point2D(pos.x, i));
		}
			return new Point2D(pos.x, pos.y - distance);
	}
	
	private Point2D DrawCheminOuest(ElementName[,] tab, Point2D pos, int distance)
	{
		for (int i = pos.x - 1; i >= pos.x - distance; i--)
		{
			tab [pos.y, i] = ElementName.Chemin;
			currentChemin.Add(new Point2D(i, pos.y));
		}
		return new Point2D(pos.x - distance, pos.y);
	}
	
	private Point2D DrawCheminSud(ElementName[,] tab, Point2D pos, int distance)
	{
		for (int i = pos.y + 1; i <= pos.y + distance; i++)
		{
			tab [i, pos.x] = ElementName.Chemin;
			currentChemin.Add(new Point2D(pos.x, i));
		}
		return new Point2D(pos.x, pos.y + distance);
	}
	
	private Point2D DrawCheminEst(ElementName[,] tab, Point2D pos, int distance)
	{
		for (int i = pos.x + 1; i <= pos.x + distance; i++)
		{
			tab [pos.y, i] = ElementName.Chemin;
			currentChemin.Add(new Point2D(i, pos.y));
		}
		return new Point2D(pos.x+distance, pos.y);
	}

	private void GenererObjectifOnCurrentChemin(ElementName[,] tab)
	{
		if (currentChemin.Count < 1)
			return;
		int value = Random.Range (0, 4);
		if (value == 3 && !cheminSansObjectif)
		{
			cheminSansObjectif = true;
			return;
		}
		int nbObjectif = 1;
		if (currentChemin.Count > 4)
			nbObjectif += (int)((currentChemin.Count - 5) / 4);
		int securite=0;
		while (nbObjectif>0)
		{
			if(securite++ >20)
			{
				Debug.LogError ("Securité a pris la main!");
				return;
			}
			value = Random.Range (0, currentChemin.Count-1);
			if(tab[currentChemin[value].y, currentChemin[value].x] != ElementName.Chemin)
				continue;
			tab[currentChemin[value].y, currentChemin[value].x] = ElementName.Objectif;
			nbObjectif--;
		}

	}

	
	private Point2D GenererCaseStartEnd(ElementName[,] tab, int hauteur, int largeur, Point2D end = null)
	{
		int posxStart, posyStart, posxEnd, posyEnd;
		
		posyStart = Random.Range(0, hauteur);
		posxStart = Random.Range(0, largeur);

		tab[posyStart, posxStart] = ElementName.Start;
		do
		{
			posyEnd = Random.Range(0, hauteur);
			posxEnd = Random.Range(0, largeur);
			
		}
		while(posyEnd == posyStart && posxStart == posxEnd);
		
		//tab[posyEnd, posxEnd] = ElementName.Red;
		if (end != null)
		{
			end.x = posxEnd;
			end.y = posyEnd;
		}
		return new Point2D(posxStart, posyStart);
	}

	private void AjouterDirection(string direction)
	{
		switch (direction)
		{
		case "O":
			listActions.Add(ElementName.LeftArrow);
			return;
		case "N":
			listActions.Add(ElementName.UpArrow);
			return;
		case "E":
			listActions.Add(ElementName.RightArrow);
			return;
		case "S":
			listActions.Add(ElementName.DownArrow);
			return;
		default:
			return;
		}
	}
}
