using Platformer.Gameplay;
using UnityEngine;
//using Platformer.Mechanics.PlayerColorSystem;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{

    public enum TokenColor{red, blue, green, none};

    /// <summary>
    /// This class contains the data required for implementing token collection mechanics.
    /// It does not perform animation of the token, this is handled in a batch by the 
    /// TokenController in the scene.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    
    public class TokenInstance : MonoBehaviour
    {
        public AudioClip tokenCollectAudio;

        public GameObject UiController;

        [Tooltip("If true, animation will start at a random position in the sequence.")]
        public bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, collectedAnimation;

        public Vector3 scaleChange;
        internal Sprite[] sprites = new Sprite[0];

        internal SpriteRenderer _renderer;

        //unique index which is assigned by the TokenController in a scene.
        internal int tokenIndex = -1;
        internal TokenController controller;
        //active frame in animation, updated by the controller.
        internal int frame = 0;
        internal bool collected = false;
        public Material mat;
        
        public TokenColor tokenColor = TokenColor.none;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (randomAnimationStartTime)
                frame = Random.Range(0, sprites.Length);
            sprites = idleAnimation;
            scaleChange = new Vector2(0.05f, 0.05f);
            UiController = GameObject.Find("UIController");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //only exectue OnPlayerEnter if the player collides with this token.
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) OnPlayerEnter(player);
        }

        void OnPlayerEnter(PlayerController player)
        {
            if(this.tokenColor != TokenColor.none)
            {
                var ColorScript = player.GetComponent<PlayerColorSystem>();
                ColorScript.AddMaterial(tokenColor, this.mat);
                UiController.GetComponent<ControlGems>().AddGem(tokenColor);
            }
            
            
            if (collected) return;
            //disable the gameObject and remove it from the controller update list.
            frame = 0;
            sprites = collectedAnimation;
            if (controller != null)
                collected = true;
            //send an event into the gameplay system to perform some behaviour.
            var ev = Schedule<PlayerTokenCollision>();
            ev.token = this;
            ev.player = player;
            player.transform.localScale += scaleChange;
        }

    }
}