
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class Gates : MonoBehaviour
{
    [SerializeField] private Team _team;

    [Header("Ball")]
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;


    [Header("Player")]
    [SerializeField] private Transform _playerOne;
    [SerializeField] private Transform _playerSpawnPoint;


    [Header("UI")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TMP_Text _scoreText;


    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;


    private IScoreService _scoreService;

    private int _currentScore;



    [Inject]
    public void Construct(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }



    private async void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;


        Destroy(other.gameObject);


    
        _scoreService.AddGoal(_team);


     
        _currentScore++;

        _scoreText.text = _currentScore.ToString();


        await WinpanelAsync();
    }



    private async Task WinpanelAsync()
    {
        _audioSource.Play();


        _winPanel.SetActive(true);


        await Task.Delay(4000);

       


        Instantiate(
            _ballPrefab,
            _ballSpawnPoint.position,
            Quaternion.identity
        );



        
        _playerOne.position =
            _playerSpawnPoint.position;



        _winPanel.SetActive(false);
    }
}


public enum Team
{
    Left,
    Right
}



public interface IScoreService
{
    void AddGoal(Team team);

    int LeftScore { get; }
    int RightScore { get; }
}