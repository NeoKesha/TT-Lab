using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace TT_Lab.ViewModels;

public class PreferencesViewModel : Screen
{
    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        if (close)
        {
            Preferences.Save();
        }
        
        return base.OnDeactivateAsync(close, cancellationToken);
    }

    public bool AreEasterEggsEnabled
    {
        get => Preferences.GetPreference<bool>(Preferences.SillinessEnabled);
        set
        {
            Preferences.SetPreference(Preferences.SillinessEnabled, value);
            NotifyOfPropertyChange();
        }
    }
}