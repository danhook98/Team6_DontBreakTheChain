using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// The main game controller. Handles input for the players, computer player, movement, etc, 
/// </summary>
public class GameController : MonoBehaviour
{
    [Header("Input Reader")]
    [SerializeField] private InputReader inputReader;
    
    [Header("Audio")]
    [SerializeField] private AudioEventChannel audioChannel;
    [SerializeField] private AudioClipSO rollDiceAudioClip; 
    [SerializeField] private AudioClipSO diceSwapAudioClip;
    [SerializeField] private AudioClipSO moveAudioClip;
    [SerializeField] private AudioClipSO chainSnapAudioClip;
    [SerializeField] private AudioClipSO gameWonAudioClip;
    [SerializeField] private AudioClipSO gameLostAudioClip;
    [SerializeField] private AudioClipSO backgroundMusicAudioClip;
    [SerializeField][Range(0, 1)] private float backgroundMusicVolume = 0.2f;

    [Header("VFX")]
    [SerializeField] private GameObject effect;

    [Header("Game Type SO")]
    [SerializeField] private GameTypeSO gameType;
    
    [Header("Tile Generator")]
    [SerializeField] private TileGenerator tileGenerator;
    
    [Header("Timer")]
    [SerializeField] private Timer timer;

    [Header("Player Data References")] 
    [SerializeField] private PlayerSO playerOne;
    [SerializeField] private PlayerSO playerTwo;
    
    [Header("Game variables")] 
    [SerializeField] private byte tilesToWin = 30;
    [SerializeField] private Vector3 playerOneStartPosition;
    [SerializeField] private Vector3 playerTwoStartPosition;
    [SerializeField] private float playerInputCooldown = 0.75f;
    [SerializeField] private byte coinsPerLane = 5;
    [SerializeField] private float gameStartDelay = 2f;
    
    [Header("Chain references")]
    [SerializeField] private HingeJoint chainCentreHinge;
    
    private byte _score = 0;
    
    private bool _isComputerOpponent;

    // Cache WaitForSeconds methods. 
    private WaitForSeconds _inputCooldownDelay;
    private WaitForSeconds _gameStartDelay;

    // Game win/lose state.
    private bool _gameWon = false;
    private bool _gameLost = false;
    
    // UnityEvents for various game states. 
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    public UnityEvent<int> OnScoreChanged;

    // Subscribe to the input events from the Input Reader.
    private void OnEnable()
    {
        inputReader.LeftPlayerRollEvent += LeftPlayerRoll;
        inputReader.LeftPlayerSwapEvent += LeftPlayerSwap;
        inputReader.RightPlayerRollEvent += RightPlayerRoll;
        inputReader.RightPlayerSwapEvent += RightPlayerSwap;
        
        playerOne.OnScoreChanged += IncreaseScore;
        playerTwo.OnScoreChanged += IncreaseScore;
    }

    // Unsubscribe from the input events when this game object gets disabled. Failure to unsubscribe would cause the 
    // bugs and other issues.
    private void OnDisable()
    {
        inputReader.LeftPlayerRollEvent -= LeftPlayerRoll;
        inputReader.LeftPlayerSwapEvent -= LeftPlayerSwap;
        inputReader.RightPlayerRollEvent -= RightPlayerRoll;
        inputReader.RightPlayerSwapEvent -= RightPlayerSwap;
        
        playerOne.OnScoreChanged -= IncreaseScore;
        playerTwo.OnScoreChanged -= IncreaseScore;
    }

    private void Awake()
    {
        _inputCooldownDelay = new WaitForSeconds(playerInputCooldown);
        _gameStartDelay = new WaitForSeconds(gameStartDelay);
        
        // Disable input until the game 'starts'. 
        inputReader.DisableInput();
    }

