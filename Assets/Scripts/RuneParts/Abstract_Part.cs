using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Part
{
    protected List<Vector2> points;

    public Abstract_Part(List<Vector2> points)
    {
        this.points = points;
    }

    protected List<Vector2> Abstract_Sample(int sparseness)
    {
        List<Vector2> newPoints = new List<Vector2>();
        for(int i = 0; i < points.Count; i++)
        {
            if(i % sparseness == 0)
            {
                newPoints.Add(points[i]);
            }
        }
        return newPoints;
    }
    protected List<Vector2> Abstract_RemoveDuplicates()
    {
        List<Vector2> newPoints = new List<Vector2>();
        for (int i = 0; i < points.Count; i++)
        {
            if (i < 1 || !points[i].Equals(points[i-1]))
            {
                newPoints.Add(points[i]);
            }
        }
        return newPoints;
    }

    protected bool Abstract_Line(float tolerance)
    {
        Vector2 start = points[0];
        Vector2 end = points[points.Count - 1];
        Vector2 line = end - start;
        for (int i = 1; i < points.Count - 1; i++)
        {
            Vector2 startToPoint = points[i] - start;
            float angle = Vector2.Angle(line, startToPoint);
            float distanceFromLine = startToPoint.magnitude * Mathf.Sin(angle);
            if (distanceFromLine > tolerance)
            {
                return false;
            }

        }
        return true;
    }

    protected List<Vector2> Get_Corners(float degreesAngleTolerance)
    {
        if(points.Count < 2)
        {
            return null;
        }
        Vector2 start = points[0];
        Vector2 end = points[points.Count - 1];
        List<Vector2> corners = new List<Vector2>();
        
        corners.Add(start);
        if (points.Count < 3)
        {
            corners.Add(end);
            return corners;
        }
        for (int i = 2; i < points.Count; i++)
        {
            Vector2 currentLine = points[i - 1] - start;
            Vector2 nextLine = points[i] - points[i - 1];
            if(Vector2.Angle(currentLine, nextLine) < 180f - degreesAngleTolerance)
            {
                corners.Add(points[i - 1]);
                start = points[i - 1];
            }
        }

        corners.Add(end);
        return corners;
    }
}
