using PubCrawlMarch23.MessageLogs;
using PubCrawlMarch23.Quiz;
using PubCrawlMarch23.Users;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PubCrawlMarch23.Dare;

public class DareService
{
    private readonly DareDA _da;
    private readonly PlayerDareDA _playerDareDa;
    private readonly MessageLogService _logs;

    private List<Dare> _dares;
    private IList<PlayerDare> _playerDares;

    /// <summary>
    /// Constructor
    /// </summary>
    public DareService() : this (new(), new(), new()) { }
    public DareService(DareDA da, PlayerDareDA playerDareDa, MessageLogService logs)
    {
        _da = da ?? new();
        _playerDareDa = playerDareDa ?? new();
        _logs = logs ?? new();

        _dares = _da.List().ToList();
        _playerDares = _playerDareDa.List();
    }

    #region CRUD

    public void AddNew(Dare dare)
    {
        var newDare = dare with { DareID = Guid.NewGuid() };
        _dares.Add(newDare);
        _da.Save(_dares);
    }

    public void Update(Dare dare)
    {
        _dares.RemoveAll(x => x.DareID == dare.DareID);
        _dares.Add(dare);
        _da.Save(_dares);
    }

    public void Delete(Guid dareID)
    {
        _dares.RemoveAll(x => x.DareID == dareID);
        _da.Save(_dares);
    }

    public List<Dare> GetDareList(string userCode)
    {
        var alreadyDone = _playerDares.Where(x => x.UserCode == userCode).Select(x => x.DareID).ToList();
        var eligibleDares = _dares.Where(x => alreadyDone.Contains(x.DareID) == false);
        return eligibleDares.ToList();
    }

    #endregion

    #region Actions

    public void PerformDare(Dare dare, User user) 
    {
        var result = new PlayerDare(dare.DareID, user.Code, dare.Score);
        _playerDares.Add(result);
        _playerDareDa.Save(_playerDares);

        // TODO: Logs
        // TODO: Leaderboard
    }

    #endregion

    public IList<Dare> Dares { get => _dares; }

}
