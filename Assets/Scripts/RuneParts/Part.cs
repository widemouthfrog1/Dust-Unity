using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : Abstract_Part
{
    public Part(List<Vector2> points) : base(points){}

    public Part RemoveDuplicates()
    {
        return new Part(Abstract_RemoveDuplicates());
    }

    public Part Sample()
    {
        return new Part(Abstract_Sample(5));
    }

    public Part Sample(int sparseness)
    {
        return new Part(Abstract_Sample(sparseness));
    }

    public Part Default_Preprocess()
    {
        return RemoveDuplicates().Sample();
    }

    public Part[] SplitByCorners()
    {
        List<Vector2> corners = Get_Corners(10);
        Part[] parts = new Part[corners.Count - 1];
        int corner = 1;
        List<Vector2> newPoints = new List<Vector2>();
        for (int i = 0; i < points.Count; i++)
        {
            newPoints.Add(points[i]);
            if (points[i].Equals(corners[corner]))
            {
                parts[corner - 1] = new Part(newPoints);
                newPoints = new List<Vector2>();
                i--; //to add this point (the corner) to the next part
                corner++;
            }
        }
        return parts;
    }

    public Part[] SplitByCorners(float degreesAngleTolerance)
    {
        List<Vector2> corners = Get_Corners(degreesAngleTolerance);
        Part[] parts = new Part[corners.Count - 1];
        int corner = 1;
        List<Vector2> newPoints = new List<Vector2>();
        for (int i = 0; i < points.Count; i++)
        {
            newPoints.Add(points[i]);
            if (points[i].Equals(corners[corner]))
            {
                parts[corner - 1] = new Part(newPoints);
                newPoints = new List<Vector2>();
                i--; //to add this point (the corner) to the next part
                corner++;
            }
        }
        return parts;
    }

    public bool Line()
    {
        return Abstract_Line(0.5f);
    }

    public bool Line(float tolerance)
    {
        return Abstract_Line(tolerance);
    }

    public bool Arrow()
    {
        List<Vector2> corners = Get_Corners(10);
        if(corners.Count != 3)
        {
            return false;
        }
        Part[] parts = SplitByCorners();
        return parts[0].Line() && parts[1].Line();
    }

    public bool Arrow(float degreesAngleTolerance, float lineTolerance)
    {
        List<Vector2> corners = Get_Corners(10);
        if (corners.Count != 3)
        {
            return false;
        }
        Part[] parts = SplitByCorners(degreesAngleTolerance);
        return parts[0].Line(lineTolerance) && parts[1].Line(lineTolerance);
    }

}
