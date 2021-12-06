using UnityEngine;
using UnityEngine.Tilemaps;


namespace MyPlatformer
{
    public class MarchingSquaresController
    {
        private SquareGrid _squareGrid;
        private Tile _tileGround;
        private Tilemap _tileMap;

        public void GenerateGrid(int[,] map, float squareSize)
        {
            _squareGrid = new SquareGrid(map, squareSize);
        }
        private void DrawTileInControlNode(bool active, Vector3 position)
        {
            if (active)
            {
                var positionTile = new Vector3Int((int)position.x, (int)position.y, 0);
                _tileMap.SetTile(positionTile, _tileGround);
            }
        }

        public void DrawTilesOnMap(Tilemap tileMap, Tile tile)
        {
            if (_squareGrid == null)
                return;
            _tileMap = tileMap;
            _tileGround = tile;

            for (var x = 0; x < _squareGrid.Squares.GetLength(0); x++)
            {
                for (var y = 0; y < _squareGrid.Squares.GetLength(1); y++)
                {

                    DrawTileInControlNode(_squareGrid.Squares[x, y]._topLeft._active,
                        _squareGrid.Squares[x, y]._topLeft._position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y]._topRight._active,
                        _squareGrid.Squares[x, y]._topRight._position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y]._bottomRight._active,
                        _squareGrid.Squares[x, y]._bottomRight._position);
                    DrawTileInControlNode(_squareGrid.Squares[x, y]._bottomLeft._active,
                        _squareGrid.Squares[x, y]._bottomLeft._position);
                }
            }

        }               
    }
    public class Node
    {
        public Vector3 _position;
        public Node(Vector3 pos)
        {
            _position = pos;
        }
    }

    public class Square
    {
        public ControlNode _topLeft, _topRight, _bottomLeft, _bottomRight;

        public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomLeft, ControlNode bottomRight)
        {
            _topLeft = topLeft;
            _topRight = topRight;
            _bottomLeft = bottomLeft;
            _bottomRight = bottomRight;
        }
    }
    public class ControlNode : Node
    {
        public bool _active;

        public ControlNode(Vector3 pos, bool active) : base(pos)
        {
            _active = active;
        }
    }
    public class SquareGrid
    {
        public Square[,] Squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, -mapHeight / 2 + y * squareSize / 2, 0);
                    controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1);
                }
            }

            Squares = new Square[nodeCountX - 1, nodeCountY - 1];

            for (int x = 0; x < nodeCountX-1; x++)
            {
                for (int y = 0; y < nodeCountY-1; y++)
                {
                    Squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1], controlNodes[x + 1, y], controlNodes[x, y]);
                }
            }
        }
    }
}
