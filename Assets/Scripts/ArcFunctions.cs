using UnityEngine;
using System.Collections;

public class ArcFunctions  {

	public static Vector3 ArcNormalised(out ArcData data, Vector3 startPosition, Vector3 startNormal, Vector3 endPosition, float t)
    {
        Vector3 currentNormal;
        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 startPoint;
        Vector3 endPoint;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;

        startPosition.y = 0;
        endPosition.y = 0;
        startNormal.y = 0;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
        }
        else
        {

        }
        radius = Vector3.Distance(circleCentre, startPosition);

        Vector3 newPos = Vector3.zero;


        if(angleToStart < 1)
        {
            


        }
        else
        {
            
        }

        Vector3 tNormal;

        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            if (properAngle > 0)
            {
                if (worldAngleTwo >= 0)
                {
                    newPos = Orbit(circleCentre, radius, -90 + t * (angleToStart) + worldAngle);
                }
                else
                {
                    newPos = Orbit(circleCentre, radius, -90 + t * (angleToStart) - worldAngle);
                }
            }
            else
            {
                if (worldAngleTwo >= 0)
                {
                    newPos = Orbit2(circleCentre, radius, -90 + t * (angleToStart) - worldAngle);
                }
                else
                {
                    newPos = Orbit2(circleCentre, radius, -90 + t * (angleToStart) + worldAngle);
                }
            }
            tNormal = Vector3.Normalize(circleCentre - newPos);
        }
        else
        {
            newPos = Vector3.Lerp(startPosition, endPosition, t);
            tNormal = Quaternion.Euler(0,90,0) * Vector3.Normalize(endPosition - startPosition);
            endNormal = Vector3.Normalize(startPosition - endPosition);
        }


        float newAngle =AngleFull(startNormal, endNormal, Vector3.up);

        data = new ArcData(startPosition, startNormal,endPosition,  -endNormal, radius, 2 * radius * Mathf.PI, angleToStart, circleCentre, ArcFunctions.ArcLength(startPosition, startNormal, endPosition), tNormal);

        return newPos;
    }

    public static Vector3 ArcEndNormal(Vector3 startPosition, Vector3 startNormal, Vector3 endPosition)
    {
        Vector3 currentNormal;
        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 startPoint;
        Vector3 endPoint;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
        }
        else
        {
            endNormal = Vector3.Normalize(startPosition - endPosition);
        }
        radius = Vector3.Distance(circleCentre, startPosition);

        return -endNormal;
    }

    public static float ArcAngleToNormalised(Vector3 startPosition, Vector3 startNormal, Vector3 endPosition, float currentAngle)
    {
        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float worldAngle;
        float worldAngleTwo;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }

        float convertetAngle = currentAngle;
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
            if (properAngle > 0)
            {
                if (worldAngleTwo >= 0)
                {
                    convertetAngle = Mathf.Abs((-90 + currentAngle) / 180);
                    //newPos = Orbit(circleCentre, radius, -90 + t * (angleToStart) + worldAngle);
                }
                else
                {
                    convertetAngle = (90 + currentAngle) / 180;
                    //newPos = Orbit(circleCentre, radius, -90 + t * (angleToStart) - worldAngle);
                }
            }
            else
            {
                if (worldAngleTwo >= 0)
                {
                    convertetAngle = (90 + currentAngle) / 180;
                    //newPos = Orbit2(circleCentre, radius, -90 + t * (angleToStart) - worldAngle);
                }
                else
                {
                    convertetAngle = (90 + currentAngle) / 180;
                    //newPos = Orbit2(circleCentre, radius, -90 + t * (angleToStart) + worldAngle);
                }
            }
            convertetAngle = 0;
        }
        else
        {
            //newPos = Vector3.Lerp(startPosition, endPosition, t);
            //tNormal = Quaternion.Euler(0, 90, 0) * Vector3.Normalize(endPosition - startPosition);
            endNormal = Vector3.Normalize(startPosition - endPosition);
            //convertetAngle = 
        }


        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
        }
        radius = Vector3.Distance(circleCentre, startPosition);

        return convertetAngle;
    }

    public static float ArcRadius(Vector3 startPosition, Vector3 startNormal, Vector3 endPosition)
    {
        Vector3 currentNormal;
        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 startPoint;
        Vector3 endPoint;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
        }
        radius = Vector3.Distance(circleCentre, startPosition);

        return radius;
    }

    public static float ArcCircumference(Vector3 startPosition, Vector3 startNormal, Vector3 endPosition)
    {
        Vector3 currentNormal;
        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 startPoint;
        Vector3 endPoint;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
        }
        radius = Vector3.Distance(circleCentre, startPosition);

        return 2 * radius * Mathf.PI;
    }

    public static float ArcLength(Vector3 startPosition, Vector3 startNormal, Vector3 endPosition)
    {
        Vector3 currentNormal;
        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 startPoint;
        Vector3 endPoint;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
        }
        else
        {

        }
        radius = Vector3.Distance(circleCentre, startPosition);
        Vector3 norm1 = Vector3.Normalize(startPosition - circleCentre);
        Vector3 norm2 = Vector3.Normalize(endPosition - circleCentre);

        float circumference = 2 * radius * Mathf.PI;
        float arcAngle = AngleFull(norm1, norm2, Vector3.up) * Mathf.Deg2Rad;
        if(properAngle < 0)
        {
            arcAngle = AngleFull(norm2, norm1, Vector3.up) * Mathf.Deg2Rad;
            
        }

        float num1 = arcAngle * circumference;

        float length = 0;
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            length = radius * Mathf.Abs(arcAngle);
        }
        else
        {
            length = Vector3.Distance(startPosition, endPosition);
        }

        return length;
    }

    public static float ArcAngle(Vector3 startPosition, Vector3 startNormal, Vector3 endPosition)
    {
        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
        }
        radius = Vector3.Distance(circleCentre, startPosition);

        Vector3 norm1 = Vector3.Normalize(startPosition - circleCentre);
        Vector3 norm2 = Vector3.Normalize(endPosition - circleCentre);
        float arcAngle = AngleFull(norm1, norm2, Vector3.up);

        return arcAngle;
    }

    public static bool DistanceFromArc(out float distance, Vector3 startPosition, Vector3 startNormal, Vector3 endPosition, Vector3 mousePosition)
    {

        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;
        float distanceFromArc = 0;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }


        bool didIntersect = false;  
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
            radius = Vector3.Distance(circleCentre, startPosition);
            distanceFromArc = Mathf.Abs(radius - Vector3.Distance(circleCentre, mousePosition));
            didIntersect = true;
        }
        else
        {
            float dist;
            DistanceFromLine(out dist, startPosition, endPosition, mousePosition);
            distanceFromArc = dist;
        }
        


        distance = distanceFromArc;

        Vector3 vectorToMouse = Vector3.Normalize(mousePosition - circleCentre);
        float angleToMouse = AngleHalf(startNormal, vectorToMouse, Vector3.up);
        Vector3 vectorToFirst = Vector3.Normalize(startPosition - circleCentre);
        float angleToFirst = AngleHalf(startNormal, vectorToFirst, Vector3.up);
        Vector3 vectorToSecond = Vector3.Normalize(endPosition - circleCentre);
        float angleToSecond = AngleHalf(startNormal, vectorToSecond, Vector3.up);

        //Debug.Log("Mouse " + angleToMouse + "First " + angleToFirst + "Second " + angleToSecond);
        if (didIntersect)
        {
            if (properAngle > 0)
            {
                if (angleToMouse > angleToFirst && angleToMouse < angleToSecond)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                float dist1;
                DistanceFromLine(out dist1, startPosition, endPosition, mousePosition);
                if (angleToMouse < angleToFirst && angleToMouse > angleToSecond)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            float dist;
            
            if(DistanceFromLine(out dist, startPosition, endPosition, mousePosition))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }

    public static bool PositionOnArc(out PointOnArcData pointData, out Vector3 newPosition, Vector3 startPosition, Vector3 startNormal, Vector3 endPosition, Vector3 mousePosition)
    {

        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;
        float distanceFromArc = 0;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }

        float distFromEndOne = 0;
        Vector3 endOnePos = Vector3.zero;
        float distFromEndTwo = 0;
        Vector3 endTwoPos = Vector3.zero;
        Vector3 vectorToMouse = Vector3.zero;
        bool didIntersect = false;


        float angleToMouse = AngleHalf(startNormal, vectorToMouse, Vector3.up);
        Vector3 vectorToFirst = Vector3.Normalize(startPosition - circleCentre);
        float angleToFirst = AngleHalf(startNormal, vectorToFirst, Vector3.up);
        Vector3 vectorToSecond = Vector3.Normalize(endPosition - circleCentre);
        float angleToSecond = AngleHalf(startNormal, vectorToSecond, Vector3.up);

        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
            radius = Vector3.Distance(circleCentre, startPosition);
            vectorToMouse = Vector3.Normalize(mousePosition - circleCentre);

            didIntersect = true;

            angleToMouse = AngleFull(startNormal, vectorToMouse, Vector3.up);
            vectorToFirst = Vector3.Normalize(startPosition - circleCentre);
            angleToFirst = AngleHalf(startNormal, vectorToFirst, Vector3.up);
            vectorToSecond = Vector3.Normalize(endPosition - circleCentre);
            angleToSecond = AngleHalf(startNormal, vectorToSecond, Vector3.up);

            
            float arcLength = ArcLength(startPosition, startNormal, endPosition);
            float arcAngle = ArcAngle(startPosition, startNormal, endPosition);

            distFromEndOne =  Mathf.Abs(angleToFirst - angleToMouse) * (arcLength);
            endOnePos = startPosition;
            distFromEndTwo = Mathf.Abs(angleToMouse - angleToSecond) * (arcLength);
            endTwoPos = endPosition;

            if (Vector3.Distance(circleCentre, mousePosition) > radius) vectorToMouse = Vector3.Normalize(mousePosition - circleCentre);
            else vectorToMouse = Vector3.Normalize(circleCentre - mousePosition);
            //Debug.Log(distFromEndOne + " " + distFromEndTwo);
        }
        else
        {
            vectorToMouse = Vector3.Normalize(mousePosition - NearestPointOnLine(startPosition, (endPosition - startPosition), mousePosition));

            angleToMouse = AngleFull(startNormal, vectorToMouse, Vector3.up);
            vectorToFirst = Vector3.Normalize(startPosition - circleCentre);
            angleToFirst = AngleHalf(startNormal, vectorToFirst, Vector3.up);
            vectorToSecond = Vector3.Normalize(endPosition - circleCentre);
            angleToSecond = AngleHalf(startNormal, vectorToSecond, Vector3.up);

            radius = Vector3.Distance(circleCentre, startPosition);

            distFromEndOne = Vector3.Distance(startPosition, NearestPointOnLine(startPosition, (endPosition - startPosition), mousePosition));
            endOnePos = startPosition;
            distFromEndTwo = Vector3.Distance(endPosition, NearestPointOnLine(startPosition, (endPosition - startPosition), mousePosition));
            endTwoPos = endPosition;
            
        }
        

        distanceFromArc = Mathf.Abs(radius - Vector3.Distance(circleCentre, mousePosition));

        //distance = distanceFromArc;

        
        

        //Debug.Log("Mouse " + angleToMouse + "First " + angleToFirst + "Second " + angleToSecond);

        if (didIntersect)
        {
            newPosition = circleCentre + (vectorToMouse * radius);
        }
        else
        {
            newPosition = NearestPointOnLine(startPosition, (endPosition - startPosition), mousePosition);
        }

        

        pointData = new PointOnArcData(angleToMouse, vectorToMouse, distFromEndOne, distFromEndTwo, endOnePos, endTwoPos);
        pointData.tNormalised = angleToStart + worldAngle;

        if (properAngle > 0)
        {
            if (angleToMouse > angleToFirst && angleToMouse < angleToSecond)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (angleToMouse < angleToFirst && angleToMouse > angleToSecond)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static Vector3 PositionAngleOnArc( Vector3 startPosition, Vector3 startNormal, Vector3 endPosition, float anglePosition)
    {

        Vector3 toEndNormal;
        Vector3 toStartNormal;
        Vector3 endNormal;
        Vector3 startInsideNormal;
        Vector3 endInsideNormal;
        Vector3 anglePoint;
        Vector3 circleCentre = Vector3.zero;
        Vector3 intersect = Vector3.zero;
        float radius;
        float angle = 0;
        float properAngle = 0;
        float angleToStart;
        float angleToEnd;
        float worldAngle;
        float worldAngleTwo;
        float distanceFromArc = 0;

        toEndNormal = Vector3.Normalize(endPosition - startPosition);
        properAngle = AngleHalf(startNormal, toEndNormal, Vector3.up);
        worldAngle = Vector3.Angle(Vector3.forward, startNormal);
        worldAngleTwo = AngleHalf(Vector3.forward, startNormal, Vector3.up);

        if (properAngle > 0)
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * startNormal;
            angleToStart = AngleFull(startInsideNormal, endInsideNormal, Vector3.up);
        }
        else
        {
            anglePoint = startNormal * (Vector3.Distance(startPosition, endPosition) * 0.5f);


            toStartNormal = Vector3.Normalize(startPosition - endPosition);
            angle = Vector3.Angle(startNormal, toEndNormal);

            endNormal = Quaternion.Euler(new Vector3(0, -angle, 0)) * -toEndNormal;

            endInsideNormal = Quaternion.Euler(new Vector3(0, 90, 0)) * endNormal;
            startInsideNormal = Quaternion.Euler(new Vector3(0, -90, 0)) * startNormal;
            angleToStart = AngleFull(endInsideNormal, startInsideNormal, Vector3.up);
        }
        if (LineLineIntersection(out intersect, startPosition, startInsideNormal * 100000, endPosition, endInsideNormal * 100000))
        {
            circleCentre = intersect;
        }
        radius = Vector3.Distance(circleCentre, startPosition);

        Vector3 newPos = (circleCentre + Quaternion.AngleAxis(anglePosition, Vector3.up) * startNormal * radius);


        return newPos;
        
    }

    public static Vector3 BiarcPoint(Vector3 positionOne, Vector3 vectorOne, Vector3 positionTwo, Vector3 vectorTwo, float t)
    {

        float combinedAngle = 0;
        combinedAngle += (AngleHalf(Vector3.forward, vectorOne, Vector3.up) + -AngleHalf(Vector3.forward, vectorTwo, Vector3.up)) / 2;
        combinedAngle += AngleHalf(Vector3.forward, Vector3.Normalize(positionTwo - positionOne), Vector3.up);

        Vector3 combined = Vector3.Normalize(Quaternion.AngleAxis(combinedAngle, Vector3.up) * Vector3.forward);

        Vector3 arcMid = Vector3.zero;

        ArcData data;
        ArcData dataCircOne;
        ArcData dataCircTwo;
        arcMid = ArcFunctions.ArcNormalised(out data, positionOne, combined, positionTwo, t);
        float circ = 0;
        ArcFunctions.ArcNormalised(out dataCircOne, positionOne, vectorOne, arcMid, 0);
        ArcFunctions.ArcNormalised(out dataCircTwo, positionTwo, -vectorTwo, arcMid, 0);
        circ = (dataCircOne.angle * dataCircOne.circumference) + (dataCircTwo.angle * dataCircTwo.circumference);

        ArcData data2;
        ArcData dataCircOne1;
        ArcData dataCircTwo1;
        arcMid = ArcFunctions.ArcNormalised(out data2, positionOne, -combined, positionTwo, t);
        float circ2 = data2.angle;
        ArcFunctions.ArcNormalised(out dataCircOne1, positionOne, vectorOne, arcMid, 0);
        ArcFunctions.ArcNormalised(out dataCircTwo1, positionTwo, -vectorTwo, arcMid, 0);
        circ2 = (dataCircOne1.angle * dataCircOne1.circumference) + (dataCircTwo1.angle * dataCircTwo1.circumference);

        if (circ < circ2)
        {
            arcMid = ArcFunctions.ArcNormalised(out data, positionOne, combined, positionTwo, t);
        }
        else if (circ > circ2)
        {
            arcMid = ArcFunctions.ArcNormalised(out data, positionOne, -combined, positionTwo, t);
        }

        return arcMid;
    }

    public static float AngleFull(Vector3 v1, Vector3 v2, Vector3 n)
    {
        float toReturn = 0;
        toReturn = Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
        if (toReturn < 0)
            toReturn = 180 + (180 - -toReturn);

        return toReturn;
    }
    public static float AngleHalf(Vector3 v1, Vector3 v2, Vector3 n)
    {
        float toReturn = 0;
        toReturn = Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;

        return toReturn;
    }

    public static Vector3 Orbit(Vector3 origin, float radius, float angle)
    {
        Vector3 newPos = new Vector3();
        float rad = angle * (Mathf.PI / 180);
        newPos.x = (origin.x + radius * Mathf.Sin(rad));
        newPos.z = (origin.z + radius * Mathf.Cos(rad));
        return newPos;
    }
    static Vector3 Orbit2(Vector3 origin, float radius, float angle)
    {
        Vector3 newPos = new Vector3();
        float rad = angle * (Mathf.PI / -180);
        newPos.x = (origin.x + radius * Mathf.Sin(rad));
        newPos.z = (origin.z + radius * Mathf.Cos(rad));
        return newPos;
    }

    static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {

        intersection = Vector3.zero;

        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //Lines are not coplanar. Take into account rounding errors.
        if ((planarFactor >= 0.001f) || (planarFactor <= -0.001f))
        {

            return false;
        }

        //Note: sqrMagnitude does x*x+y*y+z*z on the input vector.
        float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;

        if ((s >= 0.0f) && (s <= 1.0f))
        {

            intersection = linePoint1 + (lineVec1 * s);
            return true;
        }

        else
        {
            return false;
        }
    }

    public static Vector3 NearestPointOnLine(Vector3 linePnt, Vector3 lineDir, Vector3 pnt)
    {
        lineDir.Normalize();//this needs to be a unit vector
        var v = pnt - linePnt;
        var d = Vector3.Dot(v, lineDir);
        return linePnt + lineDir * d;
    }

    public static bool DistanceFromLine(out float dist, Vector3 startPosition, Vector3 endPosition, Vector3 point)
    {
        Vector3 nearestPoint = NearestPointOnLine(startPosition, (endPosition - startPosition), point);


        dist = Vector3.Distance(nearestPoint, point);
        Vector3 intersect;
        if (LineLineIntersection(out intersect, startPosition, Vector3.Normalize(endPosition - startPosition) * Vector3.Distance(startPosition, endPosition), point, Vector3.Normalize(nearestPoint - point) * 10000))
        {
            //Debug.Log(Vector3.Distance(startPosition, endPosition));
            return true;
        }
        else
        {
            return false;
        }

    }
}

