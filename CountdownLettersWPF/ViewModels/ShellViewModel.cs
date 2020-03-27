using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CountdownLettersWPF.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            ActivateItemAsync(
                IoC.Get<GameViewModel>(),
                new CancellationToken()
            );

        }
    }
}
