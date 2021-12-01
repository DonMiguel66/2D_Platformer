using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace MyPlatformer
{
    public class GeneratorController
    {
        private Tilemap _tilemap;
        private Tile _groundTile;
        private int _mapWidth;
        private int _mapHeight;
        private bool _setBorders;

        private int _fillPercent;        
        private int _smoothFactor;

        private int[,] _map;
         
        private int nearbyCountWall = 4;

        //»спользу€ Marching Squares
        private MarchingSquaresController _marchingSquaresGeneratorLevel;
        public GeneratorController(GeneratorLevelView levelView)
        {
            _tilemap = levelView.Tilemap;
            _groundTile = levelView.GroundTile;
            _mapWidth = levelView.MapWidth;
            _mapHeight = levelView.MapHeight;
            _setBorders = levelView.SetBorders;
            _fillPercent = levelView.FillPercent;
            _smoothFactor = levelView.SmoothFactor;
            _marchingSquaresGeneratorLevel = new MarchingSquaresController();
            _map = new int [_mapWidth,_mapHeight];
        }

        public void Init()
        {
            RandomFillMap();
            for (int i = 0; i < _smoothFactor; i++)
            {
                SmoothMap();
            }
            //»спользу€ Marching Squares
            _marchingSquaresGeneratorLevel.GenerateGrid(_map, 1);
            _marchingSquaresGeneratorLevel.DrawTilesOnMap(_tilemap, _groundTile);

            //DrawTiles();
        }

        private void DrawTiles()
        {
            if (_map == null)
                return;

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    Vector3Int positionTile = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);
                    if(_map[x,y] == 1)
                    {
                        _tilemap.SetTile(positionTile, _groundTile);
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neighbourWall = GetWallCount(x,y);

                    if (neighbourWall > nearbyCountWall)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbourWall < nearbyCountWall)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }

        private int GetWallCount(int x, int y)
        {
            int wallCount = 0;

            for (int gridX = x-1; gridX <= x+1; gridX++)
            {
                for (int gridY = y-1; gridY <= y+1; gridY++)
                {
                    if(gridX >=0 && gridX < _mapWidth && gridY >=0 && gridY < _mapHeight)
                    {
                        if(gridX !=x || gridY != y)
                        {
                            wallCount += _map[gridX, gridY];
                        }
                    }    
                    else
                    {
                        wallCount++;
                    }
                }
            }

            return wallCount;
        }

        private void RandomFillMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (x ==0 || x == _mapWidth-1 || y==0 || y== _mapHeight-1)
                    {
                        if(_setBorders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = (Random.Range(0, 100) < _fillPercent) ? 1 : 0;
                    }
                }
            }
        }
    }
}
