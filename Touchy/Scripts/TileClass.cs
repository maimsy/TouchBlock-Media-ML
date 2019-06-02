using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileNameType { Start, End, Touch, Normal }

public class Coords
{
    private int x ;
    private int y;

    public int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }
}




public class GeneralTile
{
    protected TileNameType type;
    public bool isSelected; //Is green square on top
    public bool isPassable;
    public bool isTouchable; //side touch only for touch tiles

    public virtual  void InitTile()
    { 
    }

    protected int numberOfTimesTouched;
    public int NumberOfTimesTouched
    {
        get
        {
            return numberOfTimesTouched;
        }
        set
        {
            if (isTouchable)
            {
                numberOfTimesTouched += value;
            }
        }
    }

}

class TileStart : GeneralTile
{
    public override void InitTile()
    {
        type = TileNameType.Start;
        isSelected = false;
        isPassable = true;
        isTouchable = false;

    }
}

class TileEnd : GeneralTile
{
    public override void InitTile()
    {
        type = TileNameType.End;
        isSelected = false;
        isPassable = true;
        isTouchable = false;
    }
}


class TileTouch : GeneralTile
{
    public override void  InitTile()
    {
        type = TileNameType.Touch;
        isSelected = false;
        isPassable = false;
        isTouchable = true;
    }
}

class TileNormal : GeneralTile
{
    public override void InitTile()
    {
        type = TileNameType.Normal;
        isSelected = false;
        isPassable = true;
        isTouchable = false;
    }
}

