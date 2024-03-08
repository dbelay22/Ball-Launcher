using UnityEngine;
using Yxp.Command;
using BallLauncher.Core;
using Yxp.Debug;

namespace BallLauncher.Core.Commands
{
    public class ApplyAppSettingsCommand : BaseCommand
    {
        AppSettings _appSettings;

        public ApplyAppSettingsCommand(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public override void Execute()
        {
            base.Execute();

            if (_appSettings == null)
            {
                YLogger.Error("ApplyAppSettingsCommand] Settings are missing!");
                
                OnFinishedExecuting();
                
                return;
            }

            YLogger.Verbose($"ApplyAppSettingsCommand] * Setting frame rate to {_appSettings.TargetFramerate}");
            Application.targetFrameRate = _appSettings.TargetFramerate;

            OnFinishedExecuting();
        }

        public override void Undo()
        {
            YLogger.Warning("Why do you want to undo application settings for ?");

            _finishedExecuting = true;
        }
    }
}
