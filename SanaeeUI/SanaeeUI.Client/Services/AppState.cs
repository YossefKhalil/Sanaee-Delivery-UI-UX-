using SanaeeUI.Client.Models;

namespace SanaeeUI.Client.Services;

public class AppState
{
    public DataMode Mode { get; private set; } = DataMode.Demo;
    public string ApiBaseUrl { get; set; } = "http://localhost:5270/";

    public event Action? Changed;

    public void SetMode(DataMode mode)
    {
        if (Mode == mode) return;
        Mode = mode;
        Changed?.Invoke();
    }

    public void ToggleMode() => SetMode(Mode == DataMode.Demo ? DataMode.Live : DataMode.Demo);
}
