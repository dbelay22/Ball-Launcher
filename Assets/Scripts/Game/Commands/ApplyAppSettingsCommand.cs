using UnityEngine;
using Yxp.Command;
using BallLauncher.Core;
using Yxp.Debug;
using Yxp.Unity.Command;

namespace BallLauncher.Commands
{
    public class ApplyAppSettingsCommand : BaseCommand
    {
        int _previousFramerate = 0;

        AppSettings _appSettings;

        public ApplyAppSettingsCommand(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public override void Execute()
        {
            _finishedExecuting = false;

            if (_appSettings == null)
            {
                YLogger.Error("ApplyAppSettingsCommand] Settings are missing!");
                
                _finishedExecuting = true;
                
                return;
            }

            _previousFramerate = Application.targetFrameRate;

            YLogger.Debug($"ApplyAppSettingsCommand] * Setting frame rate to {_appSettings.TargetFramerate}");

            Application.targetFrameRate = _appSettings.TargetFramerate;

            _finishedExecuting = true;
        }

        public override void Undo()
        {
            YLogger.Warning("Why do you want to undo application settings for ?");

            _finishedExecuting = true;
        }
    }
}
