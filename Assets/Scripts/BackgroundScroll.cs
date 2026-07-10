using UnityEngine;

namespace SpaceInvader
{
    public class BackgroundScroll : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _speed;

        private Material _material;
        private Vector2 _offset;

        private void Start() {
            _material = _spriteRenderer.material;
        }

        //Sprite Renderernya Draw modenya simple
        //shadernya unlit/Transparent
        //make sure texturenya udh set REPEAT di bagian wrap mode di inspector
        private void Update() {
            _offset = new Vector2(0f, _speed * Time.deltaTime);
            _material.mainTextureOffset += _offset;
        }
    }
}
