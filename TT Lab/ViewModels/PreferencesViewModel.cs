using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using TT_Lab.Command;
using ICommand = System.Windows.Input.ICommand;

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
    
    public ICommand SetProjectsPathCommand => new SelectFolderCommand(null, this, nameof(ProjectsPath));

    public ICommand SetPs2DiscContentPathCommand => new SelectFolderCommand(null, this, nameof(Ps2ContentPath));

    public ICommand SetXboxDiscContentPathCommand => new SelectFolderCommand(null, this, nameof(XboxContentPath));

    public bool AreEasterEggsEnabled
    {
        get => Preferences.GetPreference<bool>(Preferences.SillinessEnabled);
        set
        {
            Preferences.SetPreference(Preferences.SillinessEnabled, value);
            NotifyOfPropertyChange();
        }
    }

    public string Ps2ContentPath
    {
        get => Preferences.GetPreference<string>(Preferences.Ps2DiscContentPath);
        set
        {
            Preferences.SetPreference(Preferences.Ps2DiscContentPath, value);
            NotifyOfPropertyChange();
        }
    }

    public string XboxContentPath
    {
        get => Preferences.GetPreference<string>(Preferences.XboxDiscContentPath);
        set
        {
            Preferences.SetPreference(Preferences.XboxDiscContentPath, value);
            NotifyOfPropertyChange();
        }
    }

    public string ProjectsPath
    {
        get => Preferences.GetPreference<string>(Preferences.ProjectsPath);
        set
        {
            Preferences.SetPreference(Preferences.ProjectsPath, value);
            NotifyOfPropertyChange();
        }
    }
}