public class PointOnArcData
{
    public float angle;
    public Vector3 normal;
    public float distanceFromEndOne;
    public Vector3 endOnePosition;
    public float distanceFromEndTwo;
    public Vector3 endTwoPosition;
    public float tNormalised;

    public PointOnArcData(float _angle, Vector3 _normal, float _distanceFromEndOne, float _distanceFromEndTwo, Vector3 _endOnePosition, Vector3 _endTwoPosition)
    {
        angle = _angle;
        normal = _normal;
        distanceFromEndOne = _distanceFromEndOne;
        endOnePosition = _endOnePosition;
        distanceFromEndTwo = _distanceFromEndTwo;
        endTwoPosition = _endTwoPosition;
    }
}

public class ArcData
{
    public Vector3 startPosition;
    public Vector3 startNormal;
    public Vector3 endPosition;
    public Vector3 endNormal;
    public Vector3 tNormal;
    public float radius;
    public float circumference;
    public float angle;
    public float length;
    public Vector3 centre;

    public ArcData()
    {

    }
    public ArcData(Vector3 _startPosition, Vector3 _startNormal, Vector3 _endPosition,Vector3 _endNormal, float _radius, float _circumference, float _angle, Vector3 _centre, float _length, Vector3 _tNormal)
    {
        startPosition = _startPosition;
        startNormal = _startNormal;
        endPosition = _endPosition;
        endNormal = _endNormal;
        tNormal = _tNormal;
        radius = _radius;
        circumference = _circumference;
        angle = _angle;
        centre = _centre;
        length = _length;
    }
} 
