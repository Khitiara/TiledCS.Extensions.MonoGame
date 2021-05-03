using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace TiledCS.Extensions.MonoGame.Rendering
{
    public class TiledTileRenderer
    {
        private TiledLayerRenderer _layer;
        private TiledTileset _tileset;

        private Texture2D _texture;
        private Rectangle _sourceRectangle;

        private int _firstgid;
        private int _tileid;

        private int _gid;

        public TiledTileRenderer(TiledLayerRenderer layer, int gid)
        {
            _layer = layer;
            _gid = gid;
        }

        public void Load(ContentManager content)
        {
            TiledMapTileset tileset = _layer.Map.Source.GetTiledMapTileset(_gid);

            _firstgid = tileset.firstgid;
            _tileid = _gid - _firstgid;

            _tileset = _layer.Map.GetTilesetFromGid(tileset);

            _sourceRectangle = new Rectangle(_tileid * _tileset.TileWidth % _tileset.ImageWidth,
                _tileid * _tileset.TileWidth / _tileset.ImageWidth * _tileset.TileHeight, _tileset.TileWidth, _tileset.TileHeight);

            string assetName = Path.GetFileNameWithoutExtension(_tileset.Image);
            _texture = content.Load<Texture2D>(assetName);
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            spriteBatch.Draw(_texture, new Vector2(x * _tileset.TileWidth, y * _tileset.TileHeight), _sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
