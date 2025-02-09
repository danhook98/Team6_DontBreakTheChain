using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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

    [Header("Game Type SO")]
    [SerializeField] private GameTypeSO gameType;
    
    [Header("Tile Generator")]
    [SerializeField] private TileGenerator tileGenerator;

    [Header("Player Data References")] 
    [SerializeField] private PlayerSO playerOne;
    [SerializeField] private PlayerSO playerTwo;
    
    [Header("Game variables")] 
    [SerializeField] private byte tilesToWin = 30;
    [SerializeField] private Vector3 playerOneStartPosition;
    [SerializeField] private Vector3 playerTwoStartPosition;
    [SerializeField] private float playerInputCooldown = 0.75f;
    [SerializeField][Range(0, 1)] private float coinSpawnChancePerTile = 0.15f;
    
    [Header("Chain references")]
    [SerializeField] private HingeJoint chainCentreHinge;
    
    private byte _score = 0;
    
    private bool _isComputerOpponent;

    private WaitForSeconds _inputCooldownDelay;

    private bool _gameWon = false;
    private bool _gameLost = false;
    
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
    }

    private void Start()
    {
        // Set the start positions for the players. This slowly moves them to the point from offscreen.
        playerOne.SetStartPosition(playerOneStartPosition);
        playerTwo.SetStartPosition(playerTwoStartPosition);
        
        // Generate the tiles along each player's path.
        tileGenerator.GenerateTiles(playerOneStartPosition, tilesToWin, coinSpawnChancePerTile);
        tileGenerator.GenerateTiles(playerTwoStartPosition, tilesToWin, coinSpawnChancePerTile);
        
        // Generate the base. 
        tileGenerator.GenerateBase(Vector3.forward * -1, tilesToWin);

        _isComputerOpponent = gameType.opponentIsAi;
        
        audioChannel.PlayAudio(backgroundMusicAudioClip, backgroundMusicVolume);
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

    private void GameWon()
    {
        if (_gameWon) return;
        _gameWon = true;
        audioChannel.PlayAudioOneShot(gameWonAudioClip);
        OnWin?.Invoke();
    }

    private void GameLost()
    {
        if (_gameLost) return;
        _gameLost = true;
        audioChannel.PlayAudioOneShot(gameLostAudioClip);
        OnLose?.Invoke();
    }

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

    private void RightPlayerRoll()
    {
        if (playerTwo.JustInput) return;
        if (_isComputerOpponent) return;
        
        PlayerRoll(playerTwo);
        PlayerInputCooldown(playerTwo);
    }

    private void RightPlayerSwap()
    {
        if (playerTwo.JustInput) return;
        if (_isComputerOpponent) return;
        
        PlayerSwap(playerTwo);
        PlayerInputCooldown(playerTwo);
    }

    private void PlayerInputCooldown(PlayerSO player)
    {
        player.JustInput = true;
        StartCoroutine(ResetPlayerJustInput(player));
    }

    private IEnumerator ResetPlayerJustInput(PlayerSO player)
    {
        yield return _inputCooldownDelay;
        player.JustInput = false;
    }

    private void PlayerRoll(PlayerSO player, bool playAudio = true)
    {
        if (_gameWon || _gameLost) return;
        
        switch (player.CurrentRollState)
        {
            case RollState.Idle:
                // The roll state may be 'rolled', but the player isn't currently allowed to do anything (e.g. waiting
                // for the other player to roll).
                if (!player.CanRoll || player.CurrentTilesMoved > tilesToWin) break;

                byte roll = RollDice();
                player.UpdateCurrentRoll(roll);
                
                player.CurrentRollState = RollState.Rolled;
                
                if (playAudio)
                    audioChannel.PlayAudioOneShot(rollDiceAudioClip);
                
                // TODO: display the roll value and a message stating to press again to confirm.
                
                break;
            case RollState.Rolled:
                if (!player.CanRoll || player.CurrentTilesMoved > tilesToWin) break;
                
                player.MovePlayer(player.CurrentRoll);
                player.UpdateTilesMoved(player.CurrentRoll);
                
                player.CanRoll = false;

                if (playAudio)
                    audioChannel.PlayAudioOneShot(moveAudioClip);
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PlayerSwap(PlayerSO player)
    {
        if (_gameWon || _gameLost) return;
        
        if (!player.WantsToSwap && player.CurrentRollState == RollState.Rolled)
        {
            player.WantsToSwap = true;
        }
    }
    
    private void SwapDiceValues()
    {
        byte temp = playerOne.CurrentRoll;
        playerOne.UpdateCurrentRoll(playerTwo.CurrentRoll);
        playerTwo.UpdateCurrentRoll(temp);

        audioChannel.PlayAudioOneShot(diceSwapAudioClip);
        
        playerOne.WantsToSwap = false;
        playerTwo.WantsToSwap = false;
    }

    private void IncreaseScore()
    {
        _score++;
        Debug.Log($"Score: {_score}");
        OnScoreChanged?.Invoke(_score);
    }

    private static byte RollDice()
    {
        byte randomNumber = (byte) Random.Range(1, 6);
        return randomNumber;
    } 
}

