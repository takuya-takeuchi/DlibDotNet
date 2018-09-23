/*
 * This sample program is ported by C# from tools\imglab.
*/

using Microsoft.Extensions.CommandLineUtils;

namespace ImgLab
{

    partial class Program
    {

        #region Fields

        private const int ExitSuccess = 0;

        private const int ExitFailure = 1;

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            var app = new CommandLineApplication(false);
            app.Name = nameof(ImgLab);
            app.HelpOption("-h|--help");

            var createOption = app.Option("-c|--create", "", CommandOptionType.SingleValue);
            var clusterOption = app.Option("-cluster|--cluster", "", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                if (createOption.HasValue())
                {
                }

                if (clusterOption.HasValue())
                    ClusterDataset(app);

                return 0;
            });

            app.Execute(args);
        }

        #endregion

    }

}