    private void Start()
    {
        // Set the start positions for the players. This slowly moves them to the point from offscreen.
        playerOne.SetStartPosition(playerOneStartPosition);
        playerTwo.SetStartPosition(playerTwoStartPosition);
        
        // Generate the tiles along each player's path.
        tileGenerator.GenerateTiles(playerOneStartPosition, tilesToWin, coinsPerLane);
        tileGenerator.GenerateTiles(playerTwoStartPosition, tilesToWin, coinsPerLane, 1);
        
        // Generate the base. 
        tileGenerator.GenerateBase(Vector3.forward * -1, tilesToWin);

        _isComputerOpponent = gameType.opponentIsAi;
        
        // Start playing the background music. 
        audioChannel.PlayAudio(backgroundMusicAudioClip, backgroundMusicVolume);
        
        StartCoroutine(StartGame());
    }

    // Enables input and starts the countdown timer after a configured delay. 
    private IEnumerator StartGame()
    {
        yield return _gameStartDelay;
        timer.StartCountdown();
        inputReader.EnableInput();
    }

    // Main game loop.
    private void Update()
    {
        if (_gameWon || _gameLost) return;
        
        // The chain breaks! The '7.0f' is a magic number as the chain length cannot dynamically change. 
        if (Vector3.Distance(playerOne.Position, playerTwo.Position) >= 7.0f)
        {
            Destroy(chainCentreHinge);
            audioChannel.PlayAudioOneShot(chainSnapAudioClip);
            GameLost();
            return;
        }
        
        // Player one and two have both passed the win threshold. 
        if (playerOne.CurrentTilesMoved >= tilesToWin && playerTwo.CurrentTilesMoved >= tilesToWin)
        {
            GameWon();
            return;
        }
        
        // Player one and two have both rolled.
        if ((!playerOne.CanRoll && !playerTwo.CanRoll) && (playerOne.CurrentRollState == RollState.Rolled 
                                                           && playerTwo.CurrentRollState == RollState.Rolled))
        {
            playerOne.CanRoll = true;
            playerOne.CurrentRollState = RollState.Idle;
            playerTwo.CanRoll = true;
            playerTwo.CurrentRollState = RollState.Idle;
            
            Debug.Log("Round over");
        }

        // Both players want to swap their dice rolls.
        if (playerOne.WantsToSwap && playerTwo.WantsToSwap)
        {
            SwapDiceValues();
        }
    }

    // Called when the game has been won.
    private void GameWon()
    {
        if (_gameWon) return;
        _gameWon = true;
        audioChannel.PlayAudioOneShot(gameWonAudioClip);
        SpawnEffects();
        OnWin?.Invoke();
    }
    
    // Triggers the game lost method, used by the timer script when it reaches zero. 
    public void TriggerGameLost() => GameLost();

    // Called when the game has been lost.
    private void GameLost()
    {
        if (_gameLost) return;
        _gameLost = true;
        audioChannel.PlayAudioOneShot(gameLostAudioClip);
        OnLose?.Invoke();
    }

    // Rolls the dice for the left player.
    private void LeftPlayerRoll()
    {
        if (playerOne.JustInput) return;
        
        PlayerRoll(playerOne);

        if (_isComputerOpponent)
        {
            PlayerRoll(playerTwo, false);
            PlayerInputCooldown(playerTwo);
        }
        
        PlayerInputCooldown(playerOne);
    }

    // Sets the desire to swap for the left player.
    private void LeftPlayerSwap()
    {
        if (playerOne.JustInput) return;
        
        PlayerSwap(playerOne);

        if (_isComputerOpponent)
        {
            PlayerSwap(playerTwo);
            PlayerInputCooldown(playerTwo);
        }
        
        PlayerInputCooldown(playerOne);
    }

    // Rolls the dice for the right player.
    private void RightPlayerRoll()
    {
        if (playerTwo.JustInput) return;
        if (_isComputerOpponent) return;
        
        PlayerRoll(playerTwo);
        PlayerInputCooldown(playerTwo);
    }

