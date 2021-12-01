using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyPlatformer
{
    public class GeneratorLevelView : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Tile _groundTile;
        [SerializeField] private int _mapWidth;
        [SerializeField] private int _mapHeight;
        [SerializeField] private bool _setBorders;

        [SerializeField] 
        [Range(0,100)]
        private int _fillPercent;
        [SerializeField]
        [Range(0, 100)]
        private int _smoothFactor;

        public Tilemap Tilemap { get => _tilemap; set => _tilemap = value; }
        public Tile GroundTile { get => _groundTile; set => _groundTile = value; }
        public int MapWidth { get => _mapWidth; set => _mapWidth = value; }
        public int MapHeight { get => _mapHeight; set => _mapHeight = value; }
        public bool SetBorders { get => _setBorders; set => _setBorders = value; }
        public int FillPercent { get => _fillPercent; set => _fillPercent = value; }
        public int SmoothFactor { get => _smoothFactor; set => _smoothFactor = value; }
    }
}
