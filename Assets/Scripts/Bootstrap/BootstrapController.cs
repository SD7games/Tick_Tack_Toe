using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class BootstrapController
{
    private const string LobbySceneTag = "Lobby";

    private readonly BootstrapView _bootstrapView;

    public BootstrapController(BootstrapView bootstrapView)
    {
        _bootstrapView = bootstrapView;
        _bootstrapView.SetProgress(0);
    }

    public async Task StartAsynk()
    {
        await Task.Delay(1000);
        
        _bootstrapView.SetProgress(11);

        await Task.Delay(1000);
        
        _bootstrapView.SetProgress(28);

        await Task.Delay(1000);

        _bootstrapView.SetProgress(63);

        await Task.Delay(1000);

        _bootstrapView.SetProgress(85);

        await Task.Delay(1000);

        _bootstrapView.SetProgress(100);

        await Task.Delay(500);

        SceneManager.LoadScene(LobbySceneTag);
    }
}