    // Sets the desire to swap for the right player.
    private void RightPlayerSwap()
    {
        if (playerTwo.JustInput) return;
        if (_isComputerOpponent) return;
        
        PlayerSwap(playerTwo);
        PlayerInputCooldown(playerTwo);
    }

    // Starts an input cooldown for the given player.
    private void PlayerInputCooldown(PlayerSO player)
    {
        player.JustInput = true;
        StartCoroutine(ResetPlayerJustInput(player));
    }

    // Coroutine for the player input cooldown. 
    private IEnumerator ResetPlayerJustInput(PlayerSO player)
    {
        yield return _inputCooldownDelay;
        player.JustInput = false;
    }

    // Handles rolling the dice for the given player.
    private void PlayerRoll(PlayerSO player, bool playAudio = true)
    {
        if (_gameWon || _gameLost) return;
        
        switch (player.CurrentRollState)
        {
            case RollState.Idle:
                // The roll state may be 'rolled', but the player isn't currently allowed to do anything (e.g. waiting
                // for the other player to roll).
                if (!player.CanRoll || player.CurrentTilesMoved > tilesToWin) break;

                // Rolls a dice and triggers the UpdateCurrentRoll event for the given player.
                byte roll = RollDice();
                player.UpdateCurrentRoll(roll);
                
                // Update the player's roll state. When they press the roll dice button again, the next case statement
                // will run instead.
                player.CurrentRollState = RollState.Rolled;
                
                // Play the dice roll sound if allowed.
                if (playAudio)
                    audioChannel.PlayAudioOneShot(rollDiceAudioClip);
                
                // TODO: display the roll value and a message stating to press again to confirm.
                
                break;
            // 'Confirms' the player's choice to lock in their rolled dice value and move their character.
            case RollState.Rolled:
                // The roll state may be 'rolled', but the player isn't currently allowed to do anything (e.g. waiting
                // for the other player to roll).
                if (!player.CanRoll || player.CurrentTilesMoved > tilesToWin) break;
                
                // Move the player and trigger the UpdateTilesMoved event.
                player.MovePlayer(player.CurrentRoll);
                player.UpdateTilesMoved(player.CurrentRoll);
                
                // Allow the player to roll again. 
                player.CanRoll = false;

                // Play the player move sound if allowed.
                if (playAudio)
                    audioChannel.PlayAudioOneShot(moveAudioClip);
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // Tells the game that the given player wants to swap dice values. Both players have to do this for them to be 
    // swapped.
    private void PlayerSwap(PlayerSO player)
    {
        if (_gameWon || _gameLost) return;
        
        if (!player.WantsToSwap && player.CurrentRollState == RollState.Rolled)
        {
            player.WantsToSwap = true;
        }
    }
    
    // Swaps the dice values.
    private void SwapDiceValues()
    {
        byte temp = playerOne.CurrentRoll;
        playerOne.UpdateCurrentRoll(playerTwo.CurrentRoll);
        playerTwo.UpdateCurrentRoll(temp);

        audioChannel.PlayAudioOneShot(diceSwapAudioClip);
        
        playerOne.WantsToSwap = false;
        playerTwo.WantsToSwap = false;
    }

    // Increases the game's score and triggers the UnityEvent for any game objects listening. 
    private void IncreaseScore()
    {
        _score++;
        Debug.Log($"Score: {_score}");
        OnScoreChanged?.Invoke(_score);
    }

    private void SpawnEffects()
    {
        for (int i = 0; i < 5; i++)
        {
            float LocationX = Random.Range(-5, 5);
            float LocationZ = Random.Range(-5, 5);

            Vector3 Location = new Vector3(LocationX, 0, LocationZ);

            Instantiate(effect, Location, Quaternion.identity);
        }
    }

    // Rolls a six-sided dice.
    private static byte RollDice()
    {
        byte randomNumber = (byte) Random.Range(1, 6);
        return randomNumber;
    } 
}

