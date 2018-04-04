﻿using UnityEngine;

public class MazeCell : MonoBehaviour
{
	public IntVector2 coordinates;

	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

	private int initializedEdgeCount;
    private bool isOccupied;

	public bool IsFullyInitialized
    {
		get
        {
			return initializedEdgeCount == MazeDirections.Count;
		}
	}

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void SetCellOccupied(bool occupied)
    {
        isOccupied = occupied;
    }

	public MazeDirection RandomUninitializedDirection
    {
		get
        {
			int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);

            for (int i = 0; i < MazeDirections.Count; i++)
            {
				if (edges[i] == null)
                {
					if (skips == 0)
                    {
						return (MazeDirection)i;
					}

					skips -= 1;
				}
			}

            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
		}
	}

	public MazeCellEdge GetEdge (MazeDirection direction)
    {
		return edges[(int)direction];
	}

	public void SetEdge (MazeDirection direction, MazeCellEdge edge)
    {
		edges[(int)direction] = edge;
		initializedEdgeCount += 1;
	}